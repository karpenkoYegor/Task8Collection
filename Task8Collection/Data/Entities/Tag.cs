namespace Task8Collection.Data.Entities;

public class Tag
{
    public Guid Id { get; set; } = new Guid();
    public string Name { get; set; }
    public List<Item> Items { get; set; }
    public List<ItemTag> ItemTags { get; set; }
}