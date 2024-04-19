using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace JS.Abp.DynamicPermission;

[Dependency(ReplaceServices = true)]
public class DynamicPermissionBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "DynamicPermission";
}
