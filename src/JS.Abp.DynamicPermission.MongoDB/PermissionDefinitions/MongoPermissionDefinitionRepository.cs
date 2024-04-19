using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using JS.Abp.DynamicPermission.MongoDB;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using MongoDB.Driver.Linq;
using MongoDB.Driver;

namespace JS.Abp.DynamicPermission.PermissionDefinitions
{
    public class MongoPermissionDefinitionRepository : MongoDbRepository<DynamicPermissionMongoDbContext, PermissionDefinition, Guid>, IPermissionDefinitionRepository
    {
        public MongoPermissionDefinitionRepository(IMongoDbContextProvider<DynamicPermissionMongoDbContext> dbContextProvider)
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
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, groupName, name, parentName, displayName, isEnabled);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PermissionDefinitionConsts.GetDefaultSorting(false) : sorting);
            return await query.As<IMongoQueryable<PermissionDefinition>>()
                .PageBy<PermissionDefinition, IMongoQueryable<PermissionDefinition>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
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
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, groupName, name, parentName, displayName, isEnabled);
            return await query.As<IMongoQueryable<PermissionDefinition>>().LongCountAsync(GetCancellationToken(cancellationToken));
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