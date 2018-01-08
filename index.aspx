<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="GoverProject.index1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=9;IE=8;IE=7;" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>So Easy
    </title>
    <link type="text/css" href="Style/index.css" rel="Stylesheet" />
    <%-- index-ET.css--%>
    <%--<link type="text/css" href="Style/styles-ET.css" rel="Stylesheet" />--%>
    <script src="Js/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="Styles/ElecSign.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="Style/base-ET.css" rel="Stylesheet" />
    <script type="text/javascript" src="../Js/EasyTraining/pageLoad.js"></script>
    <style type="text/css">
        .navPoint
        {
            color: #545454;
            cursor: pointer;
            font-family: Webdings;
            font-size: 9pt;
        }
        
        .a2
        {
            background-color: #A4B6D7;
        }
        .windowbg
        {
            display: none;
            z-index: 98;
            top: 0px;
            left: 0px;
            position: absolute;
            background-color: #c9c9c9;
            -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=30)"; /*IE8*/
            filter: alpha(opacity=20); /*IE5、IE5.5、IE6、IE7*/
            opacity: .45; /*Opera9.0+、Firefox1.5+、Safari、Chrome*/
        }
    </style>
    <!--以下代码就是重点，屏幕切换点击后相应的向左或者向右展开-->
    <script type="text/javascript">
        $(function () {
         var url = document.getElementById("hidurl").value;
            if (url == "") {
            }
            else {
                $("#showContent").attr("src", url);
                document.getElementById("hidurl").value = "";
            }
        })
     </script>

    <!--以上代码就是重点，屏幕切换点击后相应的向左或者向右展开-->
    <script type="text/javascript">
        function box(srtdiv) {

            //获取DIV为‘box’的盒子
            var oBox = document.getElementById(srtdiv);
            //获取元素自身的宽度
            var L1 = oBox.offsetWidth;
            //获取元素自身的高度
            var H1 = oBox.offsetHeight;
            //获取实际页面的left值。（页面宽度减去元素自身宽度/2）

            var Left = (document.documentElement.clientWidth - L1) / 2;
            //获取实际页面的top值。（页面宽度减去元素自身高度/2）
            var top = document.documentElement.scrollTop;

            oBox.style.left = Left + 'px';
            oBox.style.top = top + H1 + 'px';

        }

        function loaddatainprocess() {
            //新建背景画布
            document.getElementById("windowBG").style.width = document.body.scrollWidth + 'px';
            document.getElementById("windowBG").style.height = document.body.scrollHeight + 'px';
            document.getElementById("windowBG").style.display = 'block';
            document.getElementById("loaddata").style.display = 'block';

            box("loaddata");
        }

        function loaddatainprocesshid() {
            //新建背景画布
            document.getElementById("windowBG").style.display = 'none';
            document.getElementById("loaddata").style.display = 'none';

            box("loaddata");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="LabRole" Value="" runat="server" />
    <asp:HiddenField ID="hidLADPRights" Value="" runat="server" />
    <asp:HiddenField ID="hidHistory" Value="" runat="server" />
    <div class="windowbg" id="windowBG">
    </div>
    <div id="loaddata" class="content2">
        <div class="line22">
            <asp:Image ID="Image1" runat="server" ImageUrl="Images/EasyTraining/wait.gif" hight="100px" />
            <br />
            <span class="SunFont">数据加载中，请不要进行任何操作</span><br /><span class="VerdFont">Data is loading, please wait……</span>
        </div>
    </div>
    <div class="wapper">
        <asp:HiddenField ID="hidurl" runat="server" />
        <div class="content">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td valign="top">
                        <div class="right">
                            <iframe src="index.aspx" frameborder="0" name="showContent" id="showContent"
                                height="1099px" scrolling="auto" marginheight="0" marginwidth="0" width="100%">
                            </iframe>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
