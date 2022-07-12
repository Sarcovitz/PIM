using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PimApi.Config;
using PimModels.Models;

namespace PimApi.Data;

public class PimDbContext: DbContext
{
    private readonly SqlConfig _sqlConfig;

    public DbSet<Catalog> Catalogs { get; set; }
    public DbSet<CatalogUser> CatalogUsers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductAttribute> ProductAttributes { get; set; }
    public DbSet<ProductAttributeProto> ProductAttributeProtos { get; set; }
    public DbSet<User> Users { get; set; }



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

        var catalog = modelBuilder.Entity<Catalog>();
        catalog.ToTable("Catalogs");

        var catalogUser = modelBuilder.Entity<CatalogUser>();
        catalogUser.ToTable("CatalogUsers");
        catalogUser.HasKey(x => new { x.CatalogId, x.UserId });
        catalogUser.HasOne(x => x.Catalog).WithMany(c => c.CatalogUsers).HasForeignKey(x=> x.CatalogId).OnDelete(DeleteBehavior.Cascade);
        catalogUser.HasOne(x => x.User).WithMany(u => u.CatalogUsers).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        catalogUser.Property(x => x.Role).HasConversion<int>();

        var category = modelBuilder.Entity<Category>();
        category.ToTable("Categories");
        category.HasOne(x => x.ParentCategory).WithMany(x => x.SubCategories).HasForeignKey(x => x.ParentCategoryId).OnDelete(DeleteBehavior.Cascade);
        category.HasOne(x => x.Catalog).WithMany(x => x.Categories).HasForeignKey(x => x.CatalogId).OnDelete(DeleteBehavior.Cascade);

        var categoryProductAttributeProto= modelBuilder.Entity<>

        var currency = modelBuilder.Entity<Currency>();
        currency.ToTable("Currencies");
        currency.HasIndex(x => x.Code).IsUnique();

        var product = modelBuilder.Entity<Product>();
        product.ToTable("Products");
        product.HasOne(x => x.Catalog).WithMany(x => x.Products).HasForeignKey(x => x.CatalogId).OnDelete(DeleteBehavior.Cascade);
        product.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId);

        var productAttribute = modelBuilder.Entity<ProductAttribute>();
        productAttribute.ToTable("ProductAttributes");
        productAttribute.HasOne(x => x.AttributeProto).WithMany(x => x.ProductAttributes).HasForeignKey(x => x.AttributeProtoId).OnDelete(DeleteBehavior.Cascade);
        productAttribute.HasOne(x => x.Product).WithMany(x => x.ProductAttributes).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);

        var productAttributeProto = modelBuilder.Entity<ProductAttributeProto>();
        productAttributeProto.ToTable("ProductAttributeProtos");
        productAttributeProto.HasOne(x => x.Catalog).WithMany(x => x.ProductAttributeProtos).HasForeignKey(x => x.CatalogId).OnDelete(DeleteBehavior.Cascade);

        var user = modelBuilder.Entity<User>();
        user.ToTable("Users");
        user.HasIndex(u => u.Username).IsUnique();
        user.HasIndex(u => u.Email).IsUnique();

        
    }
}
