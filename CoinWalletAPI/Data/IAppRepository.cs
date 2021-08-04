using CoinWalletAPI.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuppliersAndProducts.Data
{
    public interface IAppRepository
    {
        void Add<T>(T entity) where T:class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveAll();

        void AddWallet(Wallet wallet);
        Wallet UpdateWallet(Wallet wallet);

        List<Wallet> GetInfo(string userId);

        Wallet GetWalletById(int walletId);

        void AddLog(Logs log);

        List<Logs> GetLogs(String userId);



    }
}
