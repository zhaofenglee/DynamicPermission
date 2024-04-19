namespace JS.Abp.DynamicPermission.UserPermissions;

public class UserPermissionExcelDto
{
    public string? UserName { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public bool? IsActive { get; set; }
    
    public string? Email { get; set; }
    
    public string? PhoneNumber { get; set; }
    
    public string GroupName { get; set; }
    
    public string PermissionDisplayName { get; set; }
    
    public bool IsGranted { get; set; }
}