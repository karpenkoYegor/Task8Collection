using Task8Collection.Data.Entities;
using Task8Collection.Data.Repository.Intefaces;

namespace Task8Collection.Data.Repository
{
    public class FieldRepository : RepositoryBase<Field>, IFieldRepository
    {
        public FieldRepository(ApplicationDbContext context) : base(context)
        {
        }
    }

    public class IntFieldRepository : RepositoryBase<IntField>, IIntFieldRepository
    {
        public IntFieldRepository(ApplicationDbContext context) : base(context)
        {
        }
    }

    public class StringFieldRepository : RepositoryBase<StringField>, IStringFieldRepository
    {
        public StringFieldRepository(ApplicationDbContext context) : base(context)
        {
        }
    }

    public class BoolFieldRepository : RepositoryBase<BoolField>, IBoolFieldRepository
    {
        public BoolFieldRepository(ApplicationDbContext context) : base(context)
        {
        }
    }

    public class DateFieldRepository : RepositoryBase<DateField>, IDateFieldRepository
    {
        public DateFieldRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
