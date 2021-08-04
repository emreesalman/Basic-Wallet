using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinWalletAPI.Data
{
    public class Enumerations
    {

        public enum MoneyType
        {
            TL = 10,
            USD = 20,
            BTT = 30,
            ETH = 40,
        }
        public enum LogType
        {
            Cekme = 10,
            Yatırma = 20,
        }

        public enum ProcessType
        {
            Add=10,
            Withdraw=20,
        }
    }
}
