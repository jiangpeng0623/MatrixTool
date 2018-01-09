<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainingCourse.aspx.cs" Inherits="MatrixTool.View.TrainingCourse" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link type="text/css" href="../../../Style/base-ET.css" rel="Stylesheet" />
    <link type="text/css" href="../../../Style/StyleSheet.css" rel="Stylesheet" />
    <script type="text/javascript" src="../../../JS/jquery/jquery-1.4.1.js"></script>
    <script src="../../../JS/EasyTraining/pageLoad.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../../JS/Calendar3.js"></script>
    <script type="text/javascript" src="../../../JS/My97DatePicker/WdatePicker.js"></script>
    <style type="text/css">
        .windowbg {
            display: none;
            z-index: 100;
            top: 0px;
            left: 0px;
            position: absolute;
            background-color: #222222;
            -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=30)"; /*IE8*/
            filter: alpha(opacity=30); /*IE5、IE5.5、IE6、IE7*/
            opacity: .3; /*Opera9.0+、Firefox1.5+、Safari、Chrome*/
            width: 100%;
            height: 100%;
        }

        .divAddUser {
            display: none;
            position: absolute;
            background-color: White;
            z-index: 1000;
            width: 800px;
            height: 350px;
        }
    </style>

    <script type="text/javascript">
        $(function () {
            var obj = parent.document.getElementById("showContent");
            var h = obj.height;
            document.getElementById("windowbg").style.height = h + "px";
            document.getElementById("windowbg").style.width = document.body.clientWidth + "px";
            $("#divAddUser").each(function (i, o) {
                $(o).css("left", (($(document).width()) / 2 - (parseInt($(o).width()) / 2)) + "px");
                $(o).css("top", (($(document).height()) / 2 - (parseInt($(o).height()) / 2)) - 150 + "px");
            })
        })

        function esing() {
            document.getElementById("submit2").click();
        }

        function esingCanel() {
            document.getElementById("submit3").click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <div class="cont" id="printdiv">
            <div class="position">
                <asp:Label ID="labTitle" runat="server">
                <span class="SunFont">培训时间表 / </span><span class="VerdFont">Training Schedule</span>
                </asp:Label>
            </div>
            <%--搜索条件--%>
            <div>
                <table class="tab_table03">

                    <tr>
                        <td>
                            <span class="VerdFont">课程</span><span class="SunFont">号</span><br />
                            <span class="VerdFont">CourseCode</span>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtSerachForm_Course" runat="server" class="txtBox" Width="130"></asp:TextBox>
                        </td>
                        <td>
                            <span class="VerdFont">讲师</span><br />
                            <span class="VerdFont">Trainer</span>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtSerachInitial" runat="server" class="txtBox" Width="130"></asp:TextBox>
                        </td>
                        <td>
                            <span class="VerdFont">讲课时间</span><br />
                            <span class="VerdFont">CourseTime</span>
                        </td>
                        <td class="style1">
                            <input type="text" id="txtSerachTime" name="hid" runat="server" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" class="Wdate" onclick="WdatePicker();" style="width: 98%" />
                        </td>
                        <td>
                            <asp:Button ID="Btn_Click" runat="server" CssClass="in_button"
                                CausesValidation="False" Text="Search" OnClick="Btn_Search_Click" />
                            &nbsp;&nbsp;
                                <asp:Button ID="Btn_Add" runat="server" CssClass="in_button"
                                    CausesValidation="False" Text="Add" OnClick="Btn_Add_Click" /><hr />
                                <asp:Button ID="Btn_Export" runat="server" CssClass="in_button"
                                    CausesValidation="False" Text="Export" OnClick="Btn_Export_Click" />
                            &nbsp;&nbsp;
                                <asp:Button ID="btnBack" runat="server" CssClass="in_button"
                                    CausesValidation="False" Text="Back" OnClick="btnBack_Click" />
                        </td>
                    </tr>

                </table>
            </div>
            <%--列表--%>
            <div class="line4">
                <asp:GridView ID="GridView1" runat="server" Width="100%"
                    AutoGenerateColumns="False" AllowPaging="True"
                    PageSize="20" EmptyDataText="No Data！" CssClass="tablestyle"
                    DataKeyNames="ID" EnableModelValidation="True"
                    OnRowDataBound="GridView1_RowDataBound">
                    <AlternatingRowStyle CssClass="altrowstyleList" />
                    <HeaderStyle CssClass="headerstyleList" />
                    <RowStyle CssClass="rowstyleList" />
                    <EmptyDataRowStyle CssClass="td2" />
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="False">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="Label1" Text='<%#Eval("ID") %>'> </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label runat="server" ID="Label1" Text='<%#Eval("ID") %>'> </asp:Label>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="序号<br/>No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle Width="5%" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="CourseCode" HeaderText="课程号<br/>CourseCode" SortExpression="SOPNo" HtmlEncode="False">
                            <ItemStyle Wrap="False" Width="10%" />
                        </asp:BoundField>

                        <asp:BoundField DataField="CourseTitleDescription" HeaderText="描述<br/>Description" SortExpression="Edition" HtmlEncode="False">
                            <ItemStyle Wrap="True" Width="20%" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Trainer" HeaderText="讲师<br/>Trainer" SortExpression="Edition" HtmlEncode="False">
                            <ItemStyle Wrap="True" Width="10%" />
                        </asp:BoundField>

<%--                        <asp:BoundField DataField="Time" HeaderText="时间<br/>Time" SortExpression="Edition" HtmlEncode="False">
                            <ItemStyle Wrap="True" Width="20%" />
                        </asp:BoundField>--%>
                        <asp:BoundField DataField="StartTime" HeaderText="开始时间<br/>Update Time" DataFormatString="{0:yyyy-MM-dd hh:mm:ss}"  HtmlEncode="false">
                        <ItemStyle  Wrap="true" Width="10%"/>
                        </asp:BoundField>

                        <asp:BoundField DataField="EndTime" HeaderText="结束时间<br/>Update Time" DataFormatString="{0:yyyy-MM-dd hh:mm:ss}"  HtmlEncode="false">
                        <ItemStyle  Wrap="true" Width="10%"/>
                        </asp:BoundField>

                        <asp:BoundField DataField="Address" HeaderText="地点<br/>Address" SortExpression="Edition" HtmlEncode="False">
                            <ItemStyle Wrap="True" Width="20%" />
                        </asp:BoundField>

                        <%--                        <asp:TemplateField HeaderText="操作<br/>Operation">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtModify" runat="server">Modify</asp:LinkButton>
                                <asp:LinkButton ID="lbtDelete" runat="server">Inactive</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                    <PagerSettings Visible="false" />
                </asp:GridView>
            </div>

            <div class="line4" style="text-align: center">
                <asp:LinkButton ID="lnkbtnFrist" runat="server" OnClick="lnkbtnFrist_Click" Font-Underline="True">First</asp:LinkButton>&nbsp;
            <asp:LinkButton ID="lnkbtnPre" runat="server" OnClick="lnkbtnPre_Click" Font-Underline="True">Prev</asp:LinkButton>&nbsp;
            <asp:Label ID="lblCurrentPage" runat="server"></asp:Label>&nbsp;of&nbsp;<asp:Label ID="labAll" runat="server"></asp:Label>&nbsp;
            <asp:LinkButton ID="lnkbtnNext" runat="server" OnClick="lnkbtnNext_Click" Font-Underline="True">Next</asp:LinkButton>&nbsp;
            <asp:LinkButton ID="lnkbtnLast" runat="server" OnClick="lnkbtnLast_Click" Font-Underline="True">Last</asp:LinkButton>&nbsp;
            Go<asp:DropDownList ID="ddlCurrentPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
            </asp:DropDownList>&nbsp; 
            </div>
            <!--浮动层背景 -->
            <div id="windowbg" runat="server" class="windowbg">
            </div>
            <%--弹出框--%>
            <%--            <div id="divAddUser" runat="server" class="divAddUser">--%>
            <div id="divAddUser" runat="server" class="divAddUser" style="width: 400px; height: 380px; position: absolute; left: 50%; margin-left: -250px; top: 35%; margin-top: -190px;">
                <table style="width: 100%; height: 100%; text-align: center;" bgcolor="#660066" cellpadding="0"
                    cellspacing="1" border="1">
                    <tr>
                        <td style="width: 100%; height: 100%;">
                            <table style="width: 100%; height: 100%; text-align: center; font-size: 11px;" bgcolor="#FFFFFF" cellpadding="0"
                                cellspacing="0">
                                <tr>
                                    <td align="center" style="font-weight: bold; line-height: 45px;">
                                        <span class="SunFont">添加培训时间</span><span class="VerdFont">Add Training Schedule</span>
                                        <asp:HiddenField ID="hidID" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 100%; text-align: center;">
                                        <table width="98%" style="height: 100%; text-align: left" border="1" cellspacing="0"
                                            cellpadding="0" align="center">
                                            <tr bgcolor="#dddddd">
                                                <td style="width: 120px; line-height: 20px;" align="left">
                                                    <span class="VerdFont">课程</span><span class="SunFont">号</span><br />
                                                    <span class="VerdFont">CourseCode</span>
                                                </td>
                                                <td style="padding: 2px;">
                                                    <asp:TextBox ID="txtForm_Course" runat="server" Width="90%" Height="30px" OnTextChanged="CheckCourseCode"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr bgcolor="#dddddd">
                                                <td style="width: 120px; line-height: 20px;" align="left">
                                                    <span class="SunFont">讲师</span><br />
                                                    <span class="VerdFont">Trainer</span>
                                                </td>
                                                <td style="padding: 2px;">
                                                    <asp:TextBox ID="txtInitial" runat="server" Width="90%" Height="30px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr bgcolor="#dddddd">
                                                <td style="width: 120px; line-height: 20px;" align="left">
                                                    <span class="SunFont">开始时间</span><br />
                                                    <span class="VerdFont">StartTime</span>
                                                </td>
                                                <td style="padding: 2px;">
                                                    <input type="text" id="StartCalDate" name="hid" runat="server" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" class="Wdate" onclick="WdatePicker();" style="width: 90%" />
                                                </td>
                                            </tr>
                                            <tr bgcolor="#dddddd">
                                                <td style="width: 120px; line-height: 20px;" align="left">
                                                    <span class="SunFont">结束时间</span><br />
                                                    <span class="VerdFont">EndTime</span>
                                                </td>
                                                <td style="padding: 2px;">
                                                    <input type="text" id="EndCalDate" name="hid" runat="server" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" class="Wdate" onclick="WdatePicker();" style="width: 90%" />
                                                </td>
                                            </tr>
                                            <tr bgcolor="#dddddd">
                                                <td style="width: 120px; line-height: 20px;" align="left">
                                                    <span class="SunFont">地点</span><br />
                                                    <span class="VerdFont">Address</span>
                                                </td>
                                                <td style="padding: 2px;">
                                                    <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Rows="6" Width="90%"></asp:TextBox>
                                                </td>
                                            </tr>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 20px; line-height: 45px;">
                            <asp:Button ID="BtnAddSubmit" runat="server" CssClass="save_button" Text="Save" OnClick="BtnFirst_Click" />
                            <asp:Button ID="BtnCancel" CssClass="prew_button" runat="server" Text="Cancel" OnClick="BtnCancel_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
