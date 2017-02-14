<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPerson.aspx.cs" Inherits="WebUI.Pages.RYGL.AddPerson" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加人员</title>
    <script src="../../Scripts/common.js" type="text/javascript"></script>
</head>
<body>
    <div class="easyui-panel" style="width: 720px; height: 420px;">
        <div class="easyui-layout" data-options="fit:true">
            <div data-options="region:'west',split:true" style="width: 450px; padding: 10px">
                <div class="easyui-panel" title="人员信息">
                    <div style="padding: 10px 60px 20px 60px">
                        <form id="ff" method="post">
                        <table cellpadding="5">
                            <tr>
                                <td>
                                    单位名称:
                                </td>
                                <td>
                                    <input class="easyui-textbox" type="text" name="_dwmc" runat="server" id="_dwmc" data-options="required:true"
                                        value="高检院" readonly="readonly"></input>
                                    <input id="_dwbm" name="_dwbm" style="display: none"   runat="server"  value="0001" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    工作证号:
                                </td>
                                <td>
                                    <input class="easyui-textbox" type="text" name="_gzzh" id="_gzzh" runat="server" data-options="required:true,validType:'maxLength[4]'"></input>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    人员姓名:
                                </td>
                                <td>
                                    <input class="easyui-textbox" type="text" name="_mc" id="_mc" runat="server" data-options="required:true,validType:'maxLength[30]'"></input>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    登陆别名:
                                </td>
                                <td>
                                    <input class="easyui-textbox" type="text" name="_dlbm" id="_dlbm" runat="server" data-options="required:true,validType:'maxLength[30]'"></input>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    移动电话:
                                </td>
                                <td>
                                    <input class="easyui-textbox" type="text" name="_yddhhm" id="_yddhhm" runat="server" data-options="validType:'phoneRex'"></input>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    电子邮件:
                                </td>
                                <td>
                                    <input class="easyui-textbox" type="text" name="_dzyj" id="_dzyj" runat="server" data-options="validType:'email'"></input>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    是否停职:
                                </td>
                                <td>
                                    <select id="_sftz" name="_sftz" runat="server">
                                        <option value="N" selected="selected">否</option>
                                        <option value="Y">是</option>
                                    </select>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td>
                                    <input name="_gh" value="" id="_gh" runat="server"></input>
                                    <input name="_kl" value=""  id="_kl" runat="server"/>
                                    <input name="_ydwbm" value="" id="_ydwbm" runat="server" />
                                    <input name="_ydwmc" value=""  id="_ydwmc" runat="server"/>
                                    <input name="_zp" value="" id="_zp" runat="server" />
                                    <input name="_sfsc" value="N" id="_sfsc" runat="server" />
                                    <input name="_method" type="hidden"  runat="server" id="_method"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    是否临时人员:
                                </td>
                                <td>
                                    <select id="_sflsry" name="_sflsry" runat="server">
                                        <option value="Y">是</option>
                                        <option value="N">否</option>
                                    </select>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    性别:
                                </td>
                                <td>
                                    <select name="_xb" id="_xb" runat="server">
                                        <option value="1">男</option>
                                        <option value="0">女</option>
                                    </select>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    CA标识:
                                </td>
                                <td>
                                    <input name="_caid" class="easyui-textbox" type="text" />
                                </td>
                            </tr>
                        </table>
                        </form>
                    </div>
                </div>
            </div>
            <div data-options="region:'east'" style="width: 250px; height: 420px; padding: 10px">
                <div class="easyui-panel" title="角色选择">
                    <ul id="div_roletree" class="easyui-tree">
                        

                    </ul>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function submitForm() {
            if (!$("#ff").form('enableValidation').form('validate')) {
                $.messager.alert('提示', '请检查输入项', 'info');
                return;
            }

            var method = $("#_method").val();
            if (method=="add") {
                add();
            } else if(method=="edit") {
                edit();
            }
        }

        function add() {
            var jdata = $("#ff").serializeObject();
            jdata = JSON.stringify(jdata);
            $.ajax({
                url: '/Pages/RYGL/AddPerson.aspx/Add',
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
                        $('#dg').datagrid('reload');
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

        function edit() {
            var jdata = $("#ff").serializeObject();
            jdata = JSON.stringify(jdata);
            $.ajax({
                url: '/Pages/RYGL/AddPerson.aspx/Edit',
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
                        $('#dg').datagrid('reload');
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

        function clearForm() {
            $("#add_div").dialog('close');
        }

        $(document).ready(function() {

            $.ajax({
                url: '/Pages/RYGL/List.aspx/GetRoleTree',
                contentType: "application/json; charset=utf-8",
                type: 'post',
                dataType: 'json',
                success: function(data) {
                    $("#div_roletree").tree(
                    {
                        checkbox: true,
                        onlyLeafCheck: true,
                        data: JSON.parse(data.d),
                        onLoadSuccess: function(node,loaddata) {
                           
                        }
                    });
                }
            });
        });
       
    </script>
</body>
</html>
