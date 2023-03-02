using Task8Collection.Data.Entities;
using Task8Collection.Data.Repository.Intefaces;

namespace Task8Collection.Data.Repository;

public class CollectionFieldsRepository : RepositoryBase<CollectionFields>, ICollectionFieldsRepository
{
    public CollectionFieldsRepository(ApplicationDbContext context) : base(context)
    {
    }

    public IQueryable<CollectionFields> GetCollectionFieldById(Guid collectionFieldId)
    {
        var collectionField = Context.CollectionFields.Where(c => c.Id == collectionFieldId);
        return collectionField;
    }

    public IQueryable<CollectionFields> GetCollectionFieldByCollectionId(Guid collectionId)
    {
        var collectionField = Context.CollectionFields.Where(c => c.CollectionId == collectionId);
        return collectionField;
    }
}