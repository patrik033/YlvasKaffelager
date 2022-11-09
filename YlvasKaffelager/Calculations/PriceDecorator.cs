namespace YlvasKaffelager.Calculations
{
    public abstract class PriceDecorator : ICoffeePriceCalculator
    {
        private ICoffeePriceCalculator _calculator;

        public PriceDecorator(ICoffeePriceCalculator calculator)
        {
            _calculator = calculator;
        }



        public virtual decimal CalculateTotalPrice(int amount, decimal productPrice)
        {
            return this._calculator.CalculateTotalPrice(amount, productPrice);
        }
    }
}
