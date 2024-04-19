using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace JS.Abp.DynamicPermission.PermissionDefinitions
{
    public class PermissionDefinition : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string GroupName { get; set; }

        [NotNull]
        public virtual string Name { get; set; }

        [CanBeNull]
        public virtual string? ParentName { get; set; }

        [NotNull]
        public virtual string DisplayName { get; set; }

        public virtual bool IsEnabled { get; set; }

        protected PermissionDefinition()
        {

        }

        public PermissionDefinition(Guid id, string groupName, string name, string displayName, bool isEnabled, string? parentName = null)
        {

            Id = id;
            Check.NotNull(groupName, nameof(groupName));
            Check.Length(groupName, nameof(groupName), PermissionDefinitionConsts.GroupNameMaxLength, PermissionDefinitionConsts.GroupNameMinLength);
            Check.NotNull(name, nameof(name));
            Check.Length(name, nameof(name), PermissionDefinitionConsts.NameMaxLength, PermissionDefinitionConsts.NameMinLength);
            Check.NotNull(displayName, nameof(displayName));
            Check.Length(displayName, nameof(displayName), PermissionDefinitionConsts.DisplayNameMaxLength, PermissionDefinitionConsts.DisplayNameMinLength);
            Check.Length(parentName, nameof(parentName), PermissionDefinitionConsts.ParentNameMaxLength, 0);
            GroupName = groupName;
            Name = name;
            DisplayName = displayName;
            IsEnabled = isEnabled;
            ParentName = parentName;
        }

    }
}