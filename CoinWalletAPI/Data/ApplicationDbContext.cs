using CoinWalletAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace CoinWalletAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Wallet> Wallet { get; set; }
        public DbSet<Logs> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=EMRE-MONSTER\\SQLEXPRESS;Initial Catalog=WalletProjectAPP;Integrated Security=True");

            if (!optionsBuilder.IsConfigured)
            {

            }
        }


    }
}
