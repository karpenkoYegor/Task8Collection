using Task8Collection.Data.Entities;

namespace Task8Collection.Models.Home;

public class HomeViewModel
{
    public List<Tag> Tags { get; set; }
    public List<Data.Entities.Item> LastAddedItems { get; set; }
    public List<Data.Entities.Collection> CollectionsWithMostAddedItems { get; set; }

}