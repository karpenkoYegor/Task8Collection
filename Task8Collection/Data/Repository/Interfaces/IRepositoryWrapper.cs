namespace Task8Collection.Data.Repository.Intefaces;

public interface IRepositoryWrapper
{
    IUserRepository UserRepository { get; }
    ICollectionRepository CollectionRepository { get; }
    IThemeRepository ThemeRepository { get; }
    IItemRepository ItemRepository { get; }
    ITagRepository TagRepository { get; }
    IFieldRepository FieldRepository { get; }
    IDateFieldRepository DateFieldRepository { get; }
    IIntFieldRepository IntFieldRepository { get; }
    IStringFieldRepository StringFieldRepository { get; }
    IBoolFieldRepository BoolFieldRepository { get; }
    ICommentRepository CommentRepository { get; }
    ICollectionFieldsRepository CollectionFieldsRepository { get; }
    ILikedItemsRepository LikedItemsRepository { get; }
    IItemTagRepository ItemTagRepository { get; }
    void Save();
}