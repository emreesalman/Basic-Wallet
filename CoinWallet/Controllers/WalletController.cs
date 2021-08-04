using CoinWallet.Helper;
using CoinWallet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using static CoinWalletAPI.Data.Enumerations;

namespace CoinWallet.Controllers
{
    public class WalletController : Controller
    {
        WalletApi _api = new WalletApi();
        private readonly UserManager<IdentityUser> _userManager;

        public WalletController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            List<Wallet> walletInfo = new List<Wallet>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Wallet/GetInfo");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                walletInfo = JsonConvert.DeserializeObject<List<Wallet>>(result);
            }

            return View(walletInfo);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateWallet(int walletId, string type, double ammounth)
        {
            HttpClient client = _api.Initial();
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var res = await client.GetAsync($"api/Wallet/GetWalletById/{walletId}");
            var result = res.Content.ReadAsStringAsync().Result;
            var wallet = JsonConvert.DeserializeObject<Wallet>(result);

            switch (type)
            {
                case "Add":
                    wallet.Balance += ammounth;
                    break;
                case "Withdraw":
                    wallet.Balance -= ammounth;
                    break;
            }

            var content = new StringContent(
                         JsonConvert.SerializeObject(wallet),
                         Encoding.UTF8,
                         MediaTypeNames.Application.Json);

            var postTask = client.PostAsync($"api/Wallet/Update", content);

            postTask.Wait();
            var walletResult = postTask.Result;

            if (walletResult.IsSuccessStatusCode)
            {
                var newLog = new Logs
                {
                    LogType = type,
                    ProcessAmmount = ammounth,
                    ProcessingTime = DateTime.Now,
                    UserId = user.Id

                };

                var logContent = new StringContent(
                        JsonConvert.SerializeObject(newLog),
                        Encoding.UTF8,
                        MediaTypeNames.Application.Json);

                var logPostTask = client.PostAsync($"api/log/AddLog", logContent);

                return RedirectToAction("Index","Home");
            }

            return View("Index", "Home");
        }
    }
}
