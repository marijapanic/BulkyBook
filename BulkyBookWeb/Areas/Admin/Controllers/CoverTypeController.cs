using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork _db)
        {
            _unitOfWork = _db;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> coverTypes = _unitOfWork.CoverType.GetAll();
            return View(coverTypes);
        }

        // Get.
        public IActionResult Create()
        {
            return View();
        }

        // Post.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(obj);
                _unitOfWork.Save();
                TempData.Add("success", "The CoverType is sucessfully created.");
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

            var CoverTypeObject = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);

            if (CoverTypeObject == null)
            {
                return NotFound();
            }

            return View(CoverTypeObject);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(obj);
                _unitOfWork.Save();
                TempData.Add("success", "The CoverType is sucessfully updated.");
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

            var coverTypeObject = _unitOfWork.CoverType.GetFirstOrDefault(x => x.Id == Id);

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
            var coverTypeObject = _unitOfWork.CoverType.GetFirstOrDefault(x => x.Id == Id);

            if (coverTypeObject == null)
            {
                return NotFound();
            }

            _unitOfWork.CoverType.Remove(coverTypeObject);
            _unitOfWork.Save();
            TempData["success"] = "The cover type is deleted";
            return RedirectToAction("Index");
        }
    }
}
