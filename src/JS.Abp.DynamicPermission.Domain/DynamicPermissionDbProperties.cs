using Volo.Abp.Data;

namespace JS.Abp.DynamicPermission;

public static class DynamicPermissionDbProperties
{
    public static string DbTablePrefix { get; set; } = AbpCommonDbProperties.DbTablePrefix;

    public static string? DbSchema { get; set; } = AbpCommonDbProperties.DbSchema;

    public const string ConnectionStringName = "DynamicPermission";
}
