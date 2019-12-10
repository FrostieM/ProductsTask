using System.Linq;
using ProductsTask.Models.Interfaces;

namespace ProductsTask.Models
{
    public class EFProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public EFProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products => _context.Products;
        public IQueryable<Category> Categories => _context.Categories;

        public void SaveProduct(string name, string categoryType)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Type == categoryType);
            if (category == null) return;

            var product = new Product{Name = name, Category = category};
            
            _context.Products.Add(product);
            _context.SaveChanges();
        }
    }
}