public class Tenant
{
    public Tenant()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }

    public required string ClientId { get; set; }

    public required int TenantId { get; set; }

    public bool isActive { get; set; }
}