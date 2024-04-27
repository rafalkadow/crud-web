var BaseValidationRoles = function () {
    var rolesList = {};
    //backend
    rolesList['required'] = 'required';
    rolesList['minlength'] = 'min_length';
    rolesList['maxlength'] = 'max_length';
    rolesList['number'] = 'number';
    rolesList['email'] = 'valid_email';
    rolesList['equalTo'] = 'matches'; //matches[rpassword];equalTo: "#password"
    rolesList['decimal'] = 'decimal';

    var messageList = {};
    // backend | javascript
    messageList['required'] = 'required';
    messageList['minlength'] = 'minlength';
    messageList['maxlength'] = 'maxlength';
    messageList['number'] = 'number';
    messageList['email'] = 'email';
    messageList['matches'] = 'equalTo'; //matches[rpassword];equalTo: "#password"
    messageList['decimal'] = 'decimal';

    function createDecimalField(idField, typeDecimal) {
        var digits = 2;
        var placeholder = '0,00';

        if (DataPageBase.LanguageCode() == 'en') {
            switch (typeDecimal) {
                case "0":
                    digits = 0;
                    placeholder = '0';
                    break;

                case "1":
                    digits = 1;
                    placeholder = '0.0';
                    break;

                case "2":
                    digits = 2;
                    placeholder = '0.00';
                    break;

                case "3":
                    digits = 3;
                    placeholder = '0.000';
                    break;

                case "4":
                    digits = 4;
                    placeholder = '0.0000';
                    break;
            }

            $("#" + idField).inputmask({
                'alias': 'decimal',
                'autoGroup': true,
                'digits': digits,
                'digitsOptional': false,
                'placeholder': placeholder,
                'radixPoint': ".",
                rightAlign: false,
                clearMaskOnLostFocus: !1
            });
        }
        else {
            switch (typeDecimal) {
                case "0":
                    digits = 0;
                    placeholder = '0';
                    break;

                case "1":
                    digits = 1;
                    placeholder = '0,0';
                    break;

                case "2":
                    digits = 2;
                    placeholder = '0,00';
                    break;
                case "3":
                    digits = 3;
                    placeholder = '0,000';
                    break;
                case "4":
                    digits = 4;
                    placeholder = '0,0000';
                    break;
            }

            $("#" + idField).inputmask({
                'alias': 'decimal',
                'autoGroup': true,
                'digits': digits,
                'digitsOptional': false,
                'placeholder': placeholder,
                'radixPoint': ",",
                rightAlign: false,
                clearMaskOnLostFocus: !1
            });
        }
    }

    function getRulesValidation(rulesList) {
        var rulesListReturn = {};
        for (var i = 0; i < rulesList.length; i++) {
            var elementList = rulesList[i];

            $.each(rolesList, function (key, value) {
                if (elementList.indexOf(value) >= 0) {
                    if (elementList.length === value.length) {
                        rulesListReturn[key] = true;
                    }
                    else {
                        //new function for cut elements
                        var valueValidation = elementList.slice(elementList.indexOf('[') + 1, elementList.indexOf(']'));
                        if (value === 'matches') {
                            rulesListReturn[key] = "#" + valueValidation;
                        }
                        else {
                            rulesListReturn[key] = valueValidation;
                        }
                    }
                }
            });
        }
        return rulesListReturn;
    }

    function getErrorMessagesValidation(errorMessagesList) {
        var errorMessagesListReturn = {};

        $.each(errorMessagesList, function (key, value) {
            if (messageList.hasOwnProperty(key)) {
                var messageValidation = value.replace("%s", "{0}");
                var keyNew = messageList[key];
                errorMessagesListReturn[keyNew] = messageValidation;
            }
        });
        return errorMessagesListReturn;
    }

    var createValidationRoles = function (rulesValidation, FormName) {
        try {
            if (typeof rulesValidation === "undefined" || rulesValidation === null)
                return;
            for (var i = 0; i < rulesValidation.length; i++) {
                var field = rulesValidation[i].field;
                if ($("#" + field).length > 0) {
                    var label = rulesValidation[i].label;
                    var rules = rulesValidation[i].rules;
                    var rulesList = rules.split("|");
                    var rulesListReturn = getRulesValidation(rulesList);
                    var rulesObject = new Array();

                    var isDecimal = false;

                    $.each(rulesListReturn, function (key, value) {
                        rulesObject[key] = value;

                        if (key.match("^decimal")) {
                            var typeDecimal = value;

                            createDecimalField(field, typeDecimal);
                            isDecimal = true;
                        }
                    });

                    var errorsMessages = rulesValidation[i].errors;
                    errorsMessages = getErrorMessagesValidation(errorsMessages);

                    rulesObject['messages'] = errorsMessages;

                    if (isDecimal == false) {
                        var fieldValue = $('#' + FormName + ' #' + field);
                        if (typeof fieldValue !== "undefined" && fieldValue !== null
                            && fieldValue.length == 1) {
                            try {
                                $('#' + field).rules('add', rulesObject);
                            }
                            catch (e) {
                                console.log(e);
                            }
                        }
                    }
                }
            };
        } catch (err) {
            console.log(err);
        }
    };

    return {
        //main function to initiate the module
        CreateValidationRoles: function (rulesValidation, FormName) {
            createValidationRoles(rulesValidation, FormName);
        },
        CreateDecimalField: function (idField, typeDecimal) {
            createDecimalField(idField, typeDecimal);
        },
    };
}();