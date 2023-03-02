using Task8Collection.Data.Entities;

namespace Task8Collection.Models.Collection;

public class ItemModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string NormalName { get; set; }
    public int Likes { get; set; }
    public Guid CollectionId { get; set; }
    public List<Field> Fields { get; set; }
    public List<Tag> Tags { get; set; }
    public List<IntField> IntFields { get; set; } = new List<IntField>();
    public List<StringField> StringFields { get; set; } = new List<StringField>();
    public List<BoolField> BoolFields { get; set; } = new List<BoolField>();
    public List<DateField> DateFields { get; set; } = new List<DateField>();
}