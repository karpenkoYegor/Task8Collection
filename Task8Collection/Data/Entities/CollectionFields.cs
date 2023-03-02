namespace Task8Collection.Data.Entities;

public class CollectionFields
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int DataTypeField { get; set; }
    public Guid CollectionId { get; set; }
    public Collection Collection { get; set; }
}

public enum DataTypeFields
{
    Int,
    String,
    Date,
    Bool,
    LongString
}