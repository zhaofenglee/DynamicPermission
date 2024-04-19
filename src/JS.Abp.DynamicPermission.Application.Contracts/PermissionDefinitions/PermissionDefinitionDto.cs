using System;
using System.Collections.Generic;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace JS.Abp.DynamicPermission.PermissionDefinitions
{
    public class PermissionDefinitionDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string GroupName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? ParentName { get; set; }
        public string DisplayName { get; set; } = null!;
        public bool IsEnabled { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;

    }
}