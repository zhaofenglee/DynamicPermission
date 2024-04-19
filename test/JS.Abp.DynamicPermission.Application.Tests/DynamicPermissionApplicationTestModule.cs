using Volo.Abp.Modularity;

namespace JS.Abp.DynamicPermission;

[DependsOn(
    typeof(DynamicPermissionApplicationModule),
    typeof(DynamicPermissionDomainTestModule)
    )]
public class DynamicPermissionApplicationTestModule : AbpModule
{

}
