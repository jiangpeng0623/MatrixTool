<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MatrixTool.aspx.cs" Inherits="MatrixTool.View.MatrixTool" validateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta http-equiv="X-UA-Compatible" content="IE=9;IE=8;IE=7;" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
     <script src="../JS/jquery-1.11.2.min.js" type="text/javascript"></script>
     <link href="../Styles/TrainingPlanReview.css" rel="stylesheet" type="text/css"/>
     <link href="../Styles/base-ET.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
     <asp:HiddenField ID="hidUserID" runat="server"/>
    <div class="content">
        <div class="cont">
            <div class="position">
                  <span class="VerdFont"> ISOtrain</span> <span class="SunFont">培训矩阵/</span> <span class="VerdFont"> ISOtrain training matrix</span> 
            </div>

     <div class="line4" id="printdiv">
        <div class="table_wrap clear">
            <div class="lr_all" id="wrap" runat="server">
	            <div class="left" style="width:260px">
                        <div class="table_header" id="table_header" style="width:260px;">
        	            <table class="datagrid-htable" border="0" cellspacing="0" cellpadding="0" style="height: 26px; width:260px;">
                            <tbody>
                            <tr>
                                <td>
                                        <div class="datagrid-title" style="width:130px;"><span>工作组编号<br/>Job Function Code</span></div>
                                </td>
                                <td>
                       	                <div class="datagrid-title" style="width:130px;"><span>课程号<br/>Course Code</span></div>
                                </td>
                                </tr>
                            </tbody>
                            </table>
                    </div>                    <div class="table_body" id="table-body" style="width:260px;">
        	            <table class="datagrid-btable"  id="datagrid-btable01" cellspacing="0" cellpadding="0" border="0">
                                <tbody>                                        <asp:Label ID="tableLeftJF" runat="server" Text=""></asp:Label>                                </tbody>
                        </table>                    </div>
                    </div>

                    <div class="right" style="left:260px;width:984px; ">
    	            <div class="table_header table_header02">
                            <table class="datagrid-htable" border="0" cellspacing="0" cellpadding="0" style="height: 26px;">
                            <tbody>                                    <asp:Label ID="talbeRightHead" runat="server" Text=""></asp:Label>                            </tbody>
                            </table>
                    </div>                    <div class="table_body table_body02">                          <table class="datagrid-btable"  id="datagrid-btable02" cellspacing="0" cellpadding="0" border="0" >                           <tbody>
                                <asp:Label ID="talbeRightBody" runat="server" Text=""></asp:Label>
                           </tbody>
                           </table>
                    </div>                        </div>
                <div class="bar" id="box">
    	            <div class="scroll_bar" id="bar"></div>
                </div>
            </div>
        </div>
        </div>

        <div class="line4">
    <table class="tab_table03">
           <tr>
           <td style="width:3%">
                  <asp:Label ID="Label1" runat="server" Text="" BackColor="#DDDDDD" Width="20px" Height="10px"></asp:Label>
           </td>
           <td>
              Not required
           </td>
           <td  style="width:3%">
               <asp:Label ID="Label2" runat="server" Text="" BackColor="#FF2222" Width="20px" Height="10px"></asp:Label>
           </td>
           <td>
              Not trained
           </td>
           <td  style="width:3%">
             <asp:Label ID="Label3" runat="server" Text="" BackColor="#11EE11" Width="20px" Height="10px"></asp:Label>
           </td>
           <td>
              Trained
           </td >

                <td style="text-align:right;width:300px">
                    <asp:Button ID="Button1" class="save_button" runat="server" Text="Export Excel" 
            onclick="btn_save_Click" Width="120px" />
                &nbsp; &nbsp;
                    <asp:Button ID="Button2" class="prew_button" runat="server" Text="Back" onclick="ID_Back_Click"   Width="70px"  />
                    <asp:Label ID="LabForExcel" runat="server" Text="" style="display:none"></asp:Label>
                </td>
          </tr>
    </table>
      
    </div>
         
        </div>
    
    </div>
    </form>
      <%--列锁定函数--%>
    <script type="text/javascript">
        var oBox = document.getElementById('box');
        var oBar = document.getElementById('bar');
        var oWrap = document.getElementById('wrap');
        var oDatagridBtable01 = document.getElementById('datagrid-btable01');
        var oTableHeader = document.getElementById('table_header');
        var oDatagridBtable02 = document.getElementById('datagrid-btable02');
        var barTop = oBox.clientHeight - oBar.offsetHeight;
        var contTop = oWrap.clientHeight - oTableHeader.offsetHeight - oDatagridBtable01.scrollHeight;
        var contTop02 = oWrap.clientHeight - oTableHeader.offsetHeight - oDatagridBtable02.scrollHeight;


        var iTop = 0;

        oBar.onmousedown = function (ev) {
            var ev = ev || event;
            var disY = ev.clientY - this.offsetTop;

            document.onmousemove = function (ev) {
                var ev = ev || event;
                iTop = ev.clientY - disY;

                freezeTable(iTop);

            };

            document.onmouseup = function () {
                document.onmousemove = document.onmouseup = null;
            };

            return false;
        };


        mouseScrollEvt(oBox, function () {
            iTop -= 10;
            freezeTable(iTop);
        }, function () {
            iTop += 10;
            freezeTable(iTop);
        });

        mouseScrollEvt(oWrap, function () {
            iTop -= 10;
            freezeTable(iTop);
        }, function () {
            iTop += 10;
            freezeTable(iTop);
        });


        function freezeTable(iTop) {

            if (iTop < 0) iTop = 0;
            if (iTop > oBox.clientHeight - oBar.offsetHeight)
                iTop = oBox.clientHeight - oBar.offsetHeight;

            oBar.style.top = iTop + 'px';
            oDatagridBtable01.style.top = iTop / barTop * contTop + 'px';
            oDatagridBtable02.style.top = iTop / barTop * contTop02 + 'px';

        }


        function mouseScrollEvt(obj, upFn, downFn) {

            obj.onmousewheel = mouseScroll;
            if (obj.addEventListener)
                obj.addEventListener('DOMMouseScroll', mouseScroll, false);

            function mouseScroll(ev) {

                var ev = ev || event;
                var bDown = true;  //true : 向下滚动   false : 向上滚动

                if (ev.detail) {
                    bDown = ev.detail < 0 ? false : true;
                } else {
                    bDown = ev.wheelDelta < 0 ? true : false;
                }

                if (bDown) {
                    if (typeof downFn === 'function') downFn();
                } else {
                    if (typeof upFn === 'function') upFn();
                }

                if (ev.preventDefault) ev.preventDefault();
                return false;

            }

        }
    </script>
    <script type="text/javascript">
        $(function () {
            var tdNum = $("#datagrid-btable02 tr:eq(0) td").size();

            // alert(tdNum);
            $(".table_body02").width(tdNum * 90);
            $(".table_header02").width(tdNum * 90);
        })
    </script>
</body>
</html>
