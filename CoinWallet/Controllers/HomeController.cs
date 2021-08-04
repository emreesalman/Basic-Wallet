using CoinWallet.Helper;
using CoinWallet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace CoinWallet.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;

        }
        WalletApi _api = new WalletApi();

        public async Task<IActionResult> Index()
        {
            List<Wallet> wallets = new List<Wallet>();
            var user = await _userManager.GetUserAsync(HttpContext.User);    
            HttpClient client = _api.Initial();

            HttpResponseMessage res = await client.GetAsync($"api/Wallet/GetWallet/{user.Id}");

            
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                wallets = JsonConvert.DeserializeObject<List<Wallet>>(result);
                return View(wallets);
            }
            return View(wallets);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
