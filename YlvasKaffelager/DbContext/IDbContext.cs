using YlvasKaffelager.DataModels;

namespace YlvasKaffelager.DbContext
{
    public interface IDbContext
    {
        List<Coffee> Coffees { get; set; }
        List<Order> Orders { get; set; }
        public Coffee GetCoffe(int? id);
        public void AddOrder(Order order);
    }
}
