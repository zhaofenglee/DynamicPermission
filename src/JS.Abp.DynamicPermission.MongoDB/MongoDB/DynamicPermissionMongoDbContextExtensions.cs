using Volo.Abp;
using Volo.Abp.MongoDB;

namespace JS.Abp.DynamicPermission.MongoDB;

public static class DynamicPermissionMongoDbContextExtensions
{
    public static void ConfigureDynamicPermission(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
