using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using JS.Abp.DynamicPermission.PermissionDefinitions;
using JS.Abp.DynamicPermission.EntityFrameworkCore;
using Xunit;

namespace JS.Abp.DynamicPermission.EntityFrameworkCore.Domains.PermissionDefinitions
{
    public class PermissionDefinitionRepositoryTests : DynamicPermissionEntityFrameworkCoreTestBase
    {
        private readonly IPermissionDefinitionRepository _permissionDefinitionRepository;

        public PermissionDefinitionRepositoryTests()
        {
            _permissionDefinitionRepository = GetRequiredService<IPermissionDefinitionRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _permissionDefinitionRepository.GetListAsync(
                    groupName: "PermissionDefinitionsTest",
                    name: "ea3ce507632c4645a5329da5d2f5e49dac37d936aa984e3a93f658c88ad1c49d9a4dbdd556634e868a8dce107c39f7e0502197ed610044b995c3e23f8d9b011d",
                    parentName: "7eb35573a70d4527a2f484545a36819d16aa04a7217a4acbb9e56ce1c68c5757f6efe5da70c4438999a330709c631ee588cd818be2444234bc4e48558f7a82f9",
                    displayName: "displayName1",
                    isEnabled: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("653f30a2-af89-4ec6-b6b6-8321150329f1"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _permissionDefinitionRepository.GetCountAsync(
                    groupName: "PermissionDefinitionsTest",
                    name: "49b5d1cd7d4b45ce951018b0dbd088ae6bbeb1a964954c8c96b2023389c4d49905a5043329224b9ba7b8e6c81d857b435d0efb4c00e843d5b711d8a585e7442e",
                    parentName: "8f9c5d491c8547d1bb81bd4d5f91a8afd3453411c40b44cb8f96494c5840121691b6b8631834463d908f4da95f06f40960e5a5fa66184b678a3cb6c011fa07d8",
                    displayName: "displayName2",
                    isEnabled: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}