using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        IUnitOfWork _uniOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _uniOfWork = unitOfWork;
        }
        public IActionResult GetAllCompanies()
        {
            return View();
        }
        public IActionResult CompanyUpSert(int? id)
        {
            Company company = new Company();
            if (id == 0 || id == null)
            {
                return View(company);
            }
            else
            {
                var update = _uniOfWork.companyDal.GetByID(x => x.CompanyID == id);
                return View(update);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult CompanyUpsert(Company company)
        {
            if (ModelState.IsValid)
            {
                if (company.CompanyID>=1)
                {
                    _uniOfWork.companyDal.Update(company);
                    TempData["Success"] = "Company Updated";
                }
                else
                {
                    _uniOfWork.companyDal.Add(company);
                    TempData["Success"] = "Company Added";
                }
                _uniOfWork.save();
                return RedirectToAction("GetAllCompanies");
            }
            return View(company);
        }
        public IActionResult CompanyDelete(int? id)
        {
            var Deleted = _uniOfWork.companyDal.GetByID(x => x.CompanyID == id);
            if (Deleted == null)
            {
                return NotFound();
            }
            _uniOfWork.companyDal.Delete(Deleted);
            _uniOfWork.save();
            TempData["Success"] = "Company Deleted";
            return RedirectToAction("GetAllCompanies");
        }
        public IActionResult GetAll()
        {
            var result = _uniOfWork.companyDal.GetAll();
            return Json(new { data = result });
        }
    }
}
