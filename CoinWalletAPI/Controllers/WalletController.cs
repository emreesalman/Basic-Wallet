using CoinWalletAPI.Data;
using CoinWalletAPI.Models;
using Microsoft.AspNetCore.Mvc;
using SuppliersAndProducts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinWalletAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WalletController : Controller
    {
        private IAppRepository _appRepository;
        private ApplicationDbContext _context;
       
        public WalletController(IAppRepository appRepository, ApplicationDbContext context)
        {
            _appRepository = appRepository;
            _context = context;
        }

        [HttpGet("{UserId}")]
        public ActionResult GetWallet(string userId)
        {
            var wallet = _appRepository.GetInfo(userId);
            return Ok(wallet);
        }

        [HttpGet("{WalletId}")]
        public ActionResult GetWalletById(int walletId)
        {
            var wallet = _appRepository.GetWalletById(walletId);
            return Ok(wallet);
        }

        [HttpPost]
        public ActionResult CreateWallet([FromBody] Wallet wallet)
        {
            _appRepository.Add(wallet);
            _appRepository.SaveAll();
            return Ok(wallet);
        }
    
        [HttpPost]
        public ActionResult Update([FromBody] Wallet wallet)
        {
            _appRepository.UpdateWallet(wallet);
            _appRepository.SaveAll();
            return Ok(wallet);
        }

        
    }
}
