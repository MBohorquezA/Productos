using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Productos.Models.Entity;
using Microsoft.Extensions.Hosting;


namespace Productos.DAL.DataContext;

public partial class DbproductosContext : DbContext
{
    public DbproductosContext()
    {
    }

    public DbproductosContext(DbContextOptions<DbproductosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var hostEnvironment = new HostBuilder().Build().Services.GetService(typeof(IHostEnvironment)) as IHostEnvironment;
            string basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Productos.DAL");
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("cadenaSQL");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC07CEE256F9");

            entity.ToTable("Product");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
