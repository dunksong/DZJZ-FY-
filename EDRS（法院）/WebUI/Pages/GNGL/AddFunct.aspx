<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddFunct.aspx.cs" Inherits="WebUI.Pages.GNGL.AddFunct" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加功能</title>
</head>
<body>

    <div style="padding: 10px 60px 20px 60px">
        <form id="ff" method="post">
        <table cellpadding="5">
             <tr>
                <td>
                    分类名称:
                </td>
                <td>
                    <input type="hidden" name="_flbm"  id="_flbm" runat="server"  data-options="required:true" />
                    <input class="easyui-textbox" type="text" id="_flmc" runat="server" readonly="readonly" />
                </td>
            </tr>
            <tr>
                <td>单位名称:</td>
                <td>
                    <input class="easyui-textbox"  name="_dwmc" id="_dwmc" runat="server" value="高检院" readonly="readonly"/>
                    <input  type="hidden" name="_dwbm" value="0001" id="_dwbm" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    功能名称:
                </td>
                <td>
                    <input class="easyui-textbox" type="text" name="_gnmc" id="_gnmc" runat="server" data-options="required:true,validType:'maxLength[50]'"></input>
                </td>
            </tr>
            <tr>
                <td>
                    功能显示名称:
                </td>
                <td>
                    <input class="easyui-textbox" type="text" name="_gnxsmc" id="_gnxsmc" runat="server" data-options="required:true,validType:'maxLength[50]'"></input>
                </td>
            </tr>
            <tr>
                <td>
                    权限编码:
                </td>
                <td>
                    <input class="easyui-textbox" type="text" name="_gnbm" id="_gnbm" runat="server" data-options="required:true,validType:'maxLength[10]'" />
                </td>
            </tr>
            <tr>
                <td>
                    URL地址:
                </td>
                <td>
                    <input class="easyui-textbox" type="text" name="_gncxj" id="_gncxj" runat="server" data-options="required:true,vaidType:'maxLength[100]'" />
                </td>
            </tr>
            <tr>
                <td>
                    功能说明:
                </td>
                <td>
                    <input class="easyui-textbox" type="text" name="_gnsm" id="_gnsm" runat="server" data-options="required:true,validType:'maxLength[500]'" />
                </td>
            </tr>
           
            <tr style="display: none">
                <td>
                    <input type="hidden" id="_method" runat="server"/>
                    <input name="_cscs" value="" id="_cscs" runat="server"/>
                    <input name="_gncs" value="" id="_gncs" runat="server"/>
                    <input name="_sfmtck" value="N" id="_sfmtck" runat="server"/>
                    <input name="_sfsc" value="N" id="_sfsc" runat="server"/>
                    <input name="_gnxh" value="1" id="_gnxh" runat="server"/>
                    <input name="_gnct" value="" id="_gnct" runat="server"/>
                  

                </td>
            </tr>
        </table>
        </form>
       
    </div>

    <script type="text/javascript">
        function submitForm() {
            if (!$("#ff").form('enableValidation').form('validate')) {
                $.messager.alert('提示', '请检查输入项', 'info');
                return;
            }
            var jdata = $("#ff").serializeObject();
            jdata = JSON.stringify(jdata);
            var method = $("#_method").val();
            if (method == "add") {
                $.ajax({
                    url: '/Pages/GNGL/AddFunct.aspx/Add',
                    contentType: "application/json; charset=utf-8",
                    type: 'post',
                    data: "{'data':'" + jdata + "'}",
                    dataType: 'json',
                    beforeSend: function() {
                        var win = $.messager.progress({
                            title: '请稍等',
                            msg: '正在添加中...'
                        });
                    },
                    success: function(data) {
                        $.messager.progress('close');

                        //将字符串转JSON对象
                        data = JSON.parse(data.d);
                        if (data.t === "win") {
                            $.messager.alert('成功', data.v);
                            clearForm();
                            $('#dg').datagrid('reload');
                        } else {
                            $.messager.alert('失败', data.v, 'error');
                        }
                    },
                    error: function(xhr) {
                        $.messager.progress('close');

                        $.messager.alert('警告', '网络连接错误!', 'warning');
                        return false;
                    }
                });
            }
            if (method == "edit") {
                $.ajax({
                    url: '/Pages/GNGL/AddFunct.aspx/Edit',
                    contentType: "application/json; charset=utf-8",
                    type: 'post',
                    data: "{'data':'" + jdata + "'}",
                    dataType: 'json',
                    beforeSend: function () {
                        var win = $.messager.progress({
                            title: '请稍等',
                            msg: '正在添加中...'
                        });
                    },
                    success: function (data) {
                        $.messager.progress('close');

                        //将字符串转JSON对象
                        data = JSON.parse(data.d);
                        if (data.t === "win") {
                            $.messager.alert('成功', data.v);
                            clearForm();
                            $('#dg').datagrid('reload');
                        } else {
                            $.messager.alert('失败', data.v, 'error');
                        }
                    },
                    error: function (xhr) {
                        $.messager.progress('close');

                        $.messager.alert('警告', '网络连接错误!', 'warning');
                        return false;
                    }
                });
            }
        }

        function clearForm() {
            $("#div_FunManagePanel").dialog('close');
        }

    </script>
</body>
</html>
