using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Infrastructure;
using DAL.UnitOfWorks;

namespace DAL.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IRepository<Product>
    {
        public ProductRepository(IDBUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
