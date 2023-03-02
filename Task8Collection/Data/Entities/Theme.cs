namespace Task8Collection.Data.Entities;

public class Theme
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Collection> Collections { get; set; }
}