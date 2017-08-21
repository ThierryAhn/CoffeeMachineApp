namespace CoffeeMachineApp.Domain.Entities
{
    public class Badge
    {
        public int BadgeID { get; set; }
        public string BadgeName { get; set; }
        public int ? ProductID { get; set; }
        public int ? SugarQuantity { get; set; }
    }
}
