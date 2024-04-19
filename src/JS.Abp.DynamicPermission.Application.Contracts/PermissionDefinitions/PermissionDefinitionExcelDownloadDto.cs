using Volo.Abp.Application.Dtos;
using System;

namespace JS.Abp.DynamicPermission.PermissionDefinitions
{
    public class PermissionDefinitionExcelDownloadDto
    {
        public string DownloadToken { get; set; } = null!;

        public string? FilterText { get; set; }

        public string? GroupName { get; set; }
        public string? Name { get; set; }
        public string? ParentName { get; set; }
        public string? DisplayName { get; set; }
        public bool? IsEnabled { get; set; }

        public PermissionDefinitionExcelDownloadDto()
        {

        }
    }
}