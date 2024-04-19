using JS.Abp.DynamicPermission.Web.Pages.DynamicPermission.PermissionDefinitions;
using Volo.Abp.AutoMapper;
using JS.Abp.DynamicPermission.PermissionDefinitions;
using AutoMapper;

namespace JS.Abp.DynamicPermission.Web;

public class DynamicPermissionWebAutoMapperProfile : Profile
{
    public DynamicPermissionWebAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<PermissionDefinitionDto, PermissionDefinitionUpdateViewModel>();
        CreateMap<PermissionDefinitionUpdateViewModel, PermissionDefinitionUpdateDto>();
        CreateMap<PermissionDefinitionCreateViewModel, PermissionDefinitionCreateDto>();
    }
}