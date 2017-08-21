using CoffeeMachineApp.Domain.Entities;
using System.Linq;

namespace CoffeeMachineApp.Domain.Abstract
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        void SaveProduct(Product product);
        Product DeleteProduct(int productID);
    }
}
