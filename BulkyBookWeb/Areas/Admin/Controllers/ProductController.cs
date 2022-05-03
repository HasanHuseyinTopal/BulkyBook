using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        IUnitOfWork _unitOfWork;
        IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _unitOfWork = unitOfWork;
        }
        public IActionResult GetAllProducts()
        {

            return View();
        }
        public IActionResult ProductUpSert(int? productID)
        {
            ProductViewModel productViewModel = new ProductViewModel()

            {
                CategoryList = _unitOfWork.categoryDal.GetAll().Select(x => new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.CategoryID.ToString()
                }),
                CoverList = _unitOfWork.coverDal.GetAll().Select(x => new SelectListItem
                {
                    Text = x.CoverName,
                    Value = x.CoverID.ToString()
                }),
                product = new Product()
                {

                }
            };
            if (productID == 0 || productID == null)
            {
                return View(productViewModel);

            }
            else if (productID >= 1)
            {
                productViewModel.product = _unitOfWork.productDal.GetByID(x => x.ProductID == productID);
                return View(productViewModel);
            }
            return NotFound();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult ProductUpSert(ProductViewModel productViewModel, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    var fileHashName = Guid.NewGuid().ToString();
                    var uploadPath = Path.Combine(wwwRootPath, @"images\products");
                    var imageExtension = Path.GetExtension(file.FileName);
                    if (productViewModel.product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath,productViewModel.product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(uploadPath, fileHashName + imageExtension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productViewModel.product.ImageUrl = @"\images\products\" + fileHashName + imageExtension;
                }
                if (productViewModel.product.ProductID == 0)
                {
                    _unitOfWork.productDal.Add(productViewModel.product);
                    TempData["Success"] = "Product Added";
                }
                else
                {
                    _unitOfWork.productDal.Update(productViewModel.product);
                    TempData["Succcess"] = "Product Updated";
                }
                _unitOfWork.save();
                return RedirectToAction("GetAllProducts");
            }
            return View(productViewModel);
        }
        public IActionResult ProductDelete(int? productID)
        {
            if (productID == null)
            {
                return NotFound();
            }
            var result = _unitOfWork.productDal.GetByID(x => x.ProductID == productID);

            if (result != null)
            {
                TempData["Success"] = "Product Deleted";
                _unitOfWork.productDal.Delete(result);
                if (System.IO.File.Exists(Path.Combine(_webHostEnvironment.WebRootPath,result.ImageUrl.TrimStart('\\'))))
                {
                    System.IO.File.Delete(Path.Combine(_webHostEnvironment.WebRootPath, result.ImageUrl.TrimStart('\\')));
                }
                _unitOfWork.save();
                return RedirectToAction("GetAllProducts");
            }
            return NotFound();
        }
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.productDal.GetAllWithProductAndCover();
            return Json(new { data = productList });
        }
    }
}
