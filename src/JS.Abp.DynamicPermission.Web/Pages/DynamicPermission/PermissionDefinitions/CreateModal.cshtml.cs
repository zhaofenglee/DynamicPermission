using JS.Abp.DynamicPermission.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JS.Abp.DynamicPermission.PermissionDefinitions;

namespace JS.Abp.DynamicPermission.Web.Pages.DynamicPermission.PermissionDefinitions
{
    public class CreateModalModel : DynamicPermissionPageModel
    {

        [BindProperty]
        public PermissionDefinitionCreateViewModel PermissionDefinition { get; set; }

        protected IPermissionDefinitionsAppService _permissionDefinitionsAppService;

        public CreateModalModel(IPermissionDefinitionsAppService permissionDefinitionsAppService)
        {
            _permissionDefinitionsAppService = permissionDefinitionsAppService;

            PermissionDefinition = new();
        }

        public virtual async Task OnGetAsync()
        {
            PermissionDefinition = new PermissionDefinitionCreateViewModel();
            
            await Task.CompletedTask;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {

            await _permissionDefinitionsAppService.CreateAsync(ObjectMapper.Map<PermissionDefinitionCreateViewModel, PermissionDefinitionCreateDto>(PermissionDefinition));
            return NoContent();
        }
    }

    public class PermissionDefinitionCreateViewModel : PermissionDefinitionCreateDto
    {
    }
}