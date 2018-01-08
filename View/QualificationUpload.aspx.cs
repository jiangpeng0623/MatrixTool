using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.OleDb;
using DBUtility;
using System.Data.SqlClient;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using Eday;
using System.Net;
using System.Text;
using System.Diagnostics;

namespace MatrixTool.View
{
    public partial class QualificationUpload : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //    if (Session["userid"] == null || Session["userid"].ToString() == "")
            //    {
            //        Response.Write(" <script> top.location.href= '/login.aspx '; </script> ");
            //        return;
            //    }

        }
        protected void Btn_upload_Click(object sender, EventArgs e)
        {
            string path = System.Configuration.ConfigurationManager.AppSettings.GetValues("AttachPath")[0].ToString();
            bool fileOk = false;
            string fileExtension = "";
            if (FileUpload1.HasFile)
            {
                fileExtension = Path.GetExtension(this.FileUpload1.FileName).ToLower();
                if (fileExtension == ".xlsx" || fileExtension == ".xls")
                { fileOk = true; }
                if (fileOk)
                {
                    DataSet ds = new DataSet();
                    String fname = "TrainingPlan" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    string filename = fname + fileExtension;
                    this.FileUpload1.PostedFile.SaveAs(path + filename);
                    ds = ExcelToDS(fileExtension, path + filename);
                    if (ds == null)
                    {
                        string strErr = "请确认文件格式或数据源是否在Sheet1中!/Please confirm the file format and whether the data source in Sheet1!/";
                        MessageBox.Show(this, strErr);
                        return;
                    }
                    DataTable dt = ds.Tables[0];
                    if (dt.Columns.Count < 6)
                    {
                        string strErr = "请查看数据源列数!/Please check the number of data source column!/";
                        MessageBox.Show(this, strErr);
                        return;
                    }
                    int dtNum = dt.Rows.Count;
                    if (dtNum > 0)
                    {
                        string EmployeeID0 = dt.Rows[0][0].ToString().Trim();//EMPLOYEE CODE A
                        string CourseCode0 = dt.Rows[0][5].ToString().Trim(); //COURSE F
                        if (EmployeeID0 != "EMPLOYEE CODE")
                        {
                            MessageBox.Show(this, "第A列应该为EMPLOYEE CODE.！Column A should be the EMPLOYEE CODE.!/");
                            return;
                        }
                        if (CourseCode0 != "COURSE")
                        {
                            MessageBox.Show(this, "第F列应该为COURSE！Column F should be the COURSE!/");
                            return;
                        }



                        string message = "";
                        for (int i = 1; i < dtNum; i++)
                        {
                            string EmployeeID = dt.Rows[i][0].ToString().Trim();//EMPLOYEE CODE A
                            string CourseCode = dt.Rows[i][5].ToString().Trim(); //COURSE F
                            //判断非空
                            if (EmployeeID == "")
                            {
                                message += "第" + (i + 1).ToString() + "行EMPLOYEE CODE不能为空！";
                            }
                            if (CourseCode == "")
                            {
                                message += "第" + (i + 1).ToString() + "行COURSE不能为空！";
                            }
                        }
                        if (message != "")
                        {
                            ErrorReport.Value = message;
                            ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>document.getElementById(\"ErrorReport\").style[\"display\"]=\"block\";</script>");
                            return;
                        }
                        int insertNumber = 0;
                        //批量插入数据
                        for (int i = 1; i < dtNum; i++)
                        {
                            //无重复的插入
                            string EmployeeID = dt.Rows[i][0].ToString().Trim();
                            string CourseCode = dt.Rows[i][5].ToString().Trim();

                            Eday.Model.QualificationTable modueleQual = new Eday.Model.QualificationTable();
                            Eday.BLL.QualificationTable bllQual = new Eday.BLL.QualificationTable();
                            modueleQual = bllQual.GetModelByCourseAndEmploeeID(CourseCode, EmployeeID);
                            if (modueleQual == null)
                            {

                                Eday.Model.QualificationTable modueleQual2 = new Eday.Model.QualificationTable();
                                Eday.BLL.QualificationTable bllQual2 = new Eday.BLL.QualificationTable();
                                modueleQual2.ID = Guid.NewGuid().ToString();
                                modueleQual2.CourseCode = CourseCode;
                                modueleQual2.EmployeeID = EmployeeID;
                                modueleQual2.CourseTime = null; //课程未完成
                                modueleQual2.Status = "0";  //培训计划 未完成的

                                modueleQual2.CreateUserID = "";// Session["userid"].ToString();
                                modueleQual2.CreateTime = DateTime.Now;
                                modueleQual2.UpdateUserID = "";// Session["userid"].ToString();
                                modueleQual2.UpdateTime = DateTime.Now;

                                // string MD5 = bllSOP2.GetCheckCode(modueleSOP2);
                                //modueleSOP2.Checkcode = EnCode.EnCode.EasyTrainingEnCode(MD5);
                                bllQual2.Add(modueleQual2);
                                insertNumber++;
                            }
                            else
                            {
                                message += "第" + (i + 1).ToString() + "行数据重复！";
                            }
                        }

                        if (insertNumber > 0)
                        {
                            Response.Write("<script language='javascript'>alert('数据添加成功!共添加了" + insertNumber + "条培训计划数据!/Data added successfully! Added " + insertNumber + " Training Plan data! ');</script>");
                            if (message != "")
                            {
                                ErrorReport.Style["display"] = "block";
                                ErrorReport.Value = message;
                            }


                        }
                        else
                        {
                            MessageBox.Show(this, "数据添加失败！/Adding data failed!");
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "该工作簿无数据，请重新选择！/This worksheet has no data. Please select again.");
                    }

                }
                else
                {
                    string strErr = "上传失败，请确认文件格式为excel,且后缀名为 .xlsx 或 .xls!/Upload failed, please confirm the file format for excel, and the suffix named .xlsx or .xls!";
                    MessageBox.Show(this, strErr);
                }
            }
            else
            {
                string strErr = "请选择要上传的文件!/Please select the file to upload!";
                MessageBox.Show(this, strErr);
            }
        }

        protected void BtnRoster_Click(object sender, EventArgs e)
        {

            string path = System.Configuration.ConfigurationManager.AppSettings.GetValues("AttachPath")[0].ToString();
            bool fileOk = false;
            string fileExtension = "";
            if (FileUpload2.HasFile)
            {
                fileExtension = Path.GetExtension(this.FileUpload2.FileName).ToLower();
                if (fileExtension == ".xlsx" || fileExtension == ".xls")
                { fileOk = true; }
                if (fileOk)
                {
                    DataSet ds = new DataSet();
                    String fname = "TrainingRoster" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    string filename = fname + fileExtension;
                    this.FileUpload2.PostedFile.SaveAs(path + filename);
                    ds = ExcelToDS(fileExtension, path + filename);
                    if (ds == null)
                    {
                        string strErr = "请确认文件格式或数据源是否在Sheet1中!/Please confirm the file format and whether the data source in Sheet1!/";
                        MessageBox.Show(this, strErr);
                        return;
                    }
                    DataTable dt = ds.Tables[0];
                    if (dt.Columns.Count < 4)
                    {
                        string strErr = "请查看数据源列数!/Please check the number of data source column!/";
                        MessageBox.Show(this, strErr);
                        return;
                    }
                    int dtNum = dt.Rows.Count;
                    if (dtNum > 0)
                    {
                        string EmployeeID0 = dt.Rows[0][0].ToString().Trim();//initial A
                        string CourseCode0 = dt.Rows[0][2].ToString().Trim(); //课程名称 C
                        string CompleteTime0 = dt.Rows[0][3].ToString().Trim(); //时间 D
                        if (EmployeeID0 != "initial")
                        {
                            MessageBox.Show(this, "第A列应该为initial.！Column A should be the initial.!/");
                            return;
                        }
                        if (CourseCode0 != "课程名称")
                        {
                            MessageBox.Show(this, "第C列应该为课程名称！Column C should be the 课程名称！/");
                            return;
                        }
                        if (CompleteTime0 != "时间")
                        {
                            MessageBox.Show(this, "第C列应该为时间！Column C should be the 时间！/");
                            return;
                        }



                        string message = "";
                        for (int i = 1; i < dtNum; i++)
                        {
                            string EmployeeID = dt.Rows[i][0].ToString().Trim();//initial A
                            string CourseCode = dt.Rows[i][2].ToString().Trim(); //课程名称 F
                            string CompleteTime = dt.Rows[i][3].ToString().Trim(); //时间 D
                            //判断非空
                            if (EmployeeID == "")
                            {
                                message += "第" + (i + 1).ToString() + "行initial不能为空！";
                            }
                            if (CourseCode == "")
                            {
                                message += "第" + (i + 1).ToString() + "行课程名称不能为空！";
                            }
                            if (CompleteTime == "")
                            {
                                message += "第" + (i + 1).ToString() + "行时间不能为空！";
                            }
                            else
                            {
                                try
                                {
                                    DateTime time = DateTime.Parse(CompleteTime);
                                }
                                catch (Exception)
                                {
                                    message += "第" + (i + 1).ToString() + "行时间格式不正确！";
                                }
                            }
                        }
                        if (message != "")
                        {
                            ErrorReport.Value = message;
                            ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>document.getElementById(\"ErrorReport\").style[\"display\"]=\"block\";</script>");
                            return;
                        }
                        int insertNumber = 0;
                        //批量插入数据
                        for (int i = 1; i < dtNum; i++)
                        {
                            //无重复的插入
                            string EmployeeID = dt.Rows[i][0].ToString().Trim();
                            string CourseCode = dt.Rows[i][2].ToString().Trim();
                            string CompleteTime = dt.Rows[i][3].ToString().Trim();

                            Eday.Model.QualificationTable modueleQual = new Eday.Model.QualificationTable();
                            Eday.BLL.QualificationTable bllQual = new Eday.BLL.QualificationTable();
                            modueleQual = bllQual.GetModelByCourseAndEmploeeID(CourseCode, EmployeeID);
                            if (modueleQual == null)
                            {

                                Eday.Model.QualificationTable modueleQual2 = new Eday.Model.QualificationTable();
                                Eday.BLL.QualificationTable bllQual2 = new Eday.BLL.QualificationTable();
                                modueleQual2.ID = Guid.NewGuid().ToString();
                                modueleQual2.CourseCode = CourseCode;
                                modueleQual2.EmployeeID = EmployeeID;
                                modueleQual2.CourseTime = DateTime.Parse(CompleteTime); //课程已经完成
                                modueleQual2.Status = "1";  //培训Roster已完成的

                                modueleQual2.CreateUserID = "";// Session["userid"].ToString();
                                modueleQual2.CreateTime = DateTime.Now;
                                modueleQual2.UpdateUserID = "";// Session["userid"].ToString();
                                modueleQual2.UpdateTime = DateTime.Now;

                                // string MD5 = bllSOP2.GetCheckCode(modueleSOP2);
                                //modueleSOP2.Checkcode = EnCode.EnCode.EasyTrainingEnCode(MD5);
                                bllQual2.Add(modueleQual2);
                                insertNumber++;
                            }
                            else
                            {
                                DateTime dt1 = (DateTime)modueleQual.CourseTime;
                                DateTime dt2 = DateTime.Parse(CompleteTime);
                                if (dt1 < dt2)
                                {
                                    modueleQual.CourseTime = dt2;
                                    modueleQual.UpdateTime = DateTime.Now;
                                    bllQual.Update(modueleQual);
                                }

                                message += "第" + (i + 1).ToString() + "行数据重复！";
                            }
                        }

                        if (insertNumber > 0)
                        {
                            Response.Write("<script language='javascript'>alert('数据添加成功!共添加了" + insertNumber + "条培训登记表数据!/Data added successfully! Added " + insertNumber + " Training Roster data! ');</script>");
                            if (message != "")
                            {
                                ErrorReport.Style["display"] = "block";
                                ErrorReport.Value = message;
                            }
                        }
                        else
                        {
                            MessageBox.Show(this, "数据添加失败！/Adding data failed!");
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "该工作簿无数据，请重新选择！/This worksheet has no data. Please select again.");
                    }

                }
                else
                {
                    string strErr = "上传失败，请确认文件格式为excel,且后缀名为 .xlsx 或 .xls!/Upload failed, please confirm the file format for excel, and the suffix named .xlsx or .xls!";
                    MessageBox.Show(this, strErr);
                }
            }
            else
            {
                string strErr = "请选择要上传的文件!/Please select the file to upload!";
                MessageBox.Show(this, strErr);
            }
        }
        protected void BtnRoster1_Click(object sender, EventArgs e)
        {
            string deleMissing = " delete from EmployeebySupervisor";
            int n = DbHelperSQL.ExecuteSql(deleMissing);
            //string path = Server.MapPath("../upload/");
            string path = System.Configuration.ConfigurationManager.AppSettings.GetValues("AttachPath")[0].ToString();
            bool fileOk = false;
            string fileExtension = "";
            if (FileUpload3.HasFile)
            {
                fileExtension = Path.GetExtension(this.FileUpload3.FileName).ToLower();
                if (fileExtension == ".xlsx" || fileExtension == ".xls")
                { fileOk = true; }
                if (fileOk)
                {
                    if (FileUpload1.PostedFile.ContentLength > 10240 * 1000)
                    {
                        string strErr = "所选文件超过10M，导入失败!!";
                        //  MessageBox.Show(this, strErr);
                        return;
                    }


                    DataSet ds = new DataSet();
                    String fname = "MissingReport" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    string filename = fname + fileExtension;
                    this.FileUpload3.PostedFile.SaveAs(path + filename);
                    ds = ExcelToDS(path + filename);
                    DataTable dt = ds.Tables[0];

                    int nRealDate = 0;
                    int dtNum = dt.Rows.Count;
                    if (dtNum > 0)
                    {
                        for (int i = 1; i < dtNum; i++)
                        {
                            string DEPT_CODE = dt.Rows[i][0].ToString();//userID
                            string DESCRIPTION = dt.Rows[i][1].ToString();//userID
                            string EMP_ID = dt.Rows[i][2].ToString();//userID
                            string EMPLOYEE_NAME = dt.Rows[i][3].ToString();//userID
                            string EMP_STATUS = dt.Rows[i][4].ToString();//userID
                            string CLASS_CODE = dt.Rows[i][5].ToString();//userID
                            string EMP_SHIFT = dt.Rows[i][6].ToString();//userID
                            string SUP_NAME = dt.Rows[i][7].ToString();//userID
                            string SERVER_TIME = dt.Rows[i][8].ToString();//userID
                            if (DEPT_CODE == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行DEPT_CODE不能为空！/Employee initial in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }

                            if (DESCRIPTION == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行DESCRIPTION不能为空！/Supervisor in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (EMP_ID == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行EMP_ID不能为空！/Course code in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (EMPLOYEE_NAME == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行EMPLOYEE_NAME不能为空！/TASK_CODE in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (EMP_STATUS == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "EMP_STATUS不能为空！/Course description in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }

                            if (CLASS_CODE == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "CLASS_CODE不能为空！/Status in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (EMP_SHIFT == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行EMP_SHIFT不能为空！DUE_DATE in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (SUP_NAME == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行SUP_NAME不能为空！DUE_DATE in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (SERVER_TIME == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行SERVER_TIME不能为空！DUE_DATE in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                        }

                        int insertNumber = 0;
                        //批量插入数据
                        for (int i = 1; i < dtNum; i++)
                        {
                            string DEPT_CODE = dt.Rows[i][0].ToString();//userID
                            string DESCRIPTION = dt.Rows[i][1].ToString();//userID
                            string EMP_ID = dt.Rows[i][2].ToString();//userID
                            string EMPLOYEE_NAME = dt.Rows[i][3].ToString();//userID
                            string EMP_STATUS = dt.Rows[i][4].ToString();//userID
                            string CLASS_CODE = dt.Rows[i][5].ToString();//userID
                            string EMP_SHIFT = dt.Rows[i][6].ToString();//userID
                            string SUP_NAME = dt.Rows[i][7].ToString();//userID
                            string SERVER_TIME = dt.Rows[i][8].ToString();//userID
                            string Stime = DateTime.Now.ToString();

                            string sqlStr = "insert into EmployeebySupervisor (DEPT_CODE,DESCRIPTION,EMP_ID,EMPLOYEE_NAME,EMP_STATUS,CLASS_CODE,EMP_SHIFT,SUP_NAME,SERVER_TIME,Stime)values('" + DEPT_CODE + "',N'" + DESCRIPTION + "','" + EMP_ID + "','" + EMPLOYEE_NAME + "','" + EMP_STATUS + "','" + CLASS_CODE + "','" + EMP_SHIFT + "','" + SUP_NAME + "','" + SERVER_TIME + "','" + Stime + "')";
                            DbHelperSQL.ExecuteSql(sqlStr);
                            insertNumber++;
                        }

                        if (insertNumber > 0)
                        {
                            string aa = "数据添加成功!  共添加了" + insertNumber + "条数据; ";
                            MessageBox.Show(this, aa);
                        }
                        else
                        {
                            MessageBox.Show(this, "数据添加失败，请检查您的Excel模板！/Adding data failed. Please check your Excle template");
                        }


                    }
                    else
                    {
                        MessageBox.Show(this, "该工作簿无数据，请重新选择！/This worksheet has no data. Please select again.");
                    }
                }
                else
                {
                    string strErr = "上传失败，请确认文件格式为excel,且后缀名为 .xls!";
                    MessageBox.Show(this, strErr);
                }
            }
            else
            {
                string strErr = "请选择要上传的文件!/Please select the file to upload!";
                MessageBox.Show(this, strErr);
            }
        }

        protected void BtnSTJboardInput_Click(object sender, EventArgs e)
        {
            //string filePath = Server.MapPath("fileupload\\") + filename;
            string path = System.Configuration.ConfigurationManager.AppSettings.GetValues("AttachPath")[0].ToString();
            bool fileOk = false;
            string fileExtension = "";
            if (FileSTJboard.HasFile)
            {
                fileExtension = Path.GetExtension(this.FileSTJboard.FileName).ToLower();
                if (fileExtension == ".xlsx" || fileExtension == ".xls")
                { fileOk = true; }
                if (fileOk)
                {
                    if (FileSTJboard.PostedFile.ContentLength > 10240 * 1000)
                    {
                        string strErr = "所选文件超过10M，导入失败!!";
                        //  MessageBox.Show(this, strErr);
                        return;
                    }


                    DataSet ds = new DataSet();
                    String fname = "MissingReport" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    string filename = fname + fileExtension;
                    this.FileSTJboard.PostedFile.SaveAs(path + filename);
                    //ds = ExcelToDS(path + filename);
                    ds = ExcelToDS(fileExtension, path + filename, "Sheet1");
                    DataTable dt = ds.Tables[0];

                    int nRealDate = 0;
                    int dtNum = dt.Rows.Count;
                    if (dtNum > 0)
                    {
                        for (int i = 1; i < dtNum; i++)
                        {
                            string ID = Guid.NewGuid().ToString();
                            string DEPT_CODE = dt.Rows[i][0].ToString().Replace('?', ' ').Trim();//userID
                            string DEPT_DESC = dt.Rows[i][1].ToString();//userID
                            string ASSIGNED = dt.Rows[i][2].ToString();//userID
                            string REQ = dt.Rows[i][3].ToString();//userID
                            string COMP = dt.Rows[i][4].ToString();//userID
                            string DUE = dt.Rows[i][5].ToString();//userID
                            string NOTDUE = dt.Rows[i][6].ToString();//userID
                            string TRNRATIO = dt.Rows[i][7].ToString();//userID
                            string EMP_WITH_MOD = dt.Rows[i][8].ToString();//userID
                            string EMP_TOTAL = dt.Rows[i][9].ToString();//userID
                            string EMP_PERC = dt.Rows[i][10].ToString();//userID
                            string ITP = dt.Rows[i][11].ToString();//userID
                            string QUAL = dt.Rows[i][12].ToString();//userID
                            string ITP_DELTA = dt.Rows[i][13].ToString();//userID
                            string QUAL_DELTA = dt.Rows[i][14].ToString();//userID
                            string SERVER_TIME = dt.Rows[i][15].ToString();//userID
                            if (DEPT_CODE == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行DEPT_CODE不能为空！/Employee initial in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }

                            if (DEPT_DESC == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行DEPT_DESC不能为空！/DEPT_DESC in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (ASSIGNED == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行ASSIGNED不能为空！/ASSIGNED in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (REQ == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行REQ不能为空！/REQ in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (COMP == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "COMP不能为空！/COMP in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }

                            if (DUE == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "DUE不能为空！/DUE in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (NOTDUE == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行NOTDUE不能为空！NOTDUE in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (TRNRATIO == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行TRNRATIO不能为空！TRNRATIO in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (EMP_WITH_MOD == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行EMP_WITH_MOD不能为空！EMP_WITH_MOD in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (EMP_TOTAL == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行EMP_TOTAL不能为空！EMP_TOTAL in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (EMP_PERC == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行EMP_PERC不能为空！EMP_PERC in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (ITP == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行ITP不能为空！ITP in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (QUAL == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行QUAL不能为空！QUAL in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (ITP_DELTA == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行ITP_DELTA不能为空！ITP_DELTA in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (QUAL_DELTA == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行QUAL_DELTA不能为空！QUAL_DELTA in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (SERVER_TIME == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行SERVER_TIME不能为空！DUE_DATE in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                        }

                        int insertNumber = 0;
                        string pid = Guid.NewGuid().ToString();
                        DateTime AddTime = DateTime.Now;
                        try
                        {
                            DateTime startweek = AddTime.AddDays(1 - Convert.ToInt32(AddTime.DayOfWeek.ToString("d")));
                            DateTime endweek = startweek.AddDays(6);

                            string sqlSel = "select * from BoardRollingParent where Status='1' and AddTime between '" + startweek.ToString("yyyy-MM-dd") + "' and '" + endweek.ToString("yyyy-MM-dd") + "'";
                            DataTable dtParent = DBUtility.DbHelperSQL.Query(sqlSel).Tables[0];
                            if (dtParent != null && dtParent.Rows.Count > 0)
                            {
                                string sqlUp = " update BoardRollingParent set Status='0'   where Status='1' and  AddTime between '" + startweek.ToString("yyyy-MM-dd") + "' and '" + endweek.ToString("yyyy-MM-dd") + "'";
                                DBUtility.DbHelperSQL.ExecuteSql(sqlUp);
                            }
                        }
                        catch { 
                        }

                        StringBuilder strSqlParent = new StringBuilder();
                        strSqlParent.Append("insert into BoardRollingParent(");
                        strSqlParent.Append("ID,AddTime,Status)");
                        strSqlParent.Append(" values (");
                        strSqlParent.Append("@ID,@AddTime,@Status)");
                        SqlParameter[] parametersParent = {
                                                              new SqlParameter("@ID", SqlDbType.NVarChar,50),
                                                              new SqlParameter("@AddTime", SqlDbType.DateTime),
                                                              new SqlParameter("@Status",SqlDbType.NVarChar,50)};
                        parametersParent[0].Value = pid;
                        parametersParent[1].Value = AddTime;
                        parametersParent[2].Value = "1";

                        int rowsParent = DbHelperSQL.ExecuteSql(strSqlParent.ToString(), parametersParent);

                        #region 批量插入数据
                        //批量插入数据
                        for (int i = 1; i < dtNum; i++)
                        {
                            string ID = Guid.NewGuid().ToString();
                            string DEPT_CODE = dt.Rows[i][0].ToString().Replace('?', ' ').Trim();//userID
                            string DEPT_DESC = dt.Rows[i][1].ToString();//userID
                            string ASSIGNED = dt.Rows[i][2].ToString();//userID
                            string REQ = dt.Rows[i][3].ToString();//userID
                            string COMP = dt.Rows[i][4].ToString();//userID
                            string DUE = dt.Rows[i][5].ToString();//userID
                            string NOTDUE = dt.Rows[i][6].ToString();//userID
                            string TRNRATIO = dt.Rows[i][7].ToString();//userID
                            string EMP_WITH_MOD = dt.Rows[i][8].ToString();//userID
                            string EMP_TOTAL = dt.Rows[i][9].ToString();//userID
                            string EMP_PERC = dt.Rows[i][10].ToString();//userID
                            string ITP = dt.Rows[i][11].ToString();//userID
                            string QUAL = dt.Rows[i][12].ToString();//userID
                            string ITP_DELTA = dt.Rows[i][13].ToString();//userID
                            string QUAL_DELTA = dt.Rows[i][14].ToString();//userID
                            string SERVER_TIME = dt.Rows[i][15].ToString();//userID

                            StringBuilder strSql = new StringBuilder();
                            strSql.Append("insert into BoardRollingDB(");
                            strSql.Append("ID,DEPT_CODE,DEPT_DESC,ASSIGNED,REQ,COMP,DUE,NOTDUE,TRNRATIO,EMP_WITH_MOD,EMP_TOTAL,EMP_PERC,ITP,QUAL,ITP_DELTA,QUAL_DELTA,SERVER_TIME,AddTime,Pid)");
                            strSql.Append(" values (");
                            strSql.Append("@ID,@DEPT_CODE,@DEPT_DESC,@ASSIGNED,@REQ,@COMP,@DUE,@NOTDUE,@TRNRATIO,@EMP_WITH_MOD,@EMP_TOTAL,@EMP_PERC,@ITP,@QUAL,@ITP_DELTA,@QUAL_DELTA,@SERVER_TIME,@AddTime,@Pid)");
                            SqlParameter[] parameters = {
                                                            new SqlParameter("@ID", SqlDbType.NVarChar,50),
                                                            new SqlParameter("@DEPT_CODE", SqlDbType.NVarChar,50),
                                                            new SqlParameter("@DEPT_DESC", SqlDbType.NVarChar,500),
                                                            new SqlParameter("@ASSIGNED", SqlDbType.Float,8),
                                                            new SqlParameter("@REQ", SqlDbType.Float,8),
                                                            new SqlParameter("@COMP", SqlDbType.Float,8),
                                                            new SqlParameter("@DUE", SqlDbType.Float,8),
                                                            new SqlParameter("@NOTDUE", SqlDbType.Float,8),
                                                            new SqlParameter("@TRNRATIO", SqlDbType.Float,8),
                                                            new SqlParameter("@EMP_WITH_MOD", SqlDbType.Float,8),
                                                            new SqlParameter("@EMP_TOTAL", SqlDbType.Float,8),
                                                            new SqlParameter("@EMP_PERC", SqlDbType.Float,8),
                                                            new SqlParameter("@ITP", SqlDbType.Float,8),
                                                            new SqlParameter("@QUAL", SqlDbType.Float,8),
                                                            new SqlParameter("@ITP_DELTA", SqlDbType.Float,8),
                                                            new SqlParameter("@QUAL_DELTA", SqlDbType.Float,8),
                                                            new SqlParameter("@SERVER_TIME", SqlDbType.NVarChar,500),
                                                            new SqlParameter("@AddTime", SqlDbType.DateTime),
                                                            new SqlParameter("@Pid", SqlDbType.NVarChar,500)};
                            parameters[0].Value = ID;
                            parameters[1].Value = DEPT_CODE;
                            parameters[2].Value = DEPT_DESC;
                            parameters[3].Value = ASSIGNED;
                            parameters[4].Value = REQ;
                            parameters[5].Value = COMP;
                            parameters[6].Value = DUE;
                            parameters[7].Value = NOTDUE;
                            parameters[8].Value = TRNRATIO;
                            parameters[9].Value = EMP_WITH_MOD;
                            parameters[10].Value = EMP_TOTAL;
                            parameters[11].Value = EMP_PERC;
                            parameters[12].Value = ITP;
                            parameters[13].Value = QUAL;
                            parameters[14].Value = ITP_DELTA;
                            parameters[15].Value = QUAL_DELTA;
                            parameters[16].Value = SERVER_TIME;
                            parameters[17].Value = AddTime;
                            parameters[18].Value = pid;

                            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);

                            //string sqlStr = "insert into EmployeebySupervisor (DEPT_CODE,DESCRIPTION,EMP_ID,EMPLOYEE_NAME,EMP_STATUS,CLASS_CODE,EMP_SHIFT,SUP_NAME,SERVER_TIME,Stime)values('" + DEPT_CODE + "',N'" + DESCRIPTION + "','" + EMP_ID + "','" + EMPLOYEE_NAME + "','" + EMP_STATUS + "','" + CLASS_CODE + "','" + EMP_SHIFT + "','" + SUP_NAME + "','" + SERVER_TIME + "','" + Stime + "')";
                            //DbHelperSQL.ExecuteSql(sqlStr);
                            insertNumber++;
                        }
                        #endregion 
                        if (insertNumber > 0)
                        {
                            string aa = "数据添加成功!  共添加了" + insertNumber + "条数据; ";
                            InsertBoardTally(pid, AddTime);
                            MessageBox.Show(this, aa);
                        }
                        else
                        {
                            MessageBox.Show(this, "数据添加失败，请检查您的Excel模板！/Adding data failed. Please check your Excle template");
                        }


                    }
                    else
                    {
                        MessageBox.Show(this, "该工作簿无数据，请重新选择！/This worksheet has no data. Please select again.");
                    }
                }
                else
                {
                    string strErr = "上传失败，请确认文件格式为excel,且后缀名为 .xls!";
                    MessageBox.Show(this, strErr);
                }
            }
            else
            {
                string strErr = "请选择要上传的文件!/Please select the file to upload!";
                MessageBox.Show(this, strErr);
            }
        }

        protected void InsertBoardTally(string pid, DateTime addtime)
        {
            DateTime startweek = addtime.AddDays(1 - Convert.ToInt32(addtime.DayOfWeek.ToString("d")));
            DateTime endweek = startweek.AddDays(6);
            string sqlSel = "select * from BoardRollingTally where AddTime between '" + startweek.ToString("yyyy-MM-dd") + "' and '" + endweek.ToString("yyyy-MM-dd") + "'";
            DataTable dtParent = DBUtility.DbHelperSQL.Query(sqlSel).Tables[0];
            if (dtParent != null && dtParent.Rows.Count > 0)
            {
                string sqlUp = " delete BoardRollingTally  where AddTime between '" + startweek.ToString("yyyy-MM-dd") + "' and '" + endweek.ToString("yyyy-MM-dd") + "'";
                DBUtility.DbHelperSQL.ExecuteSql(sqlUp);
            }
            string SQL0 = "select top 1 * from missingReport order by ISOTrainTime asc";
            DataSet dt0 = DbHelperSQL.Query(SQL0);
            string timefunction = DateTime.Parse(dt0.Tables[0].Rows[0]["ISOTrainTime"].ToString()).ToShortDateString();
            string time=addtime.ToString("yyyy-MM-dd");
            //string time = "2017-12-14";
            StringBuilder sbSel = new StringBuilder();
            sbSel.Append("select vl.Area,SUM(Board.ASSIGNED) Assigned,[dbo].[GetDueByCodeAndDate](vl.Area,CONVERT(varchar(10), '" + timefunction + "', 121)) as Due");
            sbSel.Append(",CONVERT(varchar(10), bp.AddTime, 121) as Weekly");
            sbSel.Append(" from BoardRollingDB  Board left join BoardRollingParent bp on Board.Pid=bp.ID and bp.status = '1' left join (select DISTINCT  Area,deptcode from vlookupfortraining) vl on (Board.DEPT_CODE=vl.deptcode  or '?' + Board.DEPT_CODE=vl.deptcode ) ");
            sbSel.Append(" where 1=1 and CONVERT(varchar(10), bp.AddTime, 121)='" + time + "' ");
            sbSel.Append(" group by vl.Area,CONVERT(varchar(10), bp.AddTime, 121) ");
            sbSel.Append(" order by CONVERT(varchar(10), bp.AddTime, 121) desc,vl.Area");
            DataSet ds = DBUtility.DbHelperSQL.Query(sbSel.ToString());
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable table = ds.Tables[0];
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    string ID = Guid.NewGuid().ToString();
                    string Area = table.Rows[i]["Area"] == null || table.Rows[i]["Area"].ToString() == "" ? "Other" : table.Rows[i]["Area"].ToString();
                    string Assigned = table.Rows[i]["Assigned"].ToString();
                    string Due = table.Rows[i]["Due"].ToString();
                    if (Area == "Other")
                    {
                        string sqlSelDueOther = "SELECT count(distinct re.CourseCode ) as Due from missingReport  re INNER JOIN  EmployeebySupervisor emp  on re.userID=emp.EMP_ID   left join View_1 vl on emp.DEPT_CODE=vl.dept_code";
                        sqlSelDueOther += " where vl.dept_code is not null and DueDate>'" + time + "'";
                        DataTable dtDueOther = DBUtility.DbHelperSQL.Query(sqlSelDueOther).Tables[0];
                        Due = dtDueOther.Rows[0]["Due"].ToString();

                        continue;
                    }
                    string Weekly = table.Rows[i]["Weekly"].ToString();
                    string bak1 = Area == "Other" ? "1000" : (i + 1).ToString();
                    StringBuilder strSql = new StringBuilder();

                    strSql.Append("insert into BoardRollingTally(");
                    strSql.Append("ID,Pid,Area,Assigned,Due,Weekly,AddTime,bak1)");
                    strSql.Append(" values (");
                    strSql.Append("@ID,@Pid,@Area,@Assigned,@Due,@Weekly,@AddTime,@bak1)");
                    SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.NVarChar,50),
					new SqlParameter("@Pid", SqlDbType.NVarChar,50),
					new SqlParameter("@Area", SqlDbType.NVarChar,50),
					new SqlParameter("@Assigned", SqlDbType.Float,8),
					new SqlParameter("@Due", SqlDbType.Float,8),
					new SqlParameter("@Weekly", SqlDbType.NVarChar,50),
					new SqlParameter("@AddTime", SqlDbType.DateTime),
					new SqlParameter("@bak1", SqlDbType.NVarChar,500)};
                    parameters[0].Value = ID;
                    parameters[1].Value = pid;
                    parameters[2].Value = Area;
                    parameters[3].Value = Assigned;
                    parameters[4].Value = Due;
                    parameters[5].Value = Weekly;
                    parameters[6].Value = addtime;
                    parameters[7].Value = bak1;

                    int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
                }
            }


        }

        private DataSet ExcelToDS(string Path)
        {
            Console.WriteLine(Path);
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Path + ";" + "Extended Properties='Excel 8.0;hdr=no;imex=1;'";
            //string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;HDR=Yes;IMEX=1;'";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            string strExcel = "";
            OleDbDataAdapter myCommand = null;
            DataSet ds = null;
            strExcel = "select * from  [Sheet1$]";
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            ds = new DataSet();
            myCommand.Fill(ds);
            conn.Close();
            return ds;

        }
        public void Write(string FileName, string mess)
        {
            FileStream fs = new FileStream(FileName, FileMode.Create);
            //获得字节数组
            byte[] data = System.Text.Encoding.Default.GetBytes(mess);
            //开始写入
            fs.Write(data, 0, data.Length);
            //清空缓冲区、关闭流
            fs.Flush();
            fs.Close();
        }

        //获取excel中的数据
        public DataSet ExcelToDS(string postFileName, string Path)
        {
            Console.WriteLine(Path);
            string strConn = "";
            if (postFileName == ".xls")
                strConn = "Provider=Microsoft.Jet.Oledb.4.0; Data Source=" + Path + "; Extended Properties=\"Excel 8.0; HDR=no; IMEX=1;\"";
            else if (postFileName == ".xlsx")
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Path + "; Extended Properties=\"Excel 12.0; HDR=no; IMEX=1;\"";
            //string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Path + ";" + "Extended Properties='Excel 8.0;hdr=no;imex=1;'";
            OleDbConnection conn = new OleDbConnection(strConn);
            try
            {
                conn.Open();
                string strExcel = "";
                OleDbDataAdapter myCommand = null;
                DataSet ds = null;
                strExcel = "select * from  [Sheet1$]";
                myCommand = new OleDbDataAdapter(strExcel, strConn);
                ds = new DataSet();
                myCommand.Fill(ds);
                conn.Close();
                return ds;
            }
            catch (Exception ee)
            {
                return null;
            }
        }
        public DataSet ExcelToDS(string postFileName, string Path,string sheetName)
        {
            //string connString = " Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + ViewState["FilePath"] + ";Extended Properties=Excel 8.0";
            //OleDbConnection conn = new OleDbConnection(connString);
            //try
            //{
            //    conn.Open();
            //}
            //catch
            //{
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert('Excel文件格式不正确！');", true);
            //    return;
            //}

            Console.WriteLine(Path);
            string strConn = "";
            if (postFileName == ".xls")
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Path + ";" + "Extended Properties='Excel 8.0;hdr=no;imex=1;'";
                //strConn = "Provider=Microsoft.Jet.Oledb.4.0; Data Source=" + Path + "; Extended Properties=\"Excel 8.0; HDR=no; IMEX=1;\"";
            else if (postFileName == ".xlsx")
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Path + "; Extended Properties=\"Excel 12.0; HDR=no; IMEX=1;\"";
            //string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Path + ";" + "Extended Properties='Excel 8.0;hdr=no;imex=1;'";
            OleDbConnection conn = new OleDbConnection(strConn);
            try
            {
                conn.Open();
                string strExcel = "";
                OleDbDataAdapter myCommand = null;
                DataSet ds = null;
                strExcel = "select * from  [" + sheetName + "$]";
                myCommand = new OleDbDataAdapter(strExcel, strConn);
                ds = new DataSet();
                myCommand.Fill(ds);
                conn.Close();
                return ds;
            }
            catch (Exception ee)
            {
                return null;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string deleMissing = " delete from QualificationTable";
            int n = DbHelperSQL.ExecuteSql(deleMissing);
            DataBind();
            string strErr = "已成功删除上一版数据!/The previous version of data  has been successfully deleted!";
            MessageBox.Show(this, strErr);
        }

        protected void ID_Back_Click(object sender, EventArgs e)
        {
            Response.Write(" <script> top.location.href= '/guide.aspx '; </script> ");
            return;
        }


        private DataSet getds(string tablename)
        {

            string sql = "SELECT id,DEPT_CODE,DESCRIPTION,EMP_ID,EMPLOYEE_NAME,SUP_NAME,SERVER_TIME,Stime from   " + tablename + "";
            DataSet dss = DbHelperSQL.Query(sql);
            return dss;
        }
        private DataSet getds1(string tablename)
        {
            DateTime dq = DateTime.Now;
            DateTime dqj = dq.AddDays(7);
            //string sql = "SELECT  CourseCode，TaskCode，Description，Status，DueDate，SuperVisorCode  from  " + tablename + "";
            string sql = "SELECT (case  when missingReport.DueDate <'" + dq + "'then '过期' when missingReport.DueDate ='" + dqj + "'then'将要过期'when missingReport.DueDate>'" + dq + "'then'没有过期'end),missingReport.CourseCode,missingReport.userID,missingReport.TaskCode,missingReport.Description,missingReport.Status,missingReport.DueDate,missingReport.SuperVisorCode ,EmployeebySupervisor.DEPT_CODE,hr_info.costCenterArea from missingReport left join   EmployeebySupervisor on missingReport.userID=EmployeebySupervisor.EMP_ID left join hr_info on missingReport.userID=hr_info.userID";
            //string sql = "SELECT   missingReport.CourseCode,missingReport.userID,missingReport.TaskCode,missingReport.Description,missingReport.Status,missingReport.DueDate,missingReport.SuperVisorCode ,EmployeebySupervisor.DEPT_CODE,hr_info.costCenterArea from " + tablename + " left join   EmployeebySupervisor on missingReport.userID=EmployeebySupervisor.EMP_ID left join hr_info on missingReport.userID=hr_info.userID";
            DataSet dss = DbHelperSQL.Query(sql);
            return dss;
        }
        private ISheet createSheet(HSSFWorkbook workBook, string sheetName, DataSet dss)
        {
            ISheet sheet = workBook.CreateSheet(sheetName);
            IRow RowHead = sheet.CreateRow(0);
            for (int iColumnIndex = 0; iColumnIndex < dss.Tables[0].Columns.Count; iColumnIndex++)// 0 , dataset.table[0]row[0].counts,i++
            {

                string aa = dss.Tables[0].Columns[iColumnIndex].ColumnName.ToString();
                RowHead.CreateCell(iColumnIndex).SetCellValue(aa);  //dataset.table[0]row[0][i].value .tostring
            }

            for (int iRowIndex = 0; iRowIndex < 300; iRowIndex++)//3的位置是dataset总行数dss.Tables[0].Rows.Count
            {

                IRow RowBody = sheet.CreateRow(iRowIndex + 1);//createRow创建行，参数的意思是第几行，索引下标从0开始
                for (int iColumnIndex = 0; iColumnIndex < dss.Tables[0].Columns.Count; iColumnIndex++)
                {
                    string aaa = dss.Tables[0].Rows[iRowIndex][iColumnIndex].ToString();
                    RowBody.CreateCell(iColumnIndex).SetCellValue(aaa);
                    sheet.AutoSizeColumn(iColumnIndex);//AutoSizeColumn（column）；column是某一列，意思是设置column这一列为自动调整列宽；
                }
            }

            return sheet;


        }

        protected void UpdateMissingReport_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    CallSteven();
            //    string strErr = "完成调用";
            //    MessageBox.Show(this, strErr);

            //}
            //catch (Exception exUpdate)
            //{
            //    string strErr = exUpdate.Message.ToString();
            //    MessageBox.Show(this, strErr);
            //}
            //插入数据
            DbHelperSQL.ExecuteSql("delete missingReport");
            string sql = "select * from missingReport";
            DataSet ds = DbHelperSQL_wss.Query(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Eday.missingReport model = new Eday.missingReport();
                    string id = ds.Tables[0].Rows[i]["ID"].ToString();
                    InsertMissingReport(id);
                    
                }
            }
                string strErr = "完成调用";
                MessageBox.Show(this, strErr);
        }

        //添加ETUserProfile
        public void InsertMissingReport(string id)
        {
            try
            {
                Eday.missingReport modelU = new Eday.missingReport(id);
                Eday.missingReport model = new Eday.missingReport();
                model = modelU;
                model.ID = id;
                model.Add();
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void CallSteven()
        {
            string strCmd = "";
            DateTime dt = DateTime.Now;

            //注意：需要引入System.Diagnostics;
            Process prc = new Process();
            try
            {
                //指定调用的可执行文件
                strCmd += HttpContext.Current.Server.MapPath(@"\") + "UpLoad/update/EasyTrainingHr_Infor.exe";

                //如果可执行文件需要接收参数就加下下面这句，不同参数之间用空格隔开
                //strCmd += 参数1 + " " + 参数2 + " " + 参数n;
                //调用cmd.exe在命令提示符下执行可执行文件
                prc.StartInfo.FileName = "cmd.exe";
                prc.StartInfo.Arguments = " /c " + strCmd;
                prc.StartInfo.UseShellExecute = false;
                prc.StartInfo.RedirectStandardError = true;
                prc.StartInfo.RedirectStandardOutput = true;
                prc.StartInfo.RedirectStandardInput = true;
                prc.StartInfo.CreateNoWindow = false;
                prc.Start();
            }
            catch (Exception exU)
            {
                if (!prc.HasExited)
                {
                    prc.Close();
                }
                throw new Exception(exU.Message.ToString());
            }
        }
    }
}