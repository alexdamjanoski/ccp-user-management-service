public interface IUserManagementRepository
{
    public Task<List<User>> GetUsers();

    public Task<User?> GetUser(Guid userId);

    public Task<User?> GetUserByEmailAsync(string email);

    public Task<List<UserOrgNode>> GetUserOrgNodesAsync(Guid userId, int? tenantId);

    public Task<UserOrgNode?> GetUserOrgNodeAsync(Guid userId, Guid userOrgNodeId);

    public Task<UserOrgNodeRole> AddUserOrgNodeRoleAsync(Guid userId, Guid userOrgNodeId, Guid roleId);

    public Task DeleteUserOrgNodeRoleAsync(Guid userOrgNodeRoleId);

    public Task UpsertUserAsync(User user);

    public Task<List<UserOrgNodeRole>> GetUserOrgNodeRolesAsync(Guid userOrgNodeId);

    public Task<List<Tenant>> GetActiveTenantsAsync();

    public Task<List<Application>> GetApplicationsAsync();

    public Task<Application?> GetApplicationAsync(Guid applicationId);

    public Task<List<Role>> GetRolesAsync();

    public Task<Role?> GetRoleAsync(Guid roleId);
}