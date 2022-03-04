using BulkyBook.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Category { get; private set; }
        public ICoverTypeRepository CoverType { get; private set; }
        private ApplicationDBContext _dbContext;

        public UnitOfWork(ApplicationDBContext db)
        {
            _dbContext = db;
            Category = new CategoryRepository(db);
            CoverType = new CoverTypeRepository(db);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
