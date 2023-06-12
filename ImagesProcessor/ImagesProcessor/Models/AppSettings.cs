namespace ImageProcessorFunction.Models;

class AppSettings
{
    public string AzureWebJobsStorage { get; set; }
    public string FUNCTIONS_WORKER_RUNTIME { get; set; }
    public string DatabaseConnectionString { get; set; } = string.Empty;
    public string StorageAccountName { get; set; } = string.Empty;
    public string StorageAccountConnectionString { get; set; } = string.Empty;
}
