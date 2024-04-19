using Volo.Abp.Reflection;

namespace JS.Abp.DynamicPermission.Permissions;

public class DynamicPermissionPermissions
{
    public const string GroupName = "DynamicPermission";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(DynamicPermissionPermissions));
    }

    public static class PermissionDefinitions
    {
        public const string Default = GroupName + ".PermissionDefinitions";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}