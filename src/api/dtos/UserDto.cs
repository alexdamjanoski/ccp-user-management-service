public class UserDto
{
    public UserDto()
    {
        this.OrgNodes = new List<UserOrgNodeDto>();
    }

    public Guid Id { get; set; }

    public required string ServiceTitanId { get; set; }

    public required string Name { get; set; }

    public required string Email { get; set; }

    public required string LoginName { get; set; }

    public List<UserOrgNodeDto> OrgNodes { get; set; }

    public DateTime Created_At { get; set; }

    public DateTime Updated_At { get; set; }
}