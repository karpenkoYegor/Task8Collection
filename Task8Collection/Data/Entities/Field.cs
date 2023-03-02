namespace Task8Collection.Data.Entities;

public class Field
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid ItemId { get; set; }
    public Item Item { get; set; }

    public override string ToString()
    {
        return "-";
    }
}