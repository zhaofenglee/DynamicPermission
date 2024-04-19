using System.Collections.Generic;
using System.Threading.Tasks;
using JS.Abp.DynamicPermission.Shared;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace JS.Abp.DynamicPermission.UserPermissions;

public interface IUserPermissionAppService: IApplicationService
{
    Task<PagedResultDto<UserPermissionDto>> GetListAsync(GetUserPermissionInput input);
    
    Task<IRemoteStreamContent> GetListAsExcelFileAsync(UserPermissionExcelDownloadDto input);

    Task<DownloadTokenResultDto> GetDownloadTokenAsync();
}