namespace Task8Collection.Data.Entities;

public class Comment
{
    public Guid Id { get; set; }
    public string CommentBody { get; set; }
    public Guid ItemId { get; set; }
    public Item Item { get; set; }
    public string UserName { get; set; }
}