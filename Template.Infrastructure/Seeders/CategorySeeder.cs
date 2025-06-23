using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Entities.Materials;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Seeders;

public class CategorySeeder(TemplateDbContext dbContext) : ICategorySeeder
{
    public async Task Seed()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Roles.Any())
            {
                var categories = GetCategories();
                dbContext.Categories.AddRange(categories);
                await dbContext.SaveChangesAsync();
            }
        }
    }
    public List<Category> GetCategories()
    {
        List<Category> list = [
       new Category()
        {
            Id = 1,
            Name = "Surgery",

        },
        new Category()
        {
            Id = 2,
            Name = "Recovery",

        }
        ];
        return list;
    }
}
