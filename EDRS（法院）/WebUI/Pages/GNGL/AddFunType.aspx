<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddFunType.aspx.cs" Inherits="WebUI.Pages.GNGL.AddFunType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加功能分类</title>
</head>
<body>
    <div style="padding: 10px 60px 20px 60px">
        <form id="ff" method="post">
        <table cellpadding="5">
             <tr>
                <td>
                    上级分类
                </td>
                <td>
                    <input type="hidden" id="_fflbm"  runat="server" name="_fflbm" data-options="required:true" />
                    <input class="easyui-textbox" type="text" id="_fflmc" runat="server" readonly="readonly" name="_fflmc" data-options="required:true" />
                </td>
            </tr>
            <tr>
                <td>
                    发布单位
                </td>
                <td>
                    <input class="easyui-textbox" type="text" name="_dwmc" value="高检院" readonly="readonly"/>
                    <input  type="hidden" name="_dwbm" value="0001" data-options="required:true" />
                </td>
            </tr>
            <tr>
                <td>
                    分类编码:
                </td>
                <td>
                    <input class="easyui-textbox" type="text" name="_flbm" id="_flbm" runat="server" data-options="required:true,validType:'maxLength[12]'"></input>
                </td>
            </tr>
            <tr>
                <td>
                    分类名称:
                </td>
                <td>
                    <input class="easyui-textbox" type="text" name="_flmc" id="_flmc" runat="server" data-options="required:true,validType:'maxLength[30]'"></input>
                </td>
            </tr>
            <tr>
                <td>
                    URL地址:
                </td>
                <td>
                    <input class="easyui-textbox" type="text" name="_urldz" id="_urldz" runat="server" data-options="validType:'maxLength[100]'"></input>
                </td>
            </tr>
           
            <tr style="display: none">
                <td>
                    <input name="_flxh" value="1" />
                    <input name="_sfsc" value="N" />
                    <input type="hidden" id="_method" runat="server"/>
                </td>
            </tr>
        </table>
        </form>
        
    </div>
 
    <script type="text/javascript">
        function submitForm() {

            if (!$("#ff").form('enableValidation').form('validate')) {
                $.messager.alert('提示','请检查输入项','info');
                return;
            }

            var jdata = $("#ff").serializeObject();
            jdata = JSON.stringify(jdata);
            var method = $("#_method").val();
            if (method=="add") {
                $.ajax({
                    url: '/Pages/GNGL/AddFunType.aspx/Add',
                    contentType: "application/json; charset=utf-8",
                    type: 'post',
                    data: "{'data':'" + jdata + "'}",
                    dataType: 'json',
                    success: function (data) {
                        //将字符串转JSON对象
                        data = JSON.parse(data.d);
                        if (data.t === "win") {
                            $.messager.alert('成功', data.v);
                            clearForm();
                            loadTreeFile("");
                        } else {
                            $.messager.alert('失败', data.v, 'error');
                        }
                    },
                    error: function (xhr) {
                        $.messager.alert('警告', '网络连接错误!', 'warning');
                        return false;
                    }
                });
            }
            if (method == "edit") {
                $.ajax({
                    url: '/Pages/GNGL/AddFunType.aspx/Edit',
                    contentType: "application/json; charset=utf-8",
                    type: 'post',
                    data: "{'data':'" + jdata + "'}",
                    dataType: 'json',
                    success: function (data) {
                        //将字符串转JSON对象
                        data = JSON.parse(data.d);
                        if (data.t === "win") {
                            $.messager.alert('成功', data.v);
                            //清空表单。
                            $('#div_FunManagePanel').dialog('close');
                            loadTreeFile("");
                        } else {
                            $.messager.alert('失败', data.v, 'error');
                        }
                    },
                    error: function (xhr) {
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
