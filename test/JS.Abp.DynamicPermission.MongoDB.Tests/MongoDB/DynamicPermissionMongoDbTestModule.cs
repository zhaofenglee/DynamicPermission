using System;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace JS.Abp.DynamicPermission.MongoDB;

[DependsOn(
    typeof(DynamicPermissionApplicationTestModule),
    typeof(DynamicPermissionMongoDbModule)
)]
public class DynamicPermissionMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });
    }
}
