public class UserOrgNodeRole
{
    public UserOrgNodeRole()
    {
        this.Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }

    public required Guid RoleId { get; set; }

    public Role? Role { get; set; }

    public Guid UserOrgNodeId { get; set; }

    public UserOrgNode UserOrgNode { get; set; }

    public DateTime Created_At { get; set; }

    public DateTime Updated_At { get; set; }
}