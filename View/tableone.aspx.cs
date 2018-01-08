using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using DBUtility;
using System.IO;
using System.Data.OleDb;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using Eday;
using System.Net;
using System.Text;

namespace MatrixTool.View
{
    public partial class tableone : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string SQL0 = "select top 1 * from missingReport order by createTime asc";
                DataSet dt0 = DbHelperSQL.Query(SQL0);
                this.tbdate.Text = DateTime.Parse(dt0.Tables[0].Rows[0]["ISOTrainTime"].ToString()).ToShortDateString();
                string SQL1 = "select top 1 Stime from EmployeebySupervisor";
                DataSet dt1 = DbHelperSQL.Query(SQL1);
                if (dt1.Tables[0].Rows.Count > 0)
                {
                    this.Label1.Text = dt1.Tables[0].Rows[0]["Stime"].ToString();
                    this.Label2.Text = dt1.Tables[0].Rows[0]["Stime"].ToString();
                    dbm();
                    string xiao = this.DropDownList1.SelectedItem.Text;
                    xiaobumen(xiao);
                    
                }
                else
                {
                    this.Label1.Text = "--";
                    this.Label2.Text = "--";
                    Label5.Visible = true;
                }
            }

        }
        private void dbm()// 大部门
        {
            string SQL = "select distinct Area from vlookupfortraining";
            DataSet dt = DbHelperSQL.Query(SQL);
            this.DropDownList1.DataSource = dt;
            this.DropDownList1.DataTextField = "Area";
            this.DropDownList1.DataValueField = "Area";
            this.DropDownList1.DataBind();
            //2017-12-12 杜秋菊
            DropDownList1.Items.Insert(0,new ListItem("--Area--", "0"));
            //end
        }
        private void other()// 其他部门
        {
            string SQL2 = "select DEPT_CODE,DESCRIPTION from View_1 order by DESCRIPTION asc";
            DataSet dt2 = DbHelperSQL.Query(SQL2);
            this.cbDept.DataSource = dt2;
            this.cbDept.DataTextField = "DESCRIPTION";
            this.cbDept.DataValueField = "DEPT_CODE";
            this.cbDept.DataBind();
        }

        private void xl3() //具体部门区分人员
        {

            string SQL = "select distinct TaskCode from missingReport";
            DataSet dt = DbHelperSQL.Query(SQL);
            this.DropDownList3.DataSource = dt;
            this.DropDownList3.DataTextField = "TaskCode";
            this.DropDownList3.DataValueField = "TaskCode";
            this.DropDownList3.DataBind();
            string thankscold = this.DropDownList3.SelectedItem.Text;
            rs1(thankscold);

        }
        public void xiaobumen(string xiao)//小部门
        {
            string sql = "select distinct Area,deptcode,DEPT_DESC from vlookupfortraining where Area='" + xiao + "' and deptcode is not null order by DEPT_DESC asc";
            DataSet dt = DbHelperSQL.Query(sql);
            this.cbDept.DataSource = dt;
            this.cbDept.DataTextField = "DEPT_DESC";
            this.cbDept.DataValueField = "deptcode";
            this.cbDept.DataBind();
            //rs(bianhao);
            //xl3();
        }

        public void rs()//第一个echars图的数据
        {

            string data1 = "";
            string data2 = "";
            string data3 = "";
            string data4 = "";

            string wherestr = " 1=1 ";
            labdept.Text = "";

            for (int i = 0; i < cbDept.Items.Count; i++)
            {
                if (cbDept.Items[i].Selected)
                {
                    if (wherestr == " 1=1 ")
                    {
                        wherestr = wherestr + " and ( EmployeebySupervisor.DEPT_CODE=N'" + cbDept.Items[i].Value + "' ";
                        labdept.Text = labdept.Text + cbDept.Items[i].Text;
                    }
                    else
                    {
                        wherestr = wherestr + "or EmployeebySupervisor.DEPT_CODE=N'" + cbDept.Items[i].Value + "' ";
                        labdept.Text = labdept.Text + "," + cbDept.Items[i].Text;
                    }
                }

            }

            if (wherestr != " 1=1 ")
            {
                wherestr = wherestr + ")";
            }
            else
            {
                wherestr = " 1=0";
            }

            string sql = "SELECT distinct missingReport.CourseCode,missingReport.userID,missingReport.DueDate,EmployeebySupervisor.DEPT_CODE from missingReport INNER JOIN  EmployeebySupervisor on missingReport.userID=EmployeebySupervisor.EMP_ID where " + wherestr + " order by userID";
            DataSet dss = DbHelperSQL.Query(sql);

            int count1 = 0;
            int count2 = 0;
            int count3 = 0;
            string user = string.Empty;
            string name = string.Empty;
            string nameold = string.Empty;

            for (int i = 0; i < dss.Tables[0].Rows.Count; i++)
            {
                name = dss.Tables[0].Rows[i]["userID"].ToString();
                DateTime DueDate = DateTime.Parse(dss.Tables[0].Rows[i]["DueDate"].ToString());

                if (user != name)
                {

                    if (user != string.Empty)
                    {
                        data1 += nameold + ",";
                        data2 += count1.ToString() + ",";
                        data3 += count2.ToString() + ",";
                        data4 += count3.ToString() + ",";
                    }

                    count1 = 0;
                    count2 = 0;
                    count3 = 0;
                    user = name;
                }

                nameold = name;

                string temp = string.Empty;
                DateTime aa = DateTime.Parse(this.tbdate.Text);
                if (DueDate < aa)
                {
                    temp = "Past Due";
                    count1 = count1 + 1;
                }
                else
                {
                    if (DueDate < aa.AddDays(7))
                    {
                        temp = "< 7 Days till due";
                        count2 = count2 + 1;
                    }
                    else
                    {
                        if (DueDate > aa)
                        {
                            temp = "> 7 Days till due";
                            count3 = count3 + 1;
                        }
                    }
                }

            }

            if (dss.Tables[0].Rows.Count > 0)
            {
                data1 += dss.Tables[0].Rows[dss.Tables[0].Rows.Count-1]["userID"].ToString();
                data2 += count1.ToString();
                data3 += count2.ToString();
                data4 += count3.ToString();

            }

            this.hddata1.Value = data1;
            this.hddata2.Value = data2;
            this.hddata3.Value = data3;
            this.hddata4.Value = data4;
        }

        protected void DropDownList1_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (this.DropDownList1.SelectedItem.Text == "Area")
            {
                other();
            }
            else
            {
                string xiao = this.DropDownList1.SelectedItem.Text;
                xiaobumen(xiao);
            }

        }//大部门的触发事件
        public void rs1(string TASK_CODE)
        {
            string data1 = "";
            string data2 = "";
            string data3 = "";
            string data4 = "";
            string wherestr = " 1=1 ";
            labdept.Text = "";

            for (int i = 0; i < cbDept.Items.Count; i++)
            {
                if (cbDept.Items[i].Selected)
                {
                    if (wherestr == " 1=1 ")
                    {
                        wherestr = wherestr +  " and ( EmployeebySupervisor.DEPT_CODE=N'" + cbDept.Items[i].Value + "' ";
                        labdept.Text = labdept.Text + cbDept.Items[i].Text;
                    }
                    else
                    {
                        wherestr = wherestr + "or EmployeebySupervisor.DEPT_CODE=N'" + cbDept.Items[i].Value + "' ";
                        labdept.Text = labdept.Text + "," + cbDept.Items[i].Text;
                    }
                }
                    
            }

            if (wherestr != " 1=1 ")
            {
                wherestr = wherestr + ")";

            }
            else
            {
                wherestr = " 1=0";
            }

            string sql = "SELECT distinct missingReport.TaskCode,missingReport.CourseCode,missingReport.userID,missingReport.DueDate,EmployeebySupervisor.DEPT_CODE from missingReport INNER JOIN  EmployeebySupervisor on missingReport.userID=EmployeebySupervisor.EMP_ID where " + wherestr + " and missingReport.TaskCode='" + TASK_CODE + "' order by userID";
            DataSet dss = DbHelperSQL.Query(sql);

            int count1 = 0;
            int count2 = 0;
            int count3 = 0;
            string user = string.Empty;
            string name = string.Empty;
            string nameold = string.Empty;

            for (int i = 0; i < dss.Tables[0].Rows.Count; i++)
            {
                name = dss.Tables[0].Rows[i]["userID"].ToString();
                DateTime DueDate = DateTime.Parse(dss.Tables[0].Rows[i]["DueDate"].ToString());

                if (user != name)
                {

                    if (user != string.Empty)
                    {
                        data1 += nameold + ",";
                        data2 += count1.ToString() + ",";
                        data3 += count2.ToString() + ",";
                        data4 += count3.ToString() + ",";
                    }

                    count1 = 0;
                    count2 = 0;
                    count3 = 0;
                    user = name;
                }

                nameold = name;
                string temp = string.Empty;
                DateTime aa = DateTime.Parse(this.tbdate.Text);
                if (DueDate < aa)
                {
                    temp = "Past Due";
                    count1 = count1 + 1;
                }
                else
                {
                    if (DueDate < aa.AddDays(7))
                    {
                        temp = "< 7 Days till due";
                        count2 = count2 + 1;
                    }
                    else
                    {
                        if (DueDate > aa)
                        {
                            temp = "> 7 Days till due";
                            count3 = count3 + 1;
                        }
                    }
                }

            }

            if (dss.Tables[0].Rows.Count > 0)
            {
                data1 += dss.Tables[0].Rows[dss.Tables[0].Rows.Count - 1]["userID"].ToString();
                data2 += count1.ToString();
                data3 += count2.ToString();
                data4 += count3.ToString();

            }

            this.hddata5.Value = data1;
            this.hddata6.Value = data2;
            this.hddata7.Value = data3;
            this.hddata8.Value = data4;
        }//第二个图

        protected void Button31_Click(object sender, EventArgs e)
        {
            DataSet ds = getds("EmployeebySupervisor");
            Export("EmployeebySupervisor", ds.Tables[0], "EmployeebySupervisor");
        }

        protected void Button32_Click(object sender, EventArgs e)
        {
            DataSet ds = getds1("missingReport");
            Export("MissingReport", ds.Tables[0], "MissingReport");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            HSSFWorkbook workBook = new HSSFWorkbook();
            try
            {
                
                //ISheet sheetA = workBook.CreateSheet("sheetA");
                //ISheet sheetB = workBook.CreateSheet("sheetB");

                createSheet(workBook, "EmployeebySupervisor", getds("EmployeebySupervisor"));
                createSheet(workBook, "missingReport", getds1("missingReport"));
            }
            catch(Exception ex)
            {
                string exstr = ex.Message;

            }
            string path = Server.MapPath(@"\View\test.xls");


            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (FileStream file = new FileStream(path, FileMode.Create))
            {
                workBook.Write(file);　　//创建Excel文件。
                file.Close();
            }
            lc(path);
        }//导出（设置几个sheet表）
        private DataSet getds(string tablename)
        {

            string sql = "SELECT distinct DEPT_CODE,DESCRIPTION,EMP_ID,EMPLOYEE_NAME,SUP_NAME,SERVER_TIME,Stime from   " + tablename + "";
            DataSet dss = DbHelperSQL.Query(sql);
            return dss;
        }//导出（都哪些字段）
        private DataSet getds1(string tablename)
        {
            DateTime dq = DateTime.Parse(this.tbdate.Text);
            DateTime dqj = dq.AddDays(7);
            //string sql = "SELECT  CourseCode，TaskCode，Description，Status，DueDate，SuperVisorCode  from  " + tablename + "";
            string sql = "SELECT (case  when missingReport.DueDate <'" + dq + "'then 'Past Due' when missingReport.DueDate ='" + dqj + "'then'< 7 Days till due'when missingReport.DueDate>'" + dq + "'then'>=7 Days till due'end),missingReport.CourseCode,missingReport.userID,missingReport.TaskCode,missingReport.Description,missingReport.Status,missingReport.DueDate,missingReport.SuperVisorCode ,EmployeebySupervisor.DEPT_CODE,hr_info.costCenterArea from missingReport left join   EmployeebySupervisor on missingReport.userID=EmployeebySupervisor.EMP_ID left join hr_info on missingReport.userID=hr_info.userID";
            //string sql = "SELECT   missingReport.CourseCode,missingReport.userID,missingReport.TaskCode,missingReport.Description,missingReport.Status,missingReport.DueDate,missingReport.SuperVisorCode ,EmployeebySupervisor.DEPT_CODE,hr_info.costCenterArea from " + tablename + " left join   EmployeebySupervisor on missingReport.userID=EmployeebySupervisor.EMP_ID left join hr_info on missingReport.userID=hr_info.userID";
            DataSet dss = DbHelperSQL.Query(sql);
            return dss;
        }//导出字段（判断是否过期）


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

        public void lc(string path)
        {
            Encoding code = Encoding.GetEncoding("gb2312");
            // 读取模板文件 
            Response.ContentType = "application/x-zip-compressed";
            Response.AddHeader("Content-Disposition", "attachment;filename=z.xls");
            string filename = Server.MapPath("./test.xls");
            Response.TransmitFile(filename);
        }
        private ISheet createSheet(HSSFWorkbook workBook, string sheetName, DataSet dss)
        {
            ISheet sheet = workBook.CreateSheet(sheetName);//
            IRow RowHead = sheet.CreateRow(0);


            try
            {
                for (int iColumnIndex = 0; iColumnIndex < dss.Tables[0].Columns.Count; iColumnIndex++)// 0 , dataset.table[0]row[0].counts,i++
                {

                    string aa = dss.Tables[0].Columns[iColumnIndex].ColumnName.ToString();
                    RowHead.CreateCell(iColumnIndex).SetCellValue(aa);  //dataset.table[0]row[0][i].value .tostring
                }

                for (int iRowIndex = 0; iRowIndex < dss.Tables[0].Rows.Count; iRowIndex++)//3的位置是dataset总行数dss.Tables[0].Rows.Count
                    //for (int iRowIndex = 0; iRowIndex < 500; iRowIndex++)//3的位置是dataset总行数dss.Tables[0].Rows.Count
                {

                    IRow RowBody = sheet.CreateRow(iRowIndex + 1);//createRow创建行，参数的意思是第几行，索引下标从0开始
                    for (int iColumnIndex = 0; iColumnIndex < dss.Tables[0].Columns.Count; iColumnIndex++)
                    {
                        string aaa = dss.Tables[0].Rows[iRowIndex][iColumnIndex].ToString();
                        RowBody.CreateCell(iColumnIndex).SetCellValue(aaa);
                        sheet.AutoSizeColumn(iColumnIndex);//AutoSizeColumn（column）；column是某一列，意思是设置column这一列为自动调整列宽；
                    }
                }
            }
            catch (Exception ex)
            {
                string exstr = ex.Message;

            }
            return sheet;
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string thankscold = this.DropDownList3.SelectedItem.Text;
            rs1(thankscold);
        }

        protected void Button1_Click(object sender, EventArgs e)//返回
        {
            Response.Write(" <script> top.location.href= '/guide.aspx'; </script> ");

        }

        protected void btnshow_Click(object sender, EventArgs e)
        {
            rs();
            xl3();
        }

        protected void btnall_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cbDept.Items.Count; i++)
            {
                cbDept.Items[i].Selected = true;
            }


        }

        protected void btnclear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cbDept.Items.Count; i++)
            {
                cbDept.Items[i].Selected = false;
            }
        }



    }
}