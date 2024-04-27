"use strict";
var View = function () {
    $('#BackButton').click(function () {
        var url = DataPageBase.AppUrl('sign-in');
    
        $(location).attr('href', url);
        return false;
    });

    return {
            initForm: function () {
        }
    };
}();

$(document).ready(function () {
    View.initForm();
});