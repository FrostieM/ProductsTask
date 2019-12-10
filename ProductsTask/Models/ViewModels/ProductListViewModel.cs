using System;
using System.Collections.Generic;

namespace ProductsTask.Models.ViewModels
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public string CurrentType { get; set; }
    }
}