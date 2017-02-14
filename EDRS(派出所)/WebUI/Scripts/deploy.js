(function ($) {
    $.fn.deploy = function (url) {
        var tree = $(this);
        var isSearch = false;
        var parentid = "";
        var count = 0;
        if (url == null)
            url = '/Handler/Common/UnitCommon.aspx'

        //下拉部门
        tree.combotree({
            url: url + '?page=1',
            required: true,
            editable: true,
            valueField: 'id',
            textField: 'text',
            panelHeight: 'auto',
            panelMinHeight: '100px',
            panelMaxHeight: '300px',
            queryParams: { t: "GetTreeDW" },
            loadFilter: function (data) {
                if (data.t && data.t == "error") {
                    //tree.combotree('reload');
                    $.messager.alert('提示', data.v, 'info');
                    return [];
                } else
                    return data;
            },
            onLoadError: function (data) {
                
            },
            onBeforeExpand: function (node) {
                var v = tree.val();
                if (parentid != node.id && (v == "" || v == null)) {
                    parentid = node.id;
                    $.ajax({
                        type: "POST",
                        url: url + '?page=2',
                        data: { t: "GetTreeDW", pid: node.id },
                        dataType: 'json',
                        timeout: 10000,
                        cache: false,
                        beforeSend: function () {
                        },
                        error: function (xhr) {
                            $.messager.alert('警告', '网络连接错误!', 'warning');
                            return false;
                        },
                        success: function (data) {
                            var chilnodes = tree.combotree('tree').tree("getChildren", node.target);
                            for (var i = 0; i < chilnodes.length; i++) {
                                tree.combotree('tree').tree("remove", chilnodes[i].target);
                            }
                            //insert   append
                            tree.combotree('tree').tree('append', {
                                parent: node.target,
                                data: data
                            });
                            count = 1;
                            tree.combotree('tree').tree("expand", node.target);
                        }

                    });
                    return false;
                }
            }, onLoadSuccess: function (node, data) {
                var cook = GetQueryString("login", "UnitOption");
               
                if (!isSearch && cook != "" && cook != null) {
                    var snode = tree.combotree('tree').tree('find', cook);
                    if (snode) {
                        tree.combotree('tree').tree('expandTo', snode.target);
                        //$('#tree_select').combotree('tree').tree('scrollTo', snode.target);
                        tree.combotree('tree').tree('select', snode.target);
                        tree.combotree('setValue', snode.id);
                        return false;
                    }
                }
            }, onSelect: function (record) {
                tree.val("");
            }
        });



        //判断是否为IE
        if (! +[1, ]) {
            tree.parent().find("input[type='text']").bind('textchange', function (event, previousText) {
                isSearch = true;
                var txt = event.target.value;
                if (txt == "" || txt == null) {
                    tree.combotree('clear');
                    tree.val("");
                    tree.combotree('reload');
                    return false;
                } else {
                    $.ajax({
                        type: "POST",
                        url: url + '?page=3',
                        data: { t: "GetTreeDW", treeText: $(this).val() },
                        dataType: 'json',
                        timeout: 10000,
                        cache: false,
                        beforeSend: function () {
                        },
                        error: function (xhr) {
                            $.messager.alert('警告', '网络连接错误!', 'warning');
                            return false;
                        },
                        success: function (data) {
                            tree.combotree('clear');
                            var chilnodes = tree.combotree('tree').tree("getRoots");
                            for (var i = 0; i < chilnodes.length; i++) {
                                tree.combotree('tree').tree("remove", chilnodes[i].target);
                            }

                            if (data && data.t && data.t == "error") { }
                            else {
                                tree.combotree('tree').tree('append', {
                                    parent: null,
                                    data: data
                                });
                            }
                            tree.val(txt);
                            event.target.value = txt;
                            //tree.parent().find("input[type='text']").val(txt);
                            return false;
                        }
                    });
                }
            });
        }
        else {
            //树形选择文本搜索功能textbox-prompt
            tree.parent().find("input[type='text']").bind("input propertychange", function () {
                isSearch = true;
                var txt = tree.parent().find("input[type='text']").val();

                if (txt == "" || txt == null) {
                    tree.combotree('clear');
                    tree.val("");
                    tree.combotree('reload');
                    return false;
                } else {
                    $.ajax({
                        type: "POST",
                        url: url + '?page=4',
                        data: { t: "GetTreeDW", treeText: txt },
                        dataType: 'json',
                        timeout: 10000,
                        cache: false,
                        beforeSend: function () {
                        },
                        error: function (xhr) {
                            $.messager.alert('警告', '网络连接错误!', 'warning');
                            return false;
                        },
                        success: function (data) {
                            tree.combotree('clear');
                            var chilnodes = tree.combotree('tree').tree("getRoots");
                            for (var i = 0; i < chilnodes.length; i++) {
                                tree.combotree('tree').tree("remove", chilnodes[i].target);
                            }

                            if (data && data.t && data.t == "error") { }
                            else {
                                tree.combotree('tree').tree('append', {
                                    parent: null,
                                    data: data
                                });
                            }
                            tree.val(txt);
                            tree.parent().find("input[type='text']").val(txt);
                            return false;
                        }

                    });

                }
            });
        }

    };
})(jQuery);



(function (a) {
    a.event.special.textchange = {
        setup: function () {
            a(this).data("lastValue", this.contentEditable === "true" ? a(this).html() : a(this).val());
            a(this).bind("keyup.textchange", a.event.special.textchange.handler);
            a(this).bind("cut.textchange paste.textchange input.textchange", a.event.special.textchange.delayedHandler)

        },
        teardown: function () {

            a(this).unbind(".textchange")
        },
        handler: function () {
            a.event.special.textchange.triggerIfChanged(a(this))
        },
        delayedHandler: function () {

            var b = a(this);
            setTimeout(function () {
                a.event.special.textchange.triggerIfChanged(b)
            },
            25)
        },
        triggerIfChanged: function (b) {
            var c = b[0].contentEditable === "true" ? b.html() : b.val();

            if (c !== b.data("lastValue")) {
                b.trigger("textchange", b.data("lastValue"));
                b.data("lastValue", c)

            }
        }
    };
    a.event.special.hastext = {
        setup: function () {
            a(this).bind("textchange", a.event.special.hastext.handler)

        },
        teardown: function () {

            a(this).unbind("textchange", a.event.special.hastext.handler)
        },
        handler: function (b, c) {
            c === "" && c !== a(this).val() && a(this).trigger("hastext")

        }
    };
    a.event.special.notext = {
        setup: function () {
            a(this).bind("textchange",
            a.event.special.notext.handler)

        },
        teardown: function () {

            a(this).unbind("textchange", a.event.special.notext.handler)
        },
        handler: function (b, c) {
            a(this).val() === "" && a(this).val() !== c && a(this).trigger("notext")

        }
    }
})(jQuery);


//读取cookie值
function GetQueryString(name, value) {
    var reg = new RegExp("(\;|^)[^;]*(" + name + ")\=([^;]*)(;|$)");
    var r = reg.exec(document.cookie);
    if (r) {
        var cookievalue = r != null ? r[3] : null;
        var re = new RegExp("(^|&)" + value + "=([^&]*)(&|$)");
        var rv = cookievalue.match(re);
        if (rv != null) {
            try {
                return decodeURI(rv[2]);
            } catch (e) {

            }
            
        }
    }
    return null;
}