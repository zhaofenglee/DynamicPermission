using JS.Abp.DynamicPermission.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using JS.Abp.DynamicPermission.PermissionDefinitions;

namespace JS.Abp.DynamicPermission.Web.Pages.DynamicPermission.PermissionDefinitions
{
    public class EditModalModel : DynamicPermissionPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public PermissionDefinitionUpdateViewModel PermissionDefinition { get; set; }

        protected IPermissionDefinitionsAppService _permissionDefinitionsAppService;

        public EditModalModel(IPermissionDefinitionsAppService permissionDefinitionsAppService)
        {
            _permissionDefinitionsAppService = permissionDefinitionsAppService;

            PermissionDefinition = new();
        }

        public virtual async Task OnGetAsync()
        {
            var permissionDefinition = await _permissionDefinitionsAppService.GetAsync(Id);
            PermissionDefinition = ObjectMapper.Map<PermissionDefinitionDto, PermissionDefinitionUpdateViewModel>(permissionDefinition);

        }

        public virtual async Task<NoContentResult> OnPostAsync()
        {

            await _permissionDefinitionsAppService.UpdateAsync(Id, ObjectMapper.Map<PermissionDefinitionUpdateViewModel, PermissionDefinitionUpdateDto>(PermissionDefinition));
            return NoContent();
        }
    }

    public class PermissionDefinitionUpdateViewModel : PermissionDefinitionUpdateDto
    {
    }
}