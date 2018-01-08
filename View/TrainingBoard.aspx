<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainingBoard.aspx.cs" Inherits="MatrixTool.View.TrainingBoard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>STJ Training STJ board</title>
    <link type="text/css" href="../Styles/base-ET.css" rel="stylesheet" />
    <script type="text/javascript" src="../JS/echarts/echarts-all-3.js"></script>
    <script type="text/javascript" src="../JS/ecStat.min.js"></script>
    <script type="text/javascript" src="../JS/dataTool.min.js"></script>
    <script type="text/javascript" src="../JS/js/china.js"></script>
    <script type="text/javascript" src="../JS/world.js"></script>
    <script type="text/javascript" src="../JS/bmap.min.js"></script>
    <script type="text/javascript" src="../JS/jquery-1.4.1.js"></script>
    <style type="text/css">
        .windowbg {
            display: none;
            z-index: 100;
            top: 0px;
            left: 0px;
            width: expression($(document).width()+ "px" );
            height: expression($(document).height()+ "px" );
            position: absolute;
            background-color: #222222;
            -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=30)"; /*IE8*/
            filter: alpha(opacity=30); /*IE5、IE5.5、IE6、IE7*/
            opacity: .3; /*Opera9.0+、Firefox1.5+、Safari、Chrome*/
        }

        .divAddUser {
            display: none;
            position: absolute;
            background-color: White;
            z-index: 1000;
            width: 400px;
            height: 350px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            //            $("#BtnAdd").click(function(event) {
            //                $("#divAddUser").show();
            //                $("#windowbg").show();
            //                
            //            })
            var divbg = document.getElementById('windowbg');
            //alert($(document).height() + "px");
            //alert($(document).width() + "px");
            divbg.style.width = $(document).width() + 'px';
            divbg.style.height = $(document).height() + 'px';
            $("#divAddUser").each(function (i, o) {
                //alert($(document).width()+"px");
                $(o).css("left", (($(document).width()) / 2 - (parseInt($(o).width()) / 2)) + "px");
                $(o).css("top", (($(document).height()) / 2 - (parseInt($(o).height()) / 2)) - 150 + "px");
            })

        })
        function ClickShow()
        {
            var divbg = document.getElementById('windowbg');
            var divuser = document.getElementById('divAddUser');
            divbg.style["display"] = "block";
            divuser.style["display"] = "block";
        }
        function ClickHidden() {
            var divbg = document.getElementById('windowbg');
            var divuser = document.getElementById('divAddUser');
            divbg.style["display"] = "none";
            divuser.style["display"] = "none";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="windowbg" runat="server" class="windowbg" onclick="javascript:ClickHidden();">
        </div>
        <!--浮动层 -->
        <div id="divAddUser" runat="server" class="divAddUser">
            <table class="table" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td colspan="2" style="text-align:center; line-height:32px; font-weight:bold; font-size:14px;">
                        Parameter List
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width:35%;">Updated:</td>
                    <td style="text-align: left; width:65%;">
                        <asp:TextBox ID="txtUpdated" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">Champion:</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtChampion" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">Target:</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtTarget" runat="server">0.02</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">Updated on:</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtUpOn" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">Updated by:</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtUpby" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">Note:</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" Rows="6" Width="80%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center;">
                        <asp:Button ID="btnSet" runat="server" Text="设置" OnClick="btnSet_Click" class="in_button" style="width:100px" />
                            <input type="button" value="取消/Cancel" onclick="javascript: ClickHidden();" class="in_button" style="width:100px" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="con">
            <ul id="tags">
                <li id="li3" class="selectTag"><a onclick="selectTag('tagContent3',this)">Daily Chart Print</a></li>
                <li id="li0"><a onclick="selectTag('tagContent0',this)">Rolling STJ totals</a>
                </li>
                <li id="li1"><a onclick="selectTag('tagContent1',this)">Rolling STJ totals(Weekly)</a></li>
                <li id="li2"><a onclick="selectTag('tagContent2',this)">Rolling STJ totals(Monthly)</a></li>
            </ul>
            <div id="tagContent">
                <div id="tagContent0" class="tagContent">
                    <div style="text-align: center">
                        <h3>Rolling STJ totals</h3>
                    </div>
                    <table class="table" border="0" cellpadding="0" cellspacing="0">
                        <%--<tr>
                            <td width="25%">选择类型：</td>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlType" runat="server" Width="135px" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                    <asp:ListItem Value="1" Text="AutoMated Tally" Selected="True" />
                                    <asp:ListItem Value="2" Text="Weekly" />
                                    <asp:ListItem Value="3" Text="Monthly" />
                                </asp:DropDownList>
                            </td>
                        </tr>--%>
                        <asp:Panel ID="panelTally" runat="server">
                            <tr>
                                <td style="text-align:center;">选择日期:
                                </td>
                                <td colspan="3">
                                    <asp:DropDownList ID="ddlDays" runat="server" Width="100px"></asp:DropDownList>
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="panelWeekly" runat="server" Visible="false">
                        </asp:Panel>
                        <tr>
                            <td colspan="4" style="text-align: right;">
                                <asp:Button ID="btnSearch" runat="server" Text="查询/Search" CssClass="in_button" Width="100px" OnClick="btnSearch_Click" />
                                <asp:Button ID="btnExport" runat="server" Text="导出/Export" CssClass="in_button" Width="100px" OnClick="btnExport_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td colspan="4" style="background: #118ebd; font-weight: bold; color: #000; font-size: 14px;">
                                            <asp:Literal ID="ltTitle" runat="server">过期培训看板/PD Training</asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background: #118ebd; font-weight: bold; color: #000;">Area</td>
                                        <td style="background: #118ebd; font-weight: bold; color: #000;">Assigned</td>
                                        <td style="background: #118ebd; font-weight: bold; color: #000;">Due</td>
                                        <td style="background: #118ebd; font-weight: bold; color: #000;">Percent PD</td>
                                    </tr>
                                    <asp:Panel ID="panelRp" runat="server" Visible="false">
                                        <tr>
                                            <td colspan="4">
                                                <asp:Literal ID="Literal1" runat="server">暂无数据</asp:Literal>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <asp:Repeater ID="rpList" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# DataBinder.Eval(Container, "DataItem.Area")%>
                                                </td>
                                                <td>
                                                    <%# DataBinder.Eval(Container, "DataItem.Assigned")%>
                                                </td>
                                                <td>
                                                    <%# DataBinder.Eval(Container, "DataItem.Due")%>
                                                </td>
                                                <td>
                                                    <%# DataBinder.Eval(Container, "DataItem.PercentPD")%>%
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="tagContent1" class="tagContent">
                    <div style="text-align: center">
                        <h3>Rolling STJ totals(Weekly)</h3>
                        <table class="table" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>选择年度:
                                </td>
                                <td colspan="3" style="text-align: left">
                                    <asp:DropDownList ID="ddlYears" runat="server" Width="100px"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: right;">
                                    <asp:Button ID="btnSearchYear" runat="server" Text="查询/Search" CssClass="in_button" Width="100px" OnClick="btnSearchYear_Click" />
                                    <asp:Button ID="btnExportYear" runat="server" Text="导出/Export" CssClass="in_button" Width="100px" OnClick="btnExportYear_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td colspan="5" style="background: #118ebd; font-weight: bold; color: #000; font-size: 14px;">
                                                <asp:Literal ID="Literal2" runat="server">WEEKLY</asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="background: #118ebd; font-weight: bold; color: #000;">Week</td>
                                            <td style="background: #118ebd; font-weight: bold; color: #000;">Area</td>
                                            <td style="background: #118ebd; font-weight: bold; color: #000;">Assigned</td>
                                            <td style="background: #118ebd; font-weight: bold; color: #000;">Due</td>
                                            <td style="background: #118ebd; font-weight: bold; color: #000;">Percent PD</td>
                                        </tr>
                                        <asp:Panel ID="panelYear" runat="server" Visible="false">
                                            <tr>
                                                <td colspan="4">
                                                    <asp:Literal ID="Literal3" runat="server">暂无数据</asp:Literal>
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Repeater ID="rpYear" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# DataBinder.Eval(Container, "DataItem.Weekly")%>
                                                    </td>
                                                    <td>
                                                        <%# DataBinder.Eval(Container, "DataItem.Area")%>
                                                    </td>
                                                    <td>
                                                        <%# DataBinder.Eval(Container, "DataItem.Assigned")%>
                                                    </td>
                                                    <td>
                                                        <%# DataBinder.Eval(Container, "DataItem.Due")%>
                                                    </td>
                                                    <td>
                                                        <%# DataBinder.Eval(Container, "DataItem.PercentPD")%>%
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>


                        </table>
                    </div>
                </div>
                <div id="tagContent2" class="tagContent">
                    <div style="text-align: center">
                        <h3>Rolling STJ totals(Monthly)</h3>
                        <table class="table" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>选择年度:
                                </td>
                                <td colspan="3" style="text-align: left">
                                    <asp:DropDownList ID="ddlYearForMonth" runat="server" Width="100px"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: right;">
                                    <asp:Button ID="btnSearchMonth" runat="server" Text="查询/Search" CssClass="in_button" Width="100px" OnClick="btnSearchMonth_Click" />
                                    <asp:Button ID="btnExportMonth" runat="server" Text="导出/Export" CssClass="in_button" Width="100px" OnClick="btnExportMonth_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td colspan="4" style="background: #118ebd; font-weight: bold; color: #000; font-size: 14px;">
                                                <asp:Literal ID="Literal4" runat="server">MONTHLY</asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="background: #118ebd; font-weight: bold; color: #000;">Week</td>
                                            <td style="background: #118ebd; font-weight: bold; color: #000;">Assigned</td>
                                            <td style="background: #118ebd; font-weight: bold; color: #000;">Due</td>
                                            <td style="background: #118ebd; font-weight: bold; color: #000;">Percent PD</td>
                                        </tr>
                                        <asp:Panel ID="panel2" runat="server" Visible="false">
                                            <tr>
                                                <td colspan="4">
                                                    <asp:Literal ID="Literal5" runat="server">暂无数据</asp:Literal>
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Literal ID="ltMonthTally" runat="server"></asp:Literal>
                                    </table>
                                </td>
                            </tr>


                        </table>
                    </div>
                </div>
                <div id="tagContent3" class="tagContent selectTag">
                    <div style="text-align: center">
                        <br />
                        <div style="width: 80%; text-align: right; line-height: 36px;" id="print">
                            <input onclick="javascript: printpreview();" type="button" value="打印预览" name="button_show" class="in_button" style="width:100px" />
                            <input type="button" value="Set Parameter" onclick="javascript: ClickShow();" class="in_button" style="width:120px" />
                            <asp:Button ID="btnBack" runat="server" CssClass="in_button" Width="100px" Text="返回/back" onclick="btnBack_Click" />
                        </div>
                        <div id="printTop">
                            <table class="table" width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>选择日期:<asp:DropDownList ID="ddlDaysForCharts" runat="server" Width="100px"></asp:DropDownList>
                                    </td>
                                    <td style="text-align: right;">
                                        
                                        <asp:Button ID="Button1" runat="server" Text="查询/Search" CssClass="in_button" Width="100px" OnClick="btnSearchDayCharts_Click" />&nbsp;&nbsp;
                                    </td>
                                </tr>

                            </table>
                        </div>
                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td colspan="4" style="height:24px">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 18%; background: #118ebd; font-weight: bold; color: #fff; font-size: 14px; line-height: 38px;">
                                    <asp:Literal ID="ltData" runat="server"></asp:Literal>
                                </td>
                                <td style="width: 54%; background: #118ebd; font-weight: bold; color: #fff; font-size: 18px; line-height: 38px;">Training % Past Due By Area</td>
                                <td style="width: 18%; background: #118ebd; font-weight: bold; color: #fff; font-size: 14px; line-height: 38px;">
                                    <div>
                                        Champion:<asp:Literal ID="ltUser" runat="server"></asp:Literal>
                                    </div>
                                </td>
                                <td style="width: 10%;">
                                    <asp:Image ID="imgG" runat="server" ImageUrl="~/images/EasyTraining/g.png" />
                                    <asp:Image ID="imgX" runat="server" ImageUrl="~/images/EasyTraining/x.png" Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;line-height:32px;">Target:<asp:Literal ID="ltTarget" runat="server">0.02</asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <div id="containerBar" style="height: 300px; width: 650px"></div>
                                </td>
                            </tr>
                        </table>

                        <div id="printOther">
                            <table class="table" width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td colspan="2" style="background: #118ebd; font-weight: bold; color: #000; font-size: 14px;">
                                        <asp:Literal ID="Literal7" runat="server">MONTHLY</asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">选择年度:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:DropDownList ID="ddlYaerChart" runat="server" Width="100px"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: right;">
                                        <asp:Button ID="btnSearchYearChart" runat="server" Text="查询/Search" CssClass="in_button" Width="100px" OnClick="btnSearchYearChart_Click" />&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="container" style="height: 300px; width: 650px"></div>
                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width:40%;">
                                    Updated on:<asp:Literal ID="ltUpOn" runat="server"></asp:Literal><br />
                                    Updated by:<asp:Literal ID="ltUpUser" runat="server"></asp:Literal>
                                </td>
                                <td style="width:60%;">
                                    Note:<asp:Literal ID="ltNote" runat="server"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hidBarxAxis" runat="server" />
        <asp:HiddenField ID="hidBarDataHigh" runat="server" />
        <asp:HiddenField ID="hidBarDataLow" runat="server" />
        <asp:HiddenField ID="hidBarFlag" runat="server" />
        <asp:HiddenField ID="hidLineData" runat="server" />
    </form>
    <object id="wb" height="0" width="0" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" name="wb"></object>
    <script type="text/javascript">
        function selectTag(showContent, selfObj) {
            // 操作标签 
            var tag = document.getElementById("tags").getElementsByTagName("li");
            var taglength = tag.length;
            for (i = 0; i < taglength; i++) {
                tag[i].className = "";
            }
            selfObj.parentNode.className = "selectTag";
            // 操作内容 
            for (i = 0; j = document.getElementById("tagContent" + i) ; i++) {
                j.style.display = "none";
            }
            document.getElementById(showContent).style.display = "block";

            //add by hsz
            //$("#TextBox1").val(showContent);

            //end add
        }
        function PageSetTag(showContent, num) {
            var tag = document.getElementById("tags").getElementsByTagName("li");
            var taglength = tag.length;
            for (i = 0; i < taglength; i++) {
                tag[i].className = "";
            }
            //selfObj.parentNode.className = "selectTag";
            document.getElementById("li" + num).className = "selectTag";
            // 操作内容 
            for (i = 0; j = document.getElementById("tagContent" + i) ; i++) {
                j.style.display = "none";
            }
            document.getElementById(showContent).style.display = "block";
        }


        function printpreview() {
            // 打印页面预览     tags printTop
            document.getElementById("tags").style.display = "none";
            document.getElementById("print").style.display = "none";
            document.getElementById("printTop").style.display = "none";
            document.getElementById("printOther").style.display = "none";
            wb.execwb(7, 1);
            document.getElementById("tags").style.display = "block";
            document.getElementById("print").style.display = "block";
            document.getElementById("printTop").style.display = "block";
            document.getElementById("printOther").style.display = "block";

        }
    </script>
    <script type="text/javascript">
        var dom = document.getElementById("container");
        var lineData = document.getElementById("hidLineData").value.split(",");
        var myChart = echarts.init(dom);
        var app = {};
        option = null;
        option = {
            title: {
                text: 'Pecent PD',
                x: 'center'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                orient: 'vertical',
                right: 10,
                top: 150,
                bottom: 20,
                data: ['Pecent PD']
            },
            toolbox: {
                show: true,
                feature: {
                    dataZoom: {
                        yAxisIndex: 'none'
                    },
                    dataView: { readOnly: false },
                    magicType: { type: ['line', 'bar'] },
                    restore: {},
                    saveAsImage: {}
                }
            },
            xAxis: {
                type: 'category',
                boundaryGap: false,
                data: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
            },
            yAxis: {
                type: 'value',
                axisLabel: {
                    formatter: '{value} %'
                }
            },
            series: [
                {
                    name: 'Pecent PD',
                    type: 'line',
                    data: lineData,
                    markPoint: {
                        data: [
                            { type: 'max', name: '最大值' },
                            { type: 'min', name: '最小值' }
                        ]
                    },
                    itemStyle: {
                        normal: {
                            color: '#92D050',
                        }
                    },
                }
            ]
        };
        ;
        if (option && typeof option === "object") {
            myChart.setOption(option, true);
        }
    </script>

    <script type="text/javascript">
        var target = document.getElementById('txtTarget').value;
        var xAxisAttr = document.getElementById('hidBarxAxis').value.split(",");
        var dataHigh = document.getElementById('hidBarDataHigh').value.split(",");
        var dateLow = document.getElementById('hidBarDataLow').value.split(",");


        var dom = document.getElementById("containerBar");
        var myChart = echarts.init(dom);
        var app = {};
        option = null;
        app.title = '堆叠条形图';

        option = {
            tooltip: {
                trigger: 'axis',
                axisPointer: { // 坐标轴指示器，坐标轴触发有效
                    type: 'shadow' // 默认为直线，可选为：'line' | 'shadow'
                }
            },

            grid: {
                left: '3%',
                right: '4%',
                bottom: '3%',
                containLabel: true
            },
            xAxis: {
                type: 'category',
                data: xAxisAttr
            },
            yAxis: {
                type: 'value',
                name: '',
                nameGap: '3',
                axisLabel: {
                    formatter: '{value} %'
                }

            },
            series: [{
                name: '高于 Target',
                type: 'bar',
                stack: '总量',
                barWidth: '50%',
                data: dataHigh,
                itemStyle: {
                    normal: {
                        color: '#FF0000',
                    }
                }

            },
            {
                name: '低于 Target',
                type: 'bar',
                stack: '总量',
                barWidth: '50%',
                itemStyle: {
                    normal: {
                        color: '#92D050',
                    }
                },
                data: dateLow,
                markLine: {
                    silent: true,
                    data: [{
                        yAxis: target
                    }],
                    itemStyle: {
                        normal: {
                            color: '#FF0000'
                        }
                    }
                }
            }
            ]
        };
        if (option && typeof option === "object") {
            myChart.setOption(option, true);
        }
    </script>
</body>
</html>
