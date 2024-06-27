using JS.Abp.DynamicPermission.Localization;
using Volo.Abp.Application.Services;

namespace JS.Abp.DynamicPermission;

public abstract class DynamicPermissionAppService : ApplicationService
{
    protected DynamicPermissionAppService()
    {
        LocalizationResource = typeof(DynamicPermissionResource);
        ObjectMapperContext = typeof(DynamicPermissionApplicationModule);
    }
}
