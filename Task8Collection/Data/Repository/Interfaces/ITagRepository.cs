using Task8Collection.Data.Entities;

namespace Task8Collection.Data.Repository.Intefaces;

public interface ITagRepository : IRepositoryBase<Tag>
{
    Tag GetTagByName(string name);
}