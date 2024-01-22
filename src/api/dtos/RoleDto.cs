public class RoleDto
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public DateTime Created_At { get; set; }

    public DateTime Updated_At { get; set; }
}