"use strict";
var ErrorView = function () {
    $('#backButton').click(function () {
        var url = $(this).attr('data-action');
        $(location).attr('href', url);
    });

    return {
        //main function to initiate the module
        initForm: function () {
        }
    };
}();

$(document).ready(function () {
    ErrorView.initForm();
});