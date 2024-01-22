using Microsoft.EntityFrameworkCore;

public class UserManagementContext : DbContext
{
    public UserManagementContext() : base() { }

    public DbSet<User> Users { get; set; }

    public DbSet<UserOrgNode> UserOrgNodes { get; set; }

    public DbSet<UserOrgNodeRole> UserOrgNodeRoles { get; set; }

    public DbSet<Application> Applications { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<Tenant> Tenants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>().HasData(
            new Application
            {
                Id = new Guid("8447730d-9029-4f94-acac-61cf22088f4a"),
                Name = "Contact Center Pro",
                Created_At = DateTime.Now,
                Updated_At = DateTime.Now,
            },
            new Application
            {
                Id = new Guid("66b9117d-d182-4b9e-bc52-7f9d731c223a"),
                Name = "Marketing Pro",
                Created_At = DateTime.Now,
                Updated_At = DateTime.Now,
            },
            new Application
            {
                Id = new Guid("ec7646f2-efbc-4ccb-a151-16886e72f774"),
                Name = "Dispatch Pro",
                Created_At = DateTime.Now,
                Updated_At = DateTime.Now,
            }
        );

        modelBuilder.Entity<Role>().HasData(
            new Role
            {
                Id = new Guid("c8949dab-686e-4eb7-bd42-1602fd0254ca"),
                ApplicationId = new Guid("8447730d-9029-4f94-acac-61cf22088f4a"),
                Name = "CSR",
                Created_At = DateTime.Now,
                Updated_At = DateTime.Now
            },
            new Role
            {
                Id = new Guid("22bd9279-1059-45e6-9bcc-62f1fcf68669"),
                ApplicationId = new Guid("8447730d-9029-4f94-acac-61cf22088f4a"),
                Name = "Administrator",
                Created_At = DateTime.Now,
                Updated_At = DateTime.Now
            },
            new Role
            {
                Id = new Guid("0cb5bae7-f166-430a-b384-8df785c9243f"),
                ApplicationId = new Guid("8447730d-9029-4f94-acac-61cf22088f4a"),
                Name = "Supervisor",
                Created_At = DateTime.Now,
                Updated_At = DateTime.Now
            },
            new Role
            {
                Id = new Guid("9f6f1978-d149-433b-80c0-0643274b37ec"),
                ApplicationId = new Guid("66b9117d-d182-4b9e-bc52-7f9d731c223a"),
                Name = "Administrator",
                Created_At = DateTime.Now,
                Updated_At = DateTime.Now
            },
            new Role
            {
                Id = new Guid("4e5ae2d0-107a-473e-8b2b-0d6b53b33dff"),
                ApplicationId = new Guid("66b9117d-d182-4b9e-bc52-7f9d731c223a"),
                Name = "Marketer",
                Created_At = DateTime.Now,
                Updated_At = DateTime.Now
            }
        );

        modelBuilder.Entity<Tenant>().HasData(
            new Tenant
            {
                ClientId = "cl_cljszsiyx00xy079l4i2un2sc",
                TenantId = 2047494293,
                isActive = true
            },
            new Tenant
            {
                ClientId = "cl_cljszsinj00xw079l7phzk0ru",
                TenantId = 2047356732,
                isActive = true
            },
            new Tenant
            {
                ClientId = "cl_cljszsia503fr085iiyer9yvw",
                TenantId = 2047356693,
                isActive = true
            },
            new Tenant
            {
                ClientId = "cl_cljszshtr03fp085ian9kt9i8",
                TenantId = 2047344921,
                isActive = true
            },
            new Tenant
            {
                ClientId = "cl_cljszshg903fn085icsx1xk32",
                TenantId = 2047327254,
                isActive = true
            },
            new Tenant
            {
                ClientId = "cl_cljszsh0100tc079eh5b60o75",
                TenantId = 2047143958,
                isActive = true
            },
            new Tenant
            {
                ClientId = "cl_cljszsgic00xu079l5np9lf0y",
                TenantId = 2047079831,
                isActive = true
            },
            new Tenant
            {
                ClientId = "cl_cljszsg3p03fl085ijh0fipzj",
                TenantId = 2047062813,
                isActive = true
            },
            new Tenant
            {
                ClientId = "cl_cljszsfpe03fj085ikr3zjv0v",
                TenantId = 2047050520,
                isActive = true
            },
            new Tenant
            {
                ClientId = "cl_cljszsfba03fh085ihw96bet8",
                TenantId = 2044903189,
                isActive = true
            },
            new Tenant
            {
                ClientId = "cl_cljszsez803ff085immj6wxi9",
                TenantId = 2044901397,
                isActive = true
            }
        );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlServer("Server=localhost;Database=ContactCenterProUserManagement;User Id=sa;Password=S3rv!c3T!t@n;Encrypt=True;TrustServerCertificate=True");
        optionsBuilder.UseSqlServer("Server=tcp:contactcenterpro.database.windows.net,1433;Initial Catalog=contactcenterpro;Persist Security Info=False;User ID=contactcenterpro;Password=S3rv!c3T!t@n;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;");
    }


}