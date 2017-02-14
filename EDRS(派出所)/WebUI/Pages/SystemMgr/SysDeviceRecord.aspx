<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysDeviceRecord.aspx.cs"
    Inherits="WebUI.SysDeviceRecord" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>系统设备记录</title>
    <link href="/LigerUI/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet"
        type="text/css" />
    <link href="/LigerUI/lib/LigerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/LigerUI/lib/LigerUI/js/ligerui.all.js"></script>
    <script src="/Scripts/unit.juris.tree.js" type="text/javascript"></script>
    <style type="text/css">
        /*右边框背景颜色*/
        body
        {
            background: #eef2f5;
            }
         .l-panel-bwarp {
            background: white;
        }
        
        .l-panel-topbar
        {
            padding: 5px 0;
            border-bottom: 1px solid #ccc;
            display: inline-block;
            width: 100%;
             background: white;
        }
        .l-text-wrapper
        {
            display: inline-block;
        }
        .l-text-field
        {
            position: inherit;
            margin: 0;
        }
        .l-text, .l-textarea
        {
            width: 150px;
        }
        #add_form table tr td
        {
            padding: 8px 0px;
        }
        .l-box-select-inner
        {
            max-height: 300px;
            min-height: 300px;
        }
        .l-box-select-inner .l-tree
        {
            min-width: 400px !important;
        }
        .l-tree-icon
        {
            background: url('/images/icons/3.png') no-repeat !important;
            background-position: center center !important;
        }
        /* 按钮 */
        div#btn_search {
            color: white;
            background: #ed6d4a;
        }
         /* 内容区 */
       div#tb {
            margin-bottom: 5px;
            overflow-x: auto;
            border: 1px solid #ccc;
            border-top: 4px solid #129bbc;
            border-radius: 10px;
            background:white;
        }
    </style>
</head>
<body style="padding:15px; overflow: hidden;">
    <div id="tb" >
        <div style="padding:10px;">
            单位名称：<input type="text" name="_dwmc" id="_dwmc" class="l-text" />
            &nbsp;&nbsp; 设备型号：<input id="txt_key" class="l-text" type="text" name="txt_key" />
            <div id="btn_search" style="margin-left: 10px; display: inline-block; vertical-align: bottom;">
            </div>
        </div>
    </div>
    <div id="mainGrid" style="margin: 0px; padding: 0px">
    </div>
    <script type="text/javascript">

        var grid = null;
        var dwbm_tree;
        $(function () {

            $('#btn_search').ligerButton({
                text: '查询',
                icon: '../../images/cx.png'
            });

            var tree_node;
            var menu = $.ligerMenu({ top: 100, left: 100, width: 120, items:
            [
            { text: '全选/反选', click: function itemclick() {
                var sNode = tree_node;
                if (sNode == null) {
                    $.messager.alert('提示', '未选择任何节点，操作失败！', 'error');
                    return;
                }

                $(".l-expandable-close", sNode.target).click();
                if ($(".l-checkbox-checked", sNode.target).length == 0) {//未选中
                    $(".l-checkbox", sNode).removeClass("l-checkbox-checked").addClass("l-checkbox-unchecked");
                    $(".l-checkbox-unchecked", sNode.target).click();
                }
                else {
                    $(".l-checkbox", sNode).removeClass("l-checkbox-unchecked").addClass("l-checkbox-checked");
                    $(".l-checkbox-checked", sNode.target).click();
                }
            }
            }
            ]
            });

            //单位编码
            dwbm_tree = $("#_dwmc").unitJuris({width:150,checkbox:true});

    

            grid = $("#mainGrid").ligerGrid({
                columns: [
                { display: '单位名称', name: 'DWMC', minWidth: 150,  },
                { display: 'IP地址', name: 'IP', width: 100 },
                { display: 'MAC地址', name: 'MAC', minWidth: 150,  },                
                { display: '设备型号', name: 'DEVTYPE', minWidth: 100,  },
                { display: '设备厂家', name: 'DEVFACTORY', minWidth: 100,  },
                { display: '首次使用时间', name: 'DEVUSETIME', width: 170 },
                { display: 'TEXT', name: 'TEXT', width: 1,hide: true },
                { display: '设备唯一号', name: 'DEVSN', width: 1, hide: true,  },
                { display: '', name: 'DEVSN', width: 1, hide: true,  }
                ], rownumbers: true, pageSize: 50, pageSizeOptions: [20, 50, 100, 500]
                , width: '100%', height: '100%',       //服务器分页
                url: '/Pages/SystemMgr/SysDeviceRecord.aspx?page=1',
                alternatingRow: false, fixedCellHeight: false, usePager: true, heightDiff: -16,
                parms: { t: "ListBind",
                    key: $("#txt_key").val(),
                    dwbm: dwbm_tree.getValue()
                }, onSuccess: function (data) {
                    if (data.t) {
                        $.ligerDialog.error(data.v);
                    }
                }
            });
            $("#pageloading").hide();


            
        });

        $(document).ready(function () {

            //点击搜索按钮
            $("#btn_search").click(function () {
                if (grid.options.page > 1) {
                    gridSetParm();
                    grid.changePage("first"); //重置到第一页         
                } else {
                    grid.loadServerData({
                        t: "ListBind",
                        key: $("#txt_key").val(),
                        dwbm: dwbm_tree.getValue(),
                        page: 1, pagesize: grid.options.pageSize
                    });
                }
            });

        });

        function gridSetParm() {
            grid.setParm("key", $("#txt_key").val());
            grid.setParm("dwbm", dwbm_tree.getValue());
        }
    </script>
</body>
<script src="/LigerUI/lib/LigerUI/JScript1.js" type="text/javascript"></script>
</html>
