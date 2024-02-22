"use strict";
var BaseUpdate = function () {
    var UpdateFunction = function (formDataHandle) {
        $('#SaveButtonName,#SaveAndBackButtonName').click(function (event) {
            event.preventDefault();
            var formValidation = $("#" + DataPageForms.FormName());
            if (formValidation.validate().form()) {
                if ($(this).hasClass('BackToListPage')) {
                    formValidation.attr("name", 'BackToListPage');
                }
                formValidation.submit();
            }
            else {
                var messageText = DataPageBase.ErrorMessageValidationTitle();
                BaseUtilities.ShowMessageErrorHtmlApplication(messageText, true);
            }
            return false;
        });

        function redirectToUrl() {
            var urlRedirect = UpdateData.MainUrl();

            BaseUtilities.ShowRedirectApplication();
            $(location).attr('href', urlRedirect);
        }

        function backToList(refresh) {
            if (refresh) {
                window.setTimeout(function () {
                    redirectToUrl();
                }, 100);
            } else {
                redirectToUrl();
            }
        }

        $('#BackButtonName').click(function (event) {
            event.preventDefault();
            backToList();
        });

        var formData = {
            FormName: DataPageForms.FormName(),
            FormAction: DataPageForms.FormAction(),
            FormUpdate: DataPageForms.FormUpdate(),
            TimeoutValueAjax: DataPageBase.TimeoutValueAjax(),
            SuccessMessageTitle: DataPageBase.SuccessMessageTitle(),
            ErrorMessageTitle: DataPageBase.ErrorMessageAnotherTitle(),
            SuccessMessageAnotherTitle: DataPageBase.SuccessMessageAnotherTitle(),
            ErrorMessageAnotherTitle: DataPageBase.ErrorMessageAnotherTitle(),
            
            FormGuid: DataPageForms.FormGuid(),
            SuccessResultFunction: function (operation, message) {
                if (typeof operation !== "undefined" && operation !== null
                    && operation === 'BackToListPage') {
                    backToList(true);
                }
                if (typeof formDataHandle !== "undefined" && formDataHandle !== null) {
                    if (typeof operation !== "undefined" && operation !== null) {
                        formDataHandle.SuccessResultFunction(true, message);
                    }
                    else {
                        formDataHandle.SuccessResultFunction(true, operation);
                    }
                }
            },
            ErrorResultFunction: function (messageText) {
                BaseUtilities.ShowMessageErrorHtmlApplication(messageText, true);
            },
        };

        BaseValidation.CreateValidationForm(formData);

    };

    return {
        Update: function (formData) {
            UpdateFunction(formData);
        },
    };
}();