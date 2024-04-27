"use strict";

var BaseDataGrid = function () {
    var grid;

    var CreateDataGridFunction = function (formData) {
        grid = new DatatableApp();
        var tableName = DataPageList.TableName();

        var table = $('#' + tableName);

        var urlId = '';
        var operation = '';
        var AttUpload = false;

        if (formData != "undefined" && formData != null) {
            if (formData.urlId === "undefined" || formData.urlId == null)
                urlId = '';
            else
                urlId = formData.urlId;

            if (formData.operation === "undefined" || formData.operation == null)
                operation = '';
            else
                operation = formData.operation;

            if (formData.AttUpload === "undefined" || formData.AttUpload == null)
                AttUpload = false;
            else
                AttUpload = formData.AttUpload;
        }
        
        grid.init({
            src: $("#" + tableName),
            onSuccess: function (e) {
            },
            responsive: true,
            onError: function (e) {
                console.log(e);
            },
            loadingMessage: 'Loading...',
            dataTable: {
                "proccessing": true,
                "serverSide": true,
                "lengthMenu": [
                    [10, 20, 50, 100, 150, 200, 500],
                    [10, 20, 50, 100, 150, 200, 500]
                ],
                "pageLength": 10,
                "ajax": {
                    "url": DataPageList.TableListAction(urlId, operation),
                    "timeout": 60000,
                },
            }
        });

        $('#statusOperationButton').on('click', function (e) {
            e.preventDefault();

            var action = $(".table-group-action-input");
            if (action.val() != "" && grid.getSelectedRowsCount() > 0) {
                grid.setAjaxParam("customActionType", "group_action");
                grid.setAjaxParam("customActionName", action.val());

                var formName = DataPageList.FormListName();
                var form = $("#" + formName);
                var rows_selected = grid.getSelectedRows();

                var urlAction = '';
                if (action.val() == 'active') {
                    urlAction = DataPageAction.FormActiveAction();
                }
                else if (action.val() == 'inactive') {
                    urlAction = DataPageAction.FormInactiveAction();
                }
                else if (action.val() == 'delete') {
                    urlAction = DataPageAction.FormDeleteAction();
                }
                else if (action.val() == 'up') {
                    urlAction = DataPageAction.FormUpAction();
                }
                else if (action.val() == 'down') {
                    urlAction = DataPageAction.FormDownAction();
                }
                else if (action.val() == 'archive') {
                    urlAction = DataPageAction.FormArchiveAction();
                }
                else if (action.val() == 'first') {
                    urlAction = DataPageAction.FormFirstAction();
                }
                else if (action.val() == 'last') {
                    urlAction = DataPageAction.FormLastAction();
                }

                if (typeof action.val() === "undefined" || action.val() == null || urlAction == '') {
                    BaseUtilities.ShowMessageErrorHtmlApplication('No action selected', true); return;
                }

                if (action.val() == 'up' || action.val() == 'down') {
                    if (grid.getSelectedRowsCount() > 1) {
                        BaseUtilities.ShowMessageErrorHtmlApplication('Only one record can be selected for this action', true);
                        return;
                    }
                }

                var formData = {
                    FormName: DataPageList.FormListName(),
                    FormAction: urlAction,
                    TimeoutValueAjax: DataPageBase.TimeoutValueAjax(),
                    SuccessMessageTitle: DataPageList.SuccessMessageGeneralTitle(),
                    ErrorMessageTitle: DataPageList.ErrorMessageGeneralTitle(),

                    SuccessResultFunction: function (messageText, response, messageTextBasic) {
                        BaseUtilities.ShowMessageSuccessHtmlApplication(messageText, true);

                        grid.getDataTable().ajax.reload();
                        $('input[name="GuidList\[\]"]', form).remove();
                    },
                    ErrorResultFunction: function (messageText, messageTextBasic) {
                        BaseUtilities.ShowMessageErrorHtmlApplication(messageText, true);

                        grid.getDataTable().ajax.reload();
                        $('input[name="GuidList\[\]"]', form).remove();
                    },

                };

                if (action.val() == 'delete') {
                    $('#formDialogConfirmDeleteRecords').modal('show');
                    $('#formDialogConfirmDeleteRecordsBtn').off('click');
                    $('#formDialogConfirmDeleteRecordsBtn').click(function (event) {
                        $('#formDialogConfirmDeleteRecords').modal('hide');

                        $.each(rows_selected, function (index, rowId) {
                            $(form).append(
                                $('<input>')
                                    .attr('type', 'hidden')
                                    .attr('name', 'GuidList[]')
                                    .val(rowId)
                            );
                        });
                        BaseRequest.SendPostDataWithLoader(formData);
                    });
                }
                else {
                    $.each(rows_selected, function (index, rowId) {
                        $(form).append(
                            $('<input>')
                                .attr('type', 'hidden')
                                .attr('name', 'GuidList[]')
                                .val(rowId)
                        );
                    });
                    BaseRequest.SendPostDataWithLoader(formData);
                }

                e.preventDefault();
            } else if (action.val() == "") {
                BaseUtilities.ShowMessageErrorHtmlApplication('No action selected', true);
            } else if (grid.getSelectedRowsCount() === 0) {
                BaseUtilities.ShowMessageErrorHtmlApplication('No record selected', true);
            }
        });



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

                        grid.getDataTable().ajax.reload();
                        $('input[name="GuidList\[\]"]', form).remove();
                    },
                    ErrorResultFunction: function (messageText, messageTextBasic) {
                        BaseUtilities.ShowMessageErrorHtmlApplication(messageText, true);

                        grid.getDataTable().ajax.reload();
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
        
        $('#searchButton').on('click', function (e) {
            e.preventDefault();
            grid.submitFilter();
        });

        $('#resetButton').on('click', function (e) {
            e.preventDefault();
            grid.resetFilter();
        });

        $(document).keypress(function (event) {
            var keycode = (event.keyCode ? event.keyCode : event.which);
            if (keycode == '13') {
                event.preventDefault();
                grid.submitFilter();
            }
        });
        
    };

    var HandleGetGridFunction = function () {
        return grid;
    };

    return {
        //main function to initiate the module
        CreateDataGridPage: function (formData) {
            CreateDataGridFunction(formData);
        },
        HandleGetGrid: function () {
            return HandleGetGridFunction();
        },
    };
}();