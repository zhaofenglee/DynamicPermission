using JS.Abp.DynamicPermission.PermissionDefinitions;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace JS.Abp.DynamicPermission.EntityFrameworkCore;

[ConnectionStringName(DynamicPermissionDbProperties.ConnectionStringName)]
public class DynamicPermissionDbContext : AbpDbContext<DynamicPermissionDbContext>, IDynamicPermissionDbContext
{
    public DbSet<PermissionDefinition> PermissionDefinitions { get; set; } = null!;
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public DynamicPermissionDbContext(DbContextOptions<DynamicPermissionDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureDynamicPermission();
    }
}