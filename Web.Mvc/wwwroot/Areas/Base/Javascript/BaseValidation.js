var BaseValidation = function () {
    var CreateValidationFormFunction = function (formData) {
        var FormName = formData.FormName;
        var FormAction = formData.FormAction;

        var FormUpdate = formData.FormUpdate;
        var TimeoutValueAjax = formData.TimeoutValueAjax;
        var FormValidation = formData.FormValidation;

        var SuccessMessageTitle = formData.SuccessMessageTitle;
        var ErrorMessageTitle = formData.ErrorMessageTitle;

        var SuccessMessageAnotherTitle = formData.SuccessMessageAnotherTitle;
        var ErrorMessageAnotherTitle = formData.ErrorMessageAnotherTitle;

        var SuccessResultFunction = formData.SuccessResultFunction;
        var ErrorResultFunction = formData.ErrorResultFunction;

        var FormGuid = formData.FormGuid;

        var formValidation = $("#" + FormName);

        var ErrorMessageDefinition = 'errorMessage';
        var SuccessMessageDefinition = 'successMessage';

        if (typeof formData.ErrorMessageDefinition !== "undefined" && formData.ErrorMessageDefinition !== null) {
            ErrorMessageDefinition = formData.ErrorMessageDefinition;
        }

        if (typeof formData.SuccessMessageDefinition !== "undefined" && formData.SuccessMessageDefinition !== null) {
            SuccessMessageDefinition = formData.SuccessMessageDefinition;
        }

        var FormFiles = '';
        if (typeof formData.FormFiles !== "undefined" && formData.FormFiles !== null) {
            FormFiles = formData.FormFiles;
        }

        var IsGuidSent = true;
        if (typeof formData.IsGuidSent !== "undefined" && formData.IsGuidSent !== null) {
            IsGuidSent = formData.IsGuidSent;
        }

        try {
            formValidation.validate({
                errorElement: 'span', //default input error message container
                errorClass: 'help-block', // default input error message class
                focusInvalid: true, // do not focus the last invalid input
                ignore: "",
                invalidHandler: function (event, validator) { //display error alert on form submit
                },

                onfocusout: function (element) {
                    $(element).valid();
                },
                highlight: function (element) { // hightlight error inputs
                    $(element).parent().children().remove(".help-block");
                    $(element)
                        .closest('.col-lg-6').addClass('has-error'); // set error class to the control group
                },

                success: function (label) {
                    label.closest('.col-lg-6').removeClass('has-error');
                    label.remove();
                },

                errorPlacement: function (error, element) {
                    if (element.hasClass('select2-allow-clear')) {
                        error.insertAfter(element.parent());
                    }
                    else if (element.parent('.input-group').length) {
                        error.insertAfter(element.parent());
                    }
                    else {
                        error.insertAfter(element);
                    }
                },

                submitHandler: function (form) {
                    
                    var formData = 
                    {
                        FormName: FormName,
                        FormAction: FormAction,
                        FormUpdate: FormUpdate,
                        TimeoutValueAjax: TimeoutValueAjax,
                        SuccessMessageTitle: SuccessMessageTitle,
                        ErrorMessageTitle: ErrorMessageTitle,

                        SuccessMessageAnotherTitle: SuccessMessageAnotherTitle,
                        ErrorMessageAnotherTitle: ErrorMessageAnotherTitle,

                        FormValidation: FormValidation,
                        FormGuid: FormGuid,
                        FormFiles: FormFiles,

                        SuccessResultFunction: function (messageText, dataOperation, response) {
                            BaseUtilities.ShowMessageSuccessHtmlApplication(messageText, true);
                            $('.form-group').removeClass('has-error')
                                .removeClass('has-success');
                            $('.text-danger').remove();
                            let IsCreate = false;
                            if (typeof dataOperation !== "undefined" && dataOperation !== null) {
                                var regex = /[a-f0-9]{8}(?:-[a-f0-9]{4}){3}-[a-f0-9]{12}/i;
                                var match = regex.exec(dataOperation);
                                if (match != null && IsGuidSent) {
                                    FormGuid = dataOperation;
                                    $("#Id").val(FormGuid);
                                }
                            }

                            if (typeof response !== "undefined" && response !== null && typeof response.operationType !== "undefined"
                                && response.operationType !== null && response.operationType === 'Create') {
                                IsCreate = true;
                            }
                            
                            if (typeof SuccessResultFunction !== "undefined" && SuccessResultFunction !== null) {
                                if (typeof form !== "undefined" && form !== null && typeof form.name !== "undefined" && form.name !== null && form.name === 'BackToListPage') {
                                    if (IsCreate == true) {
                                        SuccessResultFunction('BackToListPage', SuccessMessageTitle);
                                    }
                                    else {
                                        SuccessResultFunction('BackToListPage', SuccessMessageAnotherTitle);
                                    }
                                }
                                else {
                                    if (IsCreate == true) {
                                        SuccessResultFunction(SuccessMessageTitle, dataOperation, response);
                                    }
                                    else {
                                        SuccessResultFunction(SuccessMessageAnotherTitle, dataOperation, response);
                                    }
                                }
                            }
                        },
                        ErrorResultFunction: function (messageText, dataOperation, response) {
                            BaseUtilities.ShowMessageErrorHtmlApplication(messageText, true);
                        }
                    };

                    BaseRequest.SendPostDataWithLoader(formData);
                }
            });
        } catch (err) {
            console.log(err);
        }

        $("#" + FormName + ' input').keypress(function (e) {
            if (e.which == 13) {
                var formValidation = $("#" + FormName);

                if (formValidation.validate().form()) {
                    formValidation.submit();
                }
                return false;
            }
        });

        BaseValidationRoles.CreateValidationRoles(FormValidation, FormName);
    };

    return {
        //main function to initiate the module
        CreateValidationForm: function (formData) {
            CreateValidationFormFunction(formData);
        }
    };
}();