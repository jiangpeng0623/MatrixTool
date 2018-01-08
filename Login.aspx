<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="GoverProject.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
   <meta http-equiv="X-UA-Compatible"  content="IE=9;IE=8;IE=7;"/>   
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width initial-scale=1.0 maximum-scale=1.0 user-scalable=yes" />
<title>So Easy</title>
<link href="Styles/login-ET.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
</script> 
       
</head>

<body>
    <form id="form1" runat="server">
    <!-- 背景图-->
<div style="position:absolute;z-index:-1;width:100%;height:100%;">
    	<img src="images/EasyTraining/loginbgEnd.jpg" width="100%" height="100%"  alt=""/>
</div>
<!-- 登录开始-->
<div class="wapper">
<div class="content">
<div class="top"><img src="" width="81" height="68" alt=""/> </div>
<div class="login">
<div class="line1"><%--<img src="images/EasyTraining/logo_login.png" width="536" height="118" alt=""/>--%>
</div>
<div class="line2">
<ul>
<li class="left" style="text-align:right; padding-right:15px"><asp:Label ID="Label1" runat="server"  ForeColor="Black" Font-Bold="true" Font-Size="Large" font-family="Verdana">Initial:</asp:Label></li>
<li class="right"><asp:Label ID="tbname" runat="server"  ForeColor="Black"></asp:Label></li>
</ul>
<ul>
<li class="left" style="text-align:right; vertical-align:bottom;padding-right:15px; padding-top:5px"><asp:Label ID="Label2" runat="server"  ForeColor="Black" Font-Bold="true" Font-Size="Large" font-family="Verdana" >Login Type:</asp:Label></li>
<li class="right" >
<asp:RadioButtonList  ID="RadioType" runat="server" RepeatDirection="Horizontal"
        Height="31px" Width="310px"   ForeColor="Black" Font-Bold="true" 
        font-family="Verdana">
  <asp:ListItem  Text="Line Manager" Value="0" ></asp:ListItem>   
  <asp:ListItem  Text="Employee"     Value="1" Selected="True" ></asp:ListItem>
 </asp:RadioButtonList>
 </li>
 </ul>
 <ul>
<li class="right" style="text-align:right;"><asp:Button ID="blogin" runat="server" class="sunmit" text="Login"  OnClick="blogin_Click"/></li>
</ul>
</div>
</div>
</div>
</div>
    </form>
</body>
</html>
