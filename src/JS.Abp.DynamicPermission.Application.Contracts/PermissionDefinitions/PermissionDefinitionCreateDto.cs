using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace JS.Abp.DynamicPermission.PermissionDefinitions
{
    public class PermissionDefinitionCreateDto
    {
        [Required]
        [StringLength(PermissionDefinitionConsts.GroupNameMaxLength, MinimumLength = PermissionDefinitionConsts.GroupNameMinLength)]
        public string GroupName { get; set; } = null!;
        [Required]
        [StringLength(PermissionDefinitionConsts.NameMaxLength, MinimumLength = PermissionDefinitionConsts.NameMinLength)]
        public string Name { get; set; } = null!;
        [StringLength(PermissionDefinitionConsts.ParentNameMaxLength)]
        public string? ParentName { get; set; }
        [Required]
        [StringLength(PermissionDefinitionConsts.DisplayNameMaxLength, MinimumLength = PermissionDefinitionConsts.DisplayNameMinLength)]
        public string DisplayName { get; set; } = null!;
        public bool IsEnabled { get; set; } = true;
    }
}