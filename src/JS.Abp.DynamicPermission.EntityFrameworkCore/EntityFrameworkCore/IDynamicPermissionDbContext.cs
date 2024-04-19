using JS.Abp.DynamicPermission.PermissionDefinitions;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace JS.Abp.DynamicPermission.EntityFrameworkCore;

[ConnectionStringName(DynamicPermissionDbProperties.ConnectionStringName)]
public interface IDynamicPermissionDbContext : IEfCoreDbContext
{
    DbSet<PermissionDefinition> PermissionDefinitions { get; set; }
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}