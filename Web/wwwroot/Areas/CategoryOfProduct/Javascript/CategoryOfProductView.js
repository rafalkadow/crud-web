"use strict";
var View = function () {
    var handleView = function () {
        BaseDataGrid.CreateDataGridPage();
    };

    return {
        initForm: function () {
            handleView();
        },
    };
}();

$(document).ready(function () {
    View.initForm();
});
