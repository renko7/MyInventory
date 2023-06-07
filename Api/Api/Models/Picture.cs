﻿namespace MyInventory.Api.Models;

public class Picture
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }

    public List<Item> Items { get; set; }
}
