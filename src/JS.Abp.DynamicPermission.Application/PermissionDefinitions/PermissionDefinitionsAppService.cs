using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using JS.Abp.DynamicPermission.Permissions;
using JS.Abp.DynamicPermission.PermissionDefinitions;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using JS.Abp.DynamicPermission.Shared;

namespace JS.Abp.DynamicPermission.PermissionDefinitions
{

    [Authorize(DynamicPermissionPermissions.PermissionDefinitions.Default)]
    public class PermissionDefinitionsAppService : ApplicationService, IPermissionDefinitionsAppService
    {
        protected IDistributedCache<PermissionDefinitionExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        protected IPermissionDefinitionRepository _permissionDefinitionRepository;
        protected PermissionDefinitionManager _permissionDefinitionManager;

        public PermissionDefinitionsAppService(IPermissionDefinitionRepository permissionDefinitionRepository, PermissionDefinitionManager permissionDefinitionManager, IDistributedCache<PermissionDefinitionExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _permissionDefinitionRepository = permissionDefinitionRepository;
            _permissionDefinitionManager = permissionDefinitionManager;
        }

        public virtual async Task<PagedResultDto<PermissionDefinitionDto>> GetListAsync(GetPermissionDefinitionsInput input)
        {
            var totalCount = await _permissionDefinitionRepository.GetCountAsync(input.FilterText, input.GroupName, input.Name, input.ParentName, input.DisplayName, input.IsEnabled);
            var items = await _permissionDefinitionRepository.GetListAsync(input.FilterText, input.GroupName, input.Name, input.ParentName, input.DisplayName, input.IsEnabled, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<PermissionDefinitionDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<PermissionDefinition>, List<PermissionDefinitionDto>>(items)
            };
        }

        public virtual async Task<PermissionDefinitionDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<PermissionDefinition, PermissionDefinitionDto>(await _permissionDefinitionRepository.GetAsync(id));
        }

        [Authorize(DynamicPermissionPermissions.PermissionDefinitions.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _permissionDefinitionRepository.DeleteAsync(id);
        }

        [Authorize(DynamicPermissionPermissions.PermissionDefinitions.Create)]
        public virtual async Task<PermissionDefinitionDto> CreateAsync(PermissionDefinitionCreateDto input)
        {

            var permissionDefinition = await _permissionDefinitionManager.CreateAsync(
            input.GroupName, input.Name, input.DisplayName, input.IsEnabled, input.ParentName
            );

            return ObjectMapper.Map<PermissionDefinition, PermissionDefinitionDto>(permissionDefinition);
        }

        [Authorize(DynamicPermissionPermissions.PermissionDefinitions.Edit)]
        public virtual async Task<PermissionDefinitionDto> UpdateAsync(Guid id, PermissionDefinitionUpdateDto input)
        {

            var permissionDefinition = await _permissionDefinitionManager.UpdateAsync(
            id,
            input.GroupName, input.Name, input.DisplayName, input.IsEnabled, input.ParentName, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<PermissionDefinition, PermissionDefinitionDto>(permissionDefinition);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(PermissionDefinitionExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _permissionDefinitionRepository.GetListAsync(input.FilterText, input.GroupName, input.Name, input.ParentName, input.DisplayName, input.IsEnabled);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<PermissionDefinition>, List<PermissionDefinitionExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "PermissionDefinitions.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public virtual async Task<JS.Abp.DynamicPermission.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new PermissionDefinitionExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new JS.Abp.DynamicPermission.Shared.DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}