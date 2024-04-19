using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using JS.Abp.DynamicPermission.Localization;
using JS.Abp.DynamicPermission.Web.Menus;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using JS.Abp.DynamicPermission.Permissions;

namespace JS.Abp.DynamicPermission.Web;

[DependsOn(
    typeof(DynamicPermissionApplicationContractsModule),
    typeof(AbpAspNetCoreMvcUiThemeSharedModule),
    typeof(AbpAutoMapperModule)
    )]
public class DynamicPermissionWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(DynamicPermissionResource), typeof(DynamicPermissionWebModule).Assembly);
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(DynamicPermissionWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new DynamicPermissionMenuContributor());
        });

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DynamicPermissionWebModule>();
        });

        context.Services.AddAutoMapperObjectMapper<DynamicPermissionWebModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<DynamicPermissionWebModule>(validate: true);
        });

        Configure<RazorPagesOptions>(options =>
        {
            //Configure authorization.
            options.Conventions.AuthorizePage("/PermissionDefinitions/Index", DynamicPermissionPermissions.PermissionDefinitions.Default);
        });
    }
}