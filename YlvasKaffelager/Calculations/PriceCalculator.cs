namespace YlvasKaffelager.Calculations
{
    public class PriceCalculator : IPriceCalculator
    {

        //Skickar in amount och productPrice och returnerar resultatet för total priset
        public decimal CalculateTotalPrice(int amount, decimal productPrice)
        {
            return amount * productPrice;
        }
    }
}
