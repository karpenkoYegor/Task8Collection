namespace Task8Collection.Data.Entities;

public class Collection
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string UserId { get; set; }
    public virtual User User { get; set; }
    public Guid ThemeId { get; set; }
    public Theme Theme { get; set; }
    public List<Item> Items { get; set; }
    public List<CollectionFields> Fields { get; set; }
}