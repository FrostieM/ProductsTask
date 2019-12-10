using System.Linq;
using Moq;
using ProductsTask.Controllers;
using ProductsTask.Models;
using ProductsTask.Models.Interfaces;
using Xunit;

namespace ProductsTaskTests
{
    public class CategoryControllerTests
    {
        private readonly Mock<ICategoryRepository> _repository;
        
        public CategoryControllerTests()
        {
            var mock = new Mock<ICategoryRepository>();
            mock.Setup(c => c.Categories).Returns(new[]
            {
                new Category {Id = 1, Type = "C1"},
                new Category {Id = 2, Type = "C2"},
                new Category {Id = 3, Type = "C3"},
                new Category {Id = 4, Type = "C4"},
                new Category {Id = 5, Type = "C5"},
            }.AsQueryable());

            _repository = mock;
        }

        [Fact]
        public void CanGetCategories()
        {
            var controller = new CategoryController(_repository.Object);
            var result = controller.Get().ToList();
            
            Assert.True(result.Count == 5);
            Assert.Equal("C1", result[0].Type);
            Assert.Equal("C2", result[1].Type);
            Assert.Equal("C3", result[2].Type);
            Assert.Equal("C4", result[3].Type);
            Assert.Equal("C5", result[4].Type);
        }

        [Fact]
        public void CannotSaveCategory()
        {
            var controller = new CategoryController(_repository.Object);
            var result = controller.Get("C2").ToList();
            
            _repository.Verify(m => m.SaveCategory(It.IsAny<string>()), Times.Never);
            
            Assert.True(result.Count == 5);
        }
        
        [Fact]
        public void CanSaveCategory()
        {
            var controller = new CategoryController(_repository.Object);
            controller.Get("C7");
            
            _repository.Verify(m => m.SaveCategory(It.IsAny<string>()), Times.Once);
        }
    }
}