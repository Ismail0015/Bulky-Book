using BulkyBookWeb.Models;

namespace BulkyBookWeb.Repository.Interfaces
{
    public interface ICategory
    {
         Task<IEnumerable<Category>> GetCategoriesAsync();
         Task<int> AddCategory(Category category);
         Task<Category> EditCategory(int id);
         Task<bool> UpdateCategory(Category category);
         Task<Category> DeleteCategory(int id);
         Task<bool> DeleteCategoryAsync(Category category);
         Task<Category>Details(int id);
    }
}
