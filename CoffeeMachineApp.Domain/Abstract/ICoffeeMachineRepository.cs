using CoffeeMachineApp.Domain.Entities;
using System.Linq;

namespace CoffeeMachineApp.Domain.Abstract
{
    public interface ICoffeeMachineRepository
    {
        IQueryable<CoffeeMachine> CoffeeMachines { get; }
        Drink GetProduct(int machineID, int productID, int sugarQuantity, int badgeID, bool saveDrink, bool useMug);
    }
}
