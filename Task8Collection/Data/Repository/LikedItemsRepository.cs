using Task8Collection.Data.Entities;
using Task8Collection.Data.Repository.Intefaces;

namespace Task8Collection.Data.Repository;

public class LikedItemsRepository : RepositoryBase<LikedItems>, ILikedItemsRepository
{
    public LikedItemsRepository(ApplicationDbContext context) : base(context)
    {
    }
}