using Volo.Abp.AutoMapper;
using JS.Abp.DynamicPermission.PermissionDefinitions;
using AutoMapper;

namespace JS.Abp.DynamicPermission.Blazor;

public class DynamicPermissionBlazorAutoMapperProfile : Profile
{
    public DynamicPermissionBlazorAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<PermissionDefinitionDto, PermissionDefinitionUpdateDto>();
    }
}