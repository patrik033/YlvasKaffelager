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
        private readonly ICoffeePriceCalculator _priceCalculator;

        public IDbContext _dbContext { get; set; }
        public int NumberOfOrders { get; set; }
        public OrdersController(ICoffeePriceCalculator priceCalculator, IDbContext dbContext)
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


        /// <summary>
        /// Processes your order to ensure everything is ok before it passes it to
        /// the ViewOrder Page
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult ProcessOrder(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Get the product based on the model 
                var coffee = _dbContext.GetCoffe(model.CoffeeId);
                int amount = model.Amount;
                //create a new ViewOrderModel based on the model passed to the function
                ViewOrderModel viewModel = CreateOrderModel(model, coffee, amount);
                //If everything is ok pass the new model to the ViewOrder view
                return View("ViewOrder", viewModel);
            }
            else
            {
                //If some fields were missing redirect back to the index page
                return View("Index", model);
            }
        }


        public IActionResult ViewOrder()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateOrder(ViewOrderModel model)
        {
            //Creates a new order and adds it to the "database"
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

            return RedirectToAction("CompletedOrder");
        }

        public IActionResult CompletedOrder()
        {
            return View();
        }
        private ViewOrderModel CreateOrderModel(OrderViewModel model, Coffee coffee, int amount)
        {
            //new instance of priceCalculator
            ICoffeePriceCalculator coffeePrice = new PriceCalculator();
            //populate coffePriceDecorator with the coffeePRice
            CoffeePriceDecorator coffeePriceDecorator = new CoffeePriceDecorator(coffeePrice);

            //Creates a new ViewOrderModel and returns it
            var viewModel = new ViewOrderModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Brand = coffee.Brand,
                Amount = amount,
                //calculateTotalPrice is doing all the calculations
                Total = coffeePriceDecorator.CalculateTotalPrice(amount, coffee.Price),
            };
            return viewModel;
        }
    }
}