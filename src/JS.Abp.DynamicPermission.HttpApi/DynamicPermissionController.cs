using JS.Abp.DynamicPermission.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace JS.Abp.DynamicPermission;

public abstract class DynamicPermissionController : AbpControllerBase
{
    protected DynamicPermissionController()
    {
        LocalizationResource = typeof(DynamicPermissionResource);
    }
}
