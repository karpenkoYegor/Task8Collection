namespace Task8Collection.Data.Entities;

public class LikedItems
{
    public Guid Id { get; set; }
    public Guid ItemIdGuid { get; set; }
    public string UserGuid { get; set; }
    public User User { get; set; }
}