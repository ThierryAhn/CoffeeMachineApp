using System.ComponentModel.DataAnnotations;

namespace CoffeeMachineApp.Domain.Entities
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public int MachineID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
