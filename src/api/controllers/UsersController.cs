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
    [Route("users/{id}")]
    public async Task<IActionResult> GetUserAsync(Guid id)
    {
        var data = await _userManagementRepository.GetUser(id);

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
    [Route("users/{id}/orgnodes")]
    public async Task<IActionResult> GetUserOrgNodesAsync(Guid id,
                                        [FromQuery(Name = "tenantId")] int? tenantId)
    {
        var data = await _userManagementRepository.GetUserOrgNodesAsync(id, tenantId);

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
}