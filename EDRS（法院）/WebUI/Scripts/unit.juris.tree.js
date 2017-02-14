(function ($) {
    $.fn.unitJuris = function (defaults) {
        defaults = $.extend({
            url: null,
            width: 197,
            valueFieldID: null,
            checkbox: false
        }, defaults);
        //console.log($(this).attr("id"));
        // alert(defaults.url ? defaults.url : "/Handler/ZZJG/DZJZ_Report.ashx");
        $(this).val("");
        var DwbmTree = $(this).ligerComboBox({
            resize: false, //是否调整大小 
            absolute: true, //是否加载到body               
            width: defaults.width, //197
            selectBoxWidth: 300,
            selectBoxHeight: 300,
            valueFieldID: defaults.valueFieldID ? defaults.valueFieldID : $(this).attr("id") + '_id', //caseUnit
            valueField: 'id',
            autocomplete: true, //可输入
            url: defaults.url ? defaults.url : "/Handler/ZZJG/DZJZ_Report.ashx",
            parms: { action: "GetDwbm" },
            treeLeafOnly: false,  //只能选择树叶节点有效  
            tree: {
                //                    url: "/Login.aspx",
                //                    parms: { t: "GetTreeDW" },
                idFieldName: "id",
                parentIDFieldName: "pid",
                checkbox: defaults.checkbox,
                nodeWidth: " ",
                isExpand: 2,
                onCancelselect: function (row, target) {
                    if (defaults.checkbox) {
                        this.options.autoCheckboxEven = false;
                        $("#" + row.data.id).children("div:first-child").children(".l-checkbox").click();
                    }
                },
                onSelect: function (row, target) {
                    if (defaults.checkbox) {
                        this.options.autoCheckboxEven = false;
                        $("#" + row.data.id).children("div:first-child").children(".l-checkbox").click();
                    }
                    //  div:first-child l-selected
                    //    dwbm_tree.selectValue(row.data.id);  
                    // return false;               
                    //   dwbm_tree.selectValue(row.data.id);
                    // console.log(JSON.stringify(row));
                    // alert(JSON.stringify(target));
                }, onSuccess: function (data) {
                    
                }
            },
            onSuccess: function (data) {
                if (data.t) {
                    $.ligerDialog.warn(data.v);
                }
                else {
                    this.treeManager.setData(data);
                    //设置下拉选择最大高度
                    $(this.selectBoxInner).css('max-height', this.options.selectBoxHeight);
                   
                }
            },
            onSelected: function (value, text) {
                if (defaults.checkbox) {
                    this.treeManager.options.autoCheckboxEven = true;
                }
            }, onBeforeOpen: function () {
                $(".l-selected").removeClass("l-selected");
            }
        });
        return DwbmTree;
    }
})(jQuery);