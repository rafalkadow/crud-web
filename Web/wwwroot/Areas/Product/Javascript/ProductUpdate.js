"use strict";
var Update = function () {
    var handleUpdate = function () {
        BaseUpdate.Update();
        BaseFormInit.FormInit();
        
        var headers = {};
        let token = $('input[name="__RequestVerificationToken"]').val();
        headers['__RequestVerificationToken'] = token;
        var CategoryOfProductUrl = DataPageList.TableAction("category-of-product");
        var CategoryOfProductGetUrl = DataPageList.GetAction("category-of-product");
        var CategoryOfProductIdName = "CategoryOfProductId";
        $("#" + CategoryOfProductIdName).select2({
            //minimumInputLength: 2,
            //templateResult: formatState, //this is for append country flag.
            ajax: {
                url: CategoryOfProductUrl,
                dataType: 'json',
                type: "POST",
                headers: headers,
                data: function (params) {

                    let token = $('input[name="__RequestVerificationToken"]').val();

                    var query = {
                        search: params.term,
                        type: 'public',
                        __RequestVerificationToken: token
                    }
                    // Query parameters will be ?search=[term]&type=public
                    return query;
                },
                selectOnClose: true,
                processResults: function (data) {
                    console.log(data);
                    return {
                        results: $.map(data.data, function (obj, index) {
                            var temp = obj[0];
                            var indexPar = "tagOperation='";
                            var lengthStart = temp.indexOf(indexPar) + indexPar.length;
                            var id = temp.slice(lengthStart, lengthStart + 36);
                            return { id: id, text: obj[1] };
                        })
                    };
                },
            }
        });

        var guid = $('#' + CategoryOfProductIdName).data("selector");
        var CategoryOfProductControl = $('#' + CategoryOfProductIdName);

        $.ajax({
            type: 'GET',
            headers: headers,
            url: CategoryOfProductGetUrl + guid
        }).then(function (data) {
            
            // create the option and append to Select2
            var option = new Option(data.name, data.id, true, true);
            CategoryOfProductControl.append(option).trigger('change');

            // manually trigger the `select2:select` event
            CategoryOfProductControl.trigger({
                type: 'select2:select',
                params: {
                    data: data
                }
            });
        });


        //$('#' + CategoryOfProductIdName).val($('#' + CategoryOfProductIdName + ' option:first-child').val()).trigger('change');

        
        //$('#' + CategoryOfProductIdName).val(guid).trigger('change');
        
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