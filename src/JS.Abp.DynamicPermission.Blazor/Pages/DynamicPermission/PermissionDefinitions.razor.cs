using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Blazorise;
using Blazorise.DataGrid;
using Volo.Abp.BlazoriseUI.Components;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using JS.Abp.DynamicPermission.PermissionDefinitions;
using JS.Abp.DynamicPermission.Permissions;
using JS.Abp.DynamicPermission.Shared;
using Microsoft.AspNetCore.Components;
using Volo.Abp.Authorization.Permissions;


namespace JS.Abp.DynamicPermission.Blazor.Pages.DynamicPermission
{
    public partial class PermissionDefinitions
    {
        [Inject]
        public IPermissionChecker PermissionChecker { get; set; } = default!;
        
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar {get;} = new PageToolbar();
        protected bool ShowAdvancedFilters { get; set; }
        private IReadOnlyList<PermissionDefinitionDto> PermissionDefinitionList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private int TotalCount { get; set; }
        private bool CanCreatePermissionDefinition { get; set; }
        private bool CanEditPermissionDefinition { get; set; }
        private bool CanDeletePermissionDefinition { get; set; }
        private PermissionDefinitionCreateDto NewPermissionDefinition { get; set; }
        private Validations NewPermissionDefinitionValidations { get; set; } = new();
        private PermissionDefinitionUpdateDto EditingPermissionDefinition { get; set; }
        private Validations EditingPermissionDefinitionValidations { get; set; } = new();
        private Guid EditingPermissionDefinitionId { get; set; }
        private Modal CreatePermissionDefinitionModal { get; set; } = new();
        private Modal EditPermissionDefinitionModal { get; set; } = new();
        private GetPermissionDefinitionsInput Filter { get; set; }
        private DataGridEntityActionsColumn<PermissionDefinitionDto> EntityActionsColumn { get; set; } = new();
        protected string SelectedCreateTab = "permissionDefinition-create-tab";
        protected string SelectedEditTab = "permissionDefinition-edit-tab";
        private PermissionDefinitionDto? SelectedPermissionDefinition;
        
        
        
        
        public PermissionDefinitions()
        {
            NewPermissionDefinition = new PermissionDefinitionCreateDto();
            EditingPermissionDefinition = new PermissionDefinitionUpdateDto();
            Filter = new GetPermissionDefinitionsInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            PermissionDefinitionList = new List<PermissionDefinitionDto>();
            
            
        }

        protected override async Task OnInitializedAsync()
        {
            await SetPermissionsAsync();
            
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await SetBreadcrumbItemsAsync();
                await SetToolbarItemsAsync();
                StateHasChanged();
            }
        }  

        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:PermissionDefinitions"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["ExportToExcel"], async () =>{ await DownloadAsExcelAsync(); }, IconName.Download);
            
            Toolbar.AddButton(L["NewPermissionDefinition"], async () =>
            {
                await OpenCreatePermissionDefinitionModalAsync();
            }, IconName.Add, requiredPolicyName: DynamicPermissionPermissions.PermissionDefinitions.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreatePermissionDefinition = await PermissionChecker
                .IsGrantedAsync(DynamicPermissionPermissions.PermissionDefinitions.Create);
            CanEditPermissionDefinition = await PermissionChecker
                            .IsGrantedAsync(DynamicPermissionPermissions.PermissionDefinitions.Edit);
            CanDeletePermissionDefinition = await PermissionChecker
                            .IsGrantedAsync(DynamicPermissionPermissions.PermissionDefinitions.Delete);
                            
                            
        }

        private async Task GetPermissionDefinitionsAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await PermissionDefinitionsAppService.GetListAsync(Filter);
            PermissionDefinitionList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetPermissionDefinitionsAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task DownloadAsExcelAsync()
        {
            var token = (await PermissionDefinitionsAppService.GetDownloadTokenAsync()).Token;
            var remoteService = await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("DynamicPermission") ?? await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Default");
            var culture = CultureInfo.CurrentUICulture.Name ?? CultureInfo.CurrentCulture.Name;
            if(!culture.IsNullOrEmpty())
            {
                culture = "&culture=" + culture;
            }
            await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Default");
            NavigationManager.NavigateTo($"{remoteService?.BaseUrl.EnsureEndsWith('/') ?? string.Empty}api/dynamic-permission/permission-definitions/as-excel-file?DownloadToken={token}&FilterText={Filter.FilterText}{culture}&GroupName={Filter.GroupName}&Name={Filter.Name}&ParentName={Filter.ParentName}&DisplayName={Filter.DisplayName}&IsEnabled={Filter.IsEnabled}", forceLoad: true);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<PermissionDefinitionDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetPermissionDefinitionsAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OpenCreatePermissionDefinitionModalAsync()
        {
            NewPermissionDefinition = new PermissionDefinitionCreateDto{
                
               
            };
            await NewPermissionDefinitionValidations.ClearAll();
            await CreatePermissionDefinitionModal.Show();
        }

        private async Task CloseCreatePermissionDefinitionModalAsync()
        {
            NewPermissionDefinition = new PermissionDefinitionCreateDto{
                
                
            };
            await CreatePermissionDefinitionModal.Hide();
        }

        private async Task OpenEditPermissionDefinitionModalAsync(PermissionDefinitionDto input)
        {
            var permissionDefinition = await PermissionDefinitionsAppService.GetAsync(input.Id);
            
            EditingPermissionDefinitionId = permissionDefinition.Id;
            EditingPermissionDefinition = ObjectMapper.Map<PermissionDefinitionDto, PermissionDefinitionUpdateDto>(permissionDefinition);
            await EditingPermissionDefinitionValidations.ClearAll();
            await EditPermissionDefinitionModal.Show();
        }

        private async Task DeletePermissionDefinitionAsync(PermissionDefinitionDto input)
        {
            await PermissionDefinitionsAppService.DeleteAsync(input.Id);
            await GetPermissionDefinitionsAsync();
        }

        private async Task CreatePermissionDefinitionAsync()
        {
            try
            {
                if (await NewPermissionDefinitionValidations.ValidateAll() == false)
                {
                    return;
                }

                await PermissionDefinitionsAppService.CreateAsync(NewPermissionDefinition);
                await GetPermissionDefinitionsAsync();
                await CloseCreatePermissionDefinitionModalAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CloseEditPermissionDefinitionModalAsync()
        {
            await EditPermissionDefinitionModal.Hide();
        }

        private async Task UpdatePermissionDefinitionAsync()
        {
            try
            {
                if (await EditingPermissionDefinitionValidations.ValidateAll() == false)
                {
                    return;
                }

                await PermissionDefinitionsAppService.UpdateAsync(EditingPermissionDefinitionId, EditingPermissionDefinition);
                await GetPermissionDefinitionsAsync();
                await EditPermissionDefinitionModal.Hide();                
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private void OnSelectedCreateTabChanged(string name)
        {
            SelectedCreateTab = name;
        }

        private void OnSelectedEditTabChanged(string name)
        {
            SelectedEditTab = name;
        }

        protected virtual async Task OnGroupNameChangedAsync(string? groupName)
        {
            Filter.GroupName = groupName;
            await SearchAsync();
        }
        protected virtual async Task OnNameChangedAsync(string? name)
        {
            Filter.Name = name;
            await SearchAsync();
        }
        protected virtual async Task OnParentNameChangedAsync(string? parentName)
        {
            Filter.ParentName = parentName;
            await SearchAsync();
        }
        protected virtual async Task OnDisplayNameChangedAsync(string? displayName)
        {
            Filter.DisplayName = displayName;
            await SearchAsync();
        }
        protected virtual async Task OnIsEnabledChangedAsync(bool? isEnabled)
        {
            Filter.IsEnabled = isEnabled;
            await SearchAsync();
        }
        





    }
}
