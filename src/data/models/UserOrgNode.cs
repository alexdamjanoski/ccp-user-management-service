public class UserOrgNode
{
    public UserOrgNode()
    {
        this.Id = Guid.NewGuid();
        this.Roles = new List<UserOrgNodeRole>();
    }

    public Guid Id { get; set; }

    public int TenantId { get; set; }

    public Guid? OrgNodeId { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; }

    public ICollection<UserOrgNodeRole> Roles { get; set; }

    public DateTime Created_At { get; set; }

    public DateTime Updated_At { get; set; }
}