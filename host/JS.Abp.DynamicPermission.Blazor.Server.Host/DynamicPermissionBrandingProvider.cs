﻿using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace JS.Abp.DynamicPermission.Blazor.Server.Host;

[Dependency(ReplaceServices = true)]
public class DynamicPermissionBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "DynamicPermission";
}
