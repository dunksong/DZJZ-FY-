<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="WebUI.Pages.RYGL.List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>人员管理</title>
    <link href="/Scripts/tools/easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/tools/easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/tools/easyui/src/json2.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.easyui.js" type="text/javascript"></script>
    <script src="/Scripts/tools/easyui/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="/Scripts/main/main.js" type="text/javascript"></script>


</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="easyui-datagrid" id="dg" title="人员列表" data-options="toolbar:toolbar">
		<thead>
			<tr>
				<th data-options="field:'ck',checkbox:true"></th>
				<th data-options="field:'MC',width:100">姓名</th>
				<th data-options="field:'DLBM',width:80,align:'right'">登陆别名</th>
				<th data-options="field:'GZZH',width:80,align:'right'">工作证号</th>
				<th data-options="field:'YDDHHM',width:240">电话</th>
				<th data-options="field:'DZYJ',width:60,align:'center'">电子邮件</th>
			</tr>
		</thead>
	</table>
    <script type="text/javascript">
        var toolbar = [{
            text: '添加',
            iconCls: 'icon-add',
            handler: function() {
                addPerson();
            }
        },
        {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function() {
                //修改方法
                editPerson();
            }
        },
         {
            text: '删除',
            iconCls: 'icon-remove',
            handler: function() {
                //删除方法
                delPerson();
            }
        }];
        $(document).ready(function() {
            //加载列表
            $('#dg').datagrid({
                loadFilter: function(data) {
                    if (data.d) {
                        return eval("(" + data.d + ")");
                    } else {
                        return data;
                    }
                },
                method: "post",
                url: '/Pages/RYGL/List.aspx/GetList',
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
        });

        //创建添加人员窗口
        function createAddPersonDialog() {
            $('#add_div').dialog({
                width: 750,
                height: 600,
                closed: false,
                cache: false,
                title: '添加人员',
                modal: true,
                resizable: true,
                iconCls: 'icon-save',
                buttons: [
                    {
                        text: '保存',
                        iconCls: 'icon-ok',
                        handler: function() {
                            submitForm();
                        }
                    }, {
                        text: '取消',
                        handler: function() {
                            clearForm();
                        }
                    }
                ]
            });
        }
        function addPerson() {
            createAddPersonDialog();
            $('#add_div').dialog('refresh', '/Pages/RYGL/AddPerson.aspx?method=add');
        }

        function editPerson() {
            var ckrows = $('#dg').datagrid('getSelections');
            if (ckrows.length ===1) {
                createAddPersonDialog();
        
                var gh = ckrows[0].GH;
                var dwbm = ckrows[0].DWBM;
                $('#add_div').dialog('refresh', '/Pages/RYGL/AddPerson.aspx?method=edit&&gh='+gh+'&&dwbm='+dwbm);
            } else {
                $.messager.alert('提示', '请选择您要修改的一行数据', 'info');
            }
        }

        function delPerson() {
            var ckrows = $('#dg').datagrid('getSelections');
            //是否选择了一行数据
            if (ckrows.length > 0) {
                var delNumbers = ckrows.length;
                $.messager.confirm('提示', '确定是否删除选择的['+delNumbers+']人?', function(r) {
                    if (r) {
                        var ids = new Array();
                        for (var i = 0; i < ckrows.length; i++) {
                            var row = ckrows[i];
                            //得到公正证号
                            ids[i] = row.GZZH;
                        }
                        ids = JSON.stringify(ids);
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: '/Pages/RYGL/List.aspx/Delete',
                            data: "{'ids':'"+ids+"'}",
                            dataType: 'json',
                            error: function(xhr) {
                                $.messager.alert('警告', '网络连接错误!', 'warning');
                                return false;
                            },
                            success: function (data) {
                                //将字符串转JSON对象
                                data = JSON.parse(data.d);
                                if (data.t == "win") {
                                    $('#dg').datagrid('reload');
                                    $.messager.alert('提示', data.v, 'info');
                                } else
                                    $.messager.alert('提示', data.v, 'error');
                            }
                        });
                    }
                });
            } else {
                $.messager.alert('提示', '请至少选择一行删除', 'error');

            }
        }
    </script>
    </div>
    </form>
    <!--添加人员窗口-->
    <div id="add_div"></div>  

</body>
</html>
