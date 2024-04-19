using JS.Abp.DynamicPermission.PermissionDefinitions;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace JS.Abp.DynamicPermission.MongoDB;

[DependsOn(
    typeof(DynamicPermissionDomainModule),
    typeof(AbpMongoDbModule)
    )]
public class DynamicPermissionMongoDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<DynamicPermissionMongoDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, MongoQuestionRepository>();
             */
            options.AddRepository<PermissionDefinition, PermissionDefinitions.MongoPermissionDefinitionRepository>();

        });
    }
}