namespace Task8Collection.Models.Collection;

public class CollectionModel
{
    public Guid CollectionId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid ThemeId { get; set; }
    public string UserId { get; set; }
    public List<ItemModel> Items { get; set; } = new List<ItemModel>();
}