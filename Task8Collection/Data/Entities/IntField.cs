namespace Task8Collection.Data.Entities;

public class IntField : Field
{
    public int Value { get; set; }

    public override string ToString()
    {
        return Value.ToString();
    }
}