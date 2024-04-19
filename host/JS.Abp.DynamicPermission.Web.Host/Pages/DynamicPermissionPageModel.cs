using JS.Abp.DynamicPermission.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace JS.Abp.DynamicPermission.Pages;

public abstract class DynamicPermissionPageModel : AbpPageModel
{
    protected DynamicPermissionPageModel()
    {
        LocalizationResourceType = typeof(DynamicPermissionResource);
    }
}
