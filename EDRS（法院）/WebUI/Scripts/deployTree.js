(function ($) {
    $.fn.deploy = function (isChbox, isExpandLoad, isExpandNo, fn) {
        var treeUl = $(this);
        var nodeid = "";
        var level = 0; //加载树形最大层级数
        //是否加复选框true
        if (isChbox)
            isChbox = true;
        else
            isChbox = false;
        //是否点击加载true
        if (isExpandLoad)
            isExpandLoad = true;
        else {
            isExpandLoad = false;
            level = 10
        }
        //是否展开第一节点
        if (isExpandNo)
            isExpandNo = true;
        else
            isExpandNo = false;
    
        //下拉部门
        treeUl.tree({
            url: '/Handler/Common/UnitCommonHandler.ashx?pa=1',
            required: true,
            editable: true,
            checkbox: isChbox,
            valueField: 'id',
            textField: 'text',
            queryParams: { t: "GetTreeDW", level: level, bmbm: $("#bmbm").val(), jsbm: $("#jsbm").val() },
            loadFilter: function (data) {
                if (data.t && data.t == "error") {
                    //tree.combotree('reload');
                    $.messager.alert('提示', data.v, 'info');
                    return [];
                } else
                    return data;
            },
            onBeforeExpand: function (node) {
                if (isExpandLoad) {
                    if (nodeid != node.id) {
                        nodeid = node.id;
                        $.ajax({
                            type: "POST",
                            url: '/Handler/Common/UnitCommonHandler.ashx?pa=2',
                            data: { t: "GetTreeDW", pid: node.id },
                            dataType: 'json',
                            timeout: 5000,
                            cache: false,
                            beforeSend: function () {
                            },
                            error: function (xhr) {
                                $.messager.alert('警告', '网络连接错误!', 'warning');
                                return false;
                            },
                            success: function (data) {
                                var chilnodes = treeUl.tree("getChildren", node.target);
                                for (var i = 0; i < chilnodes.length; i++) {
                                    treeUl.tree("remove", chilnodes[i].target);
                                }
                                treeUl.tree('append', {
                                    parent: node.target,
                                    data: data
                                });
                                treeUl.tree("expand", node.target);
                            }
                        });
                        return false;
                    }
                }
            }, onLoadSuccess: function (node, data) {
                var rnode = treeUl.tree("getRoot");
                if (rnode != null)
                    treeUl.tree("select", rnode.target);
                if (isExpandNo && rnode) {
                    treeUl.tree("expand", rnode.target);
                }
            }, onClick: fn
        });
    };
})(jQuery);


