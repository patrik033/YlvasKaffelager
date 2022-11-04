using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YlvasKaffelager.ViewModels
{
    public class OrderViewModel
    {
        /*Data Annotations instead of a try catch block to ensure
         those fields are not empty 
         */


        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        
        /*
         Data Annotations for the email field as well to ensure it's not empty
         */
        [Required]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Select a Product")]
        public int CoffeeId { get; set; }
        [Required]
        [Range(1,100)]
        public int Amount { get; set; }

        public List<CoffeeItem> CoffeeItems { get; set; }
        public OrderViewModel()
        {
            CoffeeItems = new List<CoffeeItem>
                    {
                        new CoffeeItem
                        {
                            Value = 1,
                            Text = "Gevalia"
                        },
                        new CoffeeItem
                        {
                            Value = 2,
                            Text = "Lavazza"
                        },
                        new CoffeeItem
                        {
                            Value = 3,
                            Text = "Zoegas"
                        },
                        new CoffeeItem
                        {
                            Value = 4,
                            Text = "Löfbergs"
                        }
                    };
        }
        public class CoffeeItem
        {
            public int Value { get; set; }
            public string Text { get; set; }
        }
    }
}