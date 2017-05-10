using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories;
using DAL.UnitOfWorks;

namespace Core.Services
{
    public class ProductService : BaseService<Product>, IService<Product>
    {
        public ProductService(IRepository<Product> genericRepository) : base(genericRepository)
        {
        }
    }
}

