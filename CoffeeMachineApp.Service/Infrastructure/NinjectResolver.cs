using CoffeeMachineApp.Domain.Abstract;
using CoffeeMachineApp.Domain.Concrete;
using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace CoffeeMachineApp.Service.Infrastructure
{
    public class NinjectResolver : System.Web.Http.Dependencies.IDependencyResolver, System.Web.Mvc.IDependencyResolver
    {
        private IKernel kernel;
        
        public NinjectResolver() : this(new StandardKernel()) { }
        
        public NinjectResolver(IKernel ninjectKernel)
        {
            kernel = ninjectKernel;
            AddBindings(kernel);
        }
        
        public IDependencyScope BeginScope()
        {
            return this;
        }
        
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        
        public void Dispose()
        {
            // do nothing
        }
        
        private void AddBindings(IKernel kernel)
        {
            //Mock<ICoffeeMachineRepository> mockMachines = new Mock<ICoffeeMachineRepository>();
            //mockMachines.Setup(c => c.CoffeeMachines).Returns(new List<CoffeeMachine> { new CoffeeMachine { MachineID = 1, SugarQuantity = 50 }}.AsQueryable());

            //Mock<IProductRepository> mockProducts = new Mock<IProductRepository>();
            //mockProducts.Setup(m => m.Products).Returns(new List<Product> { 
            //    new Product { ProductID = 1,  Name = "Tea", Quantity = 50, MachineID = 1}, 
            //    new Product { ProductID = 2, Name = "Coffee", Quantity = 50, MachineID = 1}, 
            //    new Product { ProductID = 3, Name = "Chocolate", Quantity = 50, MachineID = 1} 
            //}.AsQueryable());


            //Mock<IBadgeRepository> mockBadges = new Mock<IBadgeRepository>();
            //mockBadges.Setup(m => m.Badges).Returns(new List<Badge> { 
            //    new Badge { BadgeID = 1, BadgeName="Badge 1", ProductID = 1, SugarQuantity = 2}, 
            //    new Badge { BadgeID = 2, BadgeName="Badge 2", ProductID = 3, SugarQuantity = 1},
            //    new Badge { BadgeID = 3, BadgeName="Badge 3", ProductID = 0, SugarQuantity = 0}, 
            //}.AsQueryable());

            //kernel.Bind<ICoffeeMachineRepository>().ToConstant(mockMachines.Object);
            //kernel.Bind<IProductRepository>().ToConstant(mockProducts.Object);
            //kernel.Bind<IBadgeRepository>().ToConstant(mockBadges.Object);

            kernel.Bind<IProductRepository>().To<EFProductRepository>().InSingletonScope();
            kernel.Bind<IBadgeRepository>().To<EFBadgeRepository>().InSingletonScope();
            kernel.Bind<ICoffeeMachineRepository>().To<EFCoffeeMachineRepository>().InSingletonScope();
        }
    }
}