<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="demo.aspx.cs" Inherits="EDRS.Web.webuploader.demo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="/webuploader/webuploader.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/tools/easyui/jquery.min.js" type="text/javascript"></script>
    <script src="/webuploader/webuploader.js" type="text/javascript"></script>
    <script src="/webuploader/getting-started.js" type="text/javascript"></script>
    <style type="text/css">
        .progress
        {
            height: 10px;
            border: 1px solid red;
        }
        .progress-bar
        {
            height: 10px;
            background-color: Black;
        }
    </style>
</head>
<body>
    <div id="uploader" class="wu-example">
        <!--用来存放文件信息-->
        <div id="thelist" class="uploader-list">
        </div>
        <div class="btns">
            <div id="picker">选择文件</div>
                <button id="ctlBtn"  onclick="Up()" class="btn btn-default">开始上传</button>
        </div>
    </div>
    <script type="text/javascript">
        function Up() {
            uploader.upload();
        }
    </script>
</body>
</html>
