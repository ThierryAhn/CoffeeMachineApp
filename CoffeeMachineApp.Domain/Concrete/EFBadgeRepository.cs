using CoffeeMachineApp.Domain.Abstract;
using CoffeeMachineApp.Domain.Entities;
using System.Linq;

namespace CoffeeMachineApp.Domain.Concrete
{
    public class EFBadgeRepository : IBadgeRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Badge> Badges
        {
            get { return context.Badges; }
        }

        public void SaveBadge(Badge badge)
        {
            if (badge.BadgeID == 0)
            {
                context.Badges.Add(badge);
            }
            else
            {
                Badge dbEntry = context.Badges.Find(badge.BadgeID);
                if (dbEntry != null)
                {
                    dbEntry.BadgeName = badge.BadgeName;
                    dbEntry.ProductID = badge.ProductID;
                    dbEntry.SugarQuantity = badge.SugarQuantity;
                }
            }
            context.SaveChanges();
        }
    }
}
