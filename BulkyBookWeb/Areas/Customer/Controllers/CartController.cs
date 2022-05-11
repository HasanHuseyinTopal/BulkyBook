using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.Utulity;
using EntityLayer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Security.Claims;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        [BindProperty]
        public ShoppingCartVM shoppingCartVM { get; set; }
        IUnitOfWork _unitOfWork;
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            shoppingCartVM = new ShoppingCartVM()
            {
                OrderHeader = new(),
                ListCart = _unitOfWork.ShoppingCartDal.GetAllWithProduct(x => x.ApplicationUserID == claim.Value),
            };

            foreach (var item in shoppingCartVM.ListCart)
            {
                shoppingCartVM.OrderHeader.OrderTotal += GetPriceBasedOnQuantity(item.product.Price, item.Count);
            }
            return View(shoppingCartVM);
        }

        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            shoppingCartVM = new ShoppingCartVM()
            {
                OrderHeader = new(),
                ListCart = _unitOfWork.ShoppingCartDal.GetAllWithProduct(x => x.ApplicationUserID == claim.Value),
            };

            shoppingCartVM.OrderHeader.applicationUser = _unitOfWork.ApplicationUserDal.GetByID(x => x.Id == claim.Value);

            var result = shoppingCartVM.OrderHeader;
            result.Name = result.applicationUser.Name;
            result.PhoneNumber = result.applicationUser.PhoneNumber;
            result.StreetAdress = result.applicationUser.StreetAdress;
            result.City = result.applicationUser.City;
            result.State = result.applicationUser.State;
            result.PostalCode = result.applicationUser.PostalCode;

            foreach (var item in shoppingCartVM.ListCart)
            {
                shoppingCartVM.OrderHeader.OrderTotal += GetPriceBasedOnQuantity(item.product.Price, item.Count);
            }
            return View(shoppingCartVM);
        }
        [HttpPost]
        [ActionName("Summary")]
        [ValidateAntiForgeryToken]
        public IActionResult SummaryPost()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            shoppingCartVM.ListCart = _unitOfWork.ShoppingCartDal.GetAllWithProduct(x => x.ApplicationUserID == claim.Value);

            shoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
            shoppingCartVM.OrderHeader.OrderStatus = SD.StatusPending;
            shoppingCartVM.OrderHeader.OrderDate = DateTime.Now;
            shoppingCartVM.OrderHeader.ApplicationUserID = claim.Value;



            foreach (var item in shoppingCartVM.ListCart)
            {
                shoppingCartVM.OrderHeader.OrderTotal = GetPriceBasedOnQuantity(item.product.Price, item.Count);
            }

            _unitOfWork.OrderHeaderDal.Add(shoppingCartVM.OrderHeader);
            _unitOfWork.save();

            List<OrderDetail> orderDetails;
            foreach (var item in shoppingCartVM.ListCart)
            {
                orderDetails = new()
                {
                    new OrderDetail()
                    {
                        ProductID = item.ProductID,
                        OrderID = shoppingCartVM.OrderHeader.OrderHeaderID,
                        Price = item.product.Price,
                        Count = item.Count
                    }

                };
                _unitOfWork.OrderDetailDal.AddRange(orderDetails);
            }

            var domain = "https://localhost:7070/";
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions>()
                ,
                Mode = "payment",
                SuccessUrl = domain + $"customer/cart/OrderConfirmation?id={shoppingCartVM.OrderHeader.OrderHeaderID}",
                CancelUrl = domain + $"customer/cart/index",
            };

            foreach (var item in shoppingCartVM.ListCart)
            {
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.product.Price * 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.product.Title
                        },

                    },
                    Quantity = item.Count
                };
                options.LineItems.Add(sessionLineItem);
            }

            var service = new SessionService();
            Session session = service.Create(options);
            _unitOfWork.OrderHeaderDal.UpdateStripePaymentID(shoppingCartVM.OrderHeader.OrderHeaderID, session.Id, session.PaymentIntentId);
            _unitOfWork.save();

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);

           
        }

        public IActionResult OrderConfirmation(int id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeaderDal.GetByID(x => x.OrderHeaderID == id);
            var service = new SessionService();
            Session session = service.Get(orderHeader.SessionID);
            if (session.PaymentStatus.ToLower()=="paid")
            {
                _unitOfWork.OrderHeaderDal.UpdateStatus(id, SD.StatusApproved,SD.PaymentStatusApproved);
                _unitOfWork.save();
            }
            List<ShoppingCart> shoppingCartVM = _unitOfWork.ShoppingCartDal.GetAll(x=> x.ApplicationUserID==orderHeader.ApplicationUserID).ToList();
            _unitOfWork.ShoppingCartDal.DeleteRange(shoppingCartVM);
            _unitOfWork.save();
            return View(id);
        }
        private double GetPriceBasedOnQuantity(double Price, double Quantity)
        {
            return Price * Quantity;
        }
        public IActionResult Plus(int cartID)
        {
            var result = _unitOfWork.ShoppingCartDal.GetByID(x => x.ShoppingCartID == cartID);
            result.Count += 1;
            _unitOfWork.save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Minus(int cartID)
        {
            var result = _unitOfWork.ShoppingCartDal.GetByID(x => x.ShoppingCartID == cartID);
            if (result.Count <= 1)
            {
                _unitOfWork.ShoppingCartDal.Delete(result);
            }
            result.Count -= 1;
            _unitOfWork.save();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Remove(int cartID)
        {
            var result = _unitOfWork.ShoppingCartDal.GetByID(x => x.ShoppingCartID == cartID);
            _unitOfWork.ShoppingCartDal.Delete(result);
            _unitOfWork.save();
            return RedirectToAction(nameof(Index));
        }

    }
}
