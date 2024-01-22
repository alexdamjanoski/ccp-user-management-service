public class UserOrgNodeDto
{
    public UserOrgNodeDto()
    {
        this.Roles = new List<UserOrgNodeRoleDto>();
    }

    public Guid Id { get; set; }

    public int TenantId { get; set; }

    public Guid? OrgNodeId { get; set; }

    public List<UserOrgNodeRoleDto> Roles { get; set; }

    public DateTime Created_At { get; set; }

    public DateTime Updated_At { get; set; }
}