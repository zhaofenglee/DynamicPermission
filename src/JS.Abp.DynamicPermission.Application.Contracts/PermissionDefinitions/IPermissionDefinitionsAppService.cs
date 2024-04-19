using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using JS.Abp.DynamicPermission.Shared;

namespace JS.Abp.DynamicPermission.PermissionDefinitions
{
    public interface IPermissionDefinitionsAppService : IApplicationService
    {

        Task<PagedResultDto<PermissionDefinitionDto>> GetListAsync(GetPermissionDefinitionsInput input);

        Task<PermissionDefinitionDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<PermissionDefinitionDto> CreateAsync(PermissionDefinitionCreateDto input);

        Task<PermissionDefinitionDto> UpdateAsync(Guid id, PermissionDefinitionUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(PermissionDefinitionExcelDownloadDto input);

        Task<JS.Abp.DynamicPermission.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}