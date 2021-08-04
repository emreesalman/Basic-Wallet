using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoinWallet.Models
{
    public class Wallet 
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WalletId { get; set; }
        public string UserId { get; set; }

        public string MoneyTypeName { get; set; }

        public int MoneyType { get; set; }

        public double Balance { get; set; }
    }
}
