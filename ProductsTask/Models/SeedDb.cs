using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ProductsTask.Models
{
    public static class SeedDb
    {
        public static void CreateConnection(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();

            if (!context.Products.Any() && !context.Categories.Any())
            {
                
                var sport = new Category{Type = "Sport"};
                var music = new Category{Type = "Music"};
                var art = new Category{Type = "Art"};
                
                context.Products.AddRange(
                    new Product{Name = "Soccer ball", Category = sport},
                    new Product{Name = "Treadmill", Category = sport},
                    new Product{Name = "Dumbbell", Category = sport},
                    new Product{Name = "Headphones", Category = music},
                    new Product{Name = "Guitar", Category = music},
                    new Product{Name = "Drums", Category = music},
                    new Product{Name = "Music disks", Category = music},
                    new Product{Name = "Easel", Category = art},
                    new Product{Name = "Paint", Category = art},
                    new Product{Name = "Pencils", Category = art},
                    new Product{Name = "Art book", Category = art}

                );
                
                context.SaveChanges(); 
            }

            
        }
    }
}