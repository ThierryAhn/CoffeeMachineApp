using CoffeeMachineApp.Domain.Entities;
using CoffeeMachineApp.WebUI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace CoffeeMachineApp.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IList<Product> Products { get; set; }
        public IList<Badge> Badges { get; set; }

        private string BaseServiceUrl = "http://localhost:59958/api/";

        public int MachineID = 1;

        public HomeController()
        {
            Badges = GetBadges();
            Products = GetProducts();
        }

        IList<Badge> GetBadges()
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(string.Concat(BaseServiceUrl, "badge")).Result;
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsAsync<IList<Badge>>().Result;
            }
        }

        IList<Product> GetProducts()
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(string.Concat(BaseServiceUrl, "product")).Result;
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsAsync<IList<Product>>().Result;
            }
        }

        public ViewResult Index()
        {
            CoffeeMachineViewModel model = new CoffeeMachineViewModel()
            {
                Products = Products,
                Badges = Badges
            };
            
            return View(model);
        }

        //TODO: manage badrequest and use async await
        public string Result(CoffeeMachineViewModel model)
        {
            var client = new HttpClient();
            var path = BaseServiceUrl;

            int badgeID = model.SaveDrink ? model.SaveBadgeID : model.BadgeID;
            int productID = model.UseBadge ? Badges.FirstOrDefault(b => b.BadgeID == badgeID).ProductID ?? 0
                                           : model.ProductID;

            if(model.UseBadge)
            {
                model.SaveDrink = false;

                if(productID > 0)
                {
                    path += string.Format("coffeemachine?machineID={3}&badgeID={0}&saveDrink={1}&useMug={2}",
                                            badgeID, 
                                            model.SaveDrink, 
                                            model.UseMug,
                                            MachineID);
                }
                else
                {
                    return "Pas de boisson sur la carte !";
                }
            }
            else
            {
                path += string.Format("coffeemachine?machineID={5}&productID={0}&sugarQuantity={1}&badgeID={2}&saveDrink={3}&useMug={4}", 
                                            productID, 
                                            model.SugarQuantity,
                                            badgeID,
                                            model.SaveDrink, 
                                            model.UseMug,
                                            MachineID);
            }


            Drink drink = null;
            
            HttpResponseMessage response = client.GetAsync(path).Result;
            drink = response.Content.ReadAsAsync<Drink>().Result;

            string message = string.Empty;

            if (drink != null)
            {
                message = string.Format("Votre {0} est prêt",  drink.Name);

                var sugarQuantity = model.UseBadge
                                    ? Badges.FirstOrDefault(b => b.BadgeID == badgeID).SugarQuantity ?? 0
                                    : model.SugarQuantity;
                if (drink.SugarQuantity > 0 && drink.SugarQuantity != sugarQuantity)
                {
                    message = string.Concat(message, " Mais pas assez de sucre !");
                }
            }
            else
            {
                message = string.Format("Plus de {0}", Products.FirstOrDefault(p => p.ProductID == productID).Name);
            }

            return message;
        }

    }
}
