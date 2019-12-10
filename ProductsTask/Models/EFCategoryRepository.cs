﻿using System.Linq;
using ProductsTask.Models.Interfaces;

namespace ProductsTask.Models
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public EFCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Category> Categories => _context.Categories;

        public void SaveCategory(string category)
        {
            _context.Categories.Add(new Category{ Type = category});
            _context.SaveChanges();
        }
    }
}