namespace MyInventory.Api.Models;

public class Item
{
    public int Id { get; init; }
    public Guid PublicId { get; private set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<Item> Items { get; set; } = new List<Item>();

    public List<Picture> Picture { get; set; } = new();

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
