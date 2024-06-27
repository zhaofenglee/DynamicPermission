using Volo.Abp.Bundling;

namespace JS.Abp.DynamicPermission.Blazor.Host.Client;

public class DynamicPermissionBlazorHostBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {

    }

    public void AddStyles(BundleContext context)
    {
        context.Add("main.css", true);
    }
}
