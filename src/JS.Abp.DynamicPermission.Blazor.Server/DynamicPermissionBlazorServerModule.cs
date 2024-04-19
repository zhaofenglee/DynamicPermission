using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace JS.Abp.DynamicPermission.Blazor.Server;

[DependsOn(
    typeof(DynamicPermissionBlazorModule),
    typeof(AbpAspNetCoreComponentsServerThemingModule)
    )]
public class DynamicPermissionBlazorServerModule : AbpModule
{

}
