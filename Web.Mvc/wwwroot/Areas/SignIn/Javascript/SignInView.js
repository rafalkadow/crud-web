"use strict";
var View = function () {
    $('#SignInButton').click(function (event) {
        event.preventDefault();
        var formValidation = $("#" + DataPageForms.FormName());
        if (formValidation.validate().form()) {
            formValidation.submit(); //form validation success, call ajax form submit
        }
        return false;
    });

    $(document).keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            event.preventDefault();
            $('#SignInButton').click();
        }
    });
    
    var handle = function () {
        var formData = {
            FormName: DataPageForms.FormName(),
            FormAction: DataPageForms.FormAction(),
            TimeoutValueAjax: DataPageBase.TimeoutValueAjax(),
            SuccessMessageTitle: DataPageBase.SuccessMessageTitle(),
            ErrorMessageTitle: DataPageBase.ErrorMessageTitle(),
            SuccessMessageAnotherTitle: DataPageBase.SuccessMessageAnotherTitle(),
            ErrorMessageAnotherTitle: DataPageBase.ErrorMessageAnotherTitle(),
            
            FormGuid: DataPageForms.FormGuid(),
            SuccessResultFunction: function (message, guidRecord, response) {
                
                var urlTable = DataPageBase.StartPage();
                var url = urlTable.AppUrl;
                var returnUrl = urlTable.ReturnUrl;
                
                if (typeof response !== "undefined" && response !== null && response.message !== null) {
                    if (returnUrl == '' || returnUrl == '/' || returnUrl.indexOf('sign-out') !== -1) {
                        if (!BaseUtilities.DoesStringEndWith(url, "/")) {
                            url = url + "/" + response.message;
                        }
                        else {
                            url = url + response.message;
                        }
                    }
                    else {
                        url = url + returnUrl;
                    }
                }
                else {
                    url = url + returnUrl;
                }
                $(location).attr('href', url);
            },

            ErrorResultFunction: function (message, guidRecord, response) {
                
            }

        };

        BaseValidation.CreateValidationForm(formData);
        
        
    };

    return {
        //main function to initiate the module
        initForm: function () {
            handle();
        }
    };
}();

$(document).ready(function () {
    View.initForm();
});