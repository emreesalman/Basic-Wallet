using CoinWalletAPI.Data;
using CoinWalletAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuppliersAndProducts.Data
{
    public class AppRepository : IAppRepository
    {
        private ApplicationDbContext _context;

        public AppRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {

            _context.Remove(entity);
        }
    
        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        List<Wallet> IAppRepository.GetInfo(string userId)
        {
            var walletInfo = _context.Wallet.Where(u => u.UserId == userId).ToList();
           
            return walletInfo;
        }

        public void AddWallet(Wallet wallet)
        {
            _context.Wallet.Add(wallet);
        }
        public Wallet UpdateWallet(Wallet wallet)
        {
            _context.Wallet.Update(wallet);
            return wallet;
        }

        List<Logs> IAppRepository.GetLogs(string userId)
        {
            var LogInfo = _context.Logs.Where(u => u.UserId == userId).ToList();
           
            return LogInfo;
        }

        public void AddLog(Logs log)
        {
            _context.Logs.Add(log);
        }

        public Wallet GetWalletById(int walletId)
        {
            var wallet = _context.Wallet.FirstOrDefault(wl => wl.WalletId == walletId);
            return wallet;
        }
    }
}
