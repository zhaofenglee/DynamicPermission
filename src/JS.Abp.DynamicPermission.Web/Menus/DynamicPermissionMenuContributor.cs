using JS.Abp.DynamicPermission.Permissions;
using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using JS.Abp.DynamicPermission.Localization;
using Volo.Abp.Authorization.Permissions;

namespace JS.Abp.DynamicPermission.Web.Menus;

public class DynamicPermissionMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name != StandardMenus.Main)
        {
            return;
        }

        var moduleMenu = AddModuleMenuItem(context); 

        AddMenuItemPermissionDefinitions(context, moduleMenu);
        
        AddMenuItemUserPermissions(context, moduleMenu);
    }

    private static ApplicationMenuItem AddModuleMenuItem(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<DynamicPermissionResource>();

        var moduleMenu = new ApplicationMenuItem(
            DynamicPermissionMenus.Prefix,
            displayName: l["Menu:DynamicPermission"],
            icon: "fa fa-globe");

        //Add main menu items.
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