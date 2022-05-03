using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult GetAllCategories()
        {
            var result = _unitOfWork.categoryDal.GetAll();
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
                _unitOfWork.categoryDal.Add(category);
                _unitOfWork.save();
                return RedirectToAction("GetAllCategories");
            }
            return View();
        }

        public IActionResult CategoryUpdate (int? id)
        {
            
            if (id==null || id==0)
            {
                return NotFound();
            }
            var result = _unitOfWork.categoryDal.GetByID(x=> x.CategoryID==id);
            if (result != null)
            {
                result.CategoryCreatedDate = DateTime.Now;
                return View(result);
            }
            return NotFound();
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult CategoryUpdate (Category category)
        {
            if (ModelState.IsValid)
            {
                TempData["Success"] = "Category Updated";
                _unitOfWork.categoryDal.Update(category);
                _unitOfWork.save();
                return RedirectToAction("GetAllCategories");
            }
            return View(category);
        }
        public IActionResult CategoryDelete(int? id)
        {
           
            if (id==0 || id == null )
            {
                return NotFound();
            }
            var result = _unitOfWork.categoryDal.GetByID(x=> x.CategoryID==id);
            if (result != null)
            {
                TempData["Success"] = "Category Deleted";
                _unitOfWork.categoryDal.Delete(result);
                _unitOfWork.save();
                return RedirectToAction("GetAllCategories");
            }
            return NotFound();

        }
    }
}
