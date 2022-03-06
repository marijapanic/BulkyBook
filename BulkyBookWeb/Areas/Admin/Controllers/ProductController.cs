using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;   

        public ProductController(IUnitOfWork db, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll();
            return View(productList);
        }

        // GET
        public IActionResult Upsert(int? id)
        {
            Product product = new();

            // Use projections for dropdowns.
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(item => new SelectListItem 
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(item => new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                })
            };

            // Product doesn't exist, create it.
            if (id == null || id == 0)
            {
                return View(productVM);
            }

            // Product exist, update it.
            else
            {
            }

            return View(productVM);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string retrieveImage = _webHostEnvironment.WebRootPath;

                if (retrieveImage != null)
                { 
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(retrieveImage, @"images\products");
                    var extension = Path.GetExtension(file.FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }

                    obj.Product.ImageURl = @"images\products\" + fileName + extension;
                }
                _unitOfWork.Product.Update(obj.Product);
                _unitOfWork.Save();
                TempData.Add("success", "The Product is sucessfully updated.");
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        // Get.
        public IActionResult Delete(int Id)
        {
            if (Id <= 0)
            {
                return NotFound();
            }

            var coverTypeObject = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == Id);

            if (coverTypeObject == null)
            {
                return NotFound();
            }

            return View(coverTypeObject);
        }

        // Post.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteOnPost(int Id)
        {
            var ProductObject = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == Id);

            if (ProductObject == null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(ProductObject);
            _unitOfWork.Save();
            TempData["success"] = "The cover type is deleted";
            return RedirectToAction("Index");
        }
    }
}
