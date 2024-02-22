"use strict";

var BaseDataGrid = function () {

    var CreateDataGridFunction = function () {
        var tableName = DataPageList.TableName();
        
        var grid = new DataTable('#' + tableName, {
            "proccessing": true,
            "serverSide": true,
            "lengthMenu": [
                [10, 20, 50, 100, 150, 200, 500],
                [10, 20, 50, 100, 150, 200, 500]
            ],
            "pageLength": 10,
            "autoWidth": false,
            "responsive": true,
            "ajax": {
                "url": DataPageList.TableListAction(),
                "timeout": 60000,
                "type": 'POST',
                "data": function (data) { // add request parameters before submit

                    let token = $('input[name="__RequestVerificationToken"]').val();

                    data["__RequestVerificationToken"] = token;

                },
            },
            
        });


        var table = $('#' + tableName);
        table.on('click', '.delete', function (e) {
            e.preventDefault();

            var removeElement = $(this);
            
            $('#formDialogConfirmDeleteRecords').modal('show');
            $('#formDialogConfirmDeleteRecordsBtn').off('click');
            $('#formDialogConfirmDeleteRecordsBtn').click(function (e) {
                $('#formDialogConfirmDeleteRecords').modal('hide');

                var urlAction = DataPageAction.FormDeleteAction();
                var formName = DataPageList.FormListName();
                var form = $("#" + formName);
                var formData = {
                    FormName: DataPageList.FormListName(),
                    FormAction: urlAction,
                    TimeoutValueAjax: DataPageBase.TimeoutValueAjax(),
                    SuccessMessageTitle: DataPageList.SuccessMessageGeneralTitle(),
                    ErrorMessageTitle: DataPageList.ErrorMessageGeneralTitle(),

                    SuccessResultFunction: function (messageText, response, messageTextBasic) {
                        BaseUtilities.ShowMessageSuccessHtmlApplication(messageText, true);

                        grid.ajax.reload();
                        $('input[name="GuidList\[\]"]', form).remove();
                    },
                    ErrorResultFunction: function (messageText, messageTextBasic) {
                        BaseUtilities.ShowMessageErrorHtmlApplication(messageText, true);

                        grid.ajax.reload();
                        $('input[name="GuidList\[\]"]', form).remove();
                    },

                };

                var IdElement = removeElement.attr('tagOperation');

                $(form).append(
                    $('<input>')
                        .attr('type', 'hidden')
                        .attr('name', 'GuidList[]')
                        .val(IdElement)
                );
                BaseRequest.SendPostDataWithLoader(formData);
            });
        });
                
    };

    return {
        //main function to initiate the module
        CreateDataGridPage: function () {
            CreateDataGridFunction();
        },
    };
}();