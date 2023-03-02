namespace Task8Collection.Data.Entities;

public class DateField : Field
{
    public DateTime Value { get; set; }

    public override string ToString()
    {
        return Value.ToShortDateString();
    }
}