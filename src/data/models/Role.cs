public class Role
{
    public Role()
    {
        this.Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }

    public required string Name { get; set; }

    public Guid ApplicationId { get; set; }

    public DateTime Created_At { get; set; }

    public DateTime Updated_At { get; set; }
}