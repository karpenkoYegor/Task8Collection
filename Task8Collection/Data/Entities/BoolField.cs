namespace Task8Collection.Data.Entities;

public class BoolField : Field
{
    public bool Value { get; set; }

    public override string ToString()
    {
        return Value.ToString();
    }
}