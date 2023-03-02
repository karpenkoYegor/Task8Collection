using Task8Collection.Data.Entities;

namespace Task8Collection.Data;

public interface ISearchEngine<T>
{
    void AddItemsToIndex(IEnumerable<T> items);
    IEnumerable<T> Search(string searchTerm);
}