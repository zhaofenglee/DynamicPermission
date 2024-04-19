$(function () {
    var l = abp.localization.getResource("DynamicPermission");
	
	var userPermissionService = window.jS.abp.dynamicPermission.userPermissions.userPermission;
	
    

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            groupName: $("#GroupNameFilter").val(),
            permissionName: $("#PermissionNameFilter").val(),
            userName: $("#UserNameFilter").val(),
            isActive: (function () {
                var value = $("#IsActiveFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
            isGranted: (function () {
                var value = $("#IsGrantedFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })()
        };
    };
    
    
    
    var dataTableColumns = [
        { data: "userName" },
		{ data: "surname" },
        {
            data: "isActive",
            render: function (isEnabled) {
                return isEnabled ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
            }
        },
        { data: "email" },
        { data: "phoneNumber" },
        { data: "groupName" },
        { data: "permissionName" },
        { data: "permissionDisplayName" },
        {
            data: "isGranted",
            render: function (isEnabled) {
                return isEnabled ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
            }
        },
    ];
    
    

    var dataTable = $("#UserPermissionsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(userPermissionService.getList, getFilter),
        columnDefs: dataTableColumns
    }));
    
    

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reloadEx();;
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        userPermissionService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/dynamic-permission/user-permissions/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText },
                            { name: 'groupName', value: input.groupName },
                            { name: 'permissionName', value: input.permissionName },
                            { name: 'userName', value: input.userName },
                            { name: 'isActive', value: input.isActive },
                            { name: 'isGranted', value: input.isGranted }
                            ]);
                            
                    var downloadWindow = window.open(url, '_blank');
                    downloadWindow.focus();
            }
        )
    });


    $('#AdvancedFilterSectionToggler').on('click', function (e) {
        $('#AdvancedFilterSection').toggle();
    });

    $('#AdvancedFilterSection').on('keypress', function (e) {
        if (e.which === 13) {
            dataTable.ajax.reloadEx();;
        }
    });

    $('#AdvancedFilterSection select').change(function() {
        dataTable.ajax.reloadEx();;
    });
    
});
