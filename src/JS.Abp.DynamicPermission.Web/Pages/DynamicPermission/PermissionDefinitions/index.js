$(function () {
    var l = abp.localization.getResource("DynamicPermission");
	
	var permissionDefinitionService = window.jS.abp.dynamicPermission.permissionDefinitions.permissionDefinition;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "DynamicPermission/PermissionDefinitions/CreateModal",
        scriptUrl: abp.appPath + "Pages/DynamicPermission/PermissionDefinitions/createModal.js",
        modalClass: "permissionDefinitionCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "DynamicPermission/PermissionDefinitions/EditModal",
        scriptUrl: abp.appPath + "Pages/DynamicPermission/PermissionDefinitions/editModal.js",
        modalClass: "permissionDefinitionEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            groupName: $("#GroupNameFilter").val(),
			name: $("#NameFilter").val(),
			parentName: $("#ParentNameFilter").val(),
			displayName: $("#DisplayNameFilter").val(),
            isEnabled: (function () {
                var value = $("#IsEnabledFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })()
        };
    };
    
    
    
    var dataTableColumns = [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('DynamicPermission.PermissionDefinitions.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('DynamicPermission.PermissionDefinitions.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    permissionDefinitionService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reloadEx();;
                                        });
                                }
                            }
                        ]
                },
                width: "1rem"
            },
			{ data: "groupName" },
            { data: "parentName" },
			{ data: "name" },
			{ data: "displayName" },
            {
                data: "isEnabled",
                render: function (isEnabled) {
                    return isEnabled ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                }
            }        
    ];
    
    

    var dataTable = $("#PermissionDefinitionsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(permissionDefinitionService.getList, getFilter),
        columnDefs: dataTableColumns
    }));
    
    

    createModal.onResult(function () {
        dataTable.ajax.reloadEx();;
    });

    editModal.onResult(function () {
        dataTable.ajax.reloadEx();;
    });

    $("#NewPermissionDefinitionButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reloadEx();;
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        permissionDefinitionService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/dynamic-permission/permission-definitions/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'groupName', value: input.groupName }, 
                            { name: 'name', value: input.name }, 
                            { name: 'parentName', value: input.parentName }, 
                            { name: 'displayName', value: input.displayName }, 
                            { name: 'isEnabled', value: input.isEnabled }
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
