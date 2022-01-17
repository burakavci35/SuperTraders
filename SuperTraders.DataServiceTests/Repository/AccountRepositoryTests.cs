using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperTraders.DataService.Interface;
using SuperTraders.DataService.Models;
using SuperTraders.DataService.Repository;

namespace SuperTraders.DataServiceTests.Repository
{
    [TestClass()]
    public class AccountRepositoryTests
    {
        private IAccountRepository _accountRepository;
        private SuperTradersContext _context;
        private DbContextOptions<SuperTradersContext> _options;

        [TestInitialize]
        public void Init()
        {

            _options = new DbContextOptionsBuilder<SuperTradersContext>()
                .UseInMemoryDatabase(databaseName: "SuperTradersDB")
                .Options;





        }

        [TestMethod()]
        public async Task IsAccountExistByUserName_SendInValidUserName_FalseTest()
        {

            //Prepare
            _context = new SuperTradersContext(_options);
            _context.Accounts.Add(new Account()
                {Id = 1, UserName = "username1", Password = "password", TotalBalance = 100.99m});
            await _context.SaveChangesAsync();

            _accountRepository = new AccountRepository(_context);

            //Act
            var isAccountExist = _accountRepository.IsAccountExistByUserName("username2");


            // Assert
            Assert.IsFalse(isAccountExist);

            await _context.DisposeAsync();
        }

        [TestMethod()]
        public async Task IsAccountExistByUserName_SendValidUserName_TrueTest()
        {

            //Prepare
            _context = new SuperTradersContext(_options);
            _context.Accounts.Add(new Account()
                {Id = 1, UserName = "username1", Password = "password", TotalBalance = 100.99m});
            await _context.SaveChangesAsync();

            _accountRepository = new AccountRepository(_context);

            //Act
            var isAccountExist = _accountRepository.IsAccountExistByUserName("username1");


            // Assert
            Assert.IsTrue(isAccountExist);

            await _context.DisposeAsync();
        }

        [TestMethod()]
        public async Task GetAccountByUserName_SendInValidUserName_ReturnNullTest()
        {

            //Prepare
            _context = new SuperTradersContext(_options);
            _context.Accounts.Add(new Account()
                {Id = 1, UserName = "username1", Password = "password", TotalBalance = 100.99m});
            await _context.SaveChangesAsync();

            _accountRepository = new AccountRepository(_context);

            //Act
            var isAccountExist = await _accountRepository.GetAccountByUserName("username2");


            // Assert
            Assert.IsNull(isAccountExist);

            await _context.DisposeAsync();
        }

        [TestMethod()]
        public async Task GetAccountByUserName_SendInValidUserName_ReturnAccountTest()
        {

            //Prepare
            _context = new SuperTradersContext(_options);
            _context.Accounts.Add(new Account()
                {Id = 1, UserName = "username1", Password = "password", TotalBalance = 100.99m});
            await _context.SaveChangesAsync();

            _accountRepository = new AccountRepository(_context);

            //Act
            var isAccountExist = await _accountRepository.GetAccountByUserName("username1");


            // Assert
            Assert.IsNotNull(isAccountExist);

            await _context.DisposeAsync();
        }


        [TestMethod()]
        public async Task AddAccountShare_SendValidAccountShare_ExistOnAccountShareTest()
        {
            //Prepare
            _context = new SuperTradersContext(_options);
            _context.Accounts.Add(new Account()
                {Id = 1, UserName = "username1", Password = "password", TotalBalance = 100.99m});
            await _context.SaveChangesAsync();

            _accountRepository = new AccountRepository(_context);

            //Act
            await _accountRepository.AddAccountShare("username1", new AccountShare() {Symbol = "BTC", Amount = 11},
                50.90m);

            var foundAccount = await _accountRepository.GetAccountByUserName("username1");

            var foundShare = foundAccount.AccountShareList.FirstOrDefault(x => x.Symbol == "BTC");

            // Assert
            Assert.IsNotNull(foundShare);
            Assert.AreEqual("BTC", foundShare.Symbol);
            Assert.AreEqual(11, foundShare.Amount);


            await _context.DisposeAsync();

        }


        [TestMethod()]
        public async Task AddAccountShare_SendValidAccountShare_CheckBalanceIsOKTest()
        {
            //Prepare
            _context = new SuperTradersContext(_options);
            _context.Accounts.Add(new Account()
                {Id = 1, UserName = "username1", Password = "password", TotalBalance = 100.91m});
            await _context.SaveChangesAsync();

            _accountRepository = new AccountRepository(_context);

            //Act
            await _accountRepository.AddAccountShare("username1", new AccountShare() {Symbol = "BTC", Amount = 1},
                50.90m);

            var foundAccount = await _accountRepository.GetAccountByUserName("username1");

            //var foundShare = foundAccount.AccountShareList.FirstOrDefault(x => x.Symbol == "BTC");

            // Assert
            Assert.AreEqual(50.01m, foundAccount.TotalBalance);



            await _context.DisposeAsync();

        }


        [TestMethod()]
        public async Task RemoveAccountShare_SendValidAccountShare_ExistOnAccountShareTest()
        {
            //Prepare
            _context = new SuperTradersContext(_options);
            _context.Accounts.Add(new Account()
                { Id = 1, UserName = "username1", Password = "password", TotalBalance = 100.90m,AccountShareList = { new AccountShare(){Symbol = "BTC",Amount = 5}}});
            await _context.SaveChangesAsync();

            _accountRepository = new AccountRepository(_context);

            //Act
            await _accountRepository.RemoveAccountShare("username1", new AccountShare() { Symbol = "BTC", Amount = 1 },
                50.90m);

            var foundAccount = await _accountRepository.GetAccountByUserName("username1");

            var foundShare = foundAccount.AccountShareList.FirstOrDefault(x => x.Symbol == "BTC");

            // Assert
            Assert.IsNotNull(foundShare);
            Assert.AreEqual("BTC", foundShare.Symbol);
            Assert.AreEqual(4m, foundShare.Amount);


            await _context.DisposeAsync();

        }

        [TestMethod()]
        public async Task RemoveAccountShare_SendValidAccountShare_CheckBalanceIsOKTest()
        {
            //Prepare
            _context = new SuperTradersContext(_options);
            _context.Accounts.Add(new Account()
                { Id = 1, UserName = "username1", Password = "password", TotalBalance = 100.91m, AccountShareList = { new AccountShare() { Symbol = "BTC", Amount = 5 } } });
            await _context.SaveChangesAsync();

            _accountRepository = new AccountRepository(_context);

            //Act
            await _accountRepository.RemoveAccountShare("username1", new AccountShare() { Symbol = "BTC", Amount = 1 },
                50.90m);

            var foundAccount = await _accountRepository.GetAccountByUserName("username1");


            // Assert
            Assert.AreEqual(151.81m, foundAccount.TotalBalance);



            await _context.DisposeAsync();

        }
    }
}