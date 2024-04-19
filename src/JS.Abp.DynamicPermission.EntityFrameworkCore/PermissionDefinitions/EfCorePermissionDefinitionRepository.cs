using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using JS.Abp.DynamicPermission.EntityFrameworkCore;

namespace JS.Abp.DynamicPermission.PermissionDefinitions
{
    public class EfCorePermissionDefinitionRepository : EfCoreRepository<DynamicPermissionDbContext, PermissionDefinition, Guid>, IPermissionDefinitionRepository
    {
        public EfCorePermissionDefinitionRepository(IDbContextProvider<DynamicPermissionDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<List<PermissionDefinition>> GetListAsync(
            string? filterText = null,
            string? groupName = null,
            string? name = null,
            string? parentName = null,
            string? displayName = null,
            bool? isEnabled = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, groupName, name, parentName, displayName, isEnabled);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PermissionDefinitionConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? groupName = null,
            string? name = null,
            string? parentName = null,
            string? displayName = null,
            bool? isEnabled = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, groupName, name, parentName, displayName, isEnabled);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<PermissionDefinition> ApplyFilter(
            IQueryable<PermissionDefinition> query,
            string? filterText = null,
            string? groupName = null,
            string? name = null,
            string? parentName = null,
            string? displayName = null,
            bool? isEnabled = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.GroupName!.Contains(filterText!) || e.Name!.Contains(filterText!) || e.ParentName!.Contains(filterText!) || e.DisplayName!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(groupName), e => e.GroupName.Contains(groupName))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(parentName), e => e.ParentName.Contains(parentName))
                    .WhereIf(!string.IsNullOrWhiteSpace(displayName), e => e.DisplayName.Contains(displayName))
                    .WhereIf(isEnabled.HasValue, e => e.IsEnabled == isEnabled);
        }
    }
}