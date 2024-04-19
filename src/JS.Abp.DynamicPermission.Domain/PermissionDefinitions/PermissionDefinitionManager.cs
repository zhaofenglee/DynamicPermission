using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace JS.Abp.DynamicPermission.PermissionDefinitions
{
    public class PermissionDefinitionManager : DomainService
    {
        protected IPermissionDefinitionRepository _permissionDefinitionRepository;

        public PermissionDefinitionManager(IPermissionDefinitionRepository permissionDefinitionRepository)
        {
            _permissionDefinitionRepository = permissionDefinitionRepository;
        }

        public virtual async Task<PermissionDefinition> CreateAsync(
        string groupName, string name, string displayName, bool isEnabled, string? parentName = null)
        {
            Check.NotNullOrWhiteSpace(groupName, nameof(groupName));
            Check.Length(groupName, nameof(groupName), PermissionDefinitionConsts.GroupNameMaxLength, PermissionDefinitionConsts.GroupNameMinLength);
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), PermissionDefinitionConsts.NameMaxLength, PermissionDefinitionConsts.NameMinLength);
            Check.NotNullOrWhiteSpace(displayName, nameof(displayName));
            Check.Length(displayName, nameof(displayName), PermissionDefinitionConsts.DisplayNameMaxLength, PermissionDefinitionConsts.DisplayNameMinLength);
            Check.Length(parentName, nameof(parentName), PermissionDefinitionConsts.ParentNameMaxLength);

            var permissionDefinition = new PermissionDefinition(
             GuidGenerator.Create(),
             groupName, name, displayName, isEnabled, parentName
             );

            return await _permissionDefinitionRepository.InsertAsync(permissionDefinition);
        }

        public virtual async Task<PermissionDefinition> UpdateAsync(
            Guid id,
            string groupName, string name, string displayName, bool isEnabled, string? parentName = null, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(groupName, nameof(groupName));
            Check.Length(groupName, nameof(groupName), PermissionDefinitionConsts.GroupNameMaxLength, PermissionDefinitionConsts.GroupNameMinLength);
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), PermissionDefinitionConsts.NameMaxLength, PermissionDefinitionConsts.NameMinLength);
            Check.NotNullOrWhiteSpace(displayName, nameof(displayName));
            Check.Length(displayName, nameof(displayName), PermissionDefinitionConsts.DisplayNameMaxLength, PermissionDefinitionConsts.DisplayNameMinLength);
            Check.Length(parentName, nameof(parentName), PermissionDefinitionConsts.ParentNameMaxLength);

            var permissionDefinition = await _permissionDefinitionRepository.GetAsync(id);

            permissionDefinition.GroupName = groupName;
            permissionDefinition.Name = name;
            permissionDefinition.DisplayName = displayName;
            permissionDefinition.IsEnabled = isEnabled;
            permissionDefinition.ParentName = parentName;

            permissionDefinition.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _permissionDefinitionRepository.UpdateAsync(permissionDefinition);
        }

    }
}