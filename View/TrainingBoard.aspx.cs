using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MatrixTool.View
{
    public partial class TrainingBoard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
                InitParameter();
                this.ltTitle.Text = "Rolling STJ totals";
                //BindData();
                //BindDataForRP(this.rpList, "1");
                //BindDataForRP(this.rpYear, "2");
                BindData(this.rpList, "CONVERT(varchar(10), AddTime, 121)='" + this.ddlDays.SelectedValue + "'", " CAST(bak1 AS float)");
                BindData(this.rpYear, "", " weekly desc,  CAST(bak1 AS float)");
                this.ltMonthTally.Text = GetMonthlyTally();
                InitChartsBar();
                InitLIne();
            }
        }

        protected void InitPage()
        {
            string sqlSelDays = "SELECT distinct CONVERT(varchar(10), Addtime, 121) as AddDays FROM  BoardRollingTally  order by  CONVERT(varchar(10), Addtime, 121) desc";
            DataSet dsDays = DBUtility.DbHelperSQL.Query(sqlSelDays);
            this.ddlDays.DataSource = dsDays;
            this.ddlDays.DataValueField = "AddDays";
            this.ddlDays.DataTextField = "AddDays";
            this.ddlDays.DataBind();
            //this.ddlDays.Items.Insert(0, new ListItem("全部", ""));
            this.ddlDaysForCharts.DataSource = dsDays;
            this.ddlDaysForCharts.DataValueField = "AddDays";
            this.ddlDaysForCharts.DataTextField = "AddDays";
            this.ddlDaysForCharts.DataBind();

            string sqlSelYear = "SELECT distinct CONVERT(varchar(4), Addtime, 121) as AddYear FROM  BoardRollingTally   order by CONVERT(varchar(4), Addtime, 121) desc";
            DataSet dsYears = DBUtility.DbHelperSQL.Query(sqlSelYear);
            this.ddlYears.DataSource = dsYears;
            this.ddlYears.DataTextField = "AddYear";
            this.ddlYears.DataValueField = "AddYear";
            this.ddlYears.DataBind();
            this.ddlYears.Items.Insert(0, new ListItem("全部", ""));


            this.ddlYearForMonth.DataSource = dsYears;
            this.ddlYearForMonth.DataTextField = "AddYear";
            this.ddlYearForMonth.DataValueField = "AddYear";
            this.ddlYearForMonth.DataBind();

            this.ddlYaerChart.DataSource = dsYears;
            this.ddlYaerChart.DataTextField = "AddYear";
            this.ddlYaerChart.DataValueField = "AddYear";
            this.ddlYaerChart.DataBind();
        }

        protected void InitParameter()
        {
            StringBuilder sbSel = new StringBuilder();
            sbSel.Append("select top 1 * from BoardRollingDBParameter order by AddTime DESC");
            DataSet ds = DBUtility.DbHelperSQL.Query(sbSel.ToString());
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];

                if (row["UpdateWeek"] != null)
                {
                    this.ltData.Text = row["UpdateWeek"].ToString();
                    this.txtUpdated.Text = row["UpdateWeek"].ToString();
                }
                if (row["Champion"] != null)
                {
                    this.ltUser.Text = row["Champion"].ToString();
                    this.txtChampion.Text = row["Champion"].ToString();
                }
                if (row["Target"] != null)
                {
                    this.ltTarget.Text = row["Target"].ToString();
                    this.txtTarget.Text = row["Target"].ToString();
                }
                if (row["UpdatedOn"] != null)
                {
                    this.ltUpOn.Text = row["UpdatedOn"].ToString();
                    this.txtUpOn.Text = row["UpdatedOn"].ToString();
                }
                if (row["UpdatedBy"] != null)
                {
                    this.ltUpUser.Text = row["UpdatedBy"].ToString();
                    this.txtUpby.Text = row["UpdatedBy"].ToString();
                }
                if (row["Note"] != null)
                {
                    this.ltNote.Text = row["Note"].ToString();
                    this.txtNote.Text = row["Note"].ToString();
                }
            }
        }

        protected void BindData()
        {
            //StringBuilder sbSel = new StringBuilder();
            //sbSel.Append("select vl.Area,SUM(Board.ASSIGNED) Assigned,[dbo].[GetDueByCodeAndDate](vl.Area,CONVERT(varchar(10), bp.AddTime, 121)) as Due");
            //sbSel.Append(",cast(([dbo].[GetDueByCodeAndDate](vl.Area,CONVERT(varchar(10), bp.AddTime, 121)) /SUM(Board.ASSIGNED)*100 )  as decimal(18,2)) as PersentPD");
            //if (this.ddlType.SelectedValue == "1")
            //{
            //    sbSel.Append(",''as Weekly");

            //    sbSel.Append(" from BoardRollingDB  Board left join BoardRollingParent bp on Board.Pid=bp.ID left join vlookupfortraining vl on Board.DEPT_CODE=vl.deptcode");
            //    sbSel.Append(" where 1=1");
            //    sbSel.Append(" and CONVERT(varchar(10), bp.AddTime, 121)='" + this.ddlDays.SelectedValue + "'");

            //    sbSel.Append(" group by vl.Area,CONVERT(varchar(10), bp.AddTime, 121)");
            //    sbSel.Append(" order by vl.Area");
            //}
            //else if (this.ddlType.SelectedValue == "2")
            //{
            //    sbSel.Append(",CONVERT(varchar(10), bp.AddTime, 121) as Weekly");
            //    sbSel.Append(" from BoardRollingDB  Board left join BoardRollingParent bp on Board.Pid=bp.ID left join vlookupfortraining vl on Board.DEPT_CODE=vl.deptcode");
            //    sbSel.Append(" where 1=1");
            //    if (this.ddlYears.SelectedValue != "")
            //    {
            //        sbSel.Append(" AND CONVERT(varchar(4), bp.AddTime, 121)='" + this.ddlYears.SelectedValue + "'");
            //    }
            //    sbSel.Append(" group by vl.Area,CONVERT(varchar(10), bp.AddTime, 121)");
            //    sbSel.Append(" order by CONVERT(varchar(10), bp.AddTime, 121) desc,vl.Area");
            //}
            //else if (this.ddlType.SelectedValue == "3")
            //{

            //    sbSel.Append(" from BoardRollingDB  Board left join vlookupfortraining vl on Board.DEPT_CODE=vl.deptcode");
            //    sbSel.Append(" where 1=1");
            //    sbSel.Append(" group by vl.Area");
            //}

            //DataSet ds = DBUtility.DbHelperSQL.Query(sbSel.ToString());
            //if (ds != null && ds.Tables[0].Rows.Count == 0)
            //{
            //    this.panelRp.Visible = true;
            //}
            //else {
            //    this.panelRp.Visible = false;
            //}
            //PagedDataSource pds = new PagedDataSource();
            //pds.DataSource = ds.Tables[0].DefaultView;
            //pds.AllowPaging = false;//允许分页

            //this.rpList.DataSource = pds;
            //this.rpList.DataBind();


        }

        protected void BindDataForRP(Repeater rp, string type)
        {
            StringBuilder sbSel = new StringBuilder();
            sbSel.Append("select vl.Area,SUM(Board.ASSIGNED) Assigned,[dbo].[GetDueByCodeAndDate](vl.Area,CONVERT(varchar(10), bp.AddTime, 121)) as Due");
            sbSel.Append(",cast(([dbo].[GetDueByCodeAndDate](vl.Area,CONVERT(varchar(10), bp.AddTime, 121)) /SUM(Board.ASSIGNED)*100 )  as decimal(18,2)) as PersentPD");
            if (type == "1")
            {
                sbSel.Append(",''as Weekly");

                sbSel.Append(" from BoardRollingDB  Board left join BoardRollingParent bp on Board.Pid=bp.ID left join vlookupfortraining vl on Board.DEPT_CODE=vl.deptcode");
                sbSel.Append(" where 1=1");
                sbSel.Append(" and CONVERT(varchar(10), bp.AddTime, 121)='" + this.ddlDays.SelectedValue + "'");

                sbSel.Append(" group by vl.Area,CONVERT(varchar(10), bp.AddTime, 121)");
                sbSel.Append(" order by vl.Area");
            }
            else if (type == "2")
            {
                sbSel.Append(",CONVERT(varchar(10), bp.AddTime, 121) as Weekly");
                sbSel.Append(" from BoardRollingDB  Board left join BoardRollingParent bp on Board.Pid=bp.ID left join vlookupfortraining vl on Board.DEPT_CODE=vl.deptcode");
                sbSel.Append(" where 1=1");
                if (this.ddlYears.SelectedValue != "")
                {
                    sbSel.Append(" AND CONVERT(varchar(4), bp.AddTime, 121)='" + this.ddlYears.SelectedValue + "'");
                }
                sbSel.Append(" group by vl.Area,CONVERT(varchar(10), bp.AddTime, 121)");
                sbSel.Append(" order by CONVERT(varchar(10), bp.AddTime, 121) desc,vl.Area");
            }

            DataSet ds = DBUtility.DbHelperSQL.Query(sbSel.ToString());
            if (ds != null && ds.Tables[0].Rows.Count == 0)
            {
                this.panelRp.Visible = true;
            }
            else
            {
                this.panelRp.Visible = false;
            }
            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = ds.Tables[0].DefaultView;
            pds.AllowPaging = false;//允许分页

            rp.DataSource = pds;
            rp.DataBind();
        }

        protected void BindData(Repeater list, string strWhere, string order)
        {
            StringBuilder sbSe = new StringBuilder();
            StringBuilder sbSe0 = new StringBuilder();
            StringBuilder sbSel = new StringBuilder();
            sbSe0.Append(" SELECT 'Site' as Area,sum(Assigned) as Assigned,sum(Due) as Due,cast((sum(Due)/sum(Assigned))*100 as decimal(18,2))as PercentPD ,Weekly,AddTime,0 as bak1 FROM BoardRollingTally where 1=1");
            sbSel.Append(" SELECT Area,Assigned,Due,cast((Due/Assigned)*100 as decimal(18,2))as PercentPD ,Weekly,AddTime,bak1 FROM BoardRollingTally where 1=1");
            if (strWhere != "")
            {
                sbSe0.Append(" and " + strWhere);
                sbSel.Append(" and " + strWhere);
            }
            sbSe0.Append(" group by Weekly,AddTime ");
            sbSe.Append("select Area,Assigned,Due,convert(varchar(20),PercentPD) as PercentPD,Weekly,AddTime from( " + sbSe0.ToString() + " union " + sbSel.ToString() + ") temp order by " + order);


            DataSet ds = DBUtility.DbHelperSQL.Query(sbSe.ToString());
            if (ds != null && ds.Tables[0].Rows.Count == 0)
            {
                this.panelRp.Visible = true;
            }
            else
            {
                this.panelRp.Visible = false;
            }
            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = ds.Tables[0].DefaultView;
            pds.AllowPaging = false;//允许分页

            list.DataSource = pds;
            list.DataBind();
        }

        protected String GetMonthlyTally()
        {
            StringBuilder sbRet = new StringBuilder();
            for (int i = 1; i <= 12; i++)
            {
                string Week = GetMonthEN(i);
                string Assigned = "&nbsp;";
                string Due = "&nbsp;";
                string PercentPD = "&nbsp;";
                string month = i.ToString().PadLeft(2, '0');
                string yearAndmonth = this.ddlYearForMonth.SelectedValue + "-" + month;

                #region 获取数据
                StringBuilder sbSel = new StringBuilder();
                sbSel.Append("SELECT SUM(Assigned) Assigned,SUM(Due) Due,cast((SUM(Due)/SUM(Assigned))*100 as decimal(18,2)) as PercentPD ,CONVERT(varchar(7), Addtime, 121) AS Years");
                sbSel.Append(" FROM BoardRollingTally");
                sbSel.Append(" WHERE CONVERT(varchar(7), Addtime, 121)='" + yearAndmonth + "'");
                sbSel.Append(" group by CONVERT(varchar(7), Addtime, 121)");
                sbSel.Append(" order by CONVERT(varchar(7), Addtime, 121)");
                DataSet ds = DBUtility.DbHelperSQL.Query(sbSel.ToString());
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable table = ds.Tables[0];
                    Assigned = table.Rows[0]["Assigned"] == null || table.Rows[0]["Assigned"].ToString() == "" ? "" : table.Rows[0]["Assigned"].ToString();
                    Due = table.Rows[0]["Due"] == null || table.Rows[0]["Due"].ToString() == "" ? "" : table.Rows[0]["Due"].ToString();
                    PercentPD = table.Rows[0]["PercentPD"] == null || table.Rows[0]["PercentPD"].ToString() == "" ? "" : table.Rows[0]["PercentPD"].ToString() + "%";
                }
                #endregion

                sbRet.Append("<tr>");
                sbRet.Append("<td style=\"background:#82cecb; font-weight: bold; color: #000;\">");
                sbRet.Append(Week);
                sbRet.Append("</td>");

                sbRet.Append("<td>");
                sbRet.Append(Assigned);
                sbRet.Append("</td>");

                sbRet.Append("<td>");
                sbRet.Append(Due);
                sbRet.Append("</td>");

                sbRet.Append("<td>");
                sbRet.Append(PercentPD);
                sbRet.Append("</td>");

                sbRet.Append("</tr>");
            }

            return sbRet.ToString();
        }

        protected string GetMonthEN(int moth)
        {
            string monthEN = string.Empty;
            switch (moth)
            {
                case 1:
                    monthEN = "Jan";
                    break;
                case 2:
                    monthEN = "Feb";
                    break;
                case 3:
                    monthEN = "Mar";
                    break;
                case 4:
                    monthEN = "Apr";
                    break;
                case 5:
                    monthEN = "May";
                    break;
                case 6:
                    monthEN = "Jun";
                    break;
                case 7:
                    monthEN = "Jul";
                    break;
                case 8:
                    monthEN = "Aug";
                    break;
                case 9:
                    monthEN = "Sep";
                    break;
                case 10:
                    monthEN = "Oct";
                    break;
                case 11:
                    monthEN = "Nov";
                    break;
                case 12:
                    monthEN = "Dec";
                    break;
                default:
                    monthEN = "Jan";
                    break;
            }
            return monthEN;
        }

        /// <summary>
        /// AutoMated Tally search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //BindDataForRP(this.rpList, "1");
            BindData(this.rpList, "CONVERT(varchar(10), AddTime, 121)='" + this.ddlDays.SelectedValue + "'", " CAST(bak1 AS float)");

            if (this.rpList.Items.Count == 0)
            {
                this.panelRp.Visible = true;
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "<script>window.onload = function() { PageSetTag('tagContent0',0); };</script>", false);
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.rpList.DataSource = null;
            //this.rpList.DataBind();
            //this.panelRp.Visible = true;

            //if (this.ddlType.SelectedValue == "1")
            //{
            //    this.ltTitle.Text = "Automated Tally";
            //    this.panelTally.Visible = true;
            //    this.panelWeekly.Visible = false;
            //}
            //else if (this.ddlType.SelectedValue == "2")
            //{
            //    this.ltTitle.Text = "WEEKLY";
            //    this.panelTally.Visible = false;
            //    this.panelWeekly.Visible = true;
            //}
            //else
            //{
            //    this.ltTitle.Text = "MONTHLY";
            //}
        }

        public String ReturnHeader()
        {
            string header = string.Empty;
            //if (this.ddlType.SelectedValue == "2")
            //{
            //    header = "<td style=\"background:#118ebd;font-weight:bold; color:#000;\">Week</td>";
            //}
            return header;
        }

        public String ReturnWeek(string weekly)
        {
            StringBuilder sbRet = new StringBuilder();
            //if (this.ddlType.SelectedValue == "2")
            //{
            //    sbRet.Append("<td>");
            //    sbRet.Append(weekly);
            //    sbRet.Append("</td>");
            //}

            return sbRet.ToString();

        }

        public int RetunRows()
        {
            int rows = 4;
            //if (this.ddlType.SelectedValue == "2")
            //    rows = 5;

            return rows;
        }

        /// <summary>
        /// Weekly search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearchYear_Click(object sender, EventArgs e)
        {
            //BindDataForRP(this.rpYear, "2");
            if (this.ddlYears.SelectedValue != "")
            {
                //sbSel.Append(" AND CONVERT(varchar(4), bp.AddTime, 121)='" + this.ddlYears.SelectedValue + "'");
                BindData(this.rpYear, "CONVERT(varchar(4), AddTime, 121)='" + this.ddlYears.SelectedValue + "'", " weekly desc,  CAST(bak1 AS float)");
            }
            else
            {
                BindData(this.rpYear, "", " weekly desc,  CAST(bak1 AS float)");
            }

            if (this.rpYear.Items.Count == 0)
            {
                this.panelYear.Visible = true;
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "<script>window.onload = function() { PageSetTag('tagContent1',1); };</script>", false);
        }

        protected void btnSearchMonth_Click(object sender, EventArgs e)
        {
            this.ltMonthTally.Text = GetMonthlyTally();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "<script>window.onload = function() { PageSetTag('tagContent2',2); };</script>", false);
        }

        protected void btnSearchDayCharts_Click(object sender, EventArgs e)
        {
            InitChartsBar();
        }

        protected void InitChartsBar()
        {
            this.imgG.Visible = true;
            this.imgX.Visible = false;
            DataSet ds = ReturnData("CONVERT(varchar(10), AddTime, 121)='" + this.ddlDaysForCharts.SelectedValue + "'", " CAST(bak1 AS float)");
            DataTable table = ds.Tables[0];
            string xAxis = string.Empty;
            string dataHigh = string.Empty;
            string dataLow = string.Empty;
            double target = this.txtTarget.Text.Trim() == "" ? 0.05 : double.Parse(this.txtTarget.Text.Trim());
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string area = table.Rows[i]["Area"] == null || table.Rows[i]["Area"].ToString() == "" ? "Other" : table.Rows[i]["Area"].ToString();
                double pecent = Double.Parse(table.Rows[i]["PercentPD"].ToString());
                xAxis += "," + area;
                if (pecent > target)
                {
                    dataHigh += "," + pecent.ToString();
                    dataLow += ",0";
                    
                }
                else
                {
                    dataHigh += ",0";
                    dataLow += "," + pecent.ToString();
                }

                if (pecent > target && area == "Site")
                {
                    this.imgX.Visible = true;
                    this.imgG.Visible = false;
                }
            }
            this.hidBarxAxis.Value = xAxis.Substring(1);
            this.hidBarDataHigh.Value = dataHigh.Substring(1);
            this.hidBarDataLow.Value = dataLow.Substring(1);
        }
        protected DataSet ReturnData(string strWhere, string order)
        {

            StringBuilder sbSe = new StringBuilder();
            StringBuilder sbSel0 = new StringBuilder();
            StringBuilder sbSel1 = new StringBuilder();
            sbSel0.Append(" SELECT 'Site' as Area,sum(Assigned) as Assigned,sum(Due) as Due,cast((sum(Due)/sum(Assigned))*100 as decimal(18,2))as PercentPD ,Weekly,AddTime,0 as bak1 FROM BoardRollingTally where 1=1");
            sbSel1.Append(" SELECT Area,Assigned,Due,cast((Due/Assigned)*100 as decimal(18,2))as PercentPD ,Weekly,AddTime,bak1 FROM BoardRollingTally where 1=1");
            if (strWhere != "")
            {
                sbSel0.Append(" and " + strWhere);
                sbSel1.Append(" and " + strWhere);
            }
            sbSel0.Append(" group by Weekly,AddTime ");
            sbSe.Append("select Area,Assigned,Due, convert(varchar(20),PercentPD) as PercentPD,Weekly,AddTime from( " + sbSel0.ToString() + " union " + sbSel1.ToString() + ") temp order by " + order);

            DataSet ds = DBUtility.DbHelperSQL.Query(sbSe.ToString());
            return ds;
        }

        protected DataSet ReturnDataForOutPut(string strWhere, string order)
        {

            StringBuilder sbSe = new StringBuilder();
            StringBuilder sbSel0 = new StringBuilder();
            StringBuilder sbSel1 = new StringBuilder();
            sbSel0.Append(" select 'Site' as Area,sum(Assigned) as Assigned, sum(Due) as Due,cast((sum(Due)/sum(Assigned))*100 as decimal(18,2)) as PercentPD , Weekly,AddTime,0 as bak1  FROM BoardRollingTally where 1=1");
            sbSel1.Append(" SELECT Area,Assigned,Due,cast((Due/Assigned)*100 as decimal(18,2))as PercentPD ,Weekly,AddTime,bak1 FROM BoardRollingTally where 1=1");
            if (strWhere != "")
            {
                sbSel0.Append(" and " + strWhere);
                sbSel1.Append(" and " + strWhere);
            }
            sbSel0.Append(" group by Weekly,AddTime ");
            sbSe.Append("select Weekly,Area,Assigned,Due,convert(varchar(20),PercentPD) + '%' as PercentPD  ,AddTime from( " + sbSel0.ToString() + " union " + sbSel1.ToString() + ") temp order by " + order);

            DataSet ds = DBUtility.DbHelperSQL.Query(sbSe.ToString());
            return ds;
        }

        protected void InitLIne()
        {
            string lineData = string.Empty;
            for (int i = 1; i <= 12; i++)
            {
                string Week = GetMonthEN(i);
                string month = i.ToString().PadLeft(2, '0');
                string yearAndmonth = this.ddlYaerChart.SelectedValue + "-" + month;
                StringBuilder sbSel = new StringBuilder();
                sbSel.Append("SELECT SUM(Assigned) Assigned,SUM(Due) Due,convert(varchar(20),cast((SUM(Due)/SUM(Assigned)*100) as decimal(18,2))) + '%' as PercentPD  ,CONVERT(varchar(7), Addtime, 121) AS Years");
                sbSel.Append(" FROM BoardRollingTally");
                sbSel.Append(" WHERE CONVERT(varchar(7), Addtime, 121)='" + yearAndmonth + "'");
                sbSel.Append(" group by CONVERT(varchar(7), Addtime, 121)");
                sbSel.Append(" order by CONVERT(varchar(7), Addtime, 121)");
                DataSet ds = DBUtility.DbHelperSQL.Query(sbSel.ToString());
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable table = ds.Tables[0];
                    string PercentPD = table.Rows[0]["PercentPD"] == null || table.Rows[0]["PercentPD"].ToString() == "" ? "0" : table.Rows[0]["PercentPD"].ToString();
                    lineData += "," + PercentPD ;
                }
                else
                {
                    lineData += ",0";
                }
            }
            this.hidLineData.Value = lineData.Substring(1);
        }

        protected void btnSearchYearChart_Click(object sender, EventArgs e)
        {
            InitLIne();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            DataSet ds = ReturnDataForOutPut("CONVERT(varchar(10), AddTime, 121)='" + this.ddlDays.SelectedValue + "'", " CAST(bak1 AS float)");
            Export("Rolling STJ totals", ds.Tables[0], "Rolling STJ totals(" + this.ddlDays.SelectedValue + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "<script>window.onload = function() { PageSetTag('tagContent0',0); };</script>", false);
        }

        private void Export(string Exceltitle, DataTable table, string title)
        {
            int tRowCount = table.Rows.Count;
            int tColumnCount = table.Columns.Count;

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "gb2312";  // gb2312、utf-8、ISO8859-1(Latin-1)  
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(Exceltitle + ".xls", Encoding.UTF8));
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");//设置输出流为简体中文  
            Response.ContentType = "application/msExcel";//设置输出文件类型为excel文件。   
            Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
            //span style = "background-color: rgb(255, 255, 153);" >
            Response.Write("<style>.border  { border:.5pt solid black; }</style>");//添加样式</span>  
            this.EnableViewState = true;
            System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            //tb.RenderControl(oHtmlTextWriter);//将服务器控件的内容输出  
            Response.Write(oStringWriter.ToString());
            Response.Write("<Table style=\"text-align:center;font-family:宋体; font-size:11pt;border-bottom-color: black; border-top-color: black; width: 1350px; color: #000000; border-right-color: black;  border-left-color: black;\" border=\"1\">");
            //首行
            Response.Write("\n <TR>");
            //colspan=
            Response.Write("\n <TD colspan=" + tColumnCount + " style=\" height:35px;text-align:center;font-family:宋体; font-weight:bold;font-size:18pt; border:1px solid  #000000;background: #118ebd;\">");
            Response.Write(title);
            Response.Write("\n </TD>");
            Response.Write("\n </TR>");
            //详细行

            #region Header
            Response.Write("\n <TR>");
            for (int i = 0; i < tColumnCount; i++)
            {
                Response.Write("\n <TD style=\"background: #118ebd; font-weight: bold; color: #000;\">");
                Response.Write(table.Columns[i].ColumnName);
                Response.Write("\n </TD>");
            }
            Response.Write("\n </TR>");
            #endregion
            #region contents
            for (int j = 0; j < tRowCount; j++)
            {
                Response.Write("\n <TR>");
                for (int k = 0; k < tColumnCount; k++)
                {

                    Response.Write("\n <TD style=\" height:30px;width:120px;text-align:center;font-family:宋体; \">");

                    Response.Write(table.Rows[j][k].ToString());

                    Response.Write("\n </TD>");
                }
                Response.Write("\n </TR>");
            }
            #endregion
            Response.Write("</Table>");
            Response.End();
        }

        protected void btnExportYear_Click(object sender, EventArgs e)
        {
            StringBuilder sbSe = new StringBuilder();
            StringBuilder sbSe0 = new StringBuilder();
            StringBuilder sbSel = new StringBuilder();
            sbSe0.Append(" SELECT 'Site' as Area,sum(Assigned) as Assigned,sum(Due) as Due,cast((sum(Due)/sum(Assigned))*100 as decimal(18,2))as PercentPD ,Weekly,AddTime,0 as bak1 FROM BoardRollingTally where 1=1");
            sbSel.Append(" SELECT Area,Assigned,Due,cast((Due/Assigned)*100 as decimal(18,2))as PercentPD ,Weekly,AddTime,bak1 FROM BoardRollingTally where 1=1");
            if (this.ddlYears.SelectedValue != "")
            {
                sbSe0.Append(" and CONVERT(varchar(4), AddTime, 121)='" + this.ddlYears.SelectedValue + "'");
                sbSel.Append(" and CONVERT(varchar(4), AddTime, 121)='" + this.ddlYears.SelectedValue + "'");
            }
            sbSe0.Append(" group by Weekly,AddTime ");
            sbSe.Append("select Weekly,Area,Assigned,Due,convert(varchar(20),PercentPD) + '%' as PercentPD from( " + sbSe0.ToString() + " union " + sbSel.ToString() + ") temp order by Weekly desc,bak1");


            DataSet ds = DBUtility.DbHelperSQL.Query(sbSe.ToString());

            
            //DataSet ds = null;
            //if (this.ddlYears.SelectedValue != "")
            //{
            //    ds = ReturnData("CONVERT(varchar(4), AddTime, 121)='" + this.ddlYears.SelectedValue + "'", " weekly desc,  Area");
            //}
            //else
            //{
            //    ds = ReturnData("", " weekly desc,  Area");
            //}
            //DataSet ds = ReturnData("CONVERT(varchar(4), AddTime, 121)='" + this.ddlYears.SelectedValue + "'", " weekly desc,  Area");
            Export("Weekly", ds.Tables[0], "WEEKLY(" + this.ddlYears.SelectedItem.Text + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "<script>window.onload = function() { PageSetTag('tagContent1',1); };</script>", false);
        }


        private void ExportMonthly(string Exceltitle, string title)
        {
            string table = GetMonthlyTally();
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "gb2312";  // gb2312、utf-8、ISO8859-1(Latin-1)  
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(Exceltitle + ".xls", Encoding.UTF8));
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");//设置输出流为简体中文  
            Response.ContentType = "application/msExcel";//设置输出文件类型为excel文件。   
            Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
            //span style = "background-color: rgb(255, 255, 153);" >
            Response.Write("<style>.border  { border:.5pt solid black; }</style>");//添加样式</span>  
            this.EnableViewState = true;
            System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            //tb.RenderControl(oHtmlTextWriter);//将服务器控件的内容输出  
            Response.Write(oStringWriter.ToString());
            Response.Write("<Table style=\"text-align:center;font-family:宋体; font-size:11pt;border-bottom-color: black; border-top-color: black; width: 1350px; color: #000000; border-right-color: black;  border-left-color: black;\" border=\"1\">");
            //首行
            Response.Write("\n <TR>");
            Response.Write("\n <TD colspan=" + 4 + " style=\" height:35px;text-align:center;font-family:宋体; font-weight:bold;font-size:18pt; border:1px solid  #000000;background: #118ebd;\">");
            Response.Write(title);
            Response.Write("\n </TD>");
            Response.Write("\n </TR>");
            //详细行

            #region Header
            Response.Write("\n <TR>");
            Response.Write("\n <TD style=\"background: #118ebd; font-weight: bold; color: #000;\">Week");
            Response.Write("\n </TD>");
            Response.Write("\n <TD style=\"background: #118ebd; font-weight: bold; color: #000;\">Assigned");
            Response.Write("\n </TD>");
            Response.Write("\n <TD style=\"background: #118ebd; font-weight: bold; color: #000;\">Due");
            Response.Write("\n </TD>");
            Response.Write("\n <TD style=\"background: #118ebd; font-weight: bold; color: #000;\">Percent PD");
            Response.Write("\n </TD>");
            Response.Write("\n </TR>");
            #endregion
            #region contents
            Response.Write(table);
            #endregion
            Response.Write("</Table>");
            Response.End();
        }

        protected void btnExportMonth_Click(object sender, EventArgs e)
        {
            ExportMonthly("Monthly", "MONTHLY(" + this.ddlYearForMonth.SelectedItem.Text + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "<script>window.onload = function() { PageSetTag('tagContent2',2); };</script>", false);
        }

        protected void btnSet_Click(object sender, EventArgs e)
        {
            this.ltData.Text = this.txtUpdated.Text;
            this.ltUser.Text = this.txtChampion.Text;
            this.ltTarget.Text = this.txtTarget.Text;
            this.ltUpOn.Text = this.txtUpOn.Text;
            this.ltUpUser.Text = this.txtUpby.Text;
            this.ltNote.Text = this.txtNote.Text;
            InitChartsBar();

            StringBuilder sbSel = new StringBuilder();
            sbSel.Append("select top 1 ID from BoardRollingDBParameter order by AddTime DESC");
            DataSet ds = DBUtility.DbHelperSQL.Query(sbSel.ToString());
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string id = ds.Tables[0].Rows[0]["ID"].ToString();
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update BoardRollingDBParameter set ");
                strSql.Append("UpdateWeek=@UpdateWeek,");
                strSql.Append("Champion=@Champion,");
                strSql.Append("Target=@Target,");
                strSql.Append("UpdatedOn=@UpdatedOn,");
                strSql.Append("UpdatedBy=@UpdatedBy,");
                strSql.Append("Note=@Note");
                strSql.Append(" where ID=@ID ");
                SqlParameter[] parameters = {
					new SqlParameter("@UpdateWeek", SqlDbType.NVarChar,500),
					new SqlParameter("@Champion", SqlDbType.NVarChar,500),
					new SqlParameter("@Target", SqlDbType.NVarChar,500),
					new SqlParameter("@UpdatedOn", SqlDbType.NVarChar,500),
					new SqlParameter("@UpdatedBy", SqlDbType.NVarChar,500),
					new SqlParameter("@Note", SqlDbType.NVarChar,500),
					new SqlParameter("@ID", SqlDbType.NVarChar,50)};
                parameters[0].Value = this.txtUpdated.Text;
                parameters[1].Value = this.txtChampion.Text;
                parameters[2].Value = this.txtTarget.Text;
                parameters[3].Value = this.txtUpOn.Text;
                parameters[4].Value = this.txtUpby.Text;
                parameters[5].Value = this.txtNote.Text;
                parameters[6].Value = id;

                int rows = DBUtility.DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            }
            else
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into BoardRollingDBParameter(");
                strSql.Append("ID,UpdateWeek,Champion,Target,UpdatedOn,UpdatedBy,Note,AddTime)");
                strSql.Append(" values (");
                strSql.Append("@ID,@UpdateWeek,@Champion,@Target,@UpdatedOn,@UpdatedBy,@Note,@AddTime)");
                SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateWeek", SqlDbType.NVarChar,500),
					new SqlParameter("@Champion", SqlDbType.NVarChar,500),
					new SqlParameter("@Target", SqlDbType.NVarChar,500),
					new SqlParameter("@UpdatedOn", SqlDbType.NVarChar,500),
					new SqlParameter("@UpdatedBy", SqlDbType.NVarChar,500),
					new SqlParameter("@Note", SqlDbType.NVarChar,500),
					new SqlParameter("@AddTime", SqlDbType.DateTime)};
                parameters[0].Value = Guid.NewGuid().ToString();
                parameters[1].Value = this.txtUpdated.Text;
                parameters[2].Value = this.txtChampion.Text;
                parameters[3].Value = this.txtTarget.Text;
                parameters[4].Value = this.txtUpOn.Text;
                parameters[5].Value = this.txtUpby.Text;
                parameters[6].Value = this.txtNote.Text;

                int rows = DBUtility.DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            }
            this.windowbg.Style["display"] = "none";
            this.divAddUser.Style["display"] = "none";
        }

        protected void btnBack_Click(object sender, EventArgs e)//返回
        {
            Response.Write(" <script> top.location.href= '/guide.aspx'; </script> ");

        }
    }
}