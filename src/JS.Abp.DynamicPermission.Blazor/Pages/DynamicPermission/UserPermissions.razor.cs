using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.DataGrid;
using JS.Abp.DynamicPermission.UserPermissions;
using Microsoft.JSInterop;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using Volo.Abp.BlazoriseUI.Components;

namespace JS.Abp.DynamicPermission.Blazor.Pages.DynamicPermission
{
    public partial class UserPermissions
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar {get;} = new PageToolbar();
        
        protected bool ShowAdvancedFilters { get; set; }
        private IReadOnlyList<UserPermissionDto> UserPermissionList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private int TotalCount { get; set; }
        private GetUserPermissionInput Filter { get; set; }
        [NotNull]
        private IJSObjectReference? Module { get; set; }
        public UserPermissions()
        {
            Filter = new GetUserPermissionInput{
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            UserPermissionList = new List<UserPermissionDto>();
        }

        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                // import JavaScript
                Module = await JS.InvokeAsync<IJSObjectReference>("import", "./_content/JS.Abp.DynamicPermission.Blazor/Pages/DynamicPermission/UserPermissions.razor.js");
                //await Module.InvokeVoidAsync("sayhello");
            }
        }
        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:UserPermissions"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["ExportToExcel"], async () =>{ await DownloadAsExcelAsync(); }, IconName.Download);
            
            return ValueTask.CompletedTask;
        }
        

        private async Task GetUserPermissionExtrasAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;
            var result = await UserPermissionAppService.GetListAsync(Filter);
            UserPermissionList = result.Items;
            TotalCount = (int)result.TotalCount;
        }
        
        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<UserPermissionDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetUserPermissionExtrasAsync();
            await InvokeAsync(StateHasChanged);
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetUserPermissionExtrasAsync();
            await InvokeAsync(StateHasChanged);
        }

        private  async Task DownloadAsExcelAsync()
        {
            var token = (await UserPermissionAppService.GetDownloadTokenAsync()).Token;
          
            var remoteStream = await UserPermissionAppService.GetListAsExcelFileAsync(new UserPermissionExcelDownloadDto()
            { 
                DownloadToken = token,
                FilterText = Filter.FilterText,
                GroupName = Filter.GroupName,
                PermissionName              = Filter.PermissionName,
                UserName = Filter.UserName,
                IsActive = Filter.IsActive,
                IsGranted = Filter.IsGranted

            });

            Stream stream = remoteStream.GetStream();
            byte[] buffer = new byte[stream.Length];

            stream.Read(buffer, 0, buffer.Length);
            stream.Seek(0, SeekOrigin.Begin);
            var fileStream = new MemoryStream(buffer);
            var fileName = "UserPermissions.xlsx";
            using var streamRef = new DotNetStreamReference(stream: fileStream);
            {
                await Module.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
            }
            /*var remoteService = await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("DynamicPermission") ??
                      await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Default");
                      var culture = CultureInfo.CurrentUICulture.Name ?? CultureInfo.CurrentCulture.Name;
                      if(!culture.IsNullOrEmpty())
                      {
                          culture = "&culture=" + culture;
                      }
            NavigationManager.NavigateTo($"{remoteService?.BaseUrl.EnsureEndsWith('/') ?? string.Empty}api/dynamic-permission/user-permissions/as-excel-file?DownloadToken={token}&FilterText={Filter.FilterText}{culture}&GroupName={Filter.GroupName}&PermissionName={Filter.PermissionName}&UserName={Filter.UserName}&IsActive={Filter.IsActive}&IsGranted={Filter.IsGranted}", forceLoad: true);
            */
        }
        
        protected virtual async Task OnGroupNameChangedAsync(string? groupName)
        {
            Filter.GroupName = groupName;
            await SearchAsync();
        }
        protected virtual async Task OnPermissionNameChangedAsync(string? permissionName)
        {
            Filter.PermissionName = permissionName;
            await SearchAsync();
        }
        protected virtual async Task OnUserNameChangedAsync(string? userName)
        {
            Filter.UserName = userName;
            await SearchAsync();
        }
        
        protected virtual async Task OnIsActiveChangedAsync(bool? isActive)
        {
            Filter.IsActive = isActive;
            await SearchAsync();
        }
        
        protected virtual async Task OnIsGrantedChangedAsync(bool? isGranted)
        {
            Filter.IsGranted = isGranted;
            await SearchAsync();
        }

    }
}
