using Localization.Resources.AbpUi;
using JS.Abp.DynamicPermission.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace JS.Abp.DynamicPermission;

[DependsOn(
    typeof(DynamicPermissionApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class DynamicPermissionHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(DynamicPermissionHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<DynamicPermissionResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
