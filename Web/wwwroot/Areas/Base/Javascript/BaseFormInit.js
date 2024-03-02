"use strict";
var BaseFormInit = function () {
    var FormInitFunction = function () {
        
        //Date picker
        $('.date-picker-class').datetimepicker({
            format: 'L'
        });

        //Date and time picker
        $('.date-time-picker-class').datetimepicker({ icons: { time: 'far fa-clock' } });

        $(".inputmask").inputmask({
            'alias': 'decimal',
            'autoGroup': true,
            'digits': 4,
            'digitsOptional': false,
            'placeholder': '0.0000',
            'radixPoint': ".",
            rightAlign: false,
            clearMaskOnLostFocus: !1
        });

    };

    return {
        FormInit: function (formData) {
            FormInitFunction(formData);
        },
    };
}();