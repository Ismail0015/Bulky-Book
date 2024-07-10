using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using BulkyBookWeb.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Repository.Services
{
    public class CategoryService : ICategory
    {
        public ApplicationDbContext context;

        public CategoryService(ApplicationDbContext context)
        {
                this.context = context;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var data = await context.Categories.ToListAsync();

            return data;
        }

        public async Task<int> AddCategory(Category category)
        {
           await context.Categories.AddAsync(category);
           await context.SaveChangesAsync();
            return category.Id;
        }

        public async Task<Category> EditCategory(int id)
        {
           var data = await context.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<bool> UpdateCategory(Category category)
        {
            bool status = false;

            if(category != null)
            {
                context.Update(category);
                await context.SaveChangesAsync();
                status = true;
            }
             
            return status;
        }

        public async Task<Category> DeleteCategory(int id)
        {
            var data = await context.Categories.Where(x => x.Id == id).FirstOrDefaultAsync();

            return data;
        }

        public async Task<bool> DeleteCategoryAsync(Category category)
        {
            var status = false;

            if(category != null)
            {
                 context.Remove(category);
                 await context.SaveChangesAsync();
                 status = true;
            }

            return status;
        }

        public async Task<Category> Details(int id)
        {
            var data = await context.Categories.FindAsync(id);
            return data;
        }
    }
}
