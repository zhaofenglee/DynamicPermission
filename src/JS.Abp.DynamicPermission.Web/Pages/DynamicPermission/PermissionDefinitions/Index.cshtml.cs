using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using JS.Abp.DynamicPermission.PermissionDefinitions;
using JS.Abp.DynamicPermission.Shared;

namespace JS.Abp.DynamicPermission.Web.Pages.DynamicPermission.PermissionDefinitions
{
    public class IndexModel : AbpPageModel
    {
        public string? GroupNameFilter { get; set; }
        public string? NameFilter { get; set; }
        public string? ParentNameFilter { get; set; }
        public string? DisplayNameFilter { get; set; }
        [SelectItems(nameof(IsEnabledBoolFilterItems))]
        public string IsEnabledFilter { get; set; }

        public List<SelectListItem> IsEnabledBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };

        protected IPermissionDefinitionsAppService _permissionDefinitionsAppService;

        public IndexModel(IPermissionDefinitionsAppService permissionDefinitionsAppService)
        {
            _permissionDefinitionsAppService = permissionDefinitionsAppService;
        }

        public virtual async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}