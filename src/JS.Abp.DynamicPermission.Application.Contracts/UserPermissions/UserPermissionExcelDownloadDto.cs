namespace JS.Abp.DynamicPermission.UserPermissions;

public class UserPermissionExcelDownloadDto
{
    public string DownloadToken { get; set; } = null!;

    public string? FilterText { get; set; }
    public string? GroupName { get; set; }
    public string? PermissionName { get; set; }
    public string? UserName { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsGranted { get; set; }
}