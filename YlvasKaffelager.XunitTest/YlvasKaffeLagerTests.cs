using System.Reflection;
using YlvasKaffelager.Calculations;
using YlvasKaffelager.DataModels;
using YlvasKaffelager.DbContext;
using YlvasKaffelager.ViewModels;

namespace YlvasKaffelager.XunitTest
{
    public class YlvasKaffeLagerTests
    {
        private readonly ICoffeePriceCalculator _priceCalculator;
        private readonly DbContexts _dbContext;
        private readonly CoffeePriceDecorator _decorator;
        
        public YlvasKaffeLagerTests()
        {
            _priceCalculator = new PriceCalculator();
            _decorator = new CoffeePriceDecorator(_priceCalculator);
            _dbContext = new DbContexts();
        }

        [Fact]
        public void PriceCalculator_AddOneItem_ReturnValidResult()
        {

            //Act
            var product = _dbContext.GetCoffe(1);
            int amount = 1;
            //Arrange
            var totalPrice = _decorator.CalculateTotalPrice(amount, product.Price);
            //Assert
            Assert.Equal((amount*product.Price) * 1.25M, totalPrice);
        }

    }
}