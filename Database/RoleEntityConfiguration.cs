using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database
{
    public class RoleEntityConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(e => e.RoleId);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(256);
            builder.Property(e => e.CreateDate).IsRequired();
        }
    }
}