using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Thinktecture;

namespace Database
{
    public class SampleDbContext : DbContext
    {
        public DbSet<RoleEntity> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Server=(LocalDB)\v11.0;Database=Thinktecture;", x =>
                {
                    x.MigrationsAssembly(typeof(SampleDbContext).GetTypeInfo().Assembly.GetName().Name);
                });
            optionsBuilder.AddRowNumberSupport();
        }
    }
}