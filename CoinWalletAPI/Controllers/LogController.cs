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
    public class LogController : Controller
    {
        private IAppRepository _appRepository;
        private ApplicationDbContext _context;
       
        public LogController(IAppRepository appRepository, ApplicationDbContext context)
        {
            _appRepository = appRepository;
            _context = context;
        }

        [HttpPost]
        public ActionResult AddLog([FromBody] Logs Log)
        {
            _appRepository.AddLog(Log);
            _appRepository.SaveAll();
            return Ok(Log);
        }

        [HttpGet("{UserId}")]
        public ActionResult GetLogs(string userId)
        {
            var logs = _appRepository.GetLogs(userId);
            return Ok(logs);
        }
    }
}
