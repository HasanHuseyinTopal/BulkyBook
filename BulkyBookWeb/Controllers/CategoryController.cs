
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult GetAllCategories()
        {
            var result = _categoryService.GetAllCategory();
            return View(result);
        }

        public IActionResult CategoryAdd()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CategoryAdd(Category category)
        {
            if (category.CategoryName==category.CategoryDisplayOrder.ToString())
            {
                ModelState.AddModelError("CategoryName", "Two Sections Must Be Diffrent!");
            }
            if (ModelState.IsValid)
            {
                TempData["Success"] = "Category Added";
                _categoryService.CategoryAdd(category);
                return RedirectToAction("GetAllCategories");
            }
            return View();
        }

        public IActionResult CategoryUpdate (int? id)
        {
            var result = _categoryService.GetByID(id);
            if (id==null || id==0 || result ==null)
            {
                return NotFound();
            }
            result.CategoryCreatedDate = DateTime.Now;
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult CategoryUpdate (Category category)
        {
            if(category.CategoryName==category.CategoryDisplayOrder.ToString())
            {
                ModelState.AddModelError("CategoryName", "Two Sections Must Be Diffrent!");
            }
            if (ModelState.IsValid)
            {
                TempData["Success"] = "Category Updated";
                _categoryService.CategoryUpdate(category);
                return RedirectToAction("GetAllCategories");
            }
            return View();
        }
        public IActionResult CategoryDelete(int? id)
        {
            var result = _categoryService.GetByID(id);
            if (id==0 || id == null || result == null)
            {
                return NotFound();
            }
            TempData["Success"] = "Category Deleted";
            _categoryService.CategoryDelete(result);
            return RedirectToAction("GetAllCategories");
        }
    }
}
