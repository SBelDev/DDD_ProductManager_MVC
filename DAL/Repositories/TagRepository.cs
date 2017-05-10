using DAL.UnitOfWorks;

namespace DAL.Repositories
{
    public class TagRepository : BaseRepository<Tag>, IRepository<Tag>
    {
        public TagRepository(IDBUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
