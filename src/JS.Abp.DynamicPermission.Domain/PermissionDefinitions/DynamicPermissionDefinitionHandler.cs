using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;
using Volo.Abp.Guids;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;

namespace JS.Abp.DynamicPermission.PermissionDefinitions;

public class DynamicPermissionDefinitionHandler: ILocalEventHandler<EntityChangedEventData<PermissionDefinition>>,IUnitOfWorkEnabled,ITransientDependency
{
    // private string DynamicPermissionGroupName = DynamicPermissionConsts.GroupName;
    
    private readonly IGuidGenerator _guidGenerator;
    private readonly IPermissionGroupDefinitionRecordRepository _permissionGroupDefinitionRecordRepository;
    private readonly IPermissionDefinitionRecordRepository _permissionDefinitionRecordRepository;
    private readonly IDynamicPermissionDefinitionStoreInMemoryCache _storeCache;
    private readonly IDistributedCache _distributedCache;
    private AbpDistributedCacheOptions CacheOptions { get; }
    public DynamicPermissionDefinitionHandler(IGuidGenerator guidGenerator, 
        IPermissionGroupDefinitionRecordRepository permissionGroupDefinitionRecordRepository, 
        IPermissionDefinitionRecordRepository permissionDefinitionRecordRepository, 
        IDynamicPermissionDefinitionStoreInMemoryCache storeCache,
        IDistributedCache distributedCache,
        IOptions<AbpDistributedCacheOptions> cacheOptions)
    {
        _guidGenerator = guidGenerator;
        _permissionGroupDefinitionRecordRepository = permissionGroupDefinitionRecordRepository;
        _permissionDefinitionRecordRepository = permissionDefinitionRecordRepository;
        _storeCache = storeCache;
        _distributedCache = distributedCache;
        CacheOptions = cacheOptions.Value;
    }

    public async Task HandleEventAsync(EntityChangedEventData<PermissionDefinition> eventData)
    {
        await CreateGroupAsync(eventData.Entity.GroupName);
        var record = await _permissionDefinitionRecordRepository.FindByNameAsync(eventData.Entity.Name);
        if (eventData is EntityCreatedEventData<PermissionDefinition>)
        {
            if (record != null)
            {
                return;
            }

            await _permissionDefinitionRecordRepository.InsertAsync(new PermissionDefinitionRecord(
                _guidGenerator.Create(), eventData.Entity.GroupName, eventData.Entity.Name,  eventData.Entity.ParentName.IsNullOrWhiteSpace()?null:eventData.Entity.ParentName,
                $"L:{eventData.Entity.GroupName},{eventData.Entity.DisplayName}"), true);
            
        }
        
        if (eventData is EntityUpdatedEventData<PermissionDefinition>)
        {
            if (record == null || record.GroupName != eventData.Entity.GroupName)
            {
                return;
            }
            record.ParentName = eventData.Entity.ParentName.IsNullOrWhiteSpace()?null:eventData.Entity.ParentName;
            record.IsEnabled = eventData.Entity.IsEnabled;
            record.DisplayName = $"L:{eventData.Entity.GroupName},{eventData.Entity.DisplayName}";

            await _permissionDefinitionRecordRepository.UpdateAsync(record, true);
        }
        
        if (eventData is EntityDeletedEventData<PermissionDefinition>)
        {
            if (record == null || record.GroupName != eventData.Entity.GroupName)
            {
                return;
            }

            await _permissionDefinitionRecordRepository.DeleteAsync(record, true);
        }
        await ClearStoreCacheAsync();
    }
    
    protected virtual async Task CreateGroupAsync(string groupName)
    {
        var permissionGroupRecords = (await _permissionGroupDefinitionRecordRepository.GetListAsync())
            .ToDictionary(x => x.Name);

        if (permissionGroupRecords.ContainsKey(groupName))
        {
            return;
        }

        await _permissionGroupDefinitionRecordRepository.InsertAsync(
            new PermissionGroupDefinitionRecord(_guidGenerator.Create(), groupName,
                $"L:{groupName},Permission:{groupName}"), true);
    }
    
    protected virtual async Task ClearStoreCacheAsync()
    {
        _storeCache.LastCheckTime = null;
        _storeCache.CacheStamp = null;
        await _distributedCache.RemoveAsync($"{CacheOptions.KeyPrefix}_AbpInMemoryPermissionCacheStamp");
     
    }
}