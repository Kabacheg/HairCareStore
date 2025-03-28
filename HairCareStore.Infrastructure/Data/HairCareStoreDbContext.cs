using Hair_Care_Store.Core.Models;
using HairCareStore.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace HairCareStore.Infrastructure.Data;
public class HairCareStoreDbContext : DbContext
{
    public DbSet<Product> Products { get ; set; }
    public DbSet<Tutorial> Tutorials { get ; set; }

    public DbSet<User> Users { get ; set; }
    public HairCareStoreDbContext(DbContextOptions<HairCareStoreDbContext> options) : base(options) {}
}
