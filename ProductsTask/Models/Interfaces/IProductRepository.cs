using System.Linq;

namespace ProductsTask.Models.Interfaces
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        IQueryable<Category> Categories { get; }

        void SaveProduct(string name, string Category);
    }
}