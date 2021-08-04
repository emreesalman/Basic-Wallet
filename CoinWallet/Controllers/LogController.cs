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

namespace CoinWallet.Controllers
{
    public class LogController : Controller
    {
        WalletApi _api = new WalletApi();
        private readonly UserManager<IdentityUser> _userManager;

        public LogController( UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            List<Logs> logs = new List<Logs>();
            var user = await _userManager.GetUserAsync(HttpContext.User);
            HttpClient client = _api.Initial();

            HttpResponseMessage res = await client.GetAsync($"api/Log/GetLogs/{user.Id}");


            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                logs = JsonConvert.DeserializeObject<List<Logs>>(result);
                return View(logs);
            }
            return View(logs);
        }
     
        [HttpPost]
        public IActionResult CreateLog(Logs log)
        {
            

            HttpClient client = _api.Initial();
            var content = new StringContent(
            JsonConvert.SerializeObject(log),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);
            var postTask = client.PostAsync($"api/Logs/CreateLog", content);
            postTask.Wait();
            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
