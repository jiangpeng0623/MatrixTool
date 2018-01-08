<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tableone.aspx.cs" Inherits="MatrixTool.View.tableone" ValidateRequest="false"%>

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
            document.getElementById("dayin").style.display = "inline-block";
        }


        function printpreview() {
            // 打印页面预览     tags printTop
            document.getElementById("dayin").style.display = "none";
            document.getElementById("cbDept").style.display = "none";
            document.getElementById("divlabdept").style.display = "block";
            
            wb.execwb(7, 1);
            document.getElementById("dayin").style.display = "block";
            document.getElementById("cbDept").style.display = "block";
            document.getElementById("divlabdept").style.display = "none";

        }

     </script>
</head>
<body style="text-align: center;">
<form id="formid" runat="server">
    <div class="header">
    	<h3>员工过期培训查询ISOtrain Tracker</h3>
    </div>
    <div class="gzrz_content" style="margin-top:15px">
    	
        <div id="buttondiv">
         <%--<asp:Button ID="Button3" runat="server" CssClass="in_button" onclick="Button3_Click" Text="导出/export" Height="23px" />--%>
            <asp:Button ID="Button31" runat="server" CssClass="in_button" onclick="Button31_Click" Text="Export Employee Report" Height="23px" Width="180px" />&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button32" runat="server" CssClass="in_button" onclick="Button32_Click" Text="Export Missining Report" Height="23px" Width="180px"/>
            &nbsp;&nbsp;&nbsp;
            <input id="dayin"  class="in_button" type="button" value="打印预览/printpreview" onclick="printpreview()" style="width:80px" />
            &nbsp;&nbsp; &nbsp;<asp:Button ID="Button1" runat="server" class="in_button" 
                 Text="返回/back" onclick="Button1_Click" style="width:80px" /></div>
        <div >
        请选择部门 /Please select Department
         <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                onselectedindexchanged="DropDownList1_SelectedIndexChanged1"  >
        </asp:DropDownList>
        <asp:Button ID="btnall" runat="server" Text="全选" CssClass="in_button" OnClick="btnall_Click"  />
        报表数据日期
        <asp:TextBox ID="tbdate" runat="server"></asp:TextBox>
            
            <asp:Button ID="btnclear" runat="server" Text="清空" CssClass="in_button" OnClick="btnclear_Click"  />
        <asp:Button ID="btnshow" runat="server" Text="生成图像" CssClass="in_button" OnClick="btnshow_Click" />

            <div style="text-align:center;">
            <asp:CheckBoxList ID="cbDept" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"></asp:CheckBoxList>
            </div>
            <asp:Label ID="Label5" runat="server" Text="没有上传的数据，不能生成记录" Visible="false"></asp:Label>
            </div>
             <div style="font-size:10px">
                 报告准备日期为<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>/The report is prepared on <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
             </div>
 
            <div id="container" style="height:530px; width:1000px; margin-top:15px"  ></div>
   
            <asp:HiddenField ID="hddata1" runat="server" />
            <asp:HiddenField ID="hddata2" runat="server" />
            <asp:HiddenField ID="hddata3" runat="server" />
            <asp:HiddenField ID="hddata4" runat="server" />
            <asp:HiddenField ID="hddata5" runat="server" />
            <asp:HiddenField ID="hddata6" runat="server" />
            <asp:HiddenField ID="hddata7" runat="server" />
            <asp:HiddenField ID="hddata8" runat="server" />


        <br />
        <div style="float:left;">
            
        </div>
       
        
        <script type="text/javascript">
            var test1 = document.getElementById('hddata1').value;
            var data1 = test1.split(",");

            var test2 = document.getElementById('hddata2').value;
            var data2 = test2.split(",");

            var test3 = document.getElementById('hddata3').value;
            var data3 = test3.split(",");

            var test4 = document.getElementById('hddata4').value;
            var data4 = test4.split(",");

            var test5 = document.getElementById('hddata5').value;
            var data5 = test5.split(",");

            var test6 = document.getElementById('hddata6').value;
            var data6 = test6.split(",");

            var test7 = document.getElementById('hddata7').value;
            var data7 = test7.split(",");

            var test8 = document.getElementById('hddata8').value;
            var data8 = test8.split(",");
            

            var dom = document.getElementById("container");
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
                legend: {
                    data: ['Past Due', '< 7 Days till due', '>=7 Days till due'],
                    x: '50%'
                },
                grid: {
                    left: '30%',
                    right: '4%',
                    bottom: '3%',
                    containLabel: true
                },
                xAxis: {
                    type: 'category',
                    data: data1,
                    axisLabel: {
                        interval: 0,
                        rotate: 40
                    }
                },
                yAxis: {
                    type: 'value',
                    name: '',
                    nameGap: '3',
                    
                },
                series: [{
                    name: 'Past Due',
                    type: 'bar',
                    stack: '总量',
                    data: data2,
                   itemStyle:{  
                     normal:{  
                       color:'#FF0000',  
                       }                                                 
                    },
                    
                },
				{
				    name: '< 7 Days till due',
				    type: 'bar',
				    stack: '总量',
                    itemStyle:{  
                     normal:{  
                       color:'#FFFF00',  
                       }   
				    },
				    data: data3
				},
				{
				    name: '>=7 Days till due',
				    type: 'bar',
				    stack: '总量',
				    itemStyle:{  
                     normal:{  
                       color:'#92D050',  
                       }   
				    },
				    data: data4
				}
			]
            }; 
            if (option && typeof option === "object") {
                myChart.setOption(option, true);
            }
	</script>
    </div>
     <div class="gzrz_content">
     <div style=" text-align:center;">
     请选择任务级别/Please select task level
        	<asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" 
            onselectedindexchanged="DropDownList3_SelectedIndexChanged">
            </asp:DropDownList>
            </div>
            <div id="Div1" style="height:530px;width:1000px;"">
             <script type="text/javascript">
             var dom = document.getElementById("Div1");
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
                legend: {
                    data: ['Past Due', '< 7 Days till due', '>=7 Days till due'],
                    x:"50%"
                },
                grid: {
                    left: '30%',
                    right: '4%',
                    bottom: '3%',
                    containLabel: true
                },
                xAxis: {
                    type: 'category',
                    data: data5,
                    axisLabel: {
                        interval: 0,
                        rotate: 40
                    }
                },
                yAxis: {
                    type: 'value',
                    name: '',
                    nameGap: '3',
                    
                },
                series: [{
                    name: 'Past Due',
                    type: 'bar',
                    stack: '总量',
                    data: data6,
                   itemStyle:{  
                     normal:{  
                       color:'#FF0000',  
                       }                                                 
                    },
                    
                },
				{
				    name: '< 7 Days till due',
				    type: 'bar',
				    stack: '总量',
                    itemStyle:{  
                     normal:{  
                       color:'#FFFF00',  
                       }   
				    },
				    data: data7
				},
				{
				    name: '>=7 Days till due',
				    type: 'bar',
				    stack: '总量',
				    itemStyle:{  
                     normal:{  
                       color:'#92D050',  
                       }   
				    },
				    data: data8
				}
			]
            }; 
            if (option && typeof option === "object") {
                myChart.setOption(option, true);
            }
            </script></div>
    </div>
    <div id ="divlabdept" style="display:none;">
    <asp:Label ID="labdept" runat="server" Text=""></asp:Label>
         </div>
     </form>
    <object id="wb" height="0" width="0" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" name="wb"></object>
</body>
</html>
