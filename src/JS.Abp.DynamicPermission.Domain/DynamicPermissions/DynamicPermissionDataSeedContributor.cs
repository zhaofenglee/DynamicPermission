using System.Collections.Generic;
using System.Threading.Tasks;
using JS.Abp.DynamicPermission.PermissionDefinitions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace JS.Abp.DynamicPermission.DynamicPermissions;

public class DynamicPermissionDataSeedContributor: IDataSeedContributor, ITransientDependency
{
    private readonly IPermissionDefinitionRepository _dynamicPermissionDefinitionRepository;
    private readonly IGuidGenerator _guidGenerator;
    private readonly ICurrentTenant _currentTenant;
    
    public DynamicPermissionDataSeedContributor(IPermissionDefinitionRepository dynamicPermissionDefinitionRepository, 
        IGuidGenerator guidGenerator, 
        ICurrentTenant currentTenant)
    {
        _dynamicPermissionDefinitionRepository = dynamicPermissionDefinitionRepository;
        _guidGenerator = guidGenerator;
        _currentTenant = currentTenant;
    }

    [UnitOfWork]
    public async  Task SeedAsync(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            if (await _dynamicPermissionDefinitionRepository.GetCountAsync() > 0)
            {
                return;
            }
            List<PermissionDefinition> permissionExtras = new List<PermissionDefinition>();
            permissionExtras.Add(new PermissionDefinition(_guidGenerator.Create(), DynamicPermissionConsts.GroupName, "DynamicPermission.PermissionDefinitions", "Permission:PermissionDefinitions", true, null));
            permissionExtras.Add(new PermissionDefinition(_guidGenerator.Create(), DynamicPermissionConsts.GroupName, "DynamicPermission.PermissionDefinitions.Create", "Permission:Create",true,"DynamicPermission.PermissionDefinitions"  ));
            permissionExtras.Add(new PermissionDefinition(_guidGenerator.Create(), DynamicPermissionConsts.GroupName, "DynamicPermission.PermissionDefinitions.Edit", "Permission:Edit", true, "DynamicPermission.PermissionDefinitions"));
            permissionExtras.Add(new PermissionDefinition(_guidGenerator.Create(), DynamicPermissionConsts.GroupName, "DynamicPermission.PermissionDefinitions.Delete", "Permission:Delete", true, "DynamicPermission.PermissionDefinitions"));
            await _dynamicPermissionDefinitionRepository.InsertManyAsync(permissionExtras);
        }
    }
}