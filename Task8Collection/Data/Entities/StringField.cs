namespace Task8Collection.Data.Entities;

public class StringField : Field
{
    public string? Value { get; set; }
    public bool IsLong { get; set; }
    public override string ToString()
    {
        return Value;
    }
}