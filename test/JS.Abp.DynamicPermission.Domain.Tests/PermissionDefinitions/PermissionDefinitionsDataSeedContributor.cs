using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using JS.Abp.DynamicPermission.PermissionDefinitions;

namespace JS.Abp.DynamicPermission.PermissionDefinitions
{
    public class PermissionDefinitionsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IPermissionDefinitionRepository _permissionDefinitionRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public PermissionDefinitionsDataSeedContributor(IPermissionDefinitionRepository permissionDefinitionRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _permissionDefinitionRepository = permissionDefinitionRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _permissionDefinitionRepository.InsertAsync(new PermissionDefinition
            (
                id: Guid.Parse("653f30a2-af89-4ec6-b6b6-8321150329f1"),
                groupName: "PermissionDefinitionsTest",
                name: "ea3ce507632c4645a5329da5d2f5e49dac37d936aa984e3a93f658c88ad1c49d9a4dbdd556634e868a8dce107c39f7e0502197ed610044b995c3e23f8d9b011d",
                parentName: "7eb35573a70d4527a2f484545a36819d16aa04a7217a4acbb9e56ce1c68c5757f6efe5da70c4438999a330709c631ee588cd818be2444234bc4e48558f7a82f9",
                displayName: "displayName1",
                isEnabled: true
            ));

            await _permissionDefinitionRepository.InsertAsync(new PermissionDefinition
            (
                id: Guid.Parse("fb8ae319-f063-4407-bdbd-5ae6255f90f8"),
                groupName: "PermissionDefinitionsTest",
                name: "49b5d1cd7d4b45ce951018b0dbd088ae6bbeb1a964954c8c96b2023389c4d49905a5043329224b9ba7b8e6c81d857b435d0efb4c00e843d5b711d8a585e7442e",
                parentName: "8f9c5d491c8547d1bb81bd4d5f91a8afd3453411c40b44cb8f96494c5840121691b6b8631834463d908f4da95f06f40960e5a5fa66184b678a3cb6c011fa07d8",
                displayName: "displayName2",
                isEnabled: true
            ));

            await _unitOfWorkManager!.Current!.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}