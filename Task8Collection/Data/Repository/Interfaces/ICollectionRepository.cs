using Task8Collection.Data.Entities;

namespace Task8Collection.Data.Repository.Intefaces;

public interface ICollectionRepository : IRepositoryBase<Collection>
{
    IQueryable<Collection> GetCollectionByUserId(string userId);
    IQueryable<Collection> GetCollectionById(Guid collectionId);
}