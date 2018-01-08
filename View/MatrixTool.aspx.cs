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
    public partial class MatrixTool : PageBase
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
                    DataTable DateEmployee = new DataTable();
                    DataTable DateTable = new DataTable();

                    hidUserID.Value = Session["userid"].ToString();
                    string EmType = Session["logType"].ToString().Trim();

                    //获取LM下属员工及其关联的JF、Course
                    bool isOK=GetDate(ref DateEmployee, ref DateTable);
                    if (isOK)
                    {
                        IniPage(DateEmployee, DateTable);
                    }
                     
                   
                }
            }
        }

        //根据数据绘制表格
        void IniPage(DataTable DateEmployee, DataTable DateTable)
        {
            string strForExcel = "<table  border='1'><tr><td>工作组编号/Job Function Code</td><td>工作组描述/Job Function Name </td><td>课程号/Course Code </td><td>课程描述/Course Description </td>";
            //将数据写如Table
            #region Rightheader 横向表头
            string tbHeader = string.Empty;
            tbHeader = "<tr>";
           
            for (int m = 0; m < DateEmployee.Rows.Count; m++)
            {
                string tdStr= "<td class=\"data_td\"><div class=\"datagrid-title\"><span>" + DateEmployee.Rows[m]["userid"].ToString() + "</span></div></td>";
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
                string jobCode = DateTable.Rows[n]["code"].ToString().Trim();
                string jobName = DateTable.Rows[n]["name"].ToString().Trim();
                string CourseCode = DateTable.Rows[n]["CourseCode"].ToString().Trim();
                string CourseDesc = DateTable.Rows[n]["CourseTitleDescription"].ToString().Trim();
                LeftJF = LeftJF + "<tr>";
                LeftJF = LeftJF + "<td><div class=\"datagrid-cell\">" + jobCode + "</div></td>";
                LeftJF = LeftJF + "<td><div class=\"datagrid-cell\">" + CourseCode + "</div></td></tr>";

                strForExcel=strForExcel + "<td><div class=\"datagrid-cell\">" + jobCode + "</div></td>";
                strForExcel = strForExcel + "<td><div class=\"datagrid-cell\">" + jobName + "</div></td>";
                strForExcel = strForExcel + "<td><div class=\"datagrid-cell\">" + CourseCode + "</div></td>";
                strForExcel = strForExcel + "<td><div class=\"datagrid-cell\">" + CourseDesc + "</div></td>";


                rightBody = rightBody + "<tr>";
                for (int k = 5; k < DateTable.Columns.Count; k++)
                {
                    int Flag = int.Parse(DateTable.Rows[n][k].ToString().Trim());
                    string strFlag = "";
                    string BackGroundColor="";
                    switch(Flag)
                    {
                       case 0:
                          BackGroundColor=Common.Common.Nottrained0;
                          strFlag = Flag.ToString();
                          break;
                       case 2:
                          BackGroundColor=Common.Common.Notrequired2;
                          strFlag = "";
                          break;
                       case 1:
                          BackGroundColor=Common.Common.Trained3;
                          strFlag = Flag.ToString();
                          break;

                    }
                    rightBody = rightBody + "<td " + BackGroundColor + " ><div class=\"datagrid-cell\">" + strFlag + "</div></td>";
                    strForExcel = strForExcel + "<td " + BackGroundColor + " ><div class=\"datagrid-cell\">" + strFlag + "</div></td>";
                }
                rightBody = rightBody + "</tr>";
                strForExcel = strForExcel + "</tr>";
            }
            strForExcel+= "</table>";

            tableLeftJF.Text = LeftJF;
            talbeRightBody.Text =rightBody;
            LabForExcel.Text = strForExcel;
            #endregion 
        }

        //获取LM下属数据
        bool  GetDate(ref DataTable dsEmployee, ref DataTable dsTabel)
        {
            string LMofEmployee = "";
            string strEmployee = "select userID  from hr_info where director='"+hidUserID.Value +"' AND  status!='Desert'";
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

            string strFind = "select  AllJF.code,ALlJF.name,vcf.CourseCode,vcf.CourseTitleDescription ";
            if (dsEmployee.Rows.Count > 0)
            {
                for (int m = 0; m < dsEmployee.Rows.Count; m++)
                {
                    strFind += " ,(case  when fd" + (m + 1).ToString() + ".date is null then  '2'  when mr" + (m + 1).ToString() + ".Status is NULL  then '1' else  '0'  end)as '" + dsEmployee.Rows[m]["userID"].ToString() + "'";

                }
            }

             strFind +=" from(";
             strFind += " select  distinct a.JobfunctionID as ID,a.code,a.name From function_detail as a ";
             if (EmType == "0") //LM 登录
             {
                 strFind += " inner join   hr_info  as b  on b.director='" + hidUserID.Value + "' AND a.userID=b.userID and b.status!='Desert'";
             }
             else if (EmType == "1") //Empoyee   ',ypen,DZHO,' like '%,'+b.userID+',%' 
             {
                 strFind += " inner join   hr_info  as b  on b.director='" + LMofEmployee + "' AND a.userID=b.userID and b.status!='Desert'";
             }
             strFind +=" ) as AllJF";
             strFind += " inner join ViewCourseToJF vcf   on AllJF.code=vcf.JFCode and AllJF.ID=vcf.JFID";

            if (dsEmployee.Rows.Count > 0)
            {
                for (int m = 0; m < dsEmployee.Rows.Count; m++)
                {

                    strFind += " left join function_detail fd" + (m + 1).ToString() + " on fd" + (m + 1).ToString() + ".userID='" + dsEmployee.Rows[m]["userID"].ToString() + "' and fd" +
                        (m + 1).ToString() + ".code=AllJF.code  and fd" + (m + 1).ToString() + ".JobfunctionID=AllJF.ID";
                    strFind += " left Join missingReport mr" + (m + 1).ToString() + " on mr" +
                        (m + 1).ToString() + ".CourseCode=vcf.CourseCode and mr" + (m + 1).ToString() + ".userID='" + dsEmployee.Rows[m]["userID"].ToString() + "'";

                }
            }

            //strFind += " order by AllJF.code";



            string strFind2 = "select  AllJF.code,ALlJF.name,vcf.SOPNo as CourseCode,vcf.descriptipn as CourseTitleDescription ";
            if (dsEmployee.Rows.Count > 0)
            {
                for (int m = 0; m < dsEmployee.Rows.Count; m++)
                {
                    strFind2 += " ,(case  when fd" + (m + 1).ToString() + ".date is null then  '2'  when mr" + (m + 1).ToString() + ".Status is NULL  then '1' else  '0'  end)as '" + dsEmployee.Rows[m]["userID"].ToString() + "'";

                }
            }

            strFind2 += " from(";
            strFind2 += " select  distinct a.JobfunctionID as ID,a.code,a.name From function_detail as a";
            if (EmType == "0") //LM 登录
            {
                strFind2 += " inner join   hr_info  as b  on b.director='" + hidUserID.Value + "' AND a.userID=b.userID and b.status!='Desert' ";
            }
            else if (EmType == "1") //Empoyee   ',ypen,DZHO,' like '%,'+b.userID+',%' 
            {
                strFind2 += " inner join   hr_info  as b  on b.director='" + LMofEmployee + "' AND a.userID=b.userID and b.status!='Desert' ";
            }
            strFind2 += " ) as AllJF";
            strFind2 += " inner join ViewSOPToJF vcf   on AllJF.code=vcf.JFCode and AllJF.ID=vcf.JFID";


            if (dsEmployee.Rows.Count > 0)
            {
                for (int m = 0; m < dsEmployee.Rows.Count; m++)
                {

                    strFind2 += " left join function_detail fd" + (m + 1).ToString() + " on fd" + (m + 1).ToString() + ".userID='" + dsEmployee.Rows[m]["userID"].ToString() + "' and fd" +
                        (m + 1).ToString() + ".code=AllJF.code  and fd" + (m + 1).ToString() + ".JobfunctionID=AllJF.ID";
                    strFind2 += " left Join missingReport mr" + (m + 1).ToString() + " on mr" +
                        (m + 1).ToString() + ".CourseCode=vcf.SOPNo and mr" + (m + 1).ToString() + ".userID='" + dsEmployee.Rows[m]["userID"].ToString() + "'";

                }
            }

            //strFind2 += " order by AllJF.code";



            string strAll = "select  newid() as id,tableAll.* from (" + strFind + " union " + strFind2 + ") as tableAll order by code ";

            dsTabel = DbHelperSQL.Query(strAll).Tables[0];
            return true;
        
        }


        protected void btn_save_Click(object sender, EventArgs e)
        {
            string fileName = "MatrixTool.xls";

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