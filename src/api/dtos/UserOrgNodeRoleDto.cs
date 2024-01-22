public class UserOrgNodeRoleDto
{
    public Guid Id { get; set; }

    public Guid RoleId { get; set; }

    public string RoleName { get; set; }

    public Guid ApplicationId { get; set; }

    public DateTime Created_At { get; set; }

    public DateTime Updated_At { get; set; }
}