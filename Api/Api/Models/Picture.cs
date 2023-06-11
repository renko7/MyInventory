namespace MyInventory.Api.Models;

public class Picture
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }

    public int ItemId { get; set; }
    public Item Item { get; set; } = null!;
}
