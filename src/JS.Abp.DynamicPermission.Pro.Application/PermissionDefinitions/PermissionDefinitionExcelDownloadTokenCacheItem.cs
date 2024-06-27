using System;

namespace JS.Abp.DynamicPermission.PermissionDefinitions;

[Serializable]
public class PermissionDefinitionExcelDownloadTokenCacheItem
{
    public string Token { get; set; } = null!;
}