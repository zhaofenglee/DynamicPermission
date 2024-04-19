using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;

namespace JS.Abp.DynamicPermission.UserPermissions;

public class GetUserPermissionInput : PagedAndSortedResultRequestDto
{
    public string? FilterText { get; set; }
    public string? GroupName { get; set; }
    public string? PermissionName { get; set; }
    public string? UserName { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsGranted { get; set; }
}