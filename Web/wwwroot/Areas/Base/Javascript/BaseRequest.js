"use strict";
var BaseRequest = function () {
    var SendPostDataFunction = function (formData) {
        var FormName = formData.FormName;
        var FormAction = formData.FormAction;

        var FormUpdate = formData.FormUpdate;
        var TimeoutValueAjax = formData.TimeoutValueAjax;

        var SuccessMessageTitle = formData.SuccessMessageTitle;
        var ErrorMessageTitle = formData.ErrorMessageTitle;

        var SuccessResultFunction = formData.SuccessResultFunction;
        var ErrorResultFunction = formData.ErrorResultFunction;


        var dataFormData = new FormData();

        var form_data = $("#" + FormName).serializeArray();
        $.each(form_data, function (key, input) {
            dataFormData.append(input.name, input.value);
        });


        try {
            $.ajax({
                url: FormAction,
                type: "POST",
                data: dataFormData,
                contentType: false,
                processData: false,
                success: function (response, dataOperation) {
                    $.each(response.messages, function (key, value) {
                        if (key == 'FluentValidationErrorCustom') {
                            ErrorMessageTitle = value;
                        }
                    });
                    
                    if (response.success === true) {
                        var form = $("#" + FormName).get(0);
                        if (typeof form !== "undefined" && form !== null && typeof form.name !== "undefined" && form.name !== null && form.name === 'BackToListPage') {
                            SuccessResultFunction('BackToListPage', SuccessMessageTitle, response);
                        }
                        else {
                            SuccessResultFunction(SuccessMessageTitle, response);
                        }
                    }
                    else {
                        ErrorResultFunction(ErrorMessageTitle, response);
                    }
                },
                complete: function () {
                },
                timeout: TimeoutValueAjax,
                error: function (request, error) {
                    console.log(error);
                    console.log(request.responseText);

                    var statusText = request.statusText;
                    var status = request.status;
                    BaseUtilities.ShowErrorApplication(status, statusText, error);
                    ErrorResultFunction(ErrorMessageTitle);

                    if (request.status == 405) {
                        window.setTimeout(function () {
                            location.reload();
                        }, 3000);
                    }
                }
            });
        }
        catch (err) {
            console.log(err);
        }
    };

    var SendPostDataWithLoaderFunction = function (formData) {
        var FormName = formData.FormName;
        var FormAction = formData.FormAction;

        var FormUpdate = formData.FormUpdate;
        var TimeoutValueAjax = formData.TimeoutValueAjax;

        var SuccessMessageTitle = formData.SuccessMessageTitle;
        var SuccessMessageIsEnabled = formData.SuccessMessageIsEnabled;

        var ErrorMessageTitle = formData.ErrorMessageTitle;
        var ErrorMessageIsEnabled = formData.ErrorMessageIsEnabled;

        var SuccessMessageAnotherTitle = formData.SuccessMessageAnotherTitle;
        var SuccessMessageAnotherIsEnabled = formData.SuccessMessageAnotherIsEnabled;

        var SuccessResultFunction = formData.SuccessResultFunction;
        var ErrorResultFunction = formData.ErrorResultFunction;
        var FormGuid = formData.FormGuid;

        var dataFormData = new FormData();

        var form_data = $("#" + FormName).serializeArray();
        $.each(form_data, function (key, input) {
            dataFormData.append(input.name, input.value);
        });

        if (typeof FormGuid !== "undefined" && FormGuid !== null && FormGuid !== '' && FormGuid !== '00000000-0000-0000-0000-000000000000') {
            dataFormData.append('Id', FormGuid);
            FormAction = FormUpdate;
        }

        try {
            BaseUtilities.ShowLoaderApplication();

            $.ajax({
                url: FormAction,
                type: "POST",
                data: dataFormData,
                contentType: false,
                processData: false,
                success: function (response) {

                    var response = JSON.parse(JSON.stringify(response));
                    var guidRecord = '';
    
                    if (response.success === true) {
                        
                        if (typeof response.operationType !== "undefined"
                            && response.operationType !== null
                            && response.operationType === 'Update'
                            && typeof SuccessMessageAnotherTitle !== "undefined") {

                            guidRecord = response.guid;

                            SuccessResultFunction(SuccessMessageAnotherTitle, guidRecord);
                            BaseUtilities.ModificationSetApplication(response.entity);
                            if (SuccessMessageAnotherIsEnabled == true) {
                                BaseUtilities.ShowMessageApplication(SuccessMessageAnotherTitle);
                            }
                        }
                        else if (typeof response.guid !== "undefined" && response.guid !== null) {
                            guidRecord = response.guid;
                            
                            SuccessResultFunction(SuccessMessageTitle, guidRecord, response);
                            BaseUtilities.ModificationSetApplication(response.entity);
                            if (SuccessMessageIsEnabled == true) {
                                BaseUtilities.ShowMessageApplication(SuccessMessageTitle);
                            }
                        }
                        else {
                            if (typeof response.operationResult !== "undefined" && response.operationResult !== null) {
                                SuccessResultFunction(SuccessMessageTitle, response.operationResult);
                            }
                            else {
                                SuccessResultFunction(SuccessMessageTitle, response);
                            }

                            if (SuccessMessageIsEnabled == true) {
                                BaseUtilities.ShowMessageApplication(SuccessMessageTitle);
                            }
                        }
                        
                    }
                    else {
                        $("span").remove(".help-block");

                        $.each(response.messages, function (key, value) {
                            if (key == 'FluentValidationErrorCustom') {
                                ErrorMessageTitle = value;
                                ErrorMessageTitle = value;
                            }
                            
                            var element = $('#' + key);
                            element.closest('div.col-lg-6')
                                .removeClass('has-error')
                                .addClass(value.length > 0 ? 'has-error' : 'has-success')
                                .find('.text-danger')
                                .remove();

                            if (element.next('.help-inline').length == 0) {
                                if (element.hasClass('select2-allow-clear')) {
                                    element.parent().after(value);
                                }
                                else {
                                    if (value.indexOf('<span') >= 0) {
                                        if (element.is("select") || element.attr('data-type') == 'textbox-with-button') {
                                            element.parent().after(value);
                                        }
                                        else {
                                            element.after(value);
                                        }
                                    }
                                    else
                                        element.parent().after("<span id=\"" + key + "-error\" class=\"help-block\">" + value + "</span>");
                                }
                            }
                            else {
                                element.next('.help-inline').after(value);
                            }
                        });

                        if (typeof response.guid !== "undefined" && response.guid !== null) {
                            guidRecord = response.guid;

                            ErrorResultFunction(ErrorMessageTitle, guidRecord, response);
                        }
                        else if (typeof response !== "undefined" && response !== null) {
                            ErrorResultFunction(ErrorMessageTitle, null, response);
                        }
                        else {
                            ErrorResultFunction(ErrorMessageTitle, ErrorMessageTitle);
                        }
                        if (ErrorMessageIsEnabled == true) {
                            BaseUtilities.ShowMessageApplication(ErrorMessageTitle);
                        }
                    }
                },
                complete: function () {
                    BaseUtilities.HideLoaderApplication();
                },
                timeout: TimeoutValueAjax,
                error: function (request, error, ex) {
                    console.log(error);
                    console.log(request.responseText);

                    var statusText = request.statusText;
                    var status = request.status;
                    BaseUtilities.ShowErrorApplication(status, statusText, error);
                    BaseUtilities.HideLoaderApplication();

                    if (request.status == 405) {
                        window.setTimeout(function () {
                            location.reload();
                        }, 3000);
                    }
                }
            });
        }
        catch (err) {
            console.log(err);
            BaseUtilities.UnloadingLoaderApplication();
        }
    };

    return {
        //main function to initiate the module
        SendPostData: function (formData) {
            SendPostDataFunction(formData);
        },
        SendPostDataWithLoader: function (formData) {
            SendPostDataWithLoaderFunction(formData);
        }
    };
}();