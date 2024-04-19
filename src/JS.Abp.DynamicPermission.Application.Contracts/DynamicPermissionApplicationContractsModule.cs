using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace JS.Abp.DynamicPermission;

[DependsOn(
    typeof(DynamicPermissionDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationAbstractionsModule)
    )]
public class DynamicPermissionApplicationContractsModule : AbpModule
{

}
