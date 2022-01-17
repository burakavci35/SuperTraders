
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SuperTraders.DataService.Models
{
    public class SuperTradersContext : DbContext
    {
        public DbSet<Share> Shares { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountShare> AccountShares { get; set; }
        public DbSet<TransactionLog> TransactionLogs { get; set; }  

        public SuperTradersContext()
        {
        }

        public SuperTradersContext(DbContextOptions<SuperTradersContext> options)
            : base((DbContextOptions)options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Share>().ToTable("Share");
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<AccountShare>().ToTable("AccountShare");
            modelBuilder.Entity<TransactionLog>().ToTable("TransactionLog");


            modelBuilder.Entity<Share>().HasData(new List<Share>()
            {
                new Share(){Id = 1,LastUpDateTime = DateTime.Now.AddHours(0),Rate = 10.56m,Symbol = "BTC"},
                new Share(){Id = 2,LastUpDateTime = DateTime.Now.AddHours(-1),Rate = 11.56m,Symbol = "USD"},
                new Share(){Id = 3,LastUpDateTime = DateTime.Now.AddHours(-2),Rate = 12.56m,Symbol = "TRY"},
                new Share(){Id = 4,LastUpDateTime = DateTime.Now.AddHours(-3),Rate = 13.56m,Symbol = "EUR"}
            });

            modelBuilder.Entity<Account>().HasData(new List<Account>()
            {
                new Account(){Id = 1,UserName = "user1",Password = "pass1",TotalBalance = 100.55m},
                new Account(){Id = 2,UserName = "user2",Password = "pass2",TotalBalance = 110.55m},
                new Account(){Id = 3,UserName = "user3",Password = "pass3",TotalBalance = 120.55m},
                new Account(){Id = 4,UserName = "user4",Password = "pass4",TotalBalance = 130.55m},
                new Account(){Id = 5,UserName = "user5",Password = "pass5",TotalBalance = 140.55m},
            });

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;

            optionsBuilder.UseNpgsql($"User ID=postgres;Password=_Admin123;Server=localhost;Port=5432;Database=SuperTraders;Integrated Security=true;Pooling=true;");
        }
    }
}
