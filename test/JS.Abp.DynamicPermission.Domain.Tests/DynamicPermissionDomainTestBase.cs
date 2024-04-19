using Volo.Abp.Modularity;

namespace JS.Abp.DynamicPermission;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class DynamicPermissionDomainTestBase<TStartupModule> : DynamicPermissionTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
