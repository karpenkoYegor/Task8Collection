using Task8Collection.Data;
using Task8Collection.Data.Entities;
using Task8Collection.Data.Repository;
using Task8Collection.Data.Repository.Intefaces;

namespace Task8Collection.Data.Repository
{
    public class ThemeRepository : RepositoryBase<Theme>, IThemeRepository
    {
        public ThemeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
