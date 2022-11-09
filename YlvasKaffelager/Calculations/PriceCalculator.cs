namespace YlvasKaffelager.Calculations
{
    /// <summary>
    /// Calculate Price
    /// </summary>
    public class PriceCalculator : ICoffeePriceCalculator
    {
        /// <summary>
        /// Calculate the total price for the order
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="productPrice"></param>
        /// <returns></returns>
        //Skickar in amount och productPrice och returnerar resultatet för total priset
        public decimal CalculateTotalPrice(int amount, decimal productPrice)
        {
            return amount * productPrice;
        }
    }
}
