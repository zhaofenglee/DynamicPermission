using JS.Abp.DynamicPermission.PermissionDefinitions;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace JS.Abp.DynamicPermission.MongoDB;

[ConnectionStringName(DynamicPermissionDbProperties.ConnectionStringName)]
public interface IDynamicPermissionMongoDbContext : IAbpMongoDbContext
{
    IMongoCollection<PermissionDefinition> PermissionDefinitions { get; }
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}