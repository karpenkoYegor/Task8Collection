namespace Task8Collection.Models.Collection;

public class CollectionItem
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Likes { get; set; }
    public int CollectionId { get; set; }
}