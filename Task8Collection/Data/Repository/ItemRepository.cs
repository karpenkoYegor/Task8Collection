using Microsoft.EntityFrameworkCore;
using Task8Collection.Data.Entities;
using Task8Collection.Data.Repository.Intefaces;

namespace Task8Collection.Data.Repository
{
    public class ItemRepository : RepositoryBase<Item>, IItemRepository
    {
        public ItemRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Item GetItemById(Guid id)
        {
            return Context.Items
                .Where(i => i.Id == id)
                .Include(i => i.Fields)
                .Include(i => i.Collection)
                .Include(i => i.Comments)
                .Include(i => i.Tags)
                .Single();
        }
    }
}
