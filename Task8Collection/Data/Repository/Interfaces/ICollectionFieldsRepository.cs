using Task8Collection.Data.Entities;

namespace Task8Collection.Data.Repository.Intefaces;

public interface ICollectionFieldsRepository : IRepositoryBase<CollectionFields>
{
    IQueryable<CollectionFields> GetCollectionFieldById(Guid collectionFieldId);
    IQueryable<CollectionFields> GetCollectionFieldByCollectionId(Guid collectionId);
}