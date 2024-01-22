using System.Diagnostics;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using se.integrations;

public class ConsumeUserSyncMessagesService : BackgroundService
{
    private readonly ILogger<ConsumeUserSyncMessagesService> _logger;

    private readonly ServiceBusClient _serviceBusClient;

    private readonly ServiceBusProcessor _serviceBusProcesor;

    private readonly IUserManagementRepository _userManagementRepository;

    public ConsumeUserSyncMessagesService(IUserManagementRepository userManagementRepository,
                                            ILogger<ConsumeUserSyncMessagesService> logger)
    {
        _userManagementRepository = userManagementRepository;
        _logger = logger;
        _serviceBusClient = new ServiceBusClient("Endpoint=sb://contactcenterpro.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=CTETDcGo31P4XxaqD/60aZ5IwQZ3cSVaw+ASbH8efS0=");
        _serviceBusProcesor = _serviceBusClient.CreateProcessor("syncuserqueue");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogDebug($"ConsumeUserSyncMessagesService is starting.");

        stoppingToken.Register(() =>
            _logger.LogDebug($" ConsumeUserSyncMessagesService background task is stopping."));

        // add handler to process messages
        _serviceBusProcesor.ProcessMessageAsync += MessageHandler;

        // add handler to process any errors
        _serviceBusProcesor.ProcessErrorAsync += ErrorHandler;

        await _serviceBusProcesor.StartProcessingAsync();

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(5000, stoppingToken);
            }
            catch (TaskCanceledException exception)
            {
                _logger.LogCritical(exception, "TaskCanceledException Error", exception.Message);
            }
        }

        await _serviceBusProcesor.StopProcessingAsync();
        _logger.LogDebug($"ConsumeUserSyncMessagesService background task is stopping.");
    }

    async Task MessageHandler(ProcessMessageEventArgs args)
    {
        if (args.Message.Body is not null)
        {
            UserToSyncMessage userToSync = JsonSerializer.Deserialize<UserToSyncMessage>(args.Message.Body.ToString());

            var user = await _userManagementRepository.GetUserByEmailAsync(userToSync.Email);

            if (user is null)
            {
                user = new User()
                {
                    Name = userToSync.Name,
                    Email = userToSync.Email,
                    ServiceTitanId = userToSync.EmployeeId.ToString(),
                    LoginName = userToSync.LoginName,
                    OrgNodes = {
                    new UserOrgNode() {
                        TenantId = userToSync.ServiceTitanTenantId,
                        Roles = {
                            new UserOrgNodeRole() {
                                RoleId = new Guid("c8949dab-686e-4eb7-bd42-1602fd0254ca")  //CSR
                            },
                            new UserOrgNodeRole() {
                                RoleId = new Guid("0cb5bae7-f166-430a-b384-8df785c9243f")  //Supervisor
                            }
                        }
                    }
                }
                };
            }
            else
            {
                user.Name = userToSync.Name;

                //Update the OrgNode for the tenant specified in the message, adding new one if missing
                var orgNode = user.OrgNodes.SingleOrDefault(o => o.TenantId == userToSync.ServiceTitanTenantId);
                if (orgNode is null)
                {
                    _logger.LogDebug($"Org node from tenant {userToSync.ServiceTitanTenantId} not found. Creating it.");

                    orgNode = new UserOrgNode()
                    {
                        TenantId = userToSync.ServiceTitanTenantId,
                        Roles = {
                            new UserOrgNodeRole() {
                                RoleId = new Guid("c8949dab-686e-4eb7-bd42-1602fd0254ca")  //CSR
                            },
                            new UserOrgNodeRole() {
                                RoleId = new Guid("0cb5bae7-f166-430a-b384-8df785c9243f")  //Supervisor
                            }
                        }
                    };

                    user.OrgNodes.Add(orgNode);
                }
            }

            try
            {
                await _userManagementRepository.UpsertUserAsync(user);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException e) { }
        }

        // complete the message. message is deleted from the queue. 
        await args.CompleteMessageAsync(args.Message);
    }

    // handle any errors when receiving messages
    Task ErrorHandler(ProcessErrorEventArgs args)
    {
        _logger.LogError(args.Exception.ToString());
        return Task.CompletedTask;
    }
}