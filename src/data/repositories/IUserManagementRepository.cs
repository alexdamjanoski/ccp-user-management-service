public interface IUserManagementRepository
{
    public Task<List<User>> GetUsers();

    public Task<User?> GetUser(Guid id);

    public Task<User?> GetUserByEmailAsync(string email);

    public Task<List<UserOrgNode>> GetUserOrgNodesAsync(Guid id, int? tenantId);

    public Task UpsertUserAsync(User user);

    public Task<List<UserOrgNodeRole>> GetUserOrgNodeRolesAsync(Guid userOrgNodeId);

    public Task<List<Tenant>> GetActiveTenantsAsync();

    public Task<List<Application>> GetApplicationsAsync();

    public Task<Application?> GetApplicationAsync(Guid id);

    public Task<List<Role>> GetRolesAsync();
}