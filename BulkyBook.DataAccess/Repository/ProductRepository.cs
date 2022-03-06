using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDBContext _repository;
        public ProductRepository(ApplicationDBContext db) : base(db)
        {
            _repository = db;
        }

        public void Save()
        {
            _repository.SaveChanges();
        }

        public void Update(Product productObj)
        {
            var productFromDatabase = _repository.Products.FirstOrDefault(obj => obj.Id == productObj.Id);

            if (productFromDatabase != null)
            {
                productFromDatabase.Title = productObj.Title;
                productFromDatabase.Description = productObj.Description;
                productFromDatabase.ISBN = productObj.ISBN;
                productFromDatabase.Price = productObj.Price;
                productFromDatabase.Price100 = productObj.Price100;
                productFromDatabase.Price50 = productObj.Price50;
                productFromDatabase.ListPrice = productObj.ListPrice;
                productFromDatabase.Author = productObj.Author;
                productFromDatabase.CategoryId = productObj.CategoryId;
                productFromDatabase.CoverTypeId = productObj.CoverTypeId;

                if (productFromDatabase.ImageURl != null)
                {
                    productFromDatabase.ImageURl = productObj.ImageURl;
                }
            }
        }
    }
}
