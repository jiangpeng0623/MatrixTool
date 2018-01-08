<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainingBoardAnalysis.aspx.cs" Inherits="MatrixTool.View.TrainingBoardAnalysis" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <div id="container" style="height: 300px"></div>
        <script type="text/javascript" src="../JS/echarts/echarts-all-3.js"></script>
        <script type="text/javascript" src="../JS/ecStat.min.js"></script>
        <script type="text/javascript" src="../JS/echarts/extension/dataTool.min.js"></script>
        <script type="text/javascript" src="../JS/echarts/map/js/china.js"></script>
        <script type="text/javascript" src="../JS/echarts/map/js/world.js"></script>
        <script type="text/javascript" src="../JS/echarts/extension/bmap.min.js"></script>
        <script type="text/javascript">
            var target = 100;
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
                    data: ['Past Due', '< 7 Days till due', '>=7 Days till due']
                },
                grid: {
                    left: '3%',
                    right: '4%',
                    bottom: '3%',
                    containLabel: true
                },
                xAxis: {
                    type: 'category',
                    data: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun']
                },
                yAxis: {
                    type: 'value',
                    name: '',
                    nameGap: '3',

                },
                series: [{
                    name: '直接访问',
                    type: 'bar',
                    stack: '总量',
                    data: [10, 0, 200, 0, 390, 0, 220],
                    itemStyle: {
                        normal: {
                            color: '#FF0000',
                        }
                    }

                },
				{
				    name: '间接',
				    type: 'bar',
				    stack: '总量',
				    itemStyle: {
				        normal: {
				            color: '#92D050',
				        }
				    },
				    data: [0, 52, 0, 334, 0, 330, 0],
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
    </form>
</body>
</html>
