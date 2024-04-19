using Volo.Abp.Modularity;

namespace JS.Abp.DynamicPermission;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class DynamicPermissionApplicationTestBase<TStartupModule> : DynamicPermissionTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
