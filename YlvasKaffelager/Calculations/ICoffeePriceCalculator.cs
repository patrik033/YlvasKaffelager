using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YlvasKaffelager.Calculations
{
    public interface ICoffeePriceCalculator
    {
        decimal CalculateTotalPrice(int amount, decimal productPrice);
    }
}
