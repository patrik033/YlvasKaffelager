using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YlvasKaffelager.Calculations;
using YlvasKaffelager.DataModels;
using YlvasKaffelager.DbContext;
using YlvasKaffelager.ViewModels;

namespace YlvasKaffelager.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IPriceCalculator _priceCalculator;

        public IDbContext _dbContext { get; set; }
        public int NumberOfOrders { get; set; }
        public OrdersController(IPriceCalculator priceCalculator, IDbContext dbContext)
        {
            _dbContext = dbContext;

            NumberOfOrders = 0;
            _priceCalculator = priceCalculator;
        }
        public IActionResult Index()
        {
            var model = new OrderViewModel();

            return View(model);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Orders(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var coffee = _dbContext.GetCoffe(model.CoffeeId);
                int amount = model.Amount;
                ViewOrderModel viewModel = CreateOrderModel(model, coffee, amount);

                return View("ViewOrder", viewModel);
            }
            else
            {
                return View("Index", model);

            }
        }


        public IActionResult ViewOrder()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Confirm(ViewOrderModel model)
        {
            NumberOfOrders++;

            var order = new Order
            {
                Id = NumberOfOrders,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Brand = model.Brand,
                Amount = model.Amount,
                Total = model.Total,
            };

            _dbContext.AddOrder(order);


            return View("Completed");
        }

        public IActionResult Completed()
        {
            return View();
        }
        private ViewOrderModel CreateOrderModel(OrderViewModel model, Coffee coffee, int amount)
        {
            var viewModel = new ViewOrderModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Brand = coffee.Brand,
                Amount = amount,
                //låter CalculateTotalPrice utföra beräkningen istället
                Total = _priceCalculator.CalculateTotalPrice(amount, coffee.Price),
            };
            return viewModel;
        }
    }
}