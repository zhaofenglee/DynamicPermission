using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MiniExcelLibs;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.Content;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Integration;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Users;
using DistributedCacheEntryOptions = Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions;
using DownloadTokenResultDto = JS.Abp.DynamicPermission.Shared.DownloadTokenResultDto;

namespace JS.Abp.DynamicPermission.UserPermissions;

public class UserPermissionAppService:ApplicationService,IUserPermissionAppService
{
    private readonly IDistributedCache<UserPermissionExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
    private readonly IIdentityUserIntegrationService _identityUserIntegrationService;
    private readonly IPermissionAppService _permissionAppService;
    public UserPermissionAppService(IIdentityUserIntegrationService identityUserIntegrationService,
        IPermissionAppService permissionAppService,IDistributedCache<UserPermissionExcelDownloadTokenCacheItem, string> excelDownloadTokenCache
        )
    {
        _identityUserIntegrationService = identityUserIntegrationService;
        _permissionAppService = permissionAppService;
        _excelDownloadTokenCache = excelDownloadTokenCache;
    }
    
    public async Task<PagedResultDto<UserPermissionDto>> GetListAsync(GetUserPermissionInput input)
    {
       
        var userList = await GetUserPermissionListAsync(input);
        var totalCount = userList.Count;

        // 实现分页
        var pagedUserList = userList
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount)
            .ToList();

        return new PagedResultDto<UserPermissionDto>()
        {
            Items = pagedUserList,
            TotalCount = totalCount
        };
    }
    
    private async Task<List<UserPermissionDto>> GetUserPermissionListAsync(GetUserPermissionInput input)
    {
        var userList = await _identityUserIntegrationService.SearchAsync(new UserLookupSearchInputDto()
        {
            Filter = input.FilterText
        });
        if (userList.Items.Any())
        {
            var items = userList.Items.ToList();
            if (!string.IsNullOrWhiteSpace(input.UserName))
            {
                items = items.Where(u => u.UserName.Contains(input.UserName)).ToList();
            }
            if (input.IsActive.HasValue)
            {
                items = items.Where(u => u.IsActive == input.IsActive).ToList();
            }

            List<UserPermissionDto> result = new List<UserPermissionDto>();
            foreach (var user in items)
            {
                var permissions = await _permissionAppService.GetAsync("U",user.Id.ToString());
                if (!string.IsNullOrWhiteSpace(input.GroupName))
                {
                    permissions.Groups = permissions.Groups.Where(g => g.Name.Contains(input.GroupName)).ToList();
                }
                if (!string.IsNullOrWhiteSpace(input.PermissionName))
                {
                    permissions.Groups = permissions.Groups.Where(g => g.Permissions.Any(p => p.Name.Contains(input.PermissionName))).ToList();
                }
                if (input.IsGranted.HasValue)
                {
                    permissions.Groups = permissions.Groups.Where(g => g.Permissions.Any(p => p.IsGranted == input.IsGranted)).ToList();
                }
                if (permissions!=null&&permissions.Groups.Any())
                {
                    foreach (var group in permissions.Groups)
                    {
                        foreach (var permission in group.Permissions)
                        {
                            UserPermissionDto userPermissionDto = new UserPermissionDto();
                            userPermissionDto.UserName = user.UserName;
                            userPermissionDto.Name = user.Name;
                            userPermissionDto.Surname = user.Surname;
                            userPermissionDto.Email = user.Email;
                            userPermissionDto.IsActive = user.IsActive;
                            userPermissionDto.PhoneNumber = user.PhoneNumber;
                            userPermissionDto.GroupName = group.DisplayName;
                            userPermissionDto.PermissionName = permission.Name;
                            userPermissionDto.PermissionDisplayName = permission.DisplayName;
                            userPermissionDto.IsGranted = permission.IsGranted;
                            result.Add(userPermissionDto); 
                            
                        }
                        
                    }
                   
                }
             
            }
          
            return result;
        }
        else
        {
            return new List<UserPermissionDto>();
        }
    }

    public async Task<IRemoteStreamContent> GetListAsExcelFileAsync(UserPermissionExcelDownloadDto input)
    {
        var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
        if (downloadToken == null || input.DownloadToken != downloadToken.Token)
        {
            throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
        }

        var items = await GetUserPermissionListAsync(new GetUserPermissionInput()
        {
            FilterText = input.FilterText,
            GroupName = input.GroupName,
            PermissionName = input.PermissionName,
            UserName = input.UserName,
            IsActive = input.IsActive,
            IsGranted = input.IsGranted
        });

        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(ObjectMapper.Map<List<UserPermissionDto>, List<UserPermissionExcelDto>>(items));
        memoryStream.Seek(0, SeekOrigin.Begin);

        return new RemoteStreamContent(memoryStream, "UserPermissions.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

    }

    public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");

        await _excelDownloadTokenCache.SetAsync(
            token,
            new UserPermissionExcelDownloadTokenCacheItem { Token = token },
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
            });

        return new DownloadTokenResultDto
        {
            Token = token
        };
    }
}