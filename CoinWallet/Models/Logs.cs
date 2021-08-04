using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoinWallet.Models
{
    public class Logs
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogId { get; set; }

        public string LogType { get; set; }
        public double ProcessAmmount { get; set; }
        public DateTime ProcessingTime { get; set; }
        
        public string UserId { get; set; }


    }
}
