<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestJZ.aspx.cs" Inherits="WebUI.Pages.Business.TestJZ" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    案件编号：<asp:TextBox ID="TextBox1" runat="server" 
            Width="208px"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="测试" onclick="Button1_Click" />
    </div>
    <div>
        <asp:TextBox ID="TextBox2" runat="server" Height="482px" TextMode="MultiLine" 
            Width="903px"></asp:TextBox>
    
    </div>
    </form>
</body>
</html>
