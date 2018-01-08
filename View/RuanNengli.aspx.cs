using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBUtility;
using System.Data;
using System.IO;

namespace MatrixTool.View
{
    public partial class RuanNengli : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userid"] == null || Session["userid"].ToString() == "")
                {
                    Response.Write(" <script> top.location.href= '/login.aspx '; </script> ");
                    return;
                }
                else
                {
                    hidUserID.Value = Session["userid"].ToString();
                    DataTable DateEmployee = new DataTable();
                    DataTable DateTable = new DataTable();
                    //获取LM下属员工及其关联的JF、Course
                    bool isOK = GetDate(ref DateEmployee, ref DateTable);
                    if (isOK)
                    {
                        IniPage(DateEmployee, DateTable);
                    }
                }
            }
        }

        void IniPage(DataTable DateEmployee, DataTable DateTable)
        {
            string strForExcel = "<table border='1'><tr><td>课程名称/Course Name</td>";
            //将数据写如Table
            #region Rightheader 横向表头
            string tbHeader = string.Empty;
            tbHeader = "<tr>";

            for (int m = 0; m < DateEmployee.Rows.Count; m++)
            {
                string tdStr = "<td class=\"data_td\"><div class=\"datagrid-title\"><span>" + DateEmployee.Rows[m]["userid"].ToString() + "</span></div></td>";
                tbHeader += tdStr;
                strForExcel += tdStr;
            }

            tbHeader = tbHeader + "</tr>";
            this.talbeRightHead.Text = tbHeader;
            strForExcel += "</tr>";
            #endregion

            #region LeftBody
            string LeftJF = "";
            string rightBody = "";
            for (int n = 0; n < DateTable.Rows.Count; n++)
            {
                string CourseCode = DateTable.Rows[n]["CourseCode"].ToString().Trim();
                LeftJF = LeftJF + "<tr>";
                LeftJF = LeftJF + "<td><div class=\"datagrid-cell\">" + CourseCode + "</div></td></tr>";

                strForExcel = strForExcel + "<td><div class=\"datagrid-cell\">" + CourseCode + "</div></td>";


                rightBody = rightBody + "<tr>";
                for (int k = 2; k < DateTable.Columns.Count; k++)
                {
                    string BackGroundColor = "";
                    string Flag = DateTable.Rows[n][k].ToString();
                    if (Flag == null || Flag=="")
                    {
                        BackGroundColor = Common.Common.Notrequired2;
                    }
                    else if (Flag.Trim() == "0")
                    {
                        BackGroundColor = Common.Common.Nottrained0;
                    }
                    else
                    {
                        try
                        {
                            DateTime time = DateTime.Parse(Flag);
                            BackGroundColor = Common.Common.Trained3;
                        }
                        catch (Exception)
                        {
                           
                        }
                    }
                    rightBody = rightBody + "<td " + BackGroundColor + " ><div class=\"datagrid-cell\">" + Flag + "</div></td>";
                    strForExcel = strForExcel + "<td " + BackGroundColor + " ><div class=\"datagrid-cell\">" + Flag + "</div></td>";
                }
                rightBody = rightBody + "</tr>";
                strForExcel = strForExcel + "</tr>";
            }
          

            tableLeftJF.Text = LeftJF;
            talbeRightBody.Text = rightBody;
            LabForExcel.Text = strForExcel;
            #endregion
        }

        //
        bool GetDate(ref DataTable dsEmployee, ref DataTable dsTabel)
        {
            string LMofEmployee = "";
            string strEmployee = "select userID  from hr_info where director='" + hidUserID.Value + "' AND  status!='Desert'";
            string EmType = "";
            if (Session["userid"] == null || Session["userid"].ToString() == "")
            {
                return false;
            }
            else
            {
                EmType = Session["logType"].ToString().Trim();
            }
            if (EmType == "0") //LM 登录
            {
                strEmployee = "select userID  from hr_info where director='" + hidUserID.Value + "' AND  status!='Desert'";
            }
            else if (EmType == "1") //Empoyee 
            {
                string strLMofEmployee = "select director  from hr_info where userID='" + hidUserID.Value + "'";
                DataTable dt = DbHelperSQL.Query(strLMofEmployee).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    LMofEmployee = dt.Rows[0]["director"].ToString();
                    strEmployee = "select userID  from hr_info where director='" + LMofEmployee + "' AND  status!='Desert'";
                }
                else
                {
                    return false;
                }
            }

            dsEmployee = DbHelperSQL.Query(strEmployee).Tables[0];

            string strFind = "select newid() as id ,  AllCourse.CourseCode";

            if (dsEmployee.Rows.Count > 0)
            {
                for (int m = 0; m < dsEmployee.Rows.Count; m++)
                {
                    strFind += " ,(case  when fd" + (m + 1).ToString() + ".Status=1  then CONVERT(varchar(100), fd" + (m + 1).ToString() + ".CourseTime, 23)  else   fd" + (m + 1).ToString() +".Status  end ) as '" + dsEmployee.Rows[m]["userID"].ToString() + "'";

                }
            }

            strFind += " from(";
            strFind += " select   distinct a.CourseCode From QualificationTable as a";
           
            if (EmType == "0") //LM 登录
            {
                strFind += " inner join   hr_info  as b  on b.director='" + hidUserID.Value + "' AND a.EmployeeID=b.userID and b.status!='Desert'";
            }
            else if (EmType == "1") //Empoyee 
            {
                strFind += " inner join   hr_info  as b  on b.director='" + LMofEmployee + "' AND a.EmployeeID=b.userID and b.status!='Desert'";
            }
            strFind += " ) as AllCourse";
   
            if (dsEmployee.Rows.Count > 0)
            {
                for (int m = 0; m < dsEmployee.Rows.Count; m++)
                {
                    strFind += " left join QualificationTable fd" + (m + 1).ToString() + " on fd" + (m + 1).ToString() + ".EmployeeID='" + dsEmployee.Rows[m]["userID"].ToString() + "'  and fd" 
                        + (m + 1).ToString() + ".CourseCode=AllCourse.CourseCode ";
                }
            }

            strFind += " order by AllCourse.CourseCode ";
            dsTabel = DbHelperSQL.Query(strFind).Tables[0];
            return true;

        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            string fileName = "GeneralCompetence .xls";

            Response.Charset = "GB2312";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");


            //string style = @"<style> .text { mso-number-format:d\-mmm\-yyyy; } </script> ";
            string styleRG = "<meta http-equiv=\"content-type\' content=\"application/ms-excel; charset=gb2312\"/>";
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            LabForExcel.RenderControl(htw);
            Response.Write(styleRG);
            Response.Write(@"<style>.tb.td{border-right:solid 1px red;}</style> ");
            Response.Write(sw.ToString());
            Response.End();
        }

        protected void ID_Back_Click(object sender, EventArgs e)
        {
            Response.Write(" <script> top.location.href= '/guide.aspx '; </script> ");
            return;
        }
    }
}