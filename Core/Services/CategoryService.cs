using DAL;
using DAL.Repositories;

namespace Core.Services
{
    public class CategoryService : BaseService<Category>, IService<Category>
    {
        public CategoryService(IRepository<Category> genericRepository) : base(genericRepository)
        {
        }
    }
}
