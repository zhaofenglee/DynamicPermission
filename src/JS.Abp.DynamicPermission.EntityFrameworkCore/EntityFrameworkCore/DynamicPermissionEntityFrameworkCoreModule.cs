using JS.Abp.DynamicPermission.PermissionDefinitions;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace JS.Abp.DynamicPermission.EntityFrameworkCore;

[DependsOn(
    typeof(DynamicPermissionDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class DynamicPermissionEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<DynamicPermissionDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */
            options.AddRepository<PermissionDefinition, PermissionDefinitions.EfCorePermissionDefinitionRepository>();

        });
    }
}