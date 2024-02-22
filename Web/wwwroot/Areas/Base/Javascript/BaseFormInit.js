"use strict";
var BaseFormInit = function () {
    var FormInitFunction = function () {

       
        return;

        $(".select2-readonly").select2({
            allowClear: false,
            placeholder: DataPageForms.ComboboxText(),
            width: null,
            readonly: true,
        });

        var arrows;
        if (KTUtil.isRTL()) {
            arrows = {
                leftArrow: '<i class="la la-angle-right"></i>',
                rightArrow: '<i class="la la-angle-left"></i>'
            };
        } else {
            arrows = {
                leftArrow: '<i class="la la-angle-left"></i>',
                rightArrow: '<i class="la la-angle-right"></i>'
            };
        }

        $('.date-picker').datepicker({
            rtl: KTUtil.isRTL(),
            todayBtn: "linked",
            clearBtn: true,
            todayHighlight: true,
            format: 'yyyy-mm-dd',
            language: DataPageBase.LanguageCode(),
            templates: arrows
        });

        $('.datetimepicker-input-element').datetimepicker({
            format: 'YYYY-MM-DD HH:mm:ss',
            language: DataPageBase.LanguageCode(),
        });
        
    };

    return {
        FormInit: function (formData) {
            FormInitFunction(formData);
        },
    };
}();