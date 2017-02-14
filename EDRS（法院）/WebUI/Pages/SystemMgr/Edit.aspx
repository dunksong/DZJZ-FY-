<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="WebUI.Pages.SystemMgr.Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" language="javascript">
        $(function () { 
            
        })
    </script>
</head>
<body>
    <form method="post" action="url" class="pageForm required-validate" onsubmit="return validateCallback(this);">
    <div class="pageFormContent" layouth="56">
        <p>
            <label>
                E-Mail：</label>
            <input class="required email" name="email" type="text" size="30" />
        </p>
        <p>
            <label>
                客户名称：</label>
            <input class="required" name="name" type="text" size="30" />
        </p>
    </div>
    <div class="formBar">
        <ul>
            <li>
                <div class="buttonActive">
                    <div class="buttonContent">
                        <button type="submit">
                            保存</button></div>
                </div>
            </li>
            <li>
                <div class="button">
                    <div class="buttonContent">
                        <button type="Button" class="close">
                            取消</button></div>
                </div>
            </li>
        </ul>
    </div>
    </form>
</body>
</html>
