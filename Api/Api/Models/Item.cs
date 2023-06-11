namespace MyInventory.Api.Models;

public class Item
{
    public int Id { get; init; }
    public Guid PublicId { get; private set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public int? ParentItemId { get; set; }
    public Item? ParentItem { get; set; }
    public ICollection<Item> ChildItems { get; set; } = new List<Item>();

    public ICollection<Picture> Pictures { get; set; } = new List<Picture>();

    public Item() 
    {
        PublicId = Guid.NewGuid();
    }

    public Item(string name, string desc) : this()
    {
        Name = name;
        Description = desc;
    }
}
