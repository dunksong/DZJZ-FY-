<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfigFun.aspx.cs" Inherits="WebUI.Pages.GNGL.ConfigFun" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>功能配置</title>
    <link href="/Scripts/tools/easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/tools/easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/tools/easyui/src/json2.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.easyui.js" type="text/javascript"></script>
    <script src="/Scripts/tools/easyui/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="/Scripts/main/main.js" type="text/javascript"></script>
</head>
<body>
    <div class="easyui-panel" title="人员角色功能分配" style="width: 100%; height: 600px; padding: 10px;">
         <!--菜单开始-->
      <%--  <div style="padding: 5px 0;">
            <a href="#"  class="easyui-linkbutton" data-options="iconCls:'icon-add'">查看信息</a>
          
        </div>--%>
        <!--菜单end-->
        
        <div class="easyui-layout" data-options="fit:true">
            <!--人员角色组织结构树-->
            <div data-options="region:'west',split:true" style="width: 12%; padding: 10px">
                <ul class="easyui-tree" id="tree_role" data-options="animate:true,checkbox:true">
                   
                </ul>
            </div>
            <!--组织结构end-->
            

            <!--权限管理-->
            <div data-options="region:'center'" style="padding: 10px">
                <div >
                     <a href="#" class="easyui-linkbutton" id="btn_save"  iconcls="icon-save">保存设置</a>

                </div>
                <table id="tg" class="easyui-treegrid"   >
                    
                </table>
            </div>
        </div>
   </div>

   <script type="text/javascript" language="javascript">
       $(function() {
           //加载树形列表
           $('#tg').treegrid({
               method: "post",
               url: '/Pages/GNGL/ConfigFun.aspx',
               queryParams: { t: "LoadTreeGrid" },
               idField: 'Flbm',
               treeField: 'Flmc',
               title: "选择权限",
               rownumbers: true, //显示行号列
               fitColumns: true, //自动适应宽度
               checkOnSelect: true, //是否点击行选择复选框   
               singleSelect: false,
               collapsible: true,
               iconCls: 'icon-ok',
               onlyLeafCheck: true,
               cascadeCheck:true,
               columns: [
                   [
                       { title: '选择', field: 'Flbm', checkbox: true },
                       { title: '分类名称', field: 'Flmc' },
                       { title: '分类地址', field: 'Fldz' },
                       { title: '功能名称', field: 'Gnmc', width: 180 },
                       { title: '功能地址', field: 'Gndz' },
                       { title: '功能编码', field: 'Gnbm' }
                   ]
               ],
               loadFilter: function (data) {
                   return eval('(' + JSON.stringify(data) + ')');
               },
               onCheck: function (node, checked) {
                   var a = $("#tg").treegrid("getChildren", node.Flbm);
                   if (node.Gnbm === ""||node.Gnbm===null) {
                       for (var j = 0; j < a.length; j++) {
                           $("#tg").treegrid("select", a[j].Flbm);
                       }
                   }
               },
               onUncheck: function(node, checked) {
                   //把子节点全部取消选择
                   var childrens = $("#tg").treegrid("getChildren", node.Flbm);
                   if (node.Gnbm != "" || node.Gnbm != null) {
                       for (var ch = 0; ch < childrens.length; ch++) {
                           $("#tg").treegrid("unselect", childrens[ch].Flbm);
                       }
                   }
               }
           });
           $('#tg').treegrid({ cascadeCheck: true });
           //点击保存设置
           $("#btn_save").click(function() {
               saveAuth();
           });

           //加载人员树
            loadRoleTreeData();
       });

       //加载人员数数据
       loadRoleTreeData = function (parentid) {
           $.ajax({
               url: '/Pages/RYGL/List.aspx/GetList',
               contentType: "application/json; charset=utf-8",
               type: 'post',
               data: "{'parentid':'" + parentid + "','page':'1','rows':'1000000'}",
               dataType: 'json',
               success: function (data) {
                   $("#tree_role").tree({
                       data: JSON.parse(data.d).rows,
                       checkbox:true,
                       onClick: function (node) {
                           loadRoleAuth();
                       },
                       onExpand: function (node) {
                            
                       },
                       onCollapse: function (node) {

                       }, 
                       formatter: function (node) {
                           return node.MC;
                       },
                       onLoadSuccess: function() {
                          
                       },
                       onCheck: function(node,checked) {
                           loadRoleAuth();
                       },
                       onUncheck: function (node, checked) {
                           
                           loadRoleAuth();
                       }
                   });
               },
               error: function (xhr) {
                   alert("加载失败");
                   return false;
               }
           });
       }

       //加载选中人员或角色的权限
       loadRoleAuth=function () {
           var node = getTreeSelected();
           var ss = [];
           for (var i = 0; i < node.length; i++) {
               var row = node[i];
               ss.push(row.GH);
           }
           
           $.ajax({
               type: "POST",
               contentType: "application/json; charset=utf-8",
               url: '/Pages/GNGL/ConfigFun.aspx/GetQxByRyid',
               data: "{'ghid':'" + JSON.stringify(ss) + "'}",
               dataType: 'json',
               success: function (data) {
                   $('#tg').treegrid('uncheckAll', $("#tg").treegrid("getSelected"));
                   var nodes = JSON.parse(data.d);
                   for (var j = 0; j < nodes.length; j++) {
                       $("#tg").treegrid("select",nodes[j]);
                   }
               }
           });
       }
       //保存权限
       saveAuth = function() {
           //获取选择的角色人员
           var treenode = getTreeSelected();
           var rys = JSON.stringify(treenode);
           //获取选择的功能
           var tgnode = getGridSelected();
           var gns = JSON.stringify(tgnode);

          
           if (treenode === undefined||treenode.length==0) {
               $.messager.alert("提示", "请先选择角色或人员", "info");
               return;
           }
           if (tgnode===undefined||tgnode.length==0) {
               $.messager.alert("提示", "请先选择要关联的权限", "info");
               return;
           }

           //提交保存请求
           $.ajax({
               url: '/Pages/GNGL/ConfigFun.aspx/SaveRyAuth',
               contentType: "application/json; charset=utf-8",
               type: 'post',
               data: "{'ryids':'" + rys + "','qxids':'"+gns+"'}",
               dataType: 'json',
               success: function (data) {
                   //将字符串转JSON对象
                   data = JSON.parse(data.d);
                   if (data.t === "win") {
                       $.messager.alert('成功', data.v);
                       //重新加载权限列表
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

       //获取tree选中项 
       getTreeSelected = function () {
           var ss = [];
           var rows = $('#tree_role').tree('getChecked');
//           for (var i = 0; i < rows.length; i++) {
//               var row = rows[i];
//               ss.push(row.GH);
//           }
           return rows;
       }
       //获取tree选中项 
       getGridSelected = function() {
           var ss = [];
           var rows = $('#tg').datagrid('getSelections');
//           for (var i = 0; i < rows.length; i++) {
//               var row = rows[i];
//               ss.push(row.GNBM);
//           }
           return rows;
       }
   </script>
</body>
</html>
