<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagerSortIndex.aspx.cs" Inherits="WebUI.Pages.Template.ManagerSortIndex" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<link rel="stylesheet" type="text/css" href="/Scripts/tools/easyui/themes/default/easyui.css"/>
	<link rel="stylesheet" type="text/css" href="/Scripts/tools/easyui/themes/icon.css"/>
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
	<script type="text/javascript" src="/Scripts/tools/easyui/jquery.easyui.min.js"></script>
    <script src="/LigerUI/lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="/LigerUI/lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script> 
    <script src="/LigerUI/lib/LigerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script type="text/javascript">
        var tree;
        var treeData = <%=resultJson %>;
        $(function () {
            tree = $("#tt").tree({
                //url: '/Pages/Template/ManagerSortIndex.aspx?t=GetData',
                data : treeData,
                method: 'get',
                animate: true,
                dnd: true,

                onBeforeDrop: function(target, source, point)
                {
                    if(point == "append")
                    {
                        return false;
                    }
                    //查找拖动目的地
                    for(var i = 0;i < treeData.length;i++)
                    {
                        if(treeData[i].id == target.innerText.split('、')[0])
                        {
                            //只允许同级拖动
                            if(treeData[i].category != source.category)
                            {
                                //当文件拖动到卷下时，允许,数据第一级
                                if(point == "append" && source.category == "W")
                                    return true;
                                else
                                    return false;   
                            }
                        }
                        
                        //第二级数据
                        if(treeData[i].children)
                        {
                            for(var j=0;j<treeData[i].children.length;j++)
                            {
                                //查找到对应节点
                                if(treeData[i].children[j].id == target.innerText.split('、')[0])
                                {
                                
                                    //第二级 当出现append时，不允许拖动
                                    if(point == "append")
                                        return false;
                                    //只允许同级拖动
                                    //不同级别，不允许拖动
                                    if(treeData[i].children[j].category != source.category)
                                    {
                                        return false;
                                    }
                                    //不同父级，不允许拖动
                                    if(source.dossierparentmember != treeData[i].id)
                                        return false;
                                }
                            }
                        }
                    }
                    return true;
                }
            });
            $('#btnSave').linkbutton({    
                iconCls: 'icon-save'  ,
                onClick: saveTree
            });
        });
        function saveTree()
        {
            $.ajax(
            {
                type:"post",
                url:"ManagerSortIndex.aspx",
                data:{t:"submint",TreeData:JSON.stringify(tree.data().tree.data)},
                success:function(data)
                {
                    alert("保存成功！");
                    parent.InitData();
                }
                ,error:function(error)
                {
                    alert("网络连接错误！");
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <a href="#" id="btnSave">保存更改</a>
    <div>
    <ul id="tt" class="easyui-tree"></ul>
    </div>
    </form>
</body>
</html>
