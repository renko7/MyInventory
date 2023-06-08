﻿namespace MyInventory.Api.Models;

public class AppSettings
{
    public string DatabaseConnectionString { get; set; } = string.Empty;
    public string StorageAccountName { get; set; } = string.Empty;
    public string StorageAccountConnectionString { get; set; } = string.Empty;
}
