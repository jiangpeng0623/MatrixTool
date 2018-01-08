<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="guide.aspx.cs" Inherits="MatrixTool.guide" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
   <meta http-equiv="X-UA-Compatible"  content="IE=9;IE=8;IE=7;"/>   
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width initial-scale=1.0 maximum-scale=1.0 user-scalable=yes" />
<title>So Easy</title>
<link href="Styles/sec-ET.css" rel="stylesheet" type="text/css" />
    <style>

        body{
            background:#6b3a33;
        }
    </style>

       
</head>
<body>
    <form id="form1" runat="server">
    <div style="position:absolute;top:0;z-index:-1;width:100%;height:100%;">
    	<img src="images/EasyTraining/loginbg31.jpg" width="100%" height="100%"  alt=""/>
</div>
<div class="line2">
<ul class="guid_layout">
    <li class="name" id="useName" runat="server"></li>
   <li style="padding:  8px 0 0 50px;" class="logout"><a href="login.aspx" onclick="return confirm('确认要退出登录吗？/Are you sure to log out?')" style="color:#009FDA; border:2px ">Logout</a></li>
</ul>
</div>

<!--  内容开始  -->
<div class="wapper">
<div class="line1 guid_wrap" >

<!--  employee style="position:absolute" -->

    <div class="div01" id="PanelUp01" runat="server" >
    <div class="div_line1">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tbody>
        <tr>
          <td  rowspan="2" align="right" width=56 style="background: url(images/EasyTraining/ico.png) no-repeat center top; height:35px"></td>
          <td class="english"></td>
        </tr>
        <tr>
          <td class="tit"><a href="View/MatrixTool.aspx" ><asp:Label runat="server" ID="Label2">ISOTrain相关查询<br/>ISOTrain related</asp:Label></a></td>

              <%--<td style=" vertical-align:bottom">
                  <asp:Label runat="server" ID="labType"></asp:Label>
              </td>--%>
              
          </tr>
      </tbody>
    </table>
    </div>
    <div class="div_line2">
    <ul>
        <br />
        <%=importDataTop%>
    <li><span class="bull">&bull;</span> &nbsp; <a href="index.aspx?url=View/MatrixTool.aspx">员工资质查询ISOtrain training matrix </a></li>
    <%=DownLoadData%>



    <li><span class="bull">&bull;</span> &nbsp; <a href="index.aspx?url=View/TrainingBoard.aspx">工厂过期培训查询Site Past Due Status</a></li>
    <%=importData%>


    <li><span class="bull">&bull;</span> &nbsp; <a href="index.aspx?url=View/TrainingCourse.aspx">培训时间表Training Schedule
</a></li>
    <%=DownLoadData%>
    </ul>
    </div>
    </div>
    <div class="div01" id="PanelUp02" runat="server"  style="margin-left:7%">
    <div class="div_line1">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tbody>
        <tr>
          <td  rowspan="2" align="right" width=56 style="background: url(images/EasyTraining/ico.png) no-repeat center top; height:35px"></td>
          <td class="english"></td>
        </tr>
        <tr>
          <td class="tit"><a href="View/MatrixTool.aspx" ><asp:Label runat="server" ID="Label3">通用培训查询<br/>General Competence</asp:Label></a></td>
            <%--<td>
                <asp:Label runat="server" ID="Label1"></asp:Label>
            </td>--%>
        </tr>
      </tbody>
    </table>
    </div>
    <div class="div_line2">
    <ul>
    <br />
        <br />
        <br />
    <li><span class="bull">&bull;</span> &nbsp; <a href="index.aspx?url=View/RuanNengli.aspx" style="font-size:20px">通用能力培训<br />General Competence Training</a></li>
  <%--  <%=importData%>--%>
    </ul>
    </div>
    </div>
</div>

</div>
    </form>
</body>
</html>
