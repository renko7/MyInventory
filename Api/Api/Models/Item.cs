namespace MyInventory.Api.Models;

public class Item
{
    private readonly Guid _id = Guid.NewGuid();

    public Guid Id { get { return _id; } }
    public string Name { get; set; }
    public string Description { get; set; }

    public Item() { }

    public Item(string name, string desc)
    {
        Name = name;
        Description = desc;
    }
}
