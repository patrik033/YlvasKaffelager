namespace YlvasKaffelager.Calculations
{
    public class CoffeePriceDecorator : PriceDecorator
    {
        public CoffeePriceDecorator(ICoffeePriceCalculator calculator) : base(calculator)
        {
        }

        public override decimal CalculateTotalPrice(int amount, decimal productPrice)
        {
            //Taxes for coffee is 25% so the total price will be number of products bought times price and the taxes times the total price
            var coffeeTaxes = 1.25M;
            return base.CalculateTotalPrice(amount, (productPrice)) * coffeeTaxes;
        }
    }
}
