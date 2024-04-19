using Volo.Abp.Modularity;

namespace JS.Abp.DynamicPermission;

[DependsOn(
    typeof(DynamicPermissionDomainModule),
    typeof(DynamicPermissionTestBaseModule)
)]
public class DynamicPermissionDomainTestModule : AbpModule
{

}
