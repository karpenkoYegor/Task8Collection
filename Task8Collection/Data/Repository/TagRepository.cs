using Task8Collection.Data;
using Task8Collection.Data.Entities;
using Task8Collection.Data.Repository;
using Task8Collection.Data.Repository.Intefaces;

namespace Task8Collection.Data.Repository
{
    public class TagRepository : RepositoryBase<Tag>, ITagRepository
    {
        public TagRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Tag GetTagByName(string name)
        {
            return Context.Tags.SingleOrDefault(t => t.Name == name);
        }
    }
}
