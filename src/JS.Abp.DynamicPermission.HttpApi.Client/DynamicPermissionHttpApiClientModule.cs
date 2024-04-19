using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace JS.Abp.DynamicPermission;

[DependsOn(
    typeof(DynamicPermissionApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class DynamicPermissionHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(DynamicPermissionApplicationContractsModule).Assembly,
            DynamicPermissionRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DynamicPermissionHttpApiClientModule>();
        });
    }
}
