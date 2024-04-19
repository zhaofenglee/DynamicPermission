using System.Collections.Generic;
using System.Threading.Tasks;
using JS.Abp.DynamicPermission.PermissionDefinitions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace JS.Abp.DynamicPermission.Web.Pages.DynamicPermission.UserPermissions
{
    public class IndexModel : AbpPageModel
    {
        public string? GroupNameFilter { get; set; }
        public string? PermissionNameFilter { get; set; }
        public string? UserNameFilter { get; set; }
        public string? DisplayNameFilter { get; set; }
        [SelectItems(nameof(IsActiveFilterItems))]
        public string IsActiveFilter { get; set; }

        public List<SelectListItem> IsActiveFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };
        
        [SelectItems(nameof(IsGrantedFilterItems))]
        public string IsGrantedFilter { get; set; }

        public List<SelectListItem> IsGrantedFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };
        

        public virtual async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}