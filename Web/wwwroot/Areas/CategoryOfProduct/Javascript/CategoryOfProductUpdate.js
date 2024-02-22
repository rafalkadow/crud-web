"use strict";
var Update = function () {
    var handleUpdate = function () {
        BaseUpdate.Update();
        BaseFormInit.FormInit();
    };

    return {
        //main function to initiate the module
        initForm: function () {
            handleUpdate();
        }
    };
}();

$(document).ready(function () {
    Update.initForm();
});