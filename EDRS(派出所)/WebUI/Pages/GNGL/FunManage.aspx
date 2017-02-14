<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FunManage.aspx.cs" Inherits="WebUI.Pages.GNGL.FunManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>功能管理</title>
    <link href="/Scripts/tools/easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/tools/easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/tools/easyui/src/json2.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.easyui.js" type="text/javascript"></script>
    <script src="/Scripts/tools/easyui/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="/Scripts/main/main.js" type="text/javascript"></script>
     <script src="../../Scripts/tools/ExtendValidate.js" type="text/javascript"></script>
</head>
<body>
    <div data-options="fit:true" id="dw" style=" border: 10px solid #fff; width: 100%;">
        <!--菜单开始-->
        

        <!--功能分类树-->
            <div data-options="region:'west',toolbar:'#cd',split:true,title:'功能'" style="width: 250px; padding: 10px;">
                <ul class="easyui-tree" data-options="animate:true" id="tree_fun"></ul>
            </div>
            <!--end功能分类树-->

        <!--菜单end-->
        <%--<div class="easyui-treegrid" data-options="toolbar:'#tb',footer:'',fitColumns:'false',fit:true">
            --%>
            <!--权限管理-->
            <%--<div data-options="region:'center'" style="padding: 10px">--%>
              <div data-options="region:'center',title:'功能列表'">  
                <%--<table id="dg" class="easyui-datagrid" title="" style="width: 100%; height: 100%;" data-options="rownumbers:true,singleSelect:true,fit:true,border:false,toolbar:'#tb',footer:''">--%>
                <table id="dg" class="easyui-treegrid" style="width: 100%; height: 100%;" title="" data-options="toolbar:'#tb',footer:'',fitColumns:false,fit:true,border:false">
                    <thead>
                        <tr>
                            <th data-options="field:'ck',checkbox:true">
                            </th>
                            <th data-options="field:'GNBM',width:80">
                                权限编码
                            </th>
                            <th data-options="field:'GNMC',width:100">
                                功能名称
                            </th>
                            <th data-options="field:'GNCXJ',width:80,align:'left'">
                                功能地址
                            </th>
                            <th data-options="field:'DWBM',width:80,align:'left'">
                                单位编码
                            </th>
                        </tr>
                    </thead>
                </table>
                
                <div id="tb" style="padding: 2px 5px;">
                   <div id="cd" style="padding: 5px 0;">
            <a href="#" id="btn_addfuntype" class="easyui-linkbutton" data-options="iconCls:'icon-add'">
                添加功能分类</a> <a href="#" id="btn_addfun" name="btn_addfun" class="easyui-linkbutton" data-options="iconCls:'icon-add'">
                    添加功能项</a> <a href="#" id="btn_delfuntype" class="easyui-linkbutton" data-options="iconCls:'icon-remove'">
                        删除选中分类项</a> <a href="#" id="btn_edit" class="easyui-linkbutton" data-options="iconCls:'icon-edit'">
                            修改选中项</a> <a href="#" pu="Pages/GNGL/ConfigFun.aspx" id="link_" class="easyui-linkbutton"
                                data-options="iconCls:'icon-reload'" style="width: 80px">功能分配</a>
        </div>




                    <a href="#" class="easyui-linkbutton" id="btn_addfun1" name="btn_addfun" iconcls="icon-add" plain="true">新增功能</a>
                    <a href="#" class="easyui-linkbutton" id="btn_editfun" iconcls="icon-edit" plain="true">修改功能</a>
                    <a href="#" class="easyui-linkbutton" id="btn_delfun" iconcls="icon-remove" plain="true">删除功能</a>
               
                    
                    <br/>
                    功能名称:
                    <input class="easyui-textbox" id="txt_gnmc"  style="width: 210px;" >
                    权限编码:
                    <input class="easyui-textbox" id="txt_gnbm"  style="width: 110px">
                    <a href="#" class="easyui-linkbutton" id="btn_search"  iconcls="icon-search">查询</a>
                  
                   
               
                </div>
                
            </div>
       <%-- </div>--%>
        <div id="div_FunManagePanel">
        </div>
    </div>
   
    <script type="text/javascript">

        $(function () {
            $('#dw').layout();

            //加载功能分类树
            loadTreeFile("");

            $("#btn_addfuntype").click(function () {
                var method = "add";
                var node = getTreeSelected();
                createDialog('添加功能分类', 400, 300);
                if (node != undefined) {
                    $('#div_FunManagePanel').dialog('refresh', '/Pages/GNGL/AddFunType.aspx?method=' + method + '&fflbm=' + node.id + "&fflmc=" + node.text);
                } else {
                    $('#div_FunManagePanel').dialog('refresh', '/Pages/GNGL/AddFunType.aspx?method=' + method + '&fflbm=&fflmc=');
                }

            });

            //修改选中分类
            $("#btn_edit").click(function () {
                var method = "edit";
                var node = getTreeSelected();

                createDialog('修改功能分类', 400, 300);
                if (node != undefined) {
                    $('#div_FunManagePanel').dialog('refresh', '/Pages/GNGL/AddFunType.aspx?method=' + method + '&fflbm=' + node.id + "&fflmc=" + node.text);
                } else {
                    alert("请先选择一个分类");
                }
            });
            $("#btn_addfun").click(function () {
                var method = "add";
                var node = getTreeSelected();
                if (node != undefined && node.id != '') {
                    createDialog('添加功能', 500, 360);
                    $("#div_FunManagePanel").dialog('refresh', '/Pages/GNGL/AddFunct.aspx?method=' + method + '&&fflbm=' + node.id + "&fflmc=" + node.text);
                } else {
                    $.messager.alert('警告', '请先选中一个分类!', 'warning');
                }
            });
            $("#btn_addfun1").click(function () {
                var method = "add";
                var node = getTreeSelected();
                if (node != undefined && node.id != '') {
                    createDialog('添加功能', 500, 360);
                    $("#div_FunManagePanel").dialog('refresh', '/Pages/GNGL/AddFunct.aspx?method=' + method + '&&fflbm=' + node.id + "&fflmc=" + node.text);
                } else {
                    $.messager.alert('警告', '请先选中一个分类!', 'warning');
                }
            });
            //删除选中功能项
            $("#btn_delfun").click(function () {
                var node = getGridSelected();
                if (node != undefined) {
                    $.messager.confirm('确认对话框', '确定要删除该项？', function (r) {
                        if (r) {
                            $.ajax({
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                url: '/Pages/GNGL/FunManage.aspx/DeleteFun',
                                data: "{'id':'" + node.GNBM + "','dwbm':'" + node.DWBM + "'}",
                                dataType: 'json',
                                error: function (xhr) {
                                    $.messager.alert('警告', '网络连接错误!', 'warning');
                                    return false;
                                },
                                success: function (data) {
                                    //将字符串转JSON对象
                                    data = JSON.parse(data.d);
                                    if (data.t == "win") {

                                        $('#dg').datagrid('clearChecked');
                                        $('#dg').datagrid('clearSelections');

                                        loadDataGrid();
                                        $.messager.alert('提示', data.v, 'info');
                                    } else
                                        $.messager.alert('提示', data.v, 'error');
                                }
                            });
                        }

                    });
                } else {
                    $.messager.alert('警告', '请先选中要删除的项!', 'info');
                }
            });

            $("#btn_editfun").click(function () {
                var method = "edit";
                //得到功能的功能编码，单位编码
                var editnode = getGridSelected();
                //得到功能的分类编码，分类名称
                var funtypenode = getTreeSelected();
                if (editnode != undefined) {
                    createDialog("修改功能", 500, 360);
                    $("#div_FunManagePanel").dialog('refresh', '/Pages/GNGL/AddFunct.aspx?method=' + method + '&&fflbm=' + funtypenode.id + "&fflmc=" + funtypenode.text + "&&gnbm=" + editnode.GNBM + "&&dwbm=" + editnode.DWBM);
                } else {
                    $.messager.alert('警告', '请先选中要修改的功能项!', 'info');
                }
            });

            //删除选中项功能分类项
            $("#btn_delfuntype").click(function () {
                var node = getTreeSelected();

                if (node != undefined) {
                    $.messager.confirm('确认对话框', '确定要删除该项？', function (r) {
                        if (r) {
                            $.ajax({
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                url: '/Pages/GNGL/FunManage.aspx/DeleteFunType',
                                data: "{'id':'" + node.id + "'}",
                                dataType: 'json',
                                error: function (xhr) {
                                    $.messager.alert('警告', '网络连接错误!', 'warning');
                                    return false;
                                },
                                success: function (data) {
                                    //将字符串转JSON对象
                                    data = JSON.parse(data.d);
                                    if (data.t == "win") {
                                        $('#tree_fun').tree('uncheck', $("#tree_fun").tree("getSelected"));

                                        loadTreeFile("");
                                        $.messager.alert('提示', data.v, 'info');
                                    } else
                                        $.messager.alert('提示', data.v, 'error');
                                }
                            });
                        }

                    });
                } else {
                    $.messager.alert('警告', '请先选中要删除的项!', 'warning');
                }
            });

            $("#btn_search").click(function () {
                loadDataGrid();
            });
        });


        loadDataGrid = function () {
            var node = getTreeSelected();
            if (node == undefined) {
                $.messager.alert('警告', '请先选择一个功能分类!', 'warning');
                return;
            }
            $('#dg').datagrid('load', {
                flbm: node.id,
                gnmc: $("#txt_gnmc").val(),
                gnbm: $("#txt_gnbm").val()
            });
            //加载列表
            $('#dg').datagrid({
                loadFilter: function (data) {
                    if (data.d) {
                        return eval("(" + data.d + ")");
                    } else {
                        return data;
                    }
                },
                method: "post",
                url: '/Pages/GNGL/FunManage.aspx/GetFunData',
                contentType: "application/json; charset=utf-8",
                rownumbers: true, //显示行号列
                fitColumns: true, //自动适应宽度
                nowrap: true, //则在同一行中显示数据。设置为true可以提高加载性能    
                checkOnSelect: true, //是否点击行选择复选框            
                pagination: true, //是否显示分页
                pageSize: 10, //默认每页行数
                pageNumber: 1, //页数
                pageList: [10, 20, 50, 100], //页面可显示行数
                queryParamsType: "text" //新增加属性，text表示参数queryParams转换为字符串格式，其它为json类型
            });
            //初始化分页
            $(".pagination").pagination({
                beforePageText: '第',
                afterPageText: '页 共{pages}页',
                displayMsg: '当前显示{from}-{to}条记录 共{total}条记录'
                //layout: ['list', 'sep', 'first', 'prev', 'manual', 'links', 'next', 'last', 'refresh' ]
            });


        }

        //获取tree选中项 
        getTreeSelected = function () {
            var node = $('#tree_fun').tree('getSelected');

            if (node) {
                var s = node.text;

                if (node.attributes) {
                    s += "," + node.attributes.p1 + "," + node.attributes.p2;
                }
                return node;
            } else {
                return;
            }
        }

        //获取功能数据列表选中项
        getGridSelected = function () {
            var node = $('#dg').datagrid('getSelected');
            if (node) {
                var s = node.text;

                if (node.attributes) {
                    s += "," + node.attributes.p1 + "," + node.attributes.p2;
                }
                return node;
            } else {
                return;
            }
        }

        loadTreeFile = function (parentid) {
            $.ajax({
                url: '/Pages/GNGL/FunManage.aspx/GetFunTypeAll',
                contentType: "application/json; charset=utf-8",
                type: 'post',
                data: "{'parentid':'" + parentid + "'}",
                dataType: 'json',
                success: function (data) {
                    $("#tree_fun").tree({
                        data: JSON.parse(data.d),
                        onClick: function (node) {
                            if (node.id != '') {
                                loadDataGrid();
                            }
                        }
                    });

                },
                error: function (xhr) {
                    alert("加载失败");
                    return false;
                }
            });
        }

        //创建填出模态窗口
        function createDialog(title, w, h) {
            $('#div_FunManagePanel').dialog({
                width: w,
                height: h,
                closed: false,
                cache: false,
                title: title,
                modal: true,
                resizable: true,
                iconCls: 'icon-save',
                buttons: [{
                    text: '确定',
                    iconCls: 'icon-ok',
                    handler: function () {
                        submitForm();
                    }
                }, {
                    text: '取消',
                    handler: function () {
                        clearForm();
                    }
                }]
            });
        }
    </script>
</body>
</html>
