﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProductsTask.Models;
using ProductsTask.Models.Interfaces;

namespace ProductsTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _repository;

        public CategoryController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return _repository.Categories.ToArray();
        }

        [HttpGet]
        [Route("SaveCategory")]
        public IEnumerable<Category> Get(string type)
        {
            if (!_repository.Categories.Any(c => c.Type == type))
            {
                _repository.SaveCategory(type);
            }

            return Get();
        }
    }
}