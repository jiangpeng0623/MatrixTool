<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TEST.aspx.cs" Inherits="MatrixTool.View.TEST" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>表单</title>
    <script type="text/javascript" src="../JS/echarts/echarts-all-3.js"></script>
	<script type="text/javascript" src="../JS/ecStat.min.js"></script>
	<script type="text/javascript" src="../JS/echarts/extension/dataTool.min.js"></script>
	<script type="text/javascript" src="../JS/echarts/map/js/china.js"></script>
	<script type="text/javascript" src="../JS/echarts/map/js/world.js"></script>
	<script type="text/javascript" src="../JS/echarts/extension/bmap.min.js"></script>
    <link href="../Styles/base-ET.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function simida() {
            document.getElementById("dayin").style.display = "none";
            window.print();
            document.getElementById("dayin").style.display = "block";
        }
     </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="true" 
        onselectedindexchanged="DropDownList3_SelectedIndexChanged">
    </asp:DropDownList>
    
            <input id="dayin"  class="in_button" type="button" value="打印" onclick="simida()" />
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                onselectedindexchanged="DropDownList1_SelectedIndexChanged1"  >
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList2" runat="server" 
                onselectedindexchanged="DropDownList2_SelectedIndexChanged">
        </asp:DropDownList>
            <div id="container" style="height:530px"></div>
            <asp:HiddenField ID="hddata1" runat="server" />
            <asp:HiddenField ID="hddata2" runat="server" />
            <asp:HiddenField ID="hddata3" runat="server" />
            <asp:HiddenField ID="hddata4" runat="server" />
    </form>
</body>
</html>
