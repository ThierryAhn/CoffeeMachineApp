using CoffeeMachineApp.Domain.Abstract;
using CoffeeMachineApp.Domain.Entities;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace CoffeeMachineApp.Service.Controllers
{
    public class CoffeeMachineController : ApiController
    {
        private IBadgeRepository _badgeRepository;
        private ICoffeeMachineRepository _coffeeMachineRepository;

        public CoffeeMachineController(ICoffeeMachineRepository coffeeMachineRepository, IBadgeRepository badgeRepository)
        {
            _coffeeMachineRepository = coffeeMachineRepository;
            _badgeRepository = badgeRepository;
        }

        public IHttpActionResult Get(int machineID, int productID, int sugarQuantity, int badgeID, bool saveDrink, bool useMug = false)
        {
            Drink drink = _coffeeMachineRepository.GetProduct(machineID, productID, sugarQuantity, badgeID, saveDrink, useMug);
            string message = string.Empty;
            if (drink == null)
            {
                return Content(HttpStatusCode.BadRequest, drink);
            }
            else
            {
                return Ok(drink);
            }
        }

        public IHttpActionResult Get(int machineID, int badgeID, bool saveDrink, bool useMug = false)
        {
            Badge badge = _badgeRepository.Badges.FirstOrDefault(b => b.BadgeID == badgeID);
            int productID = badge.ProductID ?? 0;
            int sugarQuantity = badge.SugarQuantity ?? 0;
            string message = string.Empty;

            Drink drink = _coffeeMachineRepository.GetProduct(machineID, productID, sugarQuantity, badgeID, saveDrink, useMug);
            if (drink == null)
            {
                return Content(HttpStatusCode.BadRequest, drink);
            }
            else
            {
                return Ok(drink);
            }
        }
    }
}
