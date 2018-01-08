<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="MatrixTool.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .txtBox
        {}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="txtBox" Height="16px"  Width="20%" />
        <br />
        <asp:Button ID="Btn_inport" runat="server" CssClass="in_button" CausesValidation="False"  Text="Import" onclick="Btn_upload_Click" />
    
        <br />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="导出" />
    
    </div>
    </form>
</body>
</html>
