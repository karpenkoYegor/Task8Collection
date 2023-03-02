using Microsoft.AspNetCore.Mvc;
using Task8Collection.Data.Entities;

namespace Task8Collection.Models.Collection;

public class CollectionEditViewModel
{
    public Data.Entities.Collection Collection { get; set; }
    public List<Theme> Themes { get; set; }
    public CollectionFieldModel CollectionField { get; set; }
    public CollectionModel CollectionModel { get; set; }
    public CollectionItem CollectionItem { get; set; }
    public string UserName { get; set; }
    public ItemModel Item { get; set; }
}

public enum SortState
{
    Asc, Desc
}