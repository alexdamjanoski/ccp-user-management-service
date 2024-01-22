public class User
{
    public User()
    {
        this.Id = Guid.NewGuid();
        this.OrgNodes = new List<UserOrgNode>();
    }

    public Guid Id { get; set; }

    public required string ServiceTitanId { get; set; }

    public required string Name { get; set; }

    public required string Email { get; set; }

    public required string LoginName { get; set; }

    public ICollection<UserOrgNode> OrgNodes { get; set; }

    public DateTime Created_At { get; set; }

    public DateTime Updated_At { get; set; }
}