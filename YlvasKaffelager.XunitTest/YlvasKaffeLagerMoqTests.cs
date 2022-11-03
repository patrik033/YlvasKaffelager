using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using YlvasKaffelager.DataModels;
using YlvasKaffelager.DbContext;

namespace YlvasKaffelager.XunitTest
{
    public class YlvasKaffeLagerMoqTests
    {
        public Mock<IDbContext> Repo { get; set; }
        public IDbContext _sut { get; set; }

        public YlvasKaffeLagerMoqTests()
        {
            var product = new Coffee();
            product = null;

            var correctProduct = new Coffee
            {
                Id = 1,
                Brand = "Lavazza",
                Price = 49.90m
            };
            Repo = new Mock<IDbContext>();
            Repo.Setup(x => x.GetCoffe(0)).Returns(product);
            Repo.Setup(x => x.GetCoffe(1)).Returns(correctProduct);
            _sut = Repo.Object;

        }
        [Fact]
        public void GetCoffee_ZeroValue_ReturnNull()
        {
            //Act
            var product = new Coffee();
            product = null;
            //Arrange
            var result = _sut.GetCoffe(0);
            //Assert
            Assert.Equal(product, result);
        }

        [Fact]
        public void GetCoffee_OneValue_ReturnProduct()
        {
            //Act
            var correctProduct = new Coffee
            {
                Id = 1,
                Brand = "Lavazza",
                Price = 49.90m
            };
            //Arrange
            var result = _sut.GetCoffe(1);
            //Assert
            Assert.Equal(correctProduct.Price, result.Price);
            Assert.Equal(correctProduct.Brand, result.Brand);
        }
    }
}
