using CoffeeMachineApp.Domain.Abstract;
using CoffeeMachineApp.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CoffeeMachineApp.Service.Controllers
{
    public class ProductController : ApiController
    {
        private IProductRepository repository;

        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }
        
        public IEnumerable<Product> Get() {
            return repository.Products;
        }

        public Product Get(int id)
        {
            return repository.Products.FirstOrDefault(p => p.ProductID == id);
        }

        public Product Delete(int id)
        {
            return repository.DeleteProduct(id);
        }

        public void Put(Product product)
        {
            repository.SaveProduct(product);
        }
    }
}
