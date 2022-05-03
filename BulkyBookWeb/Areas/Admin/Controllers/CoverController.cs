using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public CoverController(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }
        public IActionResult GetAllCovers()
        {
            var result =_unitOfWork.coverDal.GetAll();
            return View(result);
        }
        public IActionResult CoverAdd()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult CoverAdd(Cover cover)
        {
            if (cover !=null && ModelState.IsValid)
            {
                TempData["Success"] = "Cover Added";
                _unitOfWork.coverDal.Add(cover);
                _unitOfWork.save();
                return RedirectToAction("GetAllCovers");
            }
            return View();
        }
        public IActionResult CoverDelete(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var result = _unitOfWork.coverDal.GetByID(x => x.CoverID == id);
            if (result!=null)
            {
                TempData["Success"] = "Cover Deleted";
                _unitOfWork.coverDal.Delete(result);
                _unitOfWork.save();
                return RedirectToAction("GetAllCovers");
            }
            return NotFound();
        }
        public IActionResult CoverUpdate(int? id)
        {
            if (id!=null)
            {
                var result = _unitOfWork.coverDal.GetByID(x => x.CoverID == id);
                return View(result);
            }
            return NotFound();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult CoverUpdate(Cover cover)
        {
            if (cover!=null && ModelState.IsValid)
            {
                TempData["Success"] = "Cover Updated";
                _unitOfWork.coverDal.Update(cover);
                _unitOfWork.save();
                return RedirectToAction("GetAllCovers");
            }
            return View(cover);
        }
    }
}
