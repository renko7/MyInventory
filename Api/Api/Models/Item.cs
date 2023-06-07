namespace MyInventory.Api.Models;

public class Item
{
    private readonly Guid _publicId = Guid.NewGuid();

    public int Id { get; init; }
    public Guid PublicId { get { return _publicId; } }
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<Item> Items { get; set; } = new List<Item>();

    public List<Picture> Picture { get; set; } = new();

    public Item() { }

    public Item(string name, string desc)
    {
        Name = name;
        Description = desc;
    }
}
