namespace Task8Collection.Data.Entities;

public class Item
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Likes { get; set; }
    public Guid CollectionId { get; set; }
    public Collection Collection { get; set; }
    public List<Field> Fields { get; set; }
    public List<Tag> Tags { get; set; }
    public List<Comment> Comments { get; set; }
    public List<ItemTag> ItemTags { get; set; }
}