using System.Diagnostics;
using System.Text.Json;
using Azure.Messaging.ServiceBus;

public class ProduceUserSyncMessagesService : BackgroundService
{
    private readonly ILogger<ConsumeUserSyncMessagesService> _logger;

    private readonly ServiceBusClient _serviceBusClient;

    private readonly IUserManagementRepository _userManagementRepository;

    public ProduceUserSyncMessagesService(IUserManagementRepository userManagementRepository,
                                            ILogger<ConsumeUserSyncMessagesService> logger)
    {
        _userManagementRepository = userManagementRepository;
        _logger = logger;
        _serviceBusClient = new ServiceBusClient("Endpoint=sb://contactcenterpro.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=CTETDcGo31P4XxaqD/60aZ5IwQZ3cSVaw+ASbH8efS0=");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogDebug($"ProduceUserSyncMessagesService is starting.");

        stoppingToken.Register(() =>
            _logger.LogDebug($" ProduceUserSyncMessagesService background task is stopping."));

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var tenants = await _userManagementRepository.GetActiveTenantsAsync();
                var sender = _serviceBusClient.CreateSender("syncuserqueue");
                var ServiceTitanV2Client = new se.integrations.Client()
                {
                    BaseUrl = "https://servicetitan-service.integration.scheduleengine.net/"
                };

                foreach (Tenant tenant in tenants)
                {
                    try
                    {
                        var employees = await ServiceTitanV2Client.GetEmployeesAsync(tenant.ClientId);

                        _logger.LogInformation($"enquing {employees.Data.Count()} users for client {tenant.ClientId}, tenant Id {tenant.TenantId}");

                        foreach (se.integrations.IServiceTitanEmployee employee in employees.Data)
                        {
                            if (employee.Email is not null)
                            {
                                var userToSync = new UserToSyncMessage()
                                {
                                    EmployeeId = employee.Id,
                                    Name = employee.Name,
                                    Email = employee.Email,
                                    LoginName = employee.LoginName,
                                    ServiceTitanTenantId = tenant.TenantId
                                };

                                await sender.SendMessageAsync(new ServiceBusMessage(JsonSerializer.Serialize(userToSync)));
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.LogError($"enquing users for client {tenant.ClientId} failed because {e.Message}");
                    }
                }

                await Task.Delay(60000, stoppingToken);
            }
            catch (TaskCanceledException exception)
            {
                _logger.LogCritical(exception, "TaskCanceledException Error", exception.Message);
            }
        }

        _logger.LogDebug($"ProduceUserSyncMessagesService background task is stopping.");
    }
}

public class UserToSyncMessage
{
    public required double EmployeeId { get; set; }

    public required string Name { get; set; }

    public string? Email { get; set; }

    public required string LoginName { get; set; }

    public int ServiceTitanTenantId { get; set; }
}