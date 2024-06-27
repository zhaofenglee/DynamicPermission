using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace JS.Abp.DynamicPermission;

[DependsOn(
    typeof(DynamicPermissionDomainModule),
    typeof(DynamicPermissionApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpIdentityApplicationModule)
    )]
public class DynamicPermissionApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<DynamicPermissionApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<DynamicPermissionApplicationModule>(validate: true);
        });
    }
}
