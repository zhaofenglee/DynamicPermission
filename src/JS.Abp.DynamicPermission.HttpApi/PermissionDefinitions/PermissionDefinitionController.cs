using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using JS.Abp.DynamicPermission.PermissionDefinitions;
using Volo.Abp.Content;
using JS.Abp.DynamicPermission.Shared;

namespace JS.Abp.DynamicPermission.PermissionDefinitions
{
    [RemoteService(Name = "DynamicPermission")]
    [Area("dynamicPermission")]
    [ControllerName("PermissionDefinition")]
    [Route("api/dynamic-permission/permission-definitions")]
    public class PermissionDefinitionController : AbpController, IPermissionDefinitionsAppService
    {
        protected IPermissionDefinitionsAppService _permissionDefinitionsAppService;

        public PermissionDefinitionController(IPermissionDefinitionsAppService permissionDefinitionsAppService)
        {
            _permissionDefinitionsAppService = permissionDefinitionsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<PermissionDefinitionDto>> GetListAsync(GetPermissionDefinitionsInput input)
        {
            return _permissionDefinitionsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<PermissionDefinitionDto> GetAsync(Guid id)
        {
            return _permissionDefinitionsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<PermissionDefinitionDto> CreateAsync(PermissionDefinitionCreateDto input)
        {
            return _permissionDefinitionsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<PermissionDefinitionDto> UpdateAsync(Guid id, PermissionDefinitionUpdateDto input)
        {
            return _permissionDefinitionsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _permissionDefinitionsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(PermissionDefinitionExcelDownloadDto input)
        {
            return _permissionDefinitionsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public virtual Task<JS.Abp.DynamicPermission.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _permissionDefinitionsAppService.GetDownloadTokenAsync();
        }
    }
}