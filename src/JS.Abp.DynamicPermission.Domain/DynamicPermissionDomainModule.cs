using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Caching;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace JS.Abp.DynamicPermission;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AbpPermissionManagementDomainModule),
    typeof(AbpCachingModule),
    typeof(DynamicPermissionDomainSharedModule)
)]
public class DynamicPermissionDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.Configure<PermissionManagementOptions>(options =>
        {
            options.IsDynamicPermissionStoreEnabled = true;
        });
    }
}
