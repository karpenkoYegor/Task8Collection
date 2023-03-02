using Task8Collection.Data.Entities;
using Task8Collection.Data.Repository.Intefaces;

namespace Task8Collection.Data.Repository;

public class ItemTagRepository : RepositoryBase<ItemTag>, IItemTagRepository
{
    public ItemTagRepository(ApplicationDbContext context) : base(context)
    {
    }
}