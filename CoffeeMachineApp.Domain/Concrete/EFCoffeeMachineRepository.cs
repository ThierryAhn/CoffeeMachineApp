using CoffeeMachineApp.Domain.Abstract;
using CoffeeMachineApp.Domain.Entities;
using System.Linq;

namespace CoffeeMachineApp.Domain.Concrete
{
    public class EFCoffeeMachineRepository : ICoffeeMachineRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<CoffeeMachine> CoffeeMachines
        {
            get { return context.CoffeeMachines; }
        }

        public Drink GetProduct(int machineID, int productID, int sugarQuantity, int badgeID, bool saveDrink, bool useMug)
        {
            CoffeeMachine dbEntry = context.CoffeeMachines.FirstOrDefault(c => c.MachineID == machineID);

            Drink drink = null;
            if (dbEntry != null)
            {
                Product productDbEntry = context.Products.FirstOrDefault(
                                p => p.MachineID == machineID 
                                    && p.ProductID == productID);

                if (productDbEntry != null && ((!useMug && productDbEntry.Quantity > 0) || useMug) )
                {
                    drink = new Drink() { Name = productDbEntry.Name };

                    if (!useMug)
                    {
                        productDbEntry.Quantity -= 1;                        
                    }

                    // remove sugar quantity
                    dbEntry.SugarQuantity = (dbEntry.SugarQuantity < sugarQuantity)
                                            ? 0
                                            : dbEntry.SugarQuantity - sugarQuantity;
                    drink.SugarQuantity = (dbEntry.SugarQuantity < sugarQuantity)
                                            ? dbEntry.SugarQuantity
                                            : sugarQuantity;

                    
                    // save choice in badge
                    if (saveDrink)
                    {
                        Badge badgeDbEntry = context.Badges.FirstOrDefault(b => b.BadgeID == badgeID);
                        if (badgeDbEntry == null)
                        {
                            badgeDbEntry = new Badge();
                        }

                        badgeDbEntry.ProductID = productDbEntry.ProductID;
                        badgeDbEntry.SugarQuantity = drink.SugarQuantity;
                    }
                }
                
                context.SaveChanges();
            }

            return drink;
        }
    }
}
