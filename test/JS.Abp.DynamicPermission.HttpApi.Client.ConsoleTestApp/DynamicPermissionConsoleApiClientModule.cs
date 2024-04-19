using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace JS.Abp.DynamicPermission;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(DynamicPermissionHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class DynamicPermissionConsoleApiClientModule : AbpModule
{

}
