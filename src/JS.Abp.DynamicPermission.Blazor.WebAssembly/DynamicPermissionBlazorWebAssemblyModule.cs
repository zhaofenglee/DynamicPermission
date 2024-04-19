using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace JS.Abp.DynamicPermission.Blazor.WebAssembly;

[DependsOn(
    typeof(DynamicPermissionBlazorModule),
    typeof(DynamicPermissionHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
)]
public class DynamicPermissionBlazorWebAssemblyModule : AbpModule
{

}
