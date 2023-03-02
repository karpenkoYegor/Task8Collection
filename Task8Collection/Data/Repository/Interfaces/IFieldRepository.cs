using Task8Collection.Data.Entities;

namespace Task8Collection.Data.Repository.Intefaces;

public interface IFieldRepository : IRepositoryBase<Field>
{
    
}

public interface IBoolFieldRepository : IRepositoryBase<BoolField>
{
}

public interface IIntFieldRepository : IRepositoryBase<IntField>
{

}

public interface IDateFieldRepository : IRepositoryBase<DateField>
{

}

public interface IStringFieldRepository : IRepositoryBase<StringField>
{

}