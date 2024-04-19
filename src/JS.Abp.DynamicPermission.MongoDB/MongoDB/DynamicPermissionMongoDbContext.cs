using JS.Abp.DynamicPermission.PermissionDefinitions;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace JS.Abp.DynamicPermission.MongoDB;

[ConnectionStringName(DynamicPermissionDbProperties.ConnectionStringName)]
public class DynamicPermissionMongoDbContext : AbpMongoDbContext, IDynamicPermissionMongoDbContext
{
    public IMongoCollection<PermissionDefinition> PermissionDefinitions => Collection<PermissionDefinition>();
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureDynamicPermission();

        modelBuilder.Entity<PermissionDefinition>(b => { b.CollectionName = DynamicPermissionDbProperties.DbTablePrefix + "PermissionDefinitions"; });
    }
}