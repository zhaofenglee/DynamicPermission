using System;
using JS.Abp.DynamicPermission.Shared;
using Volo.Abp.AutoMapper;
using JS.Abp.DynamicPermission.PermissionDefinitions;
using AutoMapper;
using JS.Abp.DynamicPermission.UserPermissions;

namespace JS.Abp.DynamicPermission;

public class DynamicPermissionApplicationAutoMapperProfile : Profile
{
    public DynamicPermissionApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<PermissionDefinition, PermissionDefinitionDto>();
        CreateMap<PermissionDefinition, PermissionDefinitionExcelDto>();
        
        CreateMap<UserPermissionDto, UserPermissionExcelDto>();
    }
}