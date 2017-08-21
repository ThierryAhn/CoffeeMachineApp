using CoffeeMachineApp.Domain.Abstract;
using CoffeeMachineApp.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CoffeeMachineApp.Service.Controllers
{
    public class BadgeController : ApiController
    {
        private IBadgeRepository repository;

        public BadgeController(IBadgeRepository badgeRepository)
        {
            this.repository = badgeRepository;
        }
        
        public IEnumerable<Badge> Get() {
            return repository.Badges;
        }

        public Badge Get(int id)
        {
            return repository.Badges.FirstOrDefault(p => p.BadgeID == id);
        }
    }
}
