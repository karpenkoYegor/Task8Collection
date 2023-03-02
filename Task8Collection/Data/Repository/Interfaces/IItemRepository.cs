using Task8Collection.Data.Entities;

namespace Task8Collection.Data.Repository.Intefaces;

public interface IItemRepository : IRepositoryBase<Item>
{
    Item GetItemById(Guid id);
}