using CoffeeMachineApp.Domain.Abstract;
using CoffeeMachineApp.Domain.Entities;
using CoffeeMachineApp.Service.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace CoffeMachine.UnitTests.Service
{
    [TestClass]
    public class ProductControllerTest
    {
        [TestMethod]
        public void Get_Products()
        {
            // Arrange
            Mock<IProductRepository> mockProducts = new Mock<IProductRepository>();
            mockProducts.Setup(m => m.Products).Returns(new List<Product> { 
                new Product { ProductID = 1,  Name = "Tea", Quantity = 50, MachineID = 1}, 
                new Product { ProductID = 2, Name = "Coffee", Quantity = 50, MachineID = 1}, 
                new Product { ProductID = 3, Name = "Chocolate", Quantity = 50, MachineID = 1} 
            }.AsQueryable());

            ProductController productController = new ProductController(mockProducts.Object);

            // Act
            IEnumerable<Product> result = productController.Get();

            // Assert
            Product[] productArray = result.ToArray(); 
            Assert.IsTrue(productArray.Length == 3);
            Assert.AreEqual(productArray[0].Name, "Tea");
            Assert.AreEqual(productArray[1].Name, "Coffee");
            Assert.AreEqual(productArray[2].Name, "Chocolate");
            Assert.AreEqual(productArray[0].Quantity, 50);
            Assert.AreEqual(productArray[1].Quantity, 50);
            Assert.AreEqual(productArray[2].Quantity, 50);
        }

        [TestMethod]
        public void Get_Product_By_Id()
        {
            // Arrange
            Mock<IProductRepository> mockProducts = new Mock<IProductRepository>();
            mockProducts.Setup(m => m.Products).Returns(new List<Product> { 
                new Product { ProductID = 1,  Name = "Tea", Quantity = 50, MachineID = 1}, 
                new Product { ProductID = 2, Name = "Coffee", Quantity = 50, MachineID = 1}
            }.AsQueryable());

            ProductController productController = new ProductController(mockProducts.Object);

            // Act
            Product resultTea = productController.Get(1);
            Product resultCoffee = productController.Get(2);

            // Assert
            Assert.AreEqual(resultTea.Name, "Tea");
            Assert.AreEqual(resultTea.Quantity, 50);
            Assert.AreEqual(resultCoffee.Name, "Coffee");
            Assert.AreEqual(resultCoffee.Quantity, 50);
        }

        [TestMethod]
        public void Get_Product_By_Wrong_Id()
        {
            // Arrange
            Mock<IProductRepository> mockProducts = new Mock<IProductRepository>();
            mockProducts.Setup(m => m.Products).Returns(new List<Product> { 
                new Product { ProductID = 1,  Name = "Tea", Quantity = 50, MachineID = 1}, 
                new Product { ProductID = 2, Name = "Coffee", Quantity = 50, MachineID = 1}
            }.AsQueryable());

            ProductController productController = new ProductController(mockProducts.Object);

            // Act
            Product result = productController.Get(3);

            // Assert
            Assert.AreEqual(result, null);
        }
        
        [TestMethod]
        public void Can_Delete_Product()
        {
            // Arrange
            IList<Product> products = new List<Product>() 
            {
                new Product { ProductID = 1,  Name = "Tea", Quantity = 50, MachineID = 1}, 
                new Product { ProductID = 2, Name = "Coffee", Quantity = 50, MachineID = 1}
            };
            Mock<IProductRepository> mockProducts = new Mock<IProductRepository>();
            mockProducts.Setup(m => m.Products).Returns(products.AsQueryable());

            // save product method
            mockProducts.Setup(m => m.DeleteProduct(It.IsAny<int>()))
                .Returns((int productID) =>
                {
                    Product dbEntry = products.FirstOrDefault(p => p.ProductID == productID);
                    if (dbEntry != null)
                    {
                        products.Remove(dbEntry);
                    }
                    return dbEntry;
                });

            IProductRepository repo = mockProducts.Object;

            // Act
            Product product = repo.DeleteProduct(1);
            
            // Assert
            Assert.IsTrue(repo.Products.ToArray().Length == 1);
            Assert.AreEqual(product.Name, "Tea");
            Assert.AreEqual(product.Quantity, 50);
        }

        [TestMethod]
        public void Cant_Delete_Product()
        {
            // Arrange
            IList<Product> products = new List<Product>() 
            {
                new Product { ProductID = 1,  Name = "Tea", Quantity = 50, MachineID = 1}, 
                new Product { ProductID = 2, Name = "Coffee", Quantity = 50, MachineID = 1}
            };
            Mock<IProductRepository> mockProducts = new Mock<IProductRepository>();
            mockProducts.Setup(m => m.Products).Returns(products.AsQueryable());

            // save product method
            mockProducts.Setup(m => m.DeleteProduct(It.IsAny<int>()))
                .Returns((int productID) =>
                {
                    Product dbEntry = products.FirstOrDefault(p => p.ProductID == productID);
                    if (dbEntry != null)
                    {
                        products.Remove(dbEntry);
                    }
                    return dbEntry;
                });

            IProductRepository repo = mockProducts.Object;

            // Act
            Product product = repo.DeleteProduct(3);

            // Assert
            Assert.IsTrue(repo.Products.ToArray().Length == 2);
            Assert.AreEqual(product, null);
        }

        [TestMethod]
        public void Update_Product()
        {
            // Arrange
            IList<Product> products = new List<Product>() 
            {
                new Product { ProductID = 1,  Name = "Tea", Quantity = 50, MachineID = 1}, 
                new Product { ProductID = 2, Name = "Coffee", Quantity = 50, MachineID = 1}
            };
            Mock<IProductRepository> mockProducts = new Mock<IProductRepository>();
            mockProducts.Setup(m => m.Products).Returns(products.AsQueryable());

            // save product method
            mockProducts.Setup(m => m.SaveProduct(It.IsAny<Product>()))
                .Callback((Product product) =>
                {
                    if (product.ProductID == 0)
                    {
                        products.Add(product);
                    }
                    else
                    {
                        Product dbEntry = products.FirstOrDefault(p => p.ProductID == product.ProductID);
                        if (dbEntry != null)
                        {
                            dbEntry.MachineID = product.MachineID;
                            dbEntry.Name = product.Name;
                            dbEntry.Quantity = product.Quantity;
                        }
                    }
                })
                .Verifiable();

            IProductRepository repo = mockProducts.Object;

            // Act
            repo.SaveProduct(new Product { ProductID = 1, Name = "Chocolate", Quantity = 60, MachineID = 1 });
            IEnumerable<Product> result = repo.Products;

            // Assert
            Product[] productArray = result.ToArray();
            Assert.IsTrue(productArray.Length == 2);
            Assert.AreEqual(productArray[0].Name, "Chocolate");
            Assert.AreEqual(productArray[0].Quantity, 60);
        }

        [TestMethod]
        public void Add_Product()
        {
            // Arrange
            IList<Product> products = new List<Product>() 
            {
                new Product { ProductID = 1,  Name = "Tea", Quantity = 50, MachineID = 1}, 
                new Product { ProductID = 2, Name = "Coffee", Quantity = 50, MachineID = 1}
            };
            Mock<IProductRepository> mockProducts = new Mock<IProductRepository>();
            mockProducts.Setup(m => m.Products).Returns(products.AsQueryable());

            // save product method
            mockProducts.Setup(m => m.SaveProduct(It.IsAny<Product>()))
                .Callback((Product product) =>
                    {
                        if (product.ProductID == 0)
                        {
                            products.Add(product);
                        }
                        else
                        {
                            Product dbEntry = products.FirstOrDefault(p => p.ProductID == product.ProductID);
                            if (dbEntry != null)
                            {
                                dbEntry.MachineID = product.MachineID;
                                dbEntry.Name = product.Name;
                                dbEntry.Quantity = product.Quantity;
                            }
                        }
                    })
                .Verifiable();

            IProductRepository repo = mockProducts.Object;
            
            // Act
            repo.SaveProduct(new Product { Name = "Chocolate", Quantity = 50, MachineID = 1 });
            IEnumerable<Product> result = repo.Products;
            
            // Assert
            Product[] productArray = result.ToArray();
            Assert.IsTrue(productArray.Length == 3);
            Assert.AreEqual(productArray[2].Name, "Chocolate");
        }

        [TestMethod]
        public void Add_Product_With_Wrong_Id()
        {
            // Arrange
            IList<Product> products = new List<Product>() 
            {
                new Product { ProductID = 1,  Name = "Tea", Quantity = 50, MachineID = 1}, 
                new Product { ProductID = 2, Name = "Coffee", Quantity = 50, MachineID = 1}
            };
            Mock<IProductRepository> mockProducts = new Mock<IProductRepository>();
            mockProducts.Setup(m => m.Products).Returns(products.AsQueryable());

            // save product method
            mockProducts.Setup(m => m.SaveProduct(It.IsAny<Product>()))
                .Callback((Product product) =>
                {
                    if (product.ProductID == 0)
                    {
                        products.Add(product);
                    }
                    else
                    {
                        Product dbEntry = products.FirstOrDefault(p => p.ProductID == product.ProductID);
                        if (dbEntry != null)
                        {
                            dbEntry.MachineID = product.MachineID;
                            dbEntry.Name = product.Name;
                            dbEntry.Quantity = product.Quantity;
                        }
                    }
                })
                .Verifiable();

            IProductRepository repo = mockProducts.Object;

            // Act
            repo.SaveProduct(new Product { ProductID = 3, Name = "Chocolate", Quantity = 50, MachineID = 1 });
            IEnumerable<Product> result = repo.Products;

            // Assert
            Product[] productArray = result.ToArray();
            Assert.IsTrue(productArray.Length == 2);
        }
    }
}
