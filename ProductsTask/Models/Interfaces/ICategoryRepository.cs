﻿using System.Linq;

namespace ProductsTask.Models.Interfaces
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }

        void SaveCategory(string type);
    }
}