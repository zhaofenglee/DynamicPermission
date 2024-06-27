using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Modularity;
using Xunit;

namespace JS.Abp.DynamicPermission.PermissionDefinitions
{
    public abstract class PermissionDefinitionsAppServiceTests<TStartupModule> : DynamicPermissionApplicationTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        private readonly IPermissionDefinitionsAppService _permissionDefinitionsAppService;
        private readonly IRepository<PermissionDefinition, Guid> _permissionDefinitionRepository;

        public PermissionDefinitionsAppServiceTests()
        {
            _permissionDefinitionsAppService = GetRequiredService<IPermissionDefinitionsAppService>();
            _permissionDefinitionRepository = GetRequiredService<IRepository<PermissionDefinition, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _permissionDefinitionsAppService.GetListAsync(new GetPermissionDefinitionsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.GroupName == "PermissionDefinitionsTest").ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _permissionDefinitionsAppService.GetAsync(Guid.Parse("653f30a2-af89-4ec6-b6b6-8321150329f1"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("653f30a2-af89-4ec6-b6b6-8321150329f1"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new PermissionDefinitionCreateDto
            {
                GroupName = DynamicPermissionConsts.GroupName,
                Name = "AbpTestPermission",
                ParentName = null,
                DisplayName = "AbpTestPermissionDisplayName",
                IsEnabled = true
            };

            // Act
            var serviceResult = await _permissionDefinitionsAppService.CreateAsync(input);

            // Assert
            var result = await _permissionDefinitionRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.GroupName.ShouldBe(DynamicPermissionConsts.GroupName);
            result.Name.ShouldBe("AbpTestPermission");
            result.ParentName.ShouldBe(null);
            result.DisplayName.ShouldBe("AbpTestPermissionDisplayName");
            result.IsEnabled.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new PermissionDefinitionUpdateDto()
            {
                GroupName = "PermissionDefinitionsTest",
                Name = "ea3ce507632c4645a5329da5d2f5e49dac37d936aa984e3a93f658c88ad1c49d9a4dbdd556634e868a8dce107c39f7e0502197ed610044b995c3e23f8d9b011d",
                ParentName = "7eb35573a70d4527a2f484545a36819d16aa04a7217a4acbb9e56ce1c68c5757f6efe5da70c4438999a330709c631ee588cd818be2444234bc4e48558f7a82f9",
                DisplayName = "displayName1",
                IsEnabled = true
            };

            // Act
            var serviceResult = await _permissionDefinitionsAppService.UpdateAsync(Guid.Parse("653f30a2-af89-4ec6-b6b6-8321150329f1"), input);

            // Assert
            var result = await _permissionDefinitionRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.GroupName.ShouldBe("PermissionDefinitionsTest");
            result.Name.ShouldBe("ea3ce507632c4645a5329da5d2f5e49dac37d936aa984e3a93f658c88ad1c49d9a4dbdd556634e868a8dce107c39f7e0502197ed610044b995c3e23f8d9b011d");
            result.ParentName.ShouldBe("7eb35573a70d4527a2f484545a36819d16aa04a7217a4acbb9e56ce1c68c5757f6efe5da70c4438999a330709c631ee588cd818be2444234bc4e48558f7a82f9");
            result.DisplayName.ShouldBe("displayName1");
            result.IsEnabled.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _permissionDefinitionsAppService.DeleteAsync(Guid.Parse("653f30a2-af89-4ec6-b6b6-8321150329f1"));

            // Assert
            var result = await _permissionDefinitionRepository.FindAsync(c => c.Id == Guid.Parse("653f30a2-af89-4ec6-b6b6-8321150329f1"));

            result.ShouldBeNull();
        }
    }
}