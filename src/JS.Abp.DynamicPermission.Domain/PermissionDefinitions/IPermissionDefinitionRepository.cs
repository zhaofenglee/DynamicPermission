using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace JS.Abp.DynamicPermission.PermissionDefinitions
{
    public interface IPermissionDefinitionRepository : IRepository<PermissionDefinition, Guid>
    {
        Task<List<PermissionDefinition>> GetListAsync(
            string? filterText = null,
            string? groupName = null,
            string? name = null,
            string? parentName = null,
            string? displayName = null,
            bool? isEnabled = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? groupName = null,
            string? name = null,
            string? parentName = null,
            string? displayName = null,
            bool? isEnabled = null,
            CancellationToken cancellationToken = default);
    }
}