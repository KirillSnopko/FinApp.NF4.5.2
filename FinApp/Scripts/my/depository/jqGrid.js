$(document).ready(function () {
    var current_id_depo = window.location.href.split('Details?id=')[1];
   
    $("#jqg").jqGrid({
        url: '/Depository/HistoryById?id=' + current_id_depo,
        datatype: "json",
        colNames: ['Date', 'Category', 'Comment', 'Value', 'Status'],
        colModel: [
            {
                name: 'date',
                index: 'date',
                width: 30,
                align: "center",
                // formatter: "number",
                // sorttype: "number",
                // stype: 'text',
                // search: true,
                key: true,
                searchoptions: {
                    // dataInit is the client-side event that fires upon initializing the toolbar search field for a column
                    // use it to place a third party control to customize the toolbar
                    dataInit: function (element) {
                        $(element).datepicker({
                            id: 'orderDate_datePicker',
                            dateFormat: 'yy-mm-dd',
                            //minDate: new Date(2010, 0, 1),
                            maxDate: new Date(),
                            showOn: 'focus'
                        });
                    },
                    // show search options
                    sopt: ["ge", "le", "eq"] // ge = greater or equal to, le = less or equal to, eq = equal to
                }
            },
            {
                name: 'category',
                index: 'category',
                width: 15,
                sortable: true,
                align: "center",
                stype: 'text',
                editable: true,
                edittype: 'text'
            },
            {
                name: 'comment',
                index: 'comment',
                width: 40,
                sortable: true,
                align: "center",
                stype: 'text',
                editable: true,
                edittype: 'text'
            },
            {
                name: 'value',
                index: 'value',
                width: 10,
                sortable: true,
                align: "center",
                //  stype: 'text',
                search: true,
                editable: true,
                // edittype: 'number',
                // formatter: "number",
                sorttype: "number",
                summaryType: 'sum',
                searchoptions: {
                    // show search options
                    sopt: ["ge", "le", "eq"] // ge = greater or equal to, le = less or equal to, eq = equal to
                }
            },
            {
                name: status,
                index: status,
                width: 10,
                hidden: true

            }
        ],
        grouping: true,
        rowNum: 20, // число отображаемых строк
        rowList: [20, 40, 60],
        viewrecords: true,
        pager: '#jpager',
        altRows: true,


        loadonce: true, // загрузка только один раз
        sortname: 'date', // сортировка
        sortorder: 'desc', // порядок сортировки
        caption: "History",
        width: 800,
        height: 500,
        search: true,

        loadComplete: function (data) {
            $.each(data.rows, function (i, item) {
                if (data.rows[i].Status == 'true') {
                    $("#" + options.rowId, $('#jqg')).removeClass('ui-widget-content');
                    $("#" + data.rows[i].id).find("td").eq(9).css("color", "red");
                }
            });
        },
        formatter: function (cellvalue, options) {
            var value = parseFloat(cellvalue), retult,
                op = $.extend({}, $.jgrid.formatter.number);

            if (!$.fmatter.isUndefined(options.colModel.formatoptions)) {
                op = $.extend({}, op, options.colModel.formatoptions);
            }
            retult = $.fmatter.util.NumberFormat(Math.abs(value), op);
            return (value >= 0 ? retult : '(' + retult + ')') + ' €';
        }
    });


    $('#jqg').jqGrid('filterToolbar', {
        // JSON stringify all data from search, including search toolbar operators
        stringResult: true,
        // instuct the grid toolbar to show the search options
        searchOperators: true
    });


    $("#jqg").jqGrid('navGrid', '#jpager', {

        search: true,
        multipleSearch: true,
        searchtext: "Search",
        refresh: true,
        refreshstate: 'current',
        add: false, // добавление
        del: false, // удаление
        edit: false, // редактирование
        view: false, // просмотр записи
        viewtext: "Смотреть",
        viewtitle: "Выбранная запись",
        addtext: "Добавить",
        edittext: "Изменить",
        deltext: "Delete"
    },
        update("edit"), // обновление
        update("add"), // добавление
        update("del") // удаление
    );
    function update(act) {
        return {
            closeAfterAdd: true, // закрыть после добавления
            height: 250,
            width: 400,
            closeAfterEdit: true, // закрыть после редактирования
            reloadAfterSubmit: true, // обновление
            drag: true,
            onclickSubmit: function (params) {
                var list = $("#jqg");
                var selectedRow = list.getGridParam("selrow");
                rowData = list.getRowData(selectedRow);
                if (act === "add")
                    params.url = '@Url.Action("CreateGrid")';
                else if (act === "del")
                    params.url = '@Url.Action("Home/Delete")';
                else if (act === "edit")
                    params.url = '@Url.Action("Edit")';
            },
            afterSubmit: function (response, postdata) {
                // обновление грида
                $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid')
                return [true, "", 0]
            }
        };
    };
});


$("#chngroup").change(function () {
    var vl = $(this).val();
    if (vl) {
        if (vl == "clear") {
            jQuery("#jqg").jqGrid('groupingRemove', true);
        } else {
            var GroupOption = new Object();
            var groupField = [];
            //GroupOption.groupField = groupField;
            GroupOption.plusicon = "ui-icon-folder-collapsed";
            GroupOption.minusicon = "ui-icon-folder-open";
            GroupOption.groupColumnShow = true;
            GroupOption.groupCollapse = true;
            GroupOption.groupText = ['<strong> {0} ({1}) </strong>']
            GroupOption.groupSummary = true;
            $("#jqg").setGridParam({ groupingView: GroupOption, width: 1000 });
            $("#jqg").jqGrid('groupingGroupBy', vl);
        }
    }
});