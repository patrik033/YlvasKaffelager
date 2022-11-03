using System.Reflection;
using YlvasKaffelager.Calculations;
using YlvasKaffelager.DataModels;
using YlvasKaffelager.DbContext;
using YlvasKaffelager.ViewModels;

namespace YlvasKaffelager.XunitTest
{
    public class YlvasKaffeLagerTests
    {
        private readonly IPriceCalculator _priceCalculator;
        private readonly DbContexts _dbContext;

        public YlvasKaffeLagerTests()
        {
            _priceCalculator = new PriceCalculator();
            _dbContext = new DbContexts();
        }

        [Fact]
        public void PriceCalculator_AddOneItem_ReturnValidResult()
        {

            //Act
            var product = _dbContext.GetCoffe(1);
            int amount = 1;
            //Arrange
            var totalPrice = _priceCalculator.CalculateTotalPrice(amount, product.Price);
            //Assert
            Assert.Equal(amount*product.Price, totalPrice);
        }

    }
}