using Microsoft.EntityFrameworkCore;

public class UserManagementRepository : IUserManagementRepository
{
    private readonly ILogger<UserManagementRepository> _logger;

    public UserManagementRepository(ILogger<UserManagementRepository> logger)
    {
        _logger = logger;

        using var context = new UserManagementContext();
        context.Database.EnsureCreated();
    }

    public async Task<List<User>> GetUsers()
    {
        using (var context = new UserManagementContext())
        {
            var users = await context.Users.Include(o => o.OrgNodes).ToListAsync();

            return users;
        }
    }

    public async Task<User?> GetUser(Guid id)
    {
        using (var context = new UserManagementContext())
        {
            var user = await context.Users.Include(o => o.OrgNodes).SingleOrDefaultAsync(o => o.Id.Equals(id));

            return user;
        }
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        using (var context = new UserManagementContext())
        {
            var user = await context.Users.Include(o => o.OrgNodes).SingleOrDefaultAsync(o => o.Email.ToLower().Equals(email.ToLower().Trim()));

            return user;
        }
    }

    public async Task<List<UserOrgNode>> GetUserOrgNodesAsync(Guid id, int? tenantId)
    {
        using (var context = new UserManagementContext())
        {
            var query = context.UserOrgNodes.Include(o => o.Roles).Where(o => o.UserId.Equals(id));

            _logger.LogDebug($"Tenant id is {tenantId}");
            if (tenantId is not null)
                query = query.Where(o => o.TenantId.Equals(tenantId));

            var userOrgNodes = await query.ToListAsync();

            return userOrgNodes;
        }
    }

    public async Task UpsertUserAsync(User user)
    {
        using (var context = new UserManagementContext())
        {
            var existingUser = await context.Users.Include(o => o.OrgNodes).SingleOrDefaultAsync(o => o.Email.ToLower().Equals(user.Email.ToLower().Trim()));

            if (existingUser is null)
            {
                user.Created_At = DateTime.Now;
                user.Updated_At = DateTime.Now;

                foreach (UserOrgNode userOrgNode in user.OrgNodes)
                {
                    userOrgNode.Created_At = DateTime.Now;
                    userOrgNode.Updated_At = DateTime.Now;

                    foreach (UserOrgNodeRole userOrgNodeRole in userOrgNode.Roles)
                    {
                        userOrgNodeRole.Created_At = DateTime.Now;
                        userOrgNodeRole.Updated_At = DateTime.Now;
                    }
                }

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                return;
            }

            if (!user.Email.Trim().ToLower().Equals(existingUser.Email.Trim().ToLower()))
                throw new Exception("User email doesn't match and cannot be changed");

            if (!user.LoginName.Trim().ToLower().Equals(existingUser.LoginName.Trim().ToLower()))
                throw new Exception("User login name doesn't match and cannot be changed");

            context.Users.Update(existingUser);
            existingUser.Updated_At = DateTime.Now;

            foreach (UserOrgNode orgNode in user.OrgNodes)
            {
                if (existingUser.OrgNodes.Where(o => o.TenantId.Equals(orgNode.TenantId)).Count() == 0)
                {
                    _logger.LogDebug($"Adding org node for tenant {orgNode.TenantId} to user with email {existingUser.Email} which previously had {existingUser.OrgNodes.Count()} org nodes");
                    var newOrgNode = new UserOrgNode()
                    {
                        UserId = existingUser.Id,
                        TenantId = orgNode.TenantId,
                        Roles = {
                            new UserOrgNodeRole() {
                                RoleId = new Guid("c8949dab-686e-4eb7-bd42-1602fd0254ca")  //CSR
                            },
                            new UserOrgNodeRole() {
                                RoleId = new Guid("c8949dab-686e-4eb7-bd42-1602fd0254ca")  //Supervisor
                            }
                        }
                    };

                    await context.UserOrgNodes.AddAsync(newOrgNode);
                }
            }

            await context.SaveChangesAsync();

            return;
        }
    }

    public async Task<List<UserOrgNodeRole>> GetUserOrgNodeRolesAsync(Guid userOrgNodeId)
    {
        using (var context = new UserManagementContext())
        {
            var roles = await context.UserOrgNodeRoles.Include(o => o.Role).Where(o => o.UserOrgNodeId.Equals(userOrgNodeId)).ToListAsync();

            return roles;
        }
    }

    public async Task<List<Tenant>> GetActiveTenantsAsync()
    {
        using (var context = new UserManagementContext())
        {
            return await context.Tenants.Where(o => o.isActive == true).ToListAsync();
        }
    }

    public async Task<List<Application>> GetApplicationsAsync()
    {
        using (var context = new UserManagementContext())
        {
            return await context.Applications.Include(o => o.Roles).ToListAsync();
        }
    }

    public async Task<Application?> GetApplicationAsync(Guid id)
    {
        using (var context = new UserManagementContext())
        {
            return await context.Applications.Include(o => o.Roles).SingleOrDefaultAsync(o => o.Id.Equals(id));
        }
    }

    public async Task<List<Role>> GetRolesAsync()
    {
        using (var context = new UserManagementContext())
        {
            return await context.Roles.ToListAsync();
        }
    }
}