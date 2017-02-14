<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LsyjMain.aspx.cs" Inherits="WebUI.Pages.LSYJ.LsyjMain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>律师卷宗阅读</title> 
    <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/tools/easyui/src/json2.js" type="text/javascript"></script>
    <script type="text/javascript" src="/LigerUI/lib/LigerUI/js/ligerui.all.js"></script>
    <script type="text/javascript">
        var mlTree;

        $(function () {
            $("#layout1").ligerLayout({ leftWidth: 200 });
            mlTree = $("#mlTree").ligerTree({
                nodeWidth: 150,
                url: '/Handler/ZZJG/DZJZ_LSYJ.ashx?action=getMlList',
                idFieldName: 'mlbh',
                parentIDFieldName: 'fmlbh',
                textFieldName: "mlxsmc"
            });

        });                    
    </script>
</head>
<body style="padding:2px;">
    <div id="layout1">              
    <div position="left" title="目录结构">
    <div id="treePanel" style="border: 1px solid #A3C0E8; height: 675px;
            overflow: auto;">
    <ul id="mlTree"></ul>
    </div>
    </div>             
    <div title="卷宗阅读" position="center">             
    <h4 >阅读申请单号：YJSQ-201408030001&nbsp;&nbsp;&nbsp;&nbsp;部门受案号：济南市院不捕核受[2015]37010000001号   </h4>
    <h4>打印申请：<input type="button" value="提交申请"/><input type="button" value="申请当前页" /></h4>
    </div>                      
    <div position="top">
    <h1>律师阅卷</h1>
    </div>       
    </div>     
</body>
</html>
