
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.Utulity;
using EntityLayer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

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
        public IActionResult ProductDetails(int productID)
        {

            ShoppingCart shoppingCart = new ShoppingCart()
            {
                product = _unitOfWork.productDal.GetAllWithProductAndCover().FirstOrDefault(x => x.ProductID == productID),
                ProductID = productID,
                Count = 1
            };
            return View(shoppingCart);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult ProductDetails(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserID = claim.Value;

            var cartFromDB = _unitOfWork.ShoppingCartDal.GetByID(x => x.ApplicationUserID == claim.Value && x.ProductID==shoppingCart.ProductID);
            if (cartFromDB==null)
            {
                shoppingCart.product = null;
                _unitOfWork.ShoppingCartDal.Add(shoppingCart);
            }
            else
            {

                cartFromDB.Count += shoppingCart.Count;
            }
          
            _unitOfWork.save();

            return RedirectToAction("MainPage");
        }
    }
}