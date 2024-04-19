using JS.Abp.DynamicPermission.Permissions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using JS.Abp.DynamicPermission.Localization;
using Volo.Abp.UI.Navigation;

namespace JS.Abp.DynamicPermission.Blazor.Menus;

public class DynamicPermissionMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
            var moduleMenu = AddModuleMenuItem(context);
            AddMenuItemPermissionDefinitions(context, moduleMenu);
            AddMenuItemUserPermissions(context, moduleMenu);
        }

       
    }

    private static async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        //Add main menu items.
        var l = context.GetLocalizer<DynamicPermissionResource>();

        /*context.Menu.AddItem(new ApplicationMenuItem(
            DynamicPermissionMenus.Prefix,
            displayName: l["Menu:DynamicPermission"],
            "/DynamicPermission",
            icon: "fa fa-globe"));*/

        await Task.CompletedTask;
    }
    private static ApplicationMenuItem AddModuleMenuItem(MenuConfigurationContext context)
    {
        var moduleMenu = new ApplicationMenuItem(
            DynamicPermissionMenus.Prefix,
            context.GetLocalizer<DynamicPermissionResource>()["Menu:DynamicPermission"],
            icon: "fa fa-folder"
        );

        context.Menu.Items.AddIfNotContains(moduleMenu);
        return moduleMenu;
    }
    private static void AddMenuItemPermissionDefinitions(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        parentMenu.AddItem(
            new ApplicationMenuItem(
                Menus.DynamicPermissionMenus.PermissionDefinitions,
                context.GetLocalizer<DynamicPermissionResource>()["Menu:PermissionDefinitions"],
                "/DynamicPermission/PermissionDefinitions",
                icon: "fa fa-file-alt",
                requiredPermissionName: DynamicPermissionPermissions.PermissionDefinitions.Default
            )
        );
    }
    
    private static void AddMenuItemUserPermissions(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        parentMenu.AddItem(
            new ApplicationMenuItem(
                Menus.DynamicPermissionMenus.UserPermissions,
                context.GetLocalizer<DynamicPermissionResource>()["Menu:UserPermissions"],
                "/DynamicPermission/UserPermissions",
                icon: "fa fa-file-alt",
                requiredPermissionName: "AbpIdentity.Users"
            )
        );
    }
}