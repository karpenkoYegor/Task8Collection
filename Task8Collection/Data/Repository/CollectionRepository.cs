using Microsoft.EntityFrameworkCore;
using Task8Collection.Data.Entities;
using Task8Collection.Data.Repository.Intefaces;

namespace Task8Collection.Data.Repository
{
    public class CollectionRepository : RepositoryBase<Collection>, ICollectionRepository
    {
        public CollectionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<Collection> GetCollectionByUserId(string userId)
        {
            var collection = Context.Collections
                .Where(c => c.UserId == userId)
                .Include(c => c.Theme)
                .Include(c => c.Items)
                .ThenInclude(i => i.Comments)
                .Include(c => c.Items)
                .ThenInclude(i => i.Fields)
                .Include(c => c.Items)
                .ThenInclude(i => i.Tags);
            return collection;
        }

        public IQueryable<Collection> GetCollectionById(Guid collectionId)
        {
            var collection = Context.Collections.Where(c => c.Id == collectionId);
            return collection;
        }
    }
}
