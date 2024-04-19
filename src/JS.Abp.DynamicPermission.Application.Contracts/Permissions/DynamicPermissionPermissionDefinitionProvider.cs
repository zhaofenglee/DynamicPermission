using JS.Abp.DynamicPermission.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace JS.Abp.DynamicPermission.Permissions;

public class DynamicPermissionPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        /*var myGroup = context.GetGroupOrNull(DynamicPermissionPermissions.GroupName);
        if (myGroup == null)
        {
            myGroup = context.AddGroup(DynamicPermissionPermissions.GroupName, L("Permission:DynamicPermission"));
            var permissionDefinitionPermission = myGroup.AddPermission(DynamicPermissionPermissions.PermissionDefinitions.Default, L("Permission:PermissionDefinitions"));
            permissionDefinitionPermission.AddChild(DynamicPermissionPermissions.PermissionDefinitions.Create, L("Permission:Create"));
            permissionDefinitionPermission.AddChild(DynamicPermissionPermissions.PermissionDefinitions.Edit, L("Permission:Edit"));
            permissionDefinitionPermission.AddChild(DynamicPermissionPermissions.PermissionDefinitions.Delete, L("Permission:Delete"));
        }*/
      
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<DynamicPermissionResource>(name);
    }
}