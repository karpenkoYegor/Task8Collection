using Task8Collection.Data;
using Task8Collection.Data.Repository;
using Task8Collection.Data.Repository.Intefaces;

namespace Task8Collection.Data.Repository;

public class RepositoryWrapper : IRepositoryWrapper
{
    private ApplicationDbContext _context;
    private IUserRepository _userRepository;
    private ICollectionRepository _collectionRepository;
    private IThemeRepository _themeRepository;
    private IItemRepository _itemRepository;
    private ITagRepository _tagRepository;
    private IFieldRepository _fieldRepository;
    private IIntFieldRepository _intFieldRepository;
    private IStringFieldRepository _stringFieldRepository;
    private IDateFieldRepository _dateFieldRepository;
    private IBoolFieldRepository _boolFieldRepository;
    private ICommentRepository _commentRepository;
    private ICollectionFieldsRepository _collectionFieldsRepository;
    private ILikedItemsRepository _likedItemsRepository;
    private IItemTagRepository _itemTagRepository;
    public RepositoryWrapper(ApplicationDbContext context)
    {
        _context = context;
    }

    public IUserRepository UserRepository {
        get
        {
            if (_userRepository == null)
                _userRepository = new UserRepository(_context);
            return _userRepository;
        }
    }

    public ICollectionRepository CollectionRepository
    {
        get
        {
            if(_collectionRepository == null)
                _collectionRepository = new CollectionRepository(_context);
            return _collectionRepository;
        }
    }

    public IThemeRepository ThemeRepository
    {
        get
        {
            if(_themeRepository == null)
                _themeRepository = new ThemeRepository(_context);
            return _themeRepository;
        }
    }

    public IItemRepository ItemRepository
    {
        get
        {
            if (_itemRepository == null)
                _itemRepository = new ItemRepository(_context);
            return _itemRepository;
        }
    }

    public ITagRepository TagRepository {
        get
        {
            if (_tagRepository == null)
                _tagRepository = new TagRepository(_context);
            return _tagRepository;
        }
    }
    public IFieldRepository FieldRepository {
        get
        {
            if (_fieldRepository == null)
                _fieldRepository = new FieldRepository(_context);
            return _fieldRepository;
        }
    }
    public IBoolFieldRepository BoolFieldRepository
    {
        get
        {
            if (_boolFieldRepository == null)
                _boolFieldRepository = new BoolFieldRepository(_context);
            return _boolFieldRepository;
        }
    }
    public IDateFieldRepository DateFieldRepository
    {
        get
        {
            if (_dateFieldRepository == null)
                _dateFieldRepository = new DateFieldRepository(_context);
            return _dateFieldRepository;
        }
    }
    public IIntFieldRepository IntFieldRepository
    {
        get
        {
            if (_intFieldRepository == null)
                _intFieldRepository = new IntFieldRepository(_context);
            return _intFieldRepository;
        }
    }
    public IStringFieldRepository StringFieldRepository
    {
        get
        {
            if (_stringFieldRepository == null)
                _stringFieldRepository = new StringFieldRepository(_context);
            return _stringFieldRepository;
        }
    }
    public ICommentRepository CommentRepository {
        get
        {
            if (_commentRepository == null)
                _commentRepository = new CommentRepository(_context);
            return _commentRepository;
        }
    }

    public ICollectionFieldsRepository CollectionFieldsRepository
    {
        get
        {
            if (_collectionFieldsRepository == null)
            {
                _collectionFieldsRepository = new CollectionFieldsRepository(_context);
            }
            return _collectionFieldsRepository;
        }
    }

    public ILikedItemsRepository LikedItemsRepository {
        get
        {
            if (_likedItemsRepository == null)
            {
                _likedItemsRepository = new LikedItemsRepository(_context);
            }
            return _likedItemsRepository;
        }
    }

    public IItemTagRepository ItemTagRepository {
        get
        {
            if (_itemTagRepository == null)
            {
                _itemTagRepository = new ItemTagRepository(_context);
            }
            return _itemTagRepository;
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}