using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperTraders.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SuperTraders.DataService.Models;
using SuperTraders.DataService.Repository;

namespace SuperTraders.API.Controllers.Tests
{
    [TestClass()]
    public class TradeControllerTests
    {
        private TradeController _controller;
        private SuperTradersContext _context;
        private DbContextOptions<SuperTradersContext> _options;
        [TestInitialize]
        public void Init()
        {
            _options = new DbContextOptionsBuilder<SuperTradersContext>()
                .UseInMemoryDatabase(databaseName:"SuperTradersDB")
                .Options;

          

            
        }


        [TestMethod()]
        public async Task Buy_SendInvalidUserName_BadRequestTest()
        {


            //Prepare
            _context = new SuperTradersContext(_options);
            _context.Shares.Add(new Share() {Id = 1, Symbol = "BTC", Rate = 13.67m, LastUpDateTime = DateTime.Now});
            await _context.SaveChangesAsync();

            _controller = new TradeController(new ShareRepository(_context), new AccountRepository(_context),new TransactionLogRepository(_context));

            //Act
            var actionResult = await _controller.Buy(new Trade() {Symbol = "BTC", Amount = 3.00m, UserName = "burakavci"});


            var result = (ObjectResult)actionResult;

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
         

            //Console
            Console.WriteLine(result.Value);

            await _context.DisposeAsync();
        }

        [TestMethod()]
        public async Task Buy_SendInvalidShare_BadRequestTest()
        {
            //Prepare
            _context = new SuperTradersContext(_options);
            _context.Accounts.Add(new Account(){UserName = "burakavci",Password = "password",TotalBalance = 100.00m});
            await _context.SaveChangesAsync();

            _controller = new TradeController(new ShareRepository(_context), new AccountRepository(_context), new TransactionLogRepository(_context));

            //Act
            var actionResult = await _controller.Buy(new Trade() { Symbol = "BTC", Amount = 3, UserName = "burakavci" });


            var result = (ObjectResult)actionResult;

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);


            //Console
            Console.WriteLine(result.Value);

            await _context.DisposeAsync();
        }

        [TestMethod()]
        public async Task Buy_SendNotEnoughBalance_BadRequestTest()
        {
            //Prepare
            _context = new SuperTradersContext(_options);
            _context.Shares.Add(new Share() { Id = 1, Symbol = "BTC", Rate = 13.67m, LastUpDateTime = DateTime.Now });
            _context.Accounts.Add(new Account() { UserName = "burakavci", Password = "password", TotalBalance = 100.00m });
            await _context.SaveChangesAsync();

            _controller = new TradeController(new ShareRepository(_context), new AccountRepository(_context), new TransactionLogRepository(_context));

            //Act
            var actionResult = await _controller.Buy(new Trade() { Symbol = "BTC", Amount = 10, UserName = "burakavci" });


            var result = (ObjectResult)actionResult;

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);


            //Console
            Console.WriteLine(result.Value);

            await _context.DisposeAsync();
        }

        [TestMethod()]
        public async Task Buy_SendValid_OKTest()
        {
            //Prepare
            _context = new SuperTradersContext(_options);
            _context.Shares.Add(new Share() { Id = 1, Symbol = "BTC", Rate = 13.67m, LastUpDateTime = DateTime.Now });
            _context.Accounts.Add(new Account() { UserName = "burakavci", Password = "password", TotalBalance = 100.00m });
            await _context.SaveChangesAsync();

            _controller = new TradeController(new ShareRepository(_context), new AccountRepository(_context), new TransactionLogRepository(_context));

            //Act
            var actionResult = await _controller.Buy(new Trade() { Symbol = "BTC", Amount = 3, UserName = "burakavci" });


            var result = (ObjectResult)actionResult;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);


            //Console
            Console.WriteLine(JsonConvert.SerializeObject(result.Value));

            await _context.DisposeAsync();
        }
    }
}