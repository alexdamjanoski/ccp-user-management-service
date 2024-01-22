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
                ClientId = "cl_cjtyn9lnz0000inytba9r6qau",
                TenantId = 469649111,
                isActive = true
            },
            new Tenant
            {
                ClientId = "cl_cjvf8hfwv0000ppytqcfs9p1f",
                TenantId = 357085960,
                isActive = true
            },
            new Tenant
            {
                ClientId = "cl_jr7sr4fwv0000ppytqca7rge4",
                TenantId = 777777,
                isActive = true
            },
            new Tenant
            {
                ClientId = "cl_ck1tf5sjk03fl06rzvzhjmg5y",
                TenantId = 443410308,
                isActive = true
            },
            new Tenant
            {
                ClientId = "cl_ck36316yx009d09mf6rp1y5wc",
                TenantId = 461793632,
                isActive = true
            },
            new Tenant
            {
                ClientId = "cl_ck6yh4dev23uv0cnvvrq24ig1",
                TenantId = 349984906,
                isActive = true
            },
            new Tenant
            {
                ClientId = "cl_ck7c9zdwbcyr206l95bhpq8a9",
                TenantId = 490542469,
                isActive = true
            },
            new Tenant
            {
                ClientId = "cl_ck7lwausyk6qg0cnvdveeyjil",
                TenantId = 365083280,
                isActive = true
            },
            new Tenant
            {
                ClientId = "cl_ck7ujjzgkqcef0cnvu8m3csw6",
                TenantId = 601306030,
                isActive = true
            },
            new Tenant
            {
                ClientId = "cl_ck8eng3hd3oul06o70sa7gnba",
                TenantId = 1212121111,
                isActive = true
            },
            new Tenant
            {
                ClientId = "cl_ck9ctzvrr2o1i08ruru72i1zv",
                TenantId = 666666,
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