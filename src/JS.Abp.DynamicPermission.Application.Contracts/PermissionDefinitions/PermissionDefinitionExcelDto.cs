using System;

namespace JS.Abp.DynamicPermission.PermissionDefinitions
{
    public class PermissionDefinitionExcelDto
    {
        public string GroupName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? ParentName { get; set; }
        public string DisplayName { get; set; } = null!;
        public bool IsEnabled { get; set; }
    }
}