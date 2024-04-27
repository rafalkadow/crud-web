/***
Wrapper/Helper Class for datagrid based on jQuery Datatable Plugin
***/
var DatatableApp = function() {

    var tableOptions; // main options
    var dataTable; // datatable object
    var table; // actual table jquery object
    var tableContainer; // actual table container object
    var tableWrapper; // actual table wrapper jquery object
    var tableInitialized = false;
    var ajaxParams = {}; // set filter mode
    var the;
    var checkBoxAllChecked = false;

    return {

        //main function to initiate the module
        init: function(options) {
      
            if (!$().dataTable) {
                return;
            }

            the = this;

            // default settings
            options = $.extend(true, {
                src: "", // actual table  
                filterApplyAction: "filter",
                filterCancelAction: "filter_cancel",
                resetGroupActionInputOnSuccess: true,
                loadingMessage: "Loading...",
                dataTable: {
                    "dom": '<"dt-buttons"Bli>rtp<\'table-responsive\'t>',
                        //litery oznaczaja elementy do wyswietlenia
                    "buttons":
                    [

                    ],

                    "pageLength": 10, // default records per page
             
                    "orderCellsTop": false,
                    "columnDefs": [{ // define columns sorting options(by default all columns are sortable extept the first checkbox column)
                        targets: [2, 3],
                        orderable: false
                    }],
                    //"pagingType": "bootstrap_extended", // pagination type(bootstrap, bootstrap_full_number or bootstrap_extended)
                    //"autoWidth": true, // disable fixed width and enable fluid table
                    "processing": false, // enable/disable display message box on record load
                    "serverSide": false, // enable/disable server side ajax loading

                    "ajax": { // define ajax settings
                        "url": "", // ajax URL
                        "type": "POST", // request type
                        "timeout": 20000,
                        "data": function (data) { // add request parameters before submit
  
                            $.each(ajaxParams, function(key, value) {
                                data[key] = value;
                            });
                            let token = $('input[name="__RequestVerificationToken"]').val();
                            let headers = { "RequestVerificationToken": token };

                            data["__RequestVerificationToken"] = token;
                     

                        },
                        "dataSrc": function(res) { // Manipulate the data returned from the server
                            if (res.customActionMessage) {
                            
                            }
                            if (checkBoxAllChecked == true) {
                                $('.group-checkable').prop('checked', false);
                                checkBoxAllChecked == false;
                            }
                         
                            return res.data;
                        },
                        "error": function (ex) { // handle general connection errors
                            console.log('error1');
                          
                        }
                    },

                    //"drawCallback": function(oSettings) { // run some code on table redraw
                    //    if (tableInitialized === false) { // check if table has been initialized
                    //        tableInitialized = true; // set table initialized
                    //        table.show(); // display table
                    //    }
                    //    countSelectedRecords(); // reset selected records indicator

                    //},
                    //"initComplete": function() {
                                         
                    //    $('.dt-button').hide();
                    //    $(".buttons-colvis").appendTo(".actions");
                    //    $('.buttons-colvis').show();
                       
                    //} 
                }

            }, options);

            tableOptions = options;

            // create table's jquery object
            table = $(options.src);
            //tableContainer = table.parents(".table-container");

            // apply the special class that used to restyle the default datatable
            //var tmp = $.fn.dataTableExt.oStdClasses;

            //$.fn.dataTableExt.oStdClasses.sWrapper = $.fn.dataTableExt.oStdClasses.sWrapper + " dataTables_extended_wrapper";
            //$.fn.dataTableExt.oStdClasses.sFilterInput = "form-control input-xs input-sm input-inline";
            //$.fn.dataTableExt.oStdClasses.sLengthSelect = "form-control input-xs input-sm input-inline";

            // initialize a datatable
            dataTable = table.DataTable(options.dataTable);

            // revert back to default
            //$.fn.dataTableExt.oStdClasses.sWrapper = tmp.sWrapper;
            //$.fn.dataTableExt.oStdClasses.sFilterInput = tmp.sFilterInput;
            //$.fn.dataTableExt.oStdClasses.sLengthSelect = tmp.sLengthSelect;

            // get table wrapper
            //tableWrapper = table.parents('.dataTables_wrapper');


            // handle group checkboxes check/uncheck
            $('.group-checkable', table).change(function (e) {
                e.preventDefault();
                var set = table.find('tbody > tr > td:nth-child(1) input[type="checkbox"]');
                var checked = $(this).prop("checked");
                $(set).each(function() {
                    $(this).prop("checked", checked);
                });
                checkBoxAllChecked = true;
                return false;
            });

            //handle filter submit button click
            table.on('click', '.filter-submit', function(e) {
                e.preventDefault();
                the.submitFilter();
            });

            // handle filter cancel button click
            table.on('click', '.filter-cancel', function(e) {
                e.preventDefault();
                the.resetFilter();
            });
        },

        submitFilter: function() {

            the.setAjaxParam("action", tableOptions.filterApplyAction);

            // get all typeable inputs
            $('select.datatable-input, input.datatable-input, input.form-control, select.select2', $("#search-data")).each(function() {
              
                the.setAjaxParam($(this).attr("name"), $(this).val());
            });
            
            dataTable.ajax.reload();
        },

        resetFilter: function() {
            $('select.datatable-input, input.datatable-input, input.form-control, select.select2', $("#search-data")).each(function () {
                if (!$(this).data('select2')) {
                    $(this).val("");
                }
                else {
                    $(this).val("").trigger("change.select2");
                }
            });
    
            the.clearAjaxParams();
            the.addAjaxParam("action", tableOptions.filterCancelAction);
            dataTable.ajax.reload();
        },

        getSelectedRowsCount: function() {
            return $('tbody > tr > td:nth-child(1) input[type="checkbox"]:checked', table).length;
        },

        getSelectedRows: function() {
            var rows = [];
            $('tbody > tr > td:nth-child(1) input[type="checkbox"]:checked', table).each(function() {
                rows.push($(this).val());
            });

            return rows;
        },

        setAjaxParam: function(name, value) {
            ajaxParams[name] = value;
        },

        addAjaxParam: function(name, value) {
            if (!ajaxParams[name]) {
                ajaxParams[name] = [];
            }

            skip = false;
            for (var i = 0; i < (ajaxParams[name]).length; i++) { // check for duplicates
                if (ajaxParams[name][i] === value) {
                    skip = true;
                }
            }

            if (skip === false) {
                ajaxParams[name].push(value);
            }
        },

        clearAjaxParams: function(name, value) {
            ajaxParams = {};
        },

        getDataTable: function() {
            return dataTable;
        },

        getTableWrapper: function() {
            return tableWrapper;
        },

        gettableContainer: function() {
            return tableContainer;
        },

        getTable: function() {
            return table;
        }

    };

};