using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperTraders.DataService.Interface;
using SuperTraders.DataService.Models;
using SuperTraders.DataService.Repository;

namespace SuperTraders.DataServiceTests.Repository
{
    [TestClass()]
    public class ShareRepositoryTests
    {
        private IShareRepository _repository;

        [TestInitialize]
        public void Init()
        {
            var options = new DbContextOptionsBuilder<SuperTradersContext>()
                .UseNpgsql(
                    "User ID=postgres;Password=_Admin123;Server=localhost;Port=5432;Database=SuperTraders;Integrated Security=true;Pooling=true;")
                .Options;

            _repository = new ShareRepository(new SuperTradersContext(options));
        }


        [TestMethod()]
        public async Task GetShareListTest()
        {
            //Arrange


            //Act
            var shareList = await _repository.GetShareList();


            //Asset
            Assert.IsNotNull(shareList);


        }
    }
}