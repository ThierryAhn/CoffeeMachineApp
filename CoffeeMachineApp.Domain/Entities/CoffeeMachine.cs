using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoffeeMachineApp.Domain.Entities
{
    public class CoffeeMachine
    {
        [Key]
        public int MachineID { get; set; }

        public List<Product> ProductsCollection { get; set; }
        
        public int SugarQuantity { get; set; }

        public CoffeeMachine()
        {
            ProductsCollection = new List<Product>();
        }

    }
}
