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
    public class TagService : BaseService<Tag>, IService<Tag>
    {
        public TagService(IRepository<Tag> genericRepository) : base(genericRepository)
        {
        }
    }
}
