namespace JS.Abp.DynamicPermission.PermissionDefinitions
{
    public static class PermissionDefinitionConsts
    {
        private const string DefaultSorting = "{0}GroupName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "PermissionDefinition." : string.Empty);
        }

        public const int GroupNameMinLength = 1;
        public const int GroupNameMaxLength = 128;
        public const int NameMinLength = 1;
        public const int NameMaxLength = 128;
        public const int ParentNameMaxLength = 128;
        public const int DisplayNameMinLength = 1;
        public const int DisplayNameMaxLength = 256;
    }
}