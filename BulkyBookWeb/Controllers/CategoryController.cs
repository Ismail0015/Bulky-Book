using BulkyBookWeb.Data;
using BulkyBookWeb.Migrations;
using BulkyBookWeb.Models;
using BulkyBookWeb.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategory context;
        public CategoryController(ICategory context)
        {
            this.context = context;
        }
        #region Index
        public async Task<IActionResult> Index(string search)
        {
            var data = await context.GetCategoriesAsync();
            data = data.OrderBy(x=>x.DisplayOrder);
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(c => c.Name.Contains(search)).OrderBy(x => x.DisplayOrder);
                TempData["true"] = "yes";
            }

            return View(data);
        }
        #endregion

        #region Create Category
        //GET
        public async Task<IActionResult> Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        public async Task<IActionResult> Create(Category obj)
        {
           
            if (ModelState.IsValid)
            {
                await context.AddCategory(obj);

                if (obj.Id == 0)
                {
                    TempData["error"] = "Category not Created";
                }
                else
                {
                    TempData["success"] = "Created successfully";
                }

            }
            else
            {
                return View(obj);
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Edit
        //GET
        public async Task<IActionResult> Edit(int id)
        {
            Category category = new Category();

            if (id == 0)
            {
                return BadRequest();
            }
            else
            {
                category = await context.EditCategory(id);
                if(category == null)
                {
                    return NotFound();
                }
            }
            
            return View(category);
        }

        //POST
        [HttpPost]
        public async Task<IActionResult> Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
               bool data = await context.UpdateCategory(obj);
                if(data == true)
                {
                    TempData["success"] = "Edited successfully Updated";
                }
                else
                {
                    TempData["error"] = "Category not Updated";
                }
                
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        #endregion

        #region Delete
        //GET
        public async Task<IActionResult> Delete(int id)
        {
            Category category = new Category();

            if (id == 0)
            {
                return BadRequest();
            }

            category = await context.DeleteCategory(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        //POST
        [HttpPost]
        public async Task<IActionResult> Delete(Category category)
        {

            if (!ModelState.IsValid)
            {
                bool status = await context.DeleteCategoryAsync(category);
                if (status == false)
                {
                    TempData["error"] = "Deleted Performed Unsuccessfully";
                }
                else
                {
                    TempData["success"] = "Deleted successfully";
                }

                return RedirectToAction("Index");
            }

            return View(category);
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(int id)
        {
           var category = await context.Details(id);

            return View(category);
        }
        #endregion
    }
}
