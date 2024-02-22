"use strict";
var Create = function () {
    var handleCreate = function () {
        BaseCreate.Create();
        BaseFormInit.FormInit();
        
        var headers = {};
        let token = $('input[name="__RequestVerificationToken"]').val();
        headers['__RequestVerificationToken'] = token;
        var CategoryOfProductUrl = DataPageList.TableAction("category-of-product");
        var CategoryOfProductIdName = "CategoryOfProductId";
        $("#" + CategoryOfProductIdName).select2({
            //minimumInputLength: 2,
            //templateResult: formatState, //this is for append country flag.
            ajax: {
                url: CategoryOfProductUrl,
                dataType: 'json',
                type: "POST",
                headers: headers,
                delay: 250,
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
                cache: true
            }
        });
        var guid = $('#' + CategoryOfProductIdName).data("selector");
        //$('#' + CategoryOfProductIdName).val(guid).trigger('change');
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