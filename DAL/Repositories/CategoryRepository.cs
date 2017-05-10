using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Infrastructure;
using DAL.UnitOfWorks;

namespace DAL.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, IRepository<Category>
    {
        public CategoryRepository(IDBUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
