using System.Collections.Generic;
using System.Threading.Tasks;
using Asp.Versioning;
using JS.Abp.DynamicPermission.Shared;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Content;

namespace JS.Abp.DynamicPermission.UserPermissions;

[RemoteService(Name = "DynamicPermission")]
[Area("dynamicPermission")]
[ControllerName("UserPermission")]
[Route("api/dynamic-permission/user-permissions")]

public class UserPermissionController : AbpController, IUserPermissionAppService
{
    private readonly IUserPermissionAppService _userPermissionAppService;

    public UserPermissionController(IUserPermissionAppService userPermissionAppService)
    {
        _userPermissionAppService = userPermissionAppService;
    }

    [HttpGet]
    public virtual Task<PagedResultDto<UserPermissionDto>> GetListAsync(GetUserPermissionInput input)
    {
        return _userPermissionAppService.GetListAsync(input);
    }
    [HttpGet]
    [Route("as-excel-file")]
    public Task<IRemoteStreamContent> GetListAsExcelFileAsync(UserPermissionExcelDownloadDto input)
    {
        return _userPermissionAppService.GetListAsExcelFileAsync(input);
    }
    [HttpGet]
    [Route("download-token")]
    public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        return _userPermissionAppService.GetDownloadTokenAsync();
    }
}
