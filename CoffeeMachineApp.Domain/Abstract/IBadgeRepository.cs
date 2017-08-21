using CoffeeMachineApp.Domain.Entities;
using System.Linq;

namespace CoffeeMachineApp.Domain.Abstract
{
    public interface IBadgeRepository
    {
        IQueryable<Badge> Badges { get; }
        void SaveBadge(Badge badge);
    }
}
