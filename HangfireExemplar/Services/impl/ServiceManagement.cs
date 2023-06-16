namespace HangfireExemplar.Services.impl;

public class ServiceManagement : IServiceManagement
{
    private readonly ILogger<ServiceManagement> _logger;

    public ServiceManagement(ILogger<ServiceManagement> logger)
    {
        _logger = logger;
    }

    public void SendEmail()
    {
        _logger.LogInformation("Sending email... {S}}}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
    }

    public void UpdateDatabase()
    {
        _logger.LogInformation("Updating database... {S}}}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
    }

    public void GenerateMerchandise()
    {
        _logger.LogInformation("Generating merchandise... {S}}}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
    }

    public void SyncData()
    {
        _logger.LogInformation("Syncing data... {S}}}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
    }
}