using CoffeeMachineApp.Domain.Entities;
using System.Collections.Generic;

namespace CoffeeMachineApp.WebUI.Models
{
    public class CoffeeMachineViewModel
    {
        public IList<Product> Products { get; set; }
        public IList<Badge> Badges { get; set; }

        public int ProductID { get; set; }
        public int BadgeID { get; set; }
        public int SugarQuantity { get; set; }
        public bool UseMug { get; set; }
        public bool UseBadge { get; set; }
        public bool SaveDrink { get; set; }
        public int SaveBadgeID { get; set; }
    }
}