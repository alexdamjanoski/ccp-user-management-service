using Microsoft.AspNetCore.Mvc;

[Route("api/v1/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserManagementRepository _userManagementRepository;

    private readonly ILogger<UsersController> _logger;

    public UsersController(IUserManagementRepository userManagementRepository,
                            ILogger<UsersController> logger)
    {
        _userManagementRepository = userManagementRepository;
        _logger = logger;
    }

    [HttpGet]
    [Route("users")]
    public async Task<IActionResult> GetUsersAsync()
    {
        var data = await _userManagementRepository.GetUsers();
        var response = new List<UserDto>();

        foreach (User user in data)
        {
            var userDto = new UserDto()
            {
                Id = user.Id,
                ServiceTitanId = user.ServiceTitanId,
                Name = user.Name,
                Email = user.Email,
                LoginName = user.LoginName,
                Created_At = user.Created_At,
                Updated_At = user.Updated_At
            };

            foreach (UserOrgNode userOrgNode in user.OrgNodes)
            {
                var userOrgNodeDto = new UserOrgNodeDto()
                {
                    Id = userOrgNode.Id,
                    TenantId = userOrgNode.TenantId,
                    OrgNodeId = userOrgNode.OrgNodeId,
                    Created_At = userOrgNode.Created_At,
                    Updated_At = userOrgNode.Updated_At

                };

                var orgNodeRoles = await _userManagementRepository.GetUserOrgNodeRolesAsync(userOrgNode.Id);

                foreach (UserOrgNodeRole userOrgNodeRole in orgNodeRoles)
                {
                    userOrgNodeDto.Roles.Add(new UserOrgNodeRoleDto()
                    {
                        Id = userOrgNodeRole.Id,
                        RoleId = userOrgNodeRole.RoleId,
                        RoleName = userOrgNodeRole.Role.Name,
                        ApplicationId = userOrgNodeRole.Role.ApplicationId,
                        Created_At = userOrgNodeRole.Created_At,
                        Updated_At = userOrgNodeRole.Updated_At
                    });
                }

                userDto.OrgNodes.Add(userOrgNodeDto);
            }

            response.Add(userDto);
        }

        return Ok(response);
    }

    [HttpGet]
    [Route("users/{userId}")]
    public async Task<IActionResult> GetUserAsync(Guid userId)
    {
        var data = await _userManagementRepository.GetUser(userId);

        if (data is null)
            return NotFound();

        var response = new UserDto()
        {
            Id = data.Id,
            ServiceTitanId = data.ServiceTitanId,
            Name = data.Name,
            Email = data.Email,
            LoginName = data.LoginName,
            Created_At = data.Created_At,
            Updated_At = data.Updated_At
        };

        foreach (UserOrgNode userOrgNode in data.OrgNodes)
        {
            var userOrgNodeDto = new UserOrgNodeDto()
            {
                Id = userOrgNode.Id,
                TenantId = userOrgNode.TenantId,
                OrgNodeId = userOrgNode.OrgNodeId,
                Created_At = userOrgNode.Created_At,
                Updated_At = userOrgNode.Updated_At

            };

            var orgNodeRoles = await _userManagementRepository.GetUserOrgNodeRolesAsync(userOrgNode.Id);

            foreach (UserOrgNodeRole userOrgNodeRole in orgNodeRoles)
            {
                userOrgNodeDto.Roles.Add(new UserOrgNodeRoleDto()
                {
                    Id = userOrgNodeRole.Id,
                    RoleId = userOrgNodeRole.RoleId,
                    RoleName = userOrgNodeRole.Role.Name,
                    ApplicationId = userOrgNodeRole.Role.ApplicationId,
                    Created_At = userOrgNodeRole.Created_At,
                    Updated_At = userOrgNodeRole.Updated_At
                });
            }

            response.OrgNodes.Add(userOrgNodeDto);
        }

        return Ok(response);
    }

    [HttpGet]
    [Route("users/{userId}/orgnodes")]
    public async Task<IActionResult> GetUserOrgNodesAsync(Guid userId,
                                        [FromQuery(Name = "tenantId")] int? tenantId)
    {
        var data = await _userManagementRepository.GetUserOrgNodesAsync(userId, tenantId);

        if (data is null)
            return NotFound();

        var response = new List<UserOrgNodeDto>();

        foreach (UserOrgNode userOrgNode in data)
        {
            var userOrgNodeDto = new UserOrgNodeDto()
            {
                Id = userOrgNode.Id,
                TenantId = userOrgNode.TenantId,
                OrgNodeId = userOrgNode.OrgNodeId,
                Created_At = userOrgNode.Created_At,
                Updated_At = userOrgNode.Updated_At

            };

            var orgNodeRoles = await _userManagementRepository.GetUserOrgNodeRolesAsync(userOrgNode.Id);

            foreach (UserOrgNodeRole userOrgNodeRole in orgNodeRoles)
            {
                userOrgNodeDto.Roles.Add(new UserOrgNodeRoleDto()
                {
                    Id = userOrgNodeRole.Id,
                    RoleId = userOrgNodeRole.RoleId,
                    RoleName = userOrgNodeRole.Role.Name,
                    ApplicationId = userOrgNodeRole.Role.ApplicationId,
                    Created_At = userOrgNodeRole.Created_At,
                    Updated_At = userOrgNodeRole.Updated_At
                });
            }

            response.Add(userOrgNodeDto);
        }

        return Ok(response);
    }

    [HttpGet]
    [Route("users/{userId}/orgnodes/{userOrgNodeId}")]
    public async Task<IActionResult> GetUserOrgNodeAsync(Guid userId, Guid userOrgNodeId)
    {
        var data = await _userManagementRepository.GetUserOrgNodeAsync(userId, userOrgNodeId);

        if (data is null)
            return NotFound();

        var response = new UserOrgNodeDto()
        {
            Id = data.Id,
            TenantId = data.TenantId,
            OrgNodeId = data.OrgNodeId,
            Created_At = data.Created_At,
            Updated_At = data.Updated_At

        };

        var orgNodeRoles = await _userManagementRepository.GetUserOrgNodeRolesAsync(userOrgNodeId);

        foreach (UserOrgNodeRole userOrgNodeRole in orgNodeRoles)
        {
            response.Roles.Add(new UserOrgNodeRoleDto()
            {
                Id = userOrgNodeRole.Id,
                RoleId = userOrgNodeRole.RoleId,
                RoleName = userOrgNodeRole.Role.Name,
                ApplicationId = userOrgNodeRole.Role.ApplicationId,
                Created_At = userOrgNodeRole.Created_At,
                Updated_At = userOrgNodeRole.Updated_At
            });
        }

        return Ok(response);
    }

    [HttpGet]
    [Route("users/{userId}/orgnodes/{userOrgNodeId}/roles")]
    public async Task<IActionResult> GetUserOrgNodeRolesAsync(Guid userId, Guid userOrgNodeId)
    {
        var data = await _userManagementRepository.GetUserOrgNodeRolesAsync(userOrgNodeId);

        var response = new List<UserOrgNodeRoleDto>();

        foreach (UserOrgNodeRole userOrgNodeRole in data)
        {
            response.Add(new UserOrgNodeRoleDto()
            {
                Id = userOrgNodeRole.Id,
                RoleId = userOrgNodeRole.RoleId,
                RoleName = userOrgNodeRole.Role.Name,
                ApplicationId = userOrgNodeRole.Role.ApplicationId,
                Created_At = userOrgNodeRole.Created_At,
                Updated_At = userOrgNodeRole.Updated_At
            });
        }

        return Ok(response);
    }

    [HttpPost]
    [Route("users/{userId}/orgnodes/{userOrgNodeId}/roles")]
    public async Task<IActionResult> AddUserOrgNodeRoleAsync(Guid userId, Guid userOrgNodeId, [FromBody] Guid roleId)
    {
        var data = await _userManagementRepository.AddUserOrgNodeRoleAsync(userId, userOrgNodeId, roleId);

        var response = new UserOrgNodeRoleDto()
        {
            Id = data.Id,
            RoleId = data.RoleId,
            RoleName = data.Role.Name,
            ApplicationId = data.Role.ApplicationId,
            Created_At = data.Created_At,
            Updated_At = data.Updated_At
        };

        return Ok(response);
    }
}