using CoffeeMachineApp.Domain.Entities;
using System.Data.Entity;

namespace CoffeeMachineApp.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<CoffeeMachine> CoffeeMachines { get; set; }
    }
}
