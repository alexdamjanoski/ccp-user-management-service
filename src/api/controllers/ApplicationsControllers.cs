using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;

[Route("api/v1/[controller]")]
public class ApplicationsController : ControllerBase
{
    private readonly IUserManagementRepository _userManagementRepository;

    private readonly ILogger<UsersController> _logger;

    public ApplicationsController(IUserManagementRepository userManagementRepository,
                            ILogger<UsersController> logger)
    {
        _userManagementRepository = userManagementRepository;
        _logger = logger;
    }

    [HttpGet]
    [Route("applications")]
    public async Task<IActionResult> GetApplicationsAsync()
    {
        var data = await _userManagementRepository.GetApplicationsAsync();
        var response = new List<ApplicationDto>();

        foreach (Application application in data)
        {
            var applicationDto = new ApplicationDto()
            {
                Id = application.Id,
                Name = application.Name,
                Created_At = application.Created_At,
                Updated_At = application.Updated_At
            };

            foreach (Role role in application.Roles)
            {
                var roleDto = new RoleDto()
                {
                    Id = role.Id,
                    Name = role.Name,
                    Created_At = role.Created_At,
                    Updated_At = role.Updated_At
                };

                applicationDto.Roles.Add(roleDto);
            }

            response.Add(applicationDto);
        }

        return Ok(response);
    }

    [HttpGet]
    [Route("applications/{applicationId}")]
    public async Task<IActionResult> GetApplicationAsync(Guid applicationId)
    {
        var data = await _userManagementRepository.GetApplicationAsync(applicationId);

        if (data is null)
            return NotFound();

        var response = new ApplicationDto()
        {
            Id = data.Id,
            Name = data.Name,
            Created_At = data.Created_At,
            Updated_At = data.Updated_At
        };

        foreach (Role role in data.Roles)
        {
            var roleDto = new RoleDto()
            {
                Id = role.Id,
                Name = role.Name,
                Created_At = role.Created_At,
                Updated_At = role.Updated_At
            };

            response.Roles.Add(roleDto);
        }

        return Ok(response);
    }
}