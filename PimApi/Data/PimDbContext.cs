using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PimApi.Config;
using PimModels.Models;

namespace PimApi.Data;

public class PimDbContext: DbContext
{
    private readonly SqlConfig _sqlConfig;

    public DbSet<User> Users { get; set; }
    public DbSet<Catalog> Catalogs { get; set; }
    public DbSet<CatalogUser> CatalogUsers { get; set; }
    public DbSet<Currency> Currencies { get; set; }

    public PimDbContext(DbContextOptions<PimDbContext> options, IOptions<SqlConfig> sqlConfig) : base(options)
    {
        _sqlConfig = sqlConfig.Value;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_sqlConfig.ConnectionString);//.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("PIM");

        var user = modelBuilder.Entity<User>();
        user.ToTable("Users");
        user.HasIndex(u => u.Username).IsUnique();
        user.HasIndex(u => u.Email).IsUnique();

        var catalog = modelBuilder.Entity<Catalog>();
        catalog.ToTable("Catalogs");

        var catalogUser = modelBuilder.Entity<CatalogUser>();
        catalogUser.ToTable("CatalogUsers");
        catalogUser.HasKey(x => new { x.CatalogId, x.UserId });
        catalogUser.HasOne(x => x.Catalog).WithMany(c => c.CatalogUsers).HasForeignKey(x=> x.CatalogId).OnDelete(DeleteBehavior.Cascade);
        catalogUser.HasOne(x => x.User).WithMany(u => u.CatalogUsers).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        catalogUser.Property(x => x.Role).HasConversion<int>();

        var currency = modelBuilder.Entity<Currency>();
        currency.ToTable("Currencies");
        currency.HasIndex(x => x.Code).IsUnique();
    }
}
