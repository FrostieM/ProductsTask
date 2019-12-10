using System;
using System.Diagnostics;
using System.Linq;
using Moq;
using ProductsTask.Controllers;
using ProductsTask.Models;
using ProductsTask.Models.Interfaces;
using Xunit;

namespace ProductsTaskTests
{
    public class ProductControllerTests
    {

        private readonly Mock<IProductRepository> _repository;
        
        public ProductControllerTests()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new[]
            {
                new Product {Id = 1, Name = "P1", Category = new Category {Id = 1, Type = "T1"}},
                new Product {Id = 2, Name = "P2", Category = new Category {Id = 2, Type = "T2"}},
                new Product {Id = 3, Name = "P3", Category = new Category {Id = 1, Type = "T1"}},
                new Product {Id = 4, Name = "P4", Category = new Category {Id = 3, Type = "T3"}},
                new Product {Id = 5, Name = "P5", Category = new Category {Id = 1, Type = "T1"}},
                new Product {Id = 6, Name = "P6", Category = new Category {Id = 2, Type = "T2"}}
            }.AsQueryable());
            
            _repository = mock;
        }
        
        [Fact]
        public void CanPaginate()
        {
            var controller = new ProductController(_repository.Object) {PageSize = 4};
            var result = controller.Get(null, 2).Products.ToList();
            
            Assert.True(result.Count == 2);
            
            Assert.Equal(5, result[0].Id);
            Assert.Equal("P5", result[0].Name);
            
            Assert.Equal(6, result[1].Id);
            Assert.Equal("P6", result[1].Name);
        }

        [Fact]
        public void CanFilterProductByType()
        {
            var controller = new ProductController(_repository.Object) {PageSize = 10};
            var result = controller.Get("T1").Products.ToList();
            
            Assert.True(result.Count == 3);
            Assert.True(result.All(p => p.Category.Type == "T1"));
            
            Assert.Equal("P1", result[0].Name);
            Assert.Equal("P3", result[1].Name);
            Assert.Equal("P5", result[2].Name);
        }
        
    }
}