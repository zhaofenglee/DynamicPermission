using JS.Abp.DynamicPermission.Localization;
using Volo.Abp.AspNetCore.Components;

namespace JS.Abp.DynamicPermission.Blazor.Server.Host;

public abstract class DynamicPermissionComponentBase : AbpComponentBase
{
    protected DynamicPermissionComponentBase()
    {
        LocalizationResource = typeof(DynamicPermissionResource);
    }
}
