
using DataAccessLayer.Abstract;
using EntityLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BulkyBookWeb.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult MainPage()
        {
            var result = _unitOfWork.productDal.GetAllWithProductAndCover();
            return View(result);
        }
        public IActionResult ProductDetails(int? productID)
        {
            if (productID is null)
            {
                return NotFound();
            }
            ProductDetailsModel productDetailsModel = new ProductDetailsModel()
            {
                product = _unitOfWork.productDal.GetAllWithProductAndCover().FirstOrDefault(x => x.ProductID == productID),
                Count = 1
            };
            return View(productDetailsModel);
        }
    }
}