using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsTask.Models.Interfaces;
using ProductsTask.Models.ViewModels;

namespace ProductsTask.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;
        public int PageSize { get; set; } = 6;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet]
        public ProductListViewModel Get(string category, int productPage = 1)
        {
            var allProducts = _repository.Products.Include("Category")
                .Where(p => category == null || p.Category.Type == category)
                .OrderBy(p => p.Name);

            var totalProduct = allProducts.Count();
            
            var pageProducts = allProducts.Skip((productPage - 1) * PageSize).Take(PageSize);
                
            
            return new ProductListViewModel
            {
                Products = pageProducts,
                
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    TotalItems = totalProduct,
                    ItemsPerPage = PageSize
                },
                
                CurrentType = category
            };
        }

        [HttpGet]
        [Route("SaveProduct")]
        public ProductListViewModel Get(string name, string category)
        {
            _repository.SaveProduct(name, category);
            return Get(null);
        }
    }
}