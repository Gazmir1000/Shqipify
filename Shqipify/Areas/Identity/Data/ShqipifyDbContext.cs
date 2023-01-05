using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shqipify.Areas.Identity.Data;

namespace Shqipify.Areas.Identity.Data;

public class ShqipifyDbContext : IdentityDbContext<AppUser>
{
    public ShqipifyDbContext(DbContextOptions<ShqipifyDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationEnityUserConfiguration());
    }
}

internal class ApplicationEnityUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(u => u.Firstname).HasMaxLength(30).IsRequired();
        builder.Property(u => u.Lastname).HasMaxLength(30).IsRequired();
    }
}