"use strict";
var Create = function () {
    var handleCreate = function () {
        BaseCreate.Create();
        BaseFormInit.FormInit();
    };

    return {
        //main function to initiate the module
        initForm: function () {
            handleCreate();
        }
    };
}();

$(document).ready(function () {
    Create.initForm();
});