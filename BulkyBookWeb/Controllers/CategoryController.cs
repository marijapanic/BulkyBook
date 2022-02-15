using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork db)
        {
                _unitOfWork = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
            return View(objCategoryList);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "Name and display order can not be the same value");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData.Add("success", "The category is sucessfully created.");
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        // GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //var categoryObject = _unitOfWork.Category.Categories.Find(id);
            var categoryObject = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);

            if (categoryObject == null)
            {
                return NotFound();
            }

            return View(categoryObject);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "Name and display order can not be the same value");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData.Add("success", "The category is sucessfully updated.");
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryObject = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);

            if (categoryObject == null)
            {
                return NotFound();
            }

            return View(categoryObject);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteOnPost(int? Id)
        {
            var categoryObject = _unitOfWork.Category.GetFirstOrDefault(c=> c.Id == Id);
            if (categoryObject == null)
            {
                return NotFound();
            }

            _unitOfWork.Category.Remove(categoryObject);
            _unitOfWork.Save();
            TempData.Add("success", "The category is sucessfully deleted.");
            return RedirectToAction("Index");
        }
    }
}
