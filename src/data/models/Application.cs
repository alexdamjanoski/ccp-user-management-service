public class Application
{
    public Application()
    {
        this.Id = Guid.NewGuid();
        this.Roles = new List<Role>();
    }

    public Guid Id { get; set; }

    public required string Name { get; set; }

    public ICollection<Role> Roles { get; set; }

    public DateTime Created_At { get; set; }

    public DateTime Updated_At { get; set; }
}