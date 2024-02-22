var BaseUtilities = function () {
    var ErrorMessageDefinition = 'errorMessage';
    var SuccessMessageDefinition = 'successMessage';

    var unloadingApplication = function () {
        $("#loader").hide();
    };

    var showLoaderApplication = function () {
        $("#loader").show();
        return false;
    };

    var showRedirectApplication = function () {
        toastr.success('Redirect...');
    
        return false;
    };

    function generateUniqueGuid() {
        var d = new Date().getTime();

        var guid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = (d + Math.random() * 16) % 16 | 0;
            d = Math.floor(d / 16);

            return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
        });

        return guid;
    }

    var hideLoaderApplication = function () {
        setTimeout("BaseUtilities.UnloadingLoaderApplication()", 1000);
    };

    var showErrorApplication = function (status, statusText, error) {
        var formDialogResponseApplication = $("#formDialogResponseApplication");
        var formDialogResponseApplicationHeader = $("#formDialogResponseApplicationHeader");
        var formDialogResponseApplicationText = $("#formDialogResponseApplicationText");

        var headerText = 'Informacje';

        if (status === "timeout") {
            var informationText = 'Zbyt długi czas oczekiwania na odpowiedź';
            formDialogResponseApplicationHeader.html(headerText);
            formDialogResponseApplicationText.html(informationText);
            formDialogResponseApplication.modal('show');
        }
        else if (status === 500) {
            var informationText = statusText + ' (' + status + ')';
            formDialogResponseApplicationHeader.html(headerText);
            formDialogResponseApplicationText.html(informationText);
            formDialogResponseApplication.modal('show');
        }
        else if (status === 200) {
            if (error === "parsererror") {
                var informationText = 'Wystąpił błąd aplikacji, nie udało się przetworzyć żądania';
                formDialogResponseApplicationHeader.html(headerText);
                formDialogResponseApplicationText.html(informationText);
                formDialogResponseApplication.modal('show');
            }
            else {
                formDialogResponseApplicationHeader.html(headerText);
                formDialogResponseApplicationText.html(statusText);
                formDialogResponseApplication.modal('show');
            }
        }
        else if (status === 404) {
            if (error === "error") {
                var informationText = 'Nie odnaleziono adresu strony żądania';
                formDialogResponseApplicationHeader.html(headerText);
                formDialogResponseApplicationText.html(informationText);
                formDialogResponseApplication.modal('show');
            }
        }
        else {
            var informationText = 'Wystąpił błąd aplikacji';
            formDialogResponseApplicationHeader.html(headerText);
            formDialogResponseApplicationText.html(informationText);
            formDialogResponseApplication.modal('show');
        }
    };

    var showToastrApplication = function (status, text) {
        if (status === "error") {
            toastr.options = {
                "closeButton": true,
                "debug": false,
                "newestOnTop": true,
                "progressBar": true,
                "positionClass": "toast-bottom-right",
                "preventDuplicates": true,
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "5000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            };
            toastr.error(text);
        }
        else {
            toastr.options = {
                "closeButton": true,
                "debug": false,
                "newestOnTop": true,
                "progressBar": true,
                "positionClass": "toast-bottom-right",
                "preventDuplicates": true,
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "5000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            };
            toastr.success(text);
        }
    };

    var showMessageApplication = function (informationText, actionModal) {
        var formDialogResponseApplication = $("#formDialogResponseApplication");
        var formDialogResponseApplicationHeader = $("#formDialogResponseApplicationHeader");
        var formDialogResponseApplicationText = $("#formDialogResponseApplicationText");

        var headerText = 'Informacje';
        formDialogResponseApplicationHeader.html(headerText);
        formDialogResponseApplicationText.html(informationText);
        formDialogResponseApplication.one('shown.bs.modal', function (e) {
        }).one('hidden.bs.modal', function (e) {
            if (typeof actionModal !== 'undefined' && actionModal !== "") {
                $(location).attr('href', actionModal);
            }
        }).modal("show");
    };

    var showMessageSuccessHtmlApplication = function (messageText, dateNowFlag = false) {
        
        $("#messageApplication").empty();
        $("#messageApplication").append('<div id="successMessage" class="alert alert-success alert-dismissible display-none" role="alert">'
            + '<div class="alert-icon">'
            + '<i class="flaticon-warning"></i>'
            + '</div>'
            + '<span class="alert-text">Formularz posiada błędy. Sprawdź wypełnione pola.</span>'
            + '<div class="alert-close">'
            + '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>'
            + '</div>'
            + '</div>');

        BaseUtilities.ShowToastrApplication("info", messageText);

        if (dateNowFlag) {
            var dateNow = new Date().toLocaleString();
            messageText = messageText + ' : ' + dateNow;
        }

        var successMessage = $('#' + SuccessMessageDefinition);
        successMessage.css("display", "flex");
        successMessage.removeClass("display-none");
        $('#' + SuccessMessageDefinition + ' span').first().text(messageText);

        successMessage.show();
    };

    var showMessageErrorHtmlApplication = function (messageText, dateNowFlag = false) {
        $("#messageApplication").empty();
        $("#messageApplication").append('<div id="errorMessage" class="alert alert-danger alert-dismissible display-none" role="alert">'
            + '<div class="alert-icon">'
            + '<i class="flaticon-warning"></i>'
            + '</div>'
            + '<span class="alert-text">Formularz posiada błędy. Sprawdź wypełnione pola.</span>'
            + '<div class="alert-close">'
            + '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>'
            + '</div>'
            + '</div>');

        BaseUtilities.ShowToastrApplication("error", messageText);

        if (dateNowFlag) {
            var dateNow = new Date().toLocaleString();
            messageText = messageText + ' : ' + dateNow;
        }

        var errorMessage = $('#' + ErrorMessageDefinition);
        errorMessage.css("display", "flex");
        errorMessage.removeClass("display-none");
        $('#' + ErrorMessageDefinition + ' span').first().text(messageText);

        errorMessage.show();
    };

    var hideMessageErrorHtmlApplication = function () {
        var errorMessage = $('#' + ErrorMessageDefinition);

        errorMessage.hide();
    };

    var modificationSetApplication = function (entity) {
        if (entity == null || typeof entity === 'undefined')
            return;
    };

    var checkParameter = function (parameter) {
        if (typeof parameter !== "undefined" && parameter !== null && parameter !== '') {
            if (parameter == "00000000-0000-0000-0000-000000000000")
                return '';

            return parameter;
        }
        return '';
    }

    var doesStringEndWith = function (myString, stringCheck) {
        var foundIt = (myString.lastIndexOf(stringCheck) === myString.length - stringCheck.length) > 0;
        return foundIt;
    }

    var emptyMessageHtmlApplication = function (messageText, dateNowFlag = false) {
        $("#messageApplication").empty();
    }

    return {
        ShowLoaderApplication: function () {
            showLoaderApplication();
        },
        ShowRedirectApplication: function () {
            showRedirectApplication();
        },
        HideLoaderApplication: function () {
            hideLoaderApplication();
        },
        UnloadingLoaderApplication: function () {
            unloadingApplication();
        },
        ShowErrorApplication: function (status, statusText, error) {
            showErrorApplication(status, statusText, error);
        },
        HideErrorApplication: function () {
            hideMessageErrorHtmlApplication();
        },
        ShowMessageApplication: function (informationText, actionModal) {
            showMessageApplication(informationText, actionModal);
        },
        ShowMessageSuccessHtmlApplication: function (messageText, dateNowFlag) {
            showMessageSuccessHtmlApplication(messageText, dateNowFlag);
        },
        ShowMessageErrorHtmlApplication: function (messageText, dateNowFlag) {
            showMessageErrorHtmlApplication(messageText, dateNowFlag);
        },
        GenerateUniqueGuid: function () {
            return generateUniqueGuid();
        },
        ModificationSetApplication: function (entity) {
            modificationSetApplication(entity);
        },
        CheckParameter: function (parameter) {
            return checkParameter(parameter);
        },
        DoesStringEndWith: function (myString, stringCheck) {
            return doesStringEndWith(myString, stringCheck);
        },
        EmptyMessageHtmlApplication: function () {
            emptyMessageHtmlApplication();
        },
        ShowToastrApplication: function (status, text) {
            showToastrApplication(status, text);
        },
    };
}();