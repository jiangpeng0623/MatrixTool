using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using DBUtility;
using System.Data.OleDb;
using Eday;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;


namespace MatrixTool.View
{
    public partial class CompareDataImport :PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userid"] == null || Session["userid"].ToString() == "")
            {
                Response.Write(" <script> top.location.href= '/login.aspx '; </script> ");
                return;
            }
            else
            {
                initialLabs();
            }
        }


        protected void initialLabs()
        {
            StringBuilder sbSel = new StringBuilder();
            sbSel.Append(" select COUNT(*) as Num from ISOTrain_Map_Jobfunction ");
            sbSel.Append(" select COUNT(*) as Num from ISOTrain_Trainer ");
            sbSel.Append("  select COUNT(*) as Num from ISOTrain_Course ");
            sbSel.Append("  select COUNT(*) as Num from ISOTrain_SOP ");
            sbSel.Append("  select COUNT(*) as Num  from ISOTrain_Relation");
            DataSet dsISOTrainNum = DBUtility.DbHelperSQL.Query(sbSel.ToString());
            LabTrainingPLan.Text = dsISOTrainNum.Tables[0].Rows[0]["Num"].ToString();
            LabTrainer.Text = dsISOTrainNum.Tables[1].Rows[0]["Num"].ToString();
            LabCourseSOP.Text = (Convert.ToInt16(dsISOTrainNum.Tables[2].Rows[0]["Num"].ToString()) + Convert.ToInt16(dsISOTrainNum.Tables[3].Rows[0]["Num"].ToString())).ToString();
            LabRelation.Text = dsISOTrainNum.Tables[4].Rows[0]["Num"].ToString();

        }
        protected void btn_Clear_Click(object sender, EventArgs e)
        {
            string deleMissing = " delete from ISOTrain_Map_Jobfunction";
            deleMissing += " delete from ISOTrain_Course";
            deleMissing += " delete from ISOTrain_SOP";
            deleMissing += " delete from ISOTrain_Trainer";
            deleMissing += " delete from ISOTrain_Relation";
            int n = DbHelperSQL.ExecuteSql(deleMissing);
            DataBind();
            initialLabs();
            string strErr = "已成功删除上一版数据!/The previous version of data  has been successfully deleted!";
            MessageBox.Show(this, strErr);
        }
        protected void ID_Back_Click(object sender, EventArgs e)
        {
            Response.Write(" <script> top.location.href= '/guide.aspx '; </script> ");
            return;
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
                        string EmployeeID0 = dt.Rows[0][1].ToString().Trim();
                        string JFCode0 = dt.Rows[0][2].ToString().Trim(); 
                        string ActiveFro0 = dt.Rows[0][8].ToString().Trim(); //ACTIVE_FRO F
                        if (EmployeeID0 != "EMP_ID")
                        {
                            MessageBox.Show(this, "第B列应该为EMP_ID.！Column B should be the EMP_ID.!/");
                            return;
                        }
                        if (JFCode0 != "CURR_CODE")
                        {
                            MessageBox.Show(this, "第C列应该为CURR_CODE！Column C should be the CURR_CODE!/");
                            return;
                        }
                        if (ActiveFro0 != "ACTIVE_FRO")
                        {
                            MessageBox.Show(this, "第I列应该为ACTIVE_FRO！Column I should be the ACTIVE_FRO!/");
                            return;
                        }



                        string message = "";
                        for (int i = 1; i < dtNum; i++)
                        {
                            string EmployeeID = dt.Rows[i][1].ToString().Trim();//EMPLOYEE CODE A
                            string JFCode = dt.Rows[i][2].ToString().Trim(); //COURSE F
                            string ActiveFro = dt.Rows[i][8].ToString().Trim(); //ACTIVE_FRO F
                            //判断非空
                            if (EmployeeID == "")
                            {
                                message += "第" + (i + 1).ToString() + "行EMP_ID不能为空！";
                            }
                            if (JFCode == "")
                            {
                                message += "第" + (i + 1).ToString() + "行CURR_CODE不能为空！";
                            }
                            if (ActiveFro =="")
                            {
                                message += "第" + (i + 1).ToString() + "行ACTIVE_FRO不能为空！";
                                return;
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
                            if (i > 1037)
                            {
                                int a = 0;
                            }
                            //无重复的插入
                            string EmployeeID = dt.Rows[i][1].ToString().Trim();//EMPLOYEE CODE A
                            string JFCode = dt.Rows[i][2].ToString().Trim(); //COURSE F
                            string ActiveFro = dt.Rows[i][8].ToString().Trim(); //ACTIVE_FRO F

                            Eday.Model.ISOTrain_Map_Jobfunction modueleQual = new Eday.Model.ISOTrain_Map_Jobfunction();
                            Eday.BLL.ISOTrain_Map_Jobfunction bllQual = new Eday.BLL.ISOTrain_Map_Jobfunction();
                            modueleQual = bllQual.GetModelByJFAndEmploeeID(EmployeeID,JFCode);
                            if (modueleQual == null)
                            {
                                Eday.Model.ISOTrain_Map_Jobfunction modueleQual2 = new Eday.Model.ISOTrain_Map_Jobfunction();
                                Eday.BLL.ISOTrain_Map_Jobfunction bllQual2 = new Eday.BLL.ISOTrain_Map_Jobfunction();
                                modueleQual2.ID = Guid.NewGuid().ToString();
                                modueleQual2.JFCode =JFCode;
                                modueleQual2.UserID = EmployeeID;
                                modueleQual2.ActiveDate = DateTime.Parse(ActiveFro);
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
                            initialLabs();
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

        protected void BtnTrainer_Click(object sender, EventArgs e)
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
                    String fname = "Trainer" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
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
                    if (dt.Columns.Count < 6)
                    {
                        string strErr = "请查看数据源列数!/Please check the number of data source column!/";
                        MessageBox.Show(this, strErr);
                        return;
                    }
                    int dtNum = dt.Rows.Count;
                    if (dtNum > 0)
                    {
                        string EmployeeID0 = dt.Rows[0][0].ToString().Trim();
                        string CourseCode0 = dt.Rows[0][2].ToString().Trim();
                        string TaskLevel0 = dt.Rows[0][4].ToString().Trim(); //TASK_CODE
                        string EffectiveDate0 = dt.Rows[0][5].ToString().Trim();
                        if (EmployeeID0 != "INST_CODE")
                        {
                            MessageBox.Show(this, "第A列应该为INST_CODE.！Column B should be the INST_CODE.!/");
                            return;
                        }
                        if (CourseCode0 != "COURSE_CODE")
                        {
                            MessageBox.Show(this, "第C列应该为COURSE_CODE！Column C should be the COURSE_CODE!/");
                            return;
                        }
                        if (TaskLevel0 != "TASK_CODE")
                        {
                            MessageBox.Show(this, "第E列应该为TASK_CODE！Column I should be the TASK_CODE!/");
                            return;
                        }
                        if (EffectiveDate0 != "INST_COURSE_CERTIF")
                        {
                            MessageBox.Show(this, "第F列应该为INST_COURSE_CERTIF！Column I should be the INST_COURSE_CERTIF!/");
                            return;
                        }

                        
                        string message = "";
                        for (int i = 1; i < dtNum; i++)
                        {
                            string EmployeeID = dt.Rows[i][0].ToString().Trim();
                            string CourseCode = dt.Rows[i][2].ToString().Trim();
                            string TaskLevel= dt.Rows[i][4].ToString().Trim(); //TASK_CODE
                            string EffectiveDate = dt.Rows[i][5].ToString().Trim();
                            //判断非空
                            if (EmployeeID == "")
                            {
                                message += "第" + (i + 1).ToString() + "行EMP_ID不能为空！";
                            }
                            if (CourseCode == "")
                            {
                                message += "第" + (i + 1).ToString() + "行COURSE_CODE不能为空！";
                            }
                            if (TaskLevel == "")
                            {
                                message += "第" + (i + 1).ToString() + "行TASK_CODE不能为空！";
                                return;
                            }
                            if (EffectiveDate == "")
                            {
                                message += "第" + (i + 1).ToString() + "行INST_COURSE_CERTIF不能为空！";
                                return;
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
                            string TaskLevel = dt.Rows[i][4].ToString().Trim(); //TASK_CODE
                            string EffectiveDate = dt.Rows[i][5].ToString().Trim();

                            //Eday.Model.Trainer modueleQual = new Eday.Model.Trainer();
                            //Eday.BLL.Trainer bllQual = new Eday.BLL.Trainer();
                            //modueleQual = bllQual.GetModelByJFAndEmploeeID(EmployeeID, JFCode);
                            //if (modueleQual == null)
                            //{
                            Eday.Model.ISOTrain_Trainer modueleQual2 = new Eday.Model.ISOTrain_Trainer();
                            Eday.BLL.ISOTrain_Trainer bllQual2 = new Eday.BLL.ISOTrain_Trainer();
                            modueleQual2.ID = Guid.NewGuid().ToString();
                            modueleQual2.TrainerInitial = EmployeeID;
                            modueleQual2.CourseCode = CourseCode;
                            modueleQual2.TaskLevel = TaskLevel;
                            modueleQual2.EffectiveDate=DateTime.Parse(EffectiveDate);
                            modueleQual2.CreateDate = DateTime.Now;
                            bllQual2.Add(modueleQual2);
                            insertNumber++;
                            //}
                            //else
                            //{
                            //    message += "第" + (i + 1).ToString() + "行数据重复！";
                            //}
                        }

                        if (insertNumber > 0)
                        {
                            initialLabs();
                            Response.Write("<script language='javascript'>alert('数据添加成功!共添加了" + insertNumber + "条讲师数据!/Data added successfully! Added " + insertNumber + " Trainer data! ');</script>");
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

        protected void BtnCourse_Click(object sender, EventArgs e)
        {
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
                    DataSet ds = new DataSet();
                    String fname = "Course" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    string filename = fname + fileExtension;
                    this.FileUpload3.PostedFile.SaveAs(path + filename);
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
                        string CourseCode0 = dt.Rows[0][2].ToString().Trim();//COURSE_CODE
                        string  CourseRev0= dt.Rows[0][4].ToString().Trim();//COURSE_REV
                        string dueDate0 = dt.Rows[0][7].ToString().Trim(); //DUE_DAT
                        if (CourseCode0 != "COURSE_CODE")
                        {
                            MessageBox.Show(this, "第C列应该为COURSE_CODE.！Column C should be the COURSE_CODE.!/");
                            return;
                        }
                        if (CourseRev0 != "COURSE_REV")
                        {
                            MessageBox.Show(this, "第E列应该为COURSE_REV！Column E should be the COURSE_REV!/");
                            return;
                        }
                        if (dueDate0 != "DUE_DAT")
                        {
                            MessageBox.Show(this, "第H列应该为DUE_DAT！Column H should be the DUE_DAT!/");
                            return;
                        }
                        string message = "";
                        string messageDueDate = "";
                        for (int i = 1; i < dtNum; i++)
                        {
                            string CourseCode = dt.Rows[i][2].ToString().Trim();//COURSE_CODE
                            string CourseRev = dt.Rows[i][4].ToString().Trim();//COURSE_REV
                            string dueDate = dt.Rows[i][7].ToString().Trim(); //DUE_DAT
                            //判断非空
                           
                            if (CourseCode == "")
                            {
                                message += "第" + (i + 1).ToString() + "行COURSE_CODE不能为空！";
                            }
                            if (CourseRev == "")
                            {
                                message += "第" + (i + 1).ToString() + "行COURSE_REV不能为空！";
                             
                            }
                            if (dueDate == "")
                            {
                                messageDueDate += "第" + (i + 1).ToString() + "行DUE_DAT为空！";
                               
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
                            if (i > 1037)
                            {
                                int a = 0;
                            }
                            //无重复的插入
                            string CourseCode = dt.Rows[i][2].ToString().Trim();//COURSE_CODE
                            string CourseRev = dt.Rows[i][4].ToString().Trim();//COURSE_REV
                            string dueDate = dt.Rows[i][7].ToString().Trim(); //DUE_DAT

                            //Eday.Model.Trainer modueleQual = new Eday.Model.Trainer();
                            //Eday.BLL.Trainer bllQual = new Eday.BLL.Trainer();
                            //modueleQual = bllQual.GetModelByJFAndEmploeeID(EmployeeID, JFCode);
                            //if (modueleQual == null)
                            //{
                            if (CourseCode.IndexOf("Q") == 0)
                            {
                                Eday.Model.ISOTrain_SOP modueleQual2 = new Eday.Model.ISOTrain_SOP();
                                Eday.BLL.ISOTrain_SOP bllQual2 = new Eday.BLL.ISOTrain_SOP();
                                modueleQual2.ID = Guid.NewGuid().ToString();
                                modueleQual2.SOPNo = CourseCode;
                                modueleQual2.Edition = CourseRev;
                                bllQual2.Add(modueleQual2);
                                insertNumber++;
                            }
                            else
                            {
                                if(dueDate!="")
                                {
                                    Eday.Model.ISOTrain_Course modueleQual2 = new Eday.Model.ISOTrain_Course();
                                    Eday.BLL.ISOTrain_Course bllQual2 = new Eday.BLL.ISOTrain_Course();
                                    modueleQual2.ID = Guid.NewGuid().ToString();
                                    modueleQual2.CourseCode = CourseCode;
                                    modueleQual2.CourseRevision = CourseRev;
                                    modueleQual2.EffectiveDate = DateTime.Parse(dueDate);
                                    bllQual2.Add(modueleQual2);
                                    insertNumber++;
                                }
                               
                            }
                            
                            //}
                            //else
                            //{
                            //    message += "第" + (i + 1).ToString() + "行数据重复！";
                            //}
                        }

                        if (insertNumber > 0)
                        {
                            initialLabs();
                            Response.Write("<script language='javascript'>alert('数据添加成功!共添加了" + insertNumber + "条课程数据!/Data added successfully! Added " + insertNumber + " Course data! ');</script>");
                            if (message != "")
                            {
                                ErrorReport.Style["display"] = "block";
                                ErrorReport.Value = message;
                            }
                            if (messageDueDate != "")
                            {
                                ErrorReport.Style["display"] = "block";
                                ErrorReport.Value = messageDueDate;
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

        protected void BtnRelation_Click(object sender, EventArgs e)
        {
            string path = System.Configuration.ConfigurationManager.AppSettings.GetValues("AttachPath")[0].ToString();
            bool fileOk = false;
            string fileExtension = "";
            if (FileUpload4.HasFile)
            {
                fileExtension = Path.GetExtension(this.FileUpload4.FileName).ToLower();
                if (fileExtension == ".xlsx" || fileExtension == ".xls")
                { fileOk = true; }
                if (fileOk)
                {
                    DataSet ds = new DataSet();
                    String fname = "Relation" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    string filename = fname + fileExtension;
                    this.FileUpload4.PostedFile.SaveAs(path + filename);
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
                        string JFCode0 = dt.Rows[0][0].ToString().Trim();//CURR_CODE
                        string title0 = dt.Rows[0][1].ToString().Trim();//TITLE
                        string ModuleCode0 = dt.Rows[0][6].ToString().Trim(); //MODULE_CODE
                        string ModuleDesc0 = dt.Rows[0][8].ToString().Trim(); //MOD_DESC
                        
                        string CourseCode0 = dt.Rows[0][10].ToString().Trim();//COURSE_CODE
                        string TaskLevel0 = dt.Rows[0][12].ToString().Trim();//TASK_CODE
                        string NotifyDays0 = dt.Rows[0][14].ToString().Trim();//EFFECTIVE_DAYS
                        string NeedRetraining0 = dt.Rows[0][17].ToString().Trim();//RETRAIN_REQUIRED
                        string CourseTitleDescription0 = dt.Rows[0][19].ToString().Trim();//COUR_DESC
                        string Frequency0 = dt.Rows[0][20].ToString().Trim();//FREQUENCY
                        

                        if (JFCode0 != "CURR_CODE")
                        {
                            MessageBox.Show(this, "第A列应该为CURR_CODE.！Column A should be the CURR_CODE.!/");
                            return;
                        }
                        if (title0 != "TITLE")
                        {
                            MessageBox.Show(this, "第B列应该为TITLE！Column B should be the TITLE!/");
                            return;
                        }
                        if (ModuleCode0 != "MODULE_CODE")
                        {
                            MessageBox.Show(this, "第G列应该为MODULE_CODE！Column G should be the MODULE_CODE!/");
                            return;
                        }
                        if (ModuleDesc0 != "MOD_DESC")
                        {
                            MessageBox.Show(this, "第I列应该为MOD_DESC！Column I should be the MOD_DESC!/");
                            return;
                        }

                        if (CourseCode0 != "COURSE_CODE")
                        {
                            MessageBox.Show(this, "第K列应该为COURSE_CODE.！Column K should be the COURSE_CODE.!/");
                            return;
                        }
                        if (TaskLevel0 != "TASK_CODE")
                        {
                            MessageBox.Show(this, "第M列应该为CURR_CODE.！Column M should be the TASK_CODE.!/");
                            return;
                        }
                        if (NotifyDays0 != "EFFECTIVE_DAYS")
                        {
                            MessageBox.Show(this, "第O列应该为EFFECTIVE_DAYS.！Column O should be the EFFECTIVE_DAYS.!/");
                            return;
                        }
                        if (NeedRetraining0 != "RETRAIN_REQUIRED")
                        {
                            MessageBox.Show(this, "第R列应该为RETRAIN_REQUIRED.！Column R should be the RETRAIN_REQUIRED.!/");
                            return;
                        }
                        if (CourseTitleDescription0 != "COUR_DESC")
                        {
                            MessageBox.Show(this, "第T列应该为COUR_DESC.！Column T should be the COUR_DESC.!/");
                            return;
                        }
                        if (Frequency0 != "FREQUENCY")
                        {
                            MessageBox.Show(this, "第U列应该为FREQUENCY.！Column U should be the FREQUENCY.!/");
                            return;
                        }

                        string message = "";
                        for (int i = 1; i < dtNum; i++)
                        {
                            string JFCode = dt.Rows[i][0].ToString().Trim();//CURR_CODE
                            string title = dt.Rows[i][1].ToString().Trim();//TITLE
                            string ModuleCode = dt.Rows[i][6].ToString().Trim(); //MODULE_CODE
                            string ModuleDesc = dt.Rows[i][8].ToString().Trim(); //MOD_DESC

                            string CourseCode = dt.Rows[i][10].ToString().Trim();//COURSE_CODE
                            string TaskLevel = dt.Rows[i][12].ToString().Trim();//TASK_CODE
                            string NotifyDays= dt.Rows[i][14].ToString().Trim();//EFFECTIVE_DAYS
                            string NeedRetraining = dt.Rows[i][17].ToString().Trim();//RETRAIN_REQUIRED
                            string CourseTitleDescription = dt.Rows[i][19].ToString().Trim();//COUR_DESC
                            string Frequency= dt.Rows[i][20].ToString().Trim();//FREQUENCY
                            //判断非空

                            if (JFCode == "")
                            {
                                message += "第" + (i + 1).ToString() + "行CURR_CODE不能为空！";
                            }
                            if (title == "")
                            {
                                message += "第" + (i + 1).ToString() + "行TITLE不能为空！";
                                return;
                            }
                            if (ModuleCode == "")
                            {
                                message += "第" + (i + 1).ToString() + "行MOD_DESC不能为空！";
                                return;
                            }
                            if (ModuleDesc == "")
                            {
                                message += "第" + (i + 1).ToString() + "行CURR_CODE不能为空！";
                            }
                            if (CourseCode == "")
                            {
                                message += "第" + (i + 1).ToString() + "行COURSE_CODE不能为空！";
                                return;
                            }
                            if (TaskLevel == "")
                            {
                                message += "第" + (i + 1).ToString() + "行TASK_CODE不能为空！";
                                return;
                            }
                            if (NotifyDays == "")
                            {
                                message += "第" + (i + 1).ToString() + "行EFFECTIVE_DAYS不能为空！";
                                return;
                            }
                            if (NeedRetraining == "")
                            {
                                message += "第" + (i + 1).ToString() + "行RETRAIN_REQUIRED不能为空！";
                                return;
                            }
                            if (CourseTitleDescription == "")
                            {
                                message += "第" + (i + 1).ToString() + "行COUR_DESC不能为空！";
                                return;
                            }
                            if (Frequency == "")
                            {
                                message += "第" + (i + 1).ToString() + "行FREQUENCY不能为空！";
                                return;
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
                            string JFCode = dt.Rows[i][0].ToString().Trim();//CURR_CODE
                            string title = dt.Rows[i][1].ToString().Trim();//TITLE
                            string ModuleCode = dt.Rows[i][6].ToString().Trim(); //MODULE_CODE
                            string ModuleDesc = dt.Rows[i][8].ToString().Trim(); //MOD_DESC

                            string CourseCode = dt.Rows[i][10].ToString().Trim();//COURSE_CODE
                            string TaskLevel = dt.Rows[i][12].ToString().Trim();//TASK_CODE
                            string NotifyDays = dt.Rows[i][14].ToString().Trim();//EFFECTIVE_DAYS
                            string NeedRetraining = dt.Rows[i][17].ToString().Trim();//RETRAIN_REQUIRED
                            string CourseTitleDescription = dt.Rows[i][19].ToString().Trim();//COUR_DESC
                            string Frequency = dt.Rows[i][20].ToString().Trim();//FREQUENCY

                            //Eday.Model.Trainer modueleQual = new Eday.Model.Trainer();
                            //Eday.BLL.Trainer bllQual = new Eday.BLL.Trainer();
                            //modueleQual = bllQual.GetModelByJFAndEmploeeID(EmployeeID, JFCode);
                            //if (modueleQual == null)
                            //{

                            Eday.Model.ISOTrain_Relation modueleQual2 = new Eday.Model.ISOTrain_Relation();
                            Eday.BLL.ISOTrain_Relation bllQual2 = new Eday.BLL.ISOTrain_Relation();
                                modueleQual2.ID = Guid.NewGuid().ToString();
                                modueleQual2.code = JFCode;
                                modueleQual2.name = title;
                                modueleQual2.CourseCode = CourseCode;
                                modueleQual2.ModuleCode = ModuleCode;
                                modueleQual2.Description = ModuleDesc;
                                modueleQual2.CourseCode = CourseCode;
                                modueleQual2.CourseTitleDescription = CourseTitleDescription;
                                modueleQual2.TaskLevel = TaskLevel;
                                modueleQual2.NotifyDays = NotifyDays;
                                modueleQual2.NeedRetraining = NeedRetraining;
                                modueleQual2.Frequency = Frequency;
                                bllQual2.Add(modueleQual2);
                          
                            insertNumber++;
                            //}
                            //else
                            //{
                            //    message += "第" + (i + 1).ToString() + "行数据重复！";
                            //}
                        }

                        if (insertNumber > 0)
                        {
                            initialLabs();
                            Response.Write("<script language='javascript'>alert('数据添加成功!共添加了" + insertNumber + "条关系数据!/Data added successfully! Added " + insertNumber + " Relation data! ');</script>");
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




        #region Training plan
        protected void BindGridTrainingPlan()
        {
            string strForExcel = "<table border='1'><tr><td>Initial</td><td>Dept</td><td>Area</td><td>EasyTrainingNum</td><td>ISOTrainNum</td><td>JFCode</td><td>ActiveDate</td><td>Status</td></tr>";
            StringBuilder sbSel = new StringBuilder();
            sbSel.Append("select * from ");
            sbSel.Append("( SELECT  T1.UserID,T1.JFCode,T1.ActiveDate,1 as Falg FROM    ( ");
            sbSel.Append("   select T1b.*  from  (SELECT    a.UserID,a.JFCode FROM      ISOTrain_Map_Jobfunction a");
            sbSel.Append(" EXCEPT SELECT   b.UserID,b.JFCode FROM      map_Jobfunction b,hr_info h,Jobfunction j  ");
            sbSel.Append(" where b.Status='0'  and j.Status='0' and h.status!='Desert' and j.ID=b.JobfunctionID and j.code=b.JFCode and b.userID=h.userID ) T1a ");
            sbSel.Append(" inner  join ISOTrain_Map_Jobfunction T1b on T1a.UserID=T1b.UserID and T1a.JFCode=T1b.JFCode");
            sbSel.Append(" ) AS T1 union  SELECT   T2.UserID,T2.JFCode,T2.date as ActiveDate,2 as Falg FROM    (  ");
            sbSel.Append(" select T1b.*  from ( SELECT   b.UserID,b.JFCode FROM  map_Jobfunction b,hr_info h,Jobfunction j ");
        sbSel.Append(" where b.Status='0'  and j.Status='0' and h.status!='Desert' and j.ID=b.JobfunctionID and j.code=b.JFCode and b.userID=h.userID ");
       sbSel.Append("  EXCEPT SELECT     d.UserID,d.JFCode FROM      ISOTrain_Map_Jobfunction d) T1a  ");
       sbSel.Append(" inner  join map_Jobfunction T1b on T1a.UserID=T1b.UserID and T1a.JFCode=T1b.JFCode and T1b.Status='0') AS T2 ");
       sbSel.Append(" union     SELECT  *,3 as Falg FROM   (  ");  
       sbSel.Append(" SELECT   b.UserID,b.JFCode,b.date  as ActiveDate   FROM      map_Jobfunction b,hr_info h,Jobfunction j,ISOTrain_Map_Jobfunction d ");
       sbSel.Append(" where b.Status='0'  and j.Status='0' and h.status!='Desert' and j.ID=b.JobfunctionID and j.code=b.JFCode and b.userID=h.userID ");
       sbSel.Append(" and b.UserID=d.UserID and  b.JFCode= d.JFCode and b.date<>d.ActiveDate) as T3  ) as T "); 
       
       sbSel.Append(" left  join    (select b.userID as userIDE,Count(*) as EasyTrainingNum from  map_Jobfunction b,Jobfunction j  where b.Status='0'  and j.Status='0' and j.ID=b.JobfunctionID and j.code=b.JFCode  group by b.userID  )  B on  B.userIDE= T.userID ");
       sbSel.Append(" left  join   ( select userID as userIDISO,Count(*) as ISOTrainNum from  ISOTrain_Map_Jobfunction   group by userID)  C on  C.userIDISO= T.userID  ");
       sbSel.Append(" left  join (select  userID,dept,costCenterArea from hr_info) h on  h.userID= T.userID  "); 
              sbSel.Append(" order by T.UserID,T.Falg ");

            DataSet dsISOTrainNum = DBUtility.DbHelperSQL.Query(sbSel.ToString());
            for (int i = 0; i < dsISOTrainNum.Tables[0].Rows.Count; i++)
            {
                string UserID = dsISOTrainNum.Tables[0].Rows[i]["UserID"].ToString();//UserID
                string dept = dsISOTrainNum.Tables[0].Rows[i]["dept"].ToString();//UserID
                string costCenterArea = dsISOTrainNum.Tables[0].Rows[i]["costCenterArea"].ToString();//UserID
                string ISOTrainNumTemp = dsISOTrainNum.Tables[0].Rows[i]["ISOTrainNum"].ToString();//ISOTrainNum
                string ISOTrainNum=ISOTrainNumTemp;
                if (ISOTrainNumTemp == "" || ISOTrainNumTemp == null)
                {
                    ISOTrainNum = "0";
                }
                string EasyTrainingNumTemp = dsISOTrainNum.Tables[0].Rows[i]["EasyTrainingNum"].ToString();//EasyTrainingNum
                string EasyTrainingNum = EasyTrainingNumTemp;
                if (EasyTrainingNumTemp == "" || EasyTrainingNumTemp==null)
                {
                   EasyTrainingNum="0";
                }

                string JFCode = dsISOTrainNum.Tables[0].Rows[i]["JFCode"].ToString();//JobFunction
                string Flag = dsISOTrainNum.Tables[0].Rows[i]["Falg"].ToString();//Flag
                string ActiveDate = dsISOTrainNum.Tables[0].Rows[i]["ActiveDate"].ToString();//ActiveDate
                string strFlag = "";
                if (Flag == "1")
                {
                    strFlag = "missing in Easy Training";
                }
                else if (Flag == "2")
                {
                    strFlag = "missing in ISOtrain";
                }
                else if (Flag == "3")
                {
                    strFlag = "differs between ISOtrain and Easy Training(in Easy Training)";
                }
                strForExcel += "<tr>";
                strForExcel += "<td><div class=\"datagrid-cell\">" + UserID + "</div></td>";
                strForExcel += "<td><div class=\"datagrid-cell\">" + dept + "</div></td>";
                strForExcel += "<td><div class=\"datagrid-cell\">" + costCenterArea + "</div></td>";
                strForExcel += "<td><div class=\"datagrid-cell\">" + EasyTrainingNum + "</div></td>";
                strForExcel += "<td><div class=\"datagrid-cell\">" + ISOTrainNum + "</div></td>";
                strForExcel += "<td><div class=\"datagrid-cell\">" + JFCode + "</div></td>";
                strForExcel += "<td><div class=\"datagrid-cell\">" + ActiveDate + "</div></td>";
                strForExcel += "<td><div class=\"datagrid-cell\">" + strFlag + "</div></td>";
                strForExcel += "</tr>";
            }
            strForExcel += "</table>";
            LabForTrainingPlan.Text = strForExcel;
            MessageBox.Show(this, "比对成功，可以导出Excel!/The comparison is successful, you can export Excel!");


        }
        protected void Btn_CompareTrainingPlan_Click(object sender, EventArgs e)
        {
            BindGridTrainingPlan();
        }
        protected void Btn_Export_Click(object sender, EventArgs e)
        {
            string fileName = "CompareTrainingPlan .xls";
            Export(fileName, LabForTrainingPlan);
        }
        #endregion

        #region Trainer
        protected void BindGridTrainer()
        {
            string strForExcel = "<table border='1'><tr><td>Initial</td><td>Dept</td><td>Area</td><td>EasyTrainingNum</td><td>ISOTrainNum</td><td>CourseCode</td><td>TaskLevel</td><td>EffectiveDate</td><td>Status</td></tr>";
            StringBuilder sbSel = new StringBuilder();
            sbSel.Append("  select * from ( ");
            sbSel.Append("   SELECT distinct  T1.TrainerInitial,T1.CourseCode,T1.TaskLevel,T1.EffectiveDate, 1 as Falg FROM  ");
           sbSel.Append(" (  select T1b.*  from (SELECT    a.TrainerInitial,a.CourseCode FROM      ISOTrain_Trainer a ");
     sbSel.Append(" EXCEPT SELECT   b.TrainerInitial , c.CourseCode FROM      Trainer b,form_Course c,hr_info h   where b.Status='0' and b.COurseID=c.ID and c.Status='0' and h.status!='Desert' and h.userID=b.TrainerInitial )T1a ");
     sbSel.Append("  inner  join ISOTrain_Trainer T1b on T1a.TrainerInitial=T1b.TrainerInitial and T1a.CourseCode=T1b.CourseCode) AS T1 ");
     sbSel.Append("   union SELECT distinct T2.*, 2 as Falg FROM  ");
      sbSel.Append("   (  select  b.TrainerInitial,c.CourseCode,b.TaskLevel,b.EffectiveDate  from ( ");
       sbSel.Append("   SELECT   b.TrainerInitial , c.CourseCode FROM      Trainer b,form_Course c,hr_info h    where b.Status='0' and b.COurseID=c.ID and c.Status='0' and h.status!='Desert' and h.userID=b.TrainerInitial ");
        sbSel.Append("   EXCEPT SELECT    a.TrainerInitial,a.CourseCode FROM      ISOTrain_Trainer a)T1a ");
         sbSel.Append("   inner  join Trainer b on T1a.TrainerInitial=b.TrainerInitial ");
          sbSel.Append("   inner  join form_Course c  on  T1a.CourseCode=c.CourseCode and  b.Status='0' and b.COurseID=c.ID and c.Status='0'  ) AS T2 ");
          sbSel.Append("    union  select distinct T1a.TrainerInitial,T1a.CourseCode,'' as TaskLevel,T1a.EffectiveDate, 3 as Falg  from ");
          sbSel.Append("    (  SELECT    c.CourseCode,b.effectiveDate,b.TrainerInitial,b.TaskLevel FROM      Trainer b,form_Course c,hr_info h  ");
           sbSel.Append("    where b.Status='0' and b.COurseID=c.ID and c.Status='0' and h.status!='Desert' and h.userID=b.TrainerInitial  )as T1a ");
           sbSel.Append("    inner join  ISOTrain_Trainer ic  on  T1a.CourseCode=ic.CourseCode and T1a.TrainerInitial=ic.TrainerInitial ");
            sbSel.Append("    where  datediff (d,ic.EffectiveDate,T1a.EffectiveDate) <> 0 ");
            sbSel.Append("     union select  distinct T1a.TrainerInitial,T1a.CourseCode,T1a.TaskLevel,'' as EffectiveDate, 3 as Falg  from ");
             sbSel.Append("    (  SELECT   (case  when b.TaskLevel like '%Job%'  then  'JOB'   end )  as TaskLevelTrance, c.CourseCode,b.effectiveDate,b.TrainerInitial,b.TaskLevel FROM  ");   
            sbSel.Append("      Trainer b,form_Course c,hr_info h  where b.Status='0' and b.COurseID=c.ID and c.Status='0' and h.status!='Desert' and h.userID=b.TrainerInitial )as T1a ");
            sbSel.Append("    inner join  ISOTrain_Trainer ic  on  T1a.CourseCode=ic.CourseCode and T1a.TrainerInitial=ic.TrainerInitial ");
               sbSel.Append("    where   ic.TaskLevel<>T1a.TaskLevelTrance  ) as T ");
               sbSel.Append("    left  join  (select TrainerInitial as userIDE,Count(*) as EasyTrainingNum from  Trainer b,form_Course c  "); 
                sbSel.Append("   where b.Status='0' and b.COurseID=c.ID and c.Status='0' group by TrainerInitial)  B on  B.userIDE= T.TrainerInitial ");
                 sbSel.Append("    left  join ( select TrainerInitial as userIDISO,Count(*) as ISOTrainNum from  ISOTrain_Trainer group by TrainerInitial)  C ");
                sbSel.Append("     on  C.userIDISO= T.TrainerInitial   left  join (select  userID,dept,costCenterArea from hr_info) h on ");
                 sbSel.Append("      h.userID= T.TrainerInitial  order by T.TrainerInitial,T.Falg  ");

            DataSet dsISOTrainNum = DBUtility.DbHelperSQL.Query(sbSel.ToString());
            for (int i = 0; i < dsISOTrainNum.Tables[0].Rows.Count; i++)
            {
                string UserID = dsISOTrainNum.Tables[0].Rows[i]["TrainerInitial"].ToString();//UserID
                string dept = dsISOTrainNum.Tables[0].Rows[i]["dept"].ToString();//UserID
                string costCenterArea = dsISOTrainNum.Tables[0].Rows[i]["costCenterArea"].ToString();//UserID
                string ISOTrainNumTemp = dsISOTrainNum.Tables[0].Rows[i]["ISOTrainNum"].ToString();//ISOTrainNum
                string ISOTrainNum = ISOTrainNumTemp;
                if (ISOTrainNumTemp == "" || ISOTrainNumTemp == null)
                {
                    ISOTrainNum = "0";
                }
                string EasyTrainingNumTemp = dsISOTrainNum.Tables[0].Rows[i]["EasyTrainingNum"].ToString();//EasyTrainingNum
                string EasyTrainingNum = EasyTrainingNumTemp;
                if (EasyTrainingNumTemp == "" || EasyTrainingNumTemp==null)
                {
                    EasyTrainingNum = "0";
                }

                string CourseCode = dsISOTrainNum.Tables[0].Rows[i]["CourseCode"].ToString();//CourseCode
                string TaskLevel = dsISOTrainNum.Tables[0].Rows[i]["TaskLevel"].ToString();//TaskLevel
                string EffectiveDateTemp = dsISOTrainNum.Tables[0].Rows[i]["EffectiveDate"].ToString();//EffectiveDate
                string EffectiveDate = EffectiveDateTemp;
                if (EffectiveDateTemp.Contains("1900"))
                {
                    EffectiveDate = "";
                }
                string Flag = dsISOTrainNum.Tables[0].Rows[i]["Falg"].ToString();//Falg
                string strFlag = "";
                if (Flag == "1")
                {
                    strFlag = "missing in Easy Training";
                }
                else if (Flag == "2")
                {
                    strFlag = "missing in ISOtrain";
                }
                else if (Flag == "3")
                {
                    strFlag = "differs between ISOtrain and Easy Training(in Easy Training)";
                }
                strForExcel += "<tr>";
                strForExcel += "<td><div class=\"datagrid-cell\">" + UserID + "</div></td>";
                strForExcel += "<td><div class=\"datagrid-cell\">" + dept + "</div></td>";
                strForExcel += "<td><div class=\"datagrid-cell\">" + costCenterArea + "</div></td>";
                strForExcel += "<td><div class=\"datagrid-cell\">" + EasyTrainingNum + "</div></td>";
                strForExcel += "<td><div class=\"datagrid-cell\">" + ISOTrainNum + "</div></td>";
                strForExcel += "<td><div class=\"datagrid-cell\">" + CourseCode + "</div></td>";
                strForExcel += "<td><div class=\"datagrid-cell\">" + TaskLevel + "</div></td>";
                strForExcel += "<td><div class=\"datagrid-cell\">" + EffectiveDate + "</div></td>";
                strForExcel += "<td><div class=\"datagrid-cell\">" + strFlag + "</div></td>";
                strForExcel += "</tr>";
            }
            strForExcel += "</table>";
            LabForTrainer.Text = strForExcel;
            MessageBox.Show(this, "比对成功，可以导出Excel!/The comparison is successful, you can export Excel!");


        }
        protected void Btn_CompareTrainer_Click(object sender, EventArgs e)
        {
            BindGridTrainer();
        }
        protected void btn_Trainer_Click(object sender, EventArgs e)
        {
            string fileName = "CompareTrainer .xls";
            Export(fileName, LabForTrainer);
        }
        #endregion

        #region Course and SOP
        protected ISheet createSheetSOP(HSSFWorkbook workBook, string sheetName)
        {
            StringBuilder sbSel = new StringBuilder();
            sbSel.Append("  select T.*,h.userID,h.dept,h.costCenterArea  from ( ");
            sbSel.Append("  SELECT  T4.SOPNo,T4.Edition, '' as EffectiveDate,1 as Falg FROM    (  ");
            sbSel.Append(" select T1b.*  from (SELECT    a.SOPNo FROM    ISOTrain_SOP a ");
            sbSel.Append(" EXCEPT SELECT   b.SOPNo FROM      form_SOP b where b.Status='0') T1a ");
            sbSel.Append(" inner  join ISOTrain_SOP T1b on T1a.SOPNo=T1b.SOPNo) AS T4 ");
            sbSel.Append(" union   SELECT   T5.SOPNo,T5.Edition,'' as EffectiveDate,2 as Falg FROM    ( ");
            sbSel.Append(" select T1b.*  from ( SELECT   c.SOPNo FROM      form_SOP c where c.Status='0' ");
            sbSel.Append(" EXCEPT SELECT     d.SOPNo FROM      ISOTrain_SOP d) T1a ");
            sbSel.Append(" inner  join form_SOP T1b on T1a.SOPNo=T1b.SOPNo  and T1b.Status='0') AS T5 ");
            sbSel.Append(" union SELECT  *,3 as Falg FROM   ( ");
            sbSel.Append(" SELECT   c.SOPNo,c.Edition,'' asEffectiveDate  FROM      form_SOP c ");
            sbSel.Append(" inner join ISOTrain_SOP d on c.SOPNo=d.SOPNo and c.Status='0' and  c.Edition<>d.Edition ) as T6) as T ");
            sbSel.Append("  left  join form_SOP  cc on  cc.SOPNo= T.SOPNo  and cc.Status='0' ");
            sbSel.Append(" left  join (select  userID,dept,costCenterArea from hr_info) h on  h.userID= cc.initial ");

            DataSet dsISOTrainNum = DBUtility.DbHelperSQL.Query(sbSel.ToString());
            ISheet sheet = workBook.CreateSheet(sheetName);
            sheet.AutoSizeColumn(0);
            sheet.AutoSizeColumn(1);
            sheet.AutoSizeColumn(2);
            sheet.AutoSizeColumn(3);
            sheet.AutoSizeColumn(4);
            sheet.AutoSizeColumn(5);
         
            IRow RowHead = sheet.CreateRow(0);
            RowHead.CreateCell(0).SetCellValue("SOP Code");
            RowHead.CreateCell(1).SetCellValue("SOP Author");
            RowHead.CreateCell(2).SetCellValue("Department");
            RowHead.CreateCell(3).SetCellValue("Area");
            RowHead.CreateCell(4).SetCellValue("Edition");
            RowHead.CreateCell(5).SetCellValue("Status");
            for (int i = 0; i < dsISOTrainNum.Tables[0].Rows.Count; i++)
            {
                // string ISOTrainNum = dsISOTrainNum.Tables[0].Rows[i]["ISOTrainNum"].ToString();//ISOTrainNum
                //string EasyTrainingNum = dsISOTrainNum.Tables[0].Rows[i]["EasyTrainingNum"].ToString();//EasyTrainingNum

                string CourseCode = dsISOTrainNum.Tables[0].Rows[i]["SOPNo"].ToString();//CourseCode
                string userID = dsISOTrainNum.Tables[0].Rows[i]["userID"].ToString();//userID
                string dept = dsISOTrainNum.Tables[0].Rows[i]["dept"].ToString();//dept
                string costCenterArea = dsISOTrainNum.Tables[0].Rows[i]["costCenterArea"].ToString();//costCenterArea
                string CourseRevision = dsISOTrainNum.Tables[0].Rows[i]["Edition"].ToString();//CourseRevision
                string CourseRevision2 = CourseRevision.PadLeft(4, '0');
                string Flag = dsISOTrainNum.Tables[0].Rows[i]["Falg"].ToString();//Falg
                string strFlag = "";
                if (Flag == "1")
                {
                    strFlag = "missing in Easy Training";
                }
                else if (Flag == "2")
                {
                    strFlag = "missing in ISOtrain";
                }
                else if (Flag == "3")
                {
                    strFlag = "differs between ISOtrain and Easy Training(in Easy Training)";
                }
                IRow RowBody = sheet.CreateRow(i + 1);
                RowBody.CreateCell(0).SetCellValue(CourseCode);
                RowBody.CreateCell(1).SetCellValue(userID);
                RowBody.CreateCell(2).SetCellValue(dept);
                RowBody.CreateCell(3).SetCellValue(costCenterArea);
                RowBody.CreateCell(4).SetCellValue(CourseRevision2);
                RowBody.CreateCell(5).SetCellValue(strFlag);
            }
            return sheet;
        }
        protected ISheet createSheetCourse(HSSFWorkbook workBook, string sheetName)
        {
            StringBuilder sbSel = new StringBuilder();
            sbSel.Append("   select T.*,h.userID,h.dept,h.costCenterArea from ( ");
            sbSel.Append(" SELECT  T1.CourseCode,T1.CourseRevision,T1.EffectiveDate,1 as Falg FROM    ( ");
            sbSel.Append(" select T1b.*  from (SELECT    a.CourseCode FROM    ISOTrain_Course a,ISOTrain_Relation ir  where a.CourseCode=ir.CourseCode ");
            sbSel.Append(" EXCEPT SELECT   b.CourseCode FROM      form_Course b where b.Status='0') T1a ");
            sbSel.Append(" inner  join ISOTrain_Course T1b on T1a.CourseCode=T1b.CourseCode) AS T1 ");
            sbSel.Append(" union   SELECT   T2.CourseCode,T2.CourseRevision,T2.EffectiveDate,2 as Falg FROM    (  ");
            sbSel.Append(" select T1b.*  from ( ");
            sbSel.Append(" SELECT   c.CourseCode FROM      form_Course c where c.Status='0' ");
            sbSel.Append(" EXCEPT SELECT     d.CourseCode FROM      ISOTrain_Course d,ISOTrain_Relation ir  where d.CourseCode=ir.CourseCode) T1a ");
            sbSel.Append(" inner  join form_Course T1b on T1a.CourseCode=T1b.CourseCode  and T1b.Status='0') AS T2 ");
            sbSel.Append(" union SELECT distinct *,3 as Falg FROM   (  ");
   sbSel.Append(" SELECT   c.CourseCode,c.CourseRevision,'' as EffectiveDate  FROM      form_Course c ");
   sbSel.Append(" inner join ISOTrain_Course d on c.CourseCode=d.CourseCode and c.Status='0' and  c.CourseRevision<>d.CourseRevision");
    sbSel.Append("   inner join ISOTrain_Relation ir  on d.CourseCode=ir.CourseCode) as T3 ");
sbSel.Append(" union SELECT distinct *,3 as Falg FROM   (  ");
   sbSel.Append(" SELECT   c.CourseCode,'' as CourseRevision,c.EffectiveDate  FROM      form_Course c ");
   sbSel.Append(" inner join ISOTrain_Course d on c.CourseCode=d.CourseCode and c.Status='0' and   datediff (d,c.EffectiveDate,d.EffectiveDate) <> 0");
        sbSel.Append("   inner join ISOTrain_Relation ir  on d.CourseCode=ir.CourseCode) as T4");
            sbSel.Append(" ) as T ");
            sbSel.Append("  left  join form_Course  cc on  cc.CourseCode= T.CourseCode and cc.Status='0'  ");
            sbSel.Append("  left  join (select  userID,dept,costCenterArea from hr_info) h on  h.userID= cc.initial  ");


            DataSet dsISOTrainNum = DBUtility.DbHelperSQL.Query(sbSel.ToString());
            ISheet sheet = workBook.CreateSheet(sheetName);
            sheet.AutoSizeColumn(0);
            sheet.AutoSizeColumn(1);
            sheet.AutoSizeColumn(2);
            sheet.AutoSizeColumn(3);
            sheet.AutoSizeColumn(4);
            sheet.AutoSizeColumn(5);
            sheet.AutoSizeColumn(6);
            IRow RowHead = sheet.CreateRow(0);
            RowHead.CreateCell(0).SetCellValue("CourseCode");
            RowHead.CreateCell(1).SetCellValue("Course Owner");
            RowHead.CreateCell(2).SetCellValue("Department");
            RowHead.CreateCell(3).SetCellValue("Area");
            RowHead.CreateCell(4).SetCellValue("CourseRevision");
            RowHead.CreateCell(5).SetCellValue("EffectiveDate");
            RowHead.CreateCell(6).SetCellValue("Falg");
            for (int i = 0; i < dsISOTrainNum.Tables[0].Rows.Count; i++)
            {
                // string ISOTrainNum = dsISOTrainNum.Tables[0].Rows[i]["ISOTrainNum"].ToString();//ISOTrainNum
                //string EasyTrainingNum = dsISOTrainNum.Tables[0].Rows[i]["EasyTrainingNum"].ToString();//EasyTrainingNum

                string CourseCode = dsISOTrainNum.Tables[0].Rows[i]["CourseCode"].ToString();//CourseCode
                string userID = dsISOTrainNum.Tables[0].Rows[i]["userID"].ToString();//userID
                string dept = dsISOTrainNum.Tables[0].Rows[i]["dept"].ToString();//dept
                string costCenterArea = dsISOTrainNum.Tables[0].Rows[i]["costCenterArea"].ToString();//costCenterArea
                string CourseRevision = dsISOTrainNum.Tables[0].Rows[i]["CourseRevision"].ToString();//CourseRevision
                string CourseRevision2 = CourseRevision;
                if(CourseRevision!="")
                {
                     CourseRevision2 = CourseRevision.PadLeft(4, '0');
                }
                
                
                string EffectiveDateTemp = dsISOTrainNum.Tables[0].Rows[i]["EffectiveDate"].ToString();//EffectiveDate
                string EffectiveDate = EffectiveDateTemp;
                if (EffectiveDateTemp.Contains("1900"))
                {
                    EffectiveDate = "";
                }
                string Flag = dsISOTrainNum.Tables[0].Rows[i]["Falg"].ToString();//Falg
                string strFlag = "";
                if (Flag == "1")
                {
                    strFlag = "missing in Easy Training";
                }
                else if (Flag == "2")
                {
                    strFlag = "missing in ISOtrain";
                }
                else if (Flag == "3")
                {
                    strFlag = "differs between ISOtrain and Easy Training(in Easy Training)";
                }
                IRow RowBody = sheet.CreateRow(i + 1);
                RowBody.CreateCell(0).SetCellValue(CourseCode);
                RowBody.CreateCell(1).SetCellValue(userID);
                RowBody.CreateCell(2).SetCellValue(dept);
                RowBody.CreateCell(3).SetCellValue(costCenterArea);
                RowBody.CreateCell(4).SetCellValue(CourseRevision2);
                RowBody.CreateCell(5).SetCellValue(EffectiveDate);
                RowBody.CreateCell(6).SetCellValue(strFlag);
            }
            return sheet;

        }
        protected void Btn_CompareCourseAndSOP_Click(object sender, EventArgs e)
        {
            string fileName = "CompareCourseAndSOP.xls";
            string path = this.MapPath(fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            HSSFWorkbook workBook = new HSSFWorkbook();
            createSheetCourse(workBook, "CompareCourse"); //CompareCourse
            createSheetSOP(workBook, "CompareSOP"); //CompareSOP
            
            using (FileStream file = new FileStream(path, FileMode.Create))
            {
                workBook.Write(file);　　//创建Excel文件。
                file.Close();
                MessageBox.Show(this, "比对成功，可以导出Excel!/The comparison is successful, you can export Excel!");
            }
        }
        protected void btn_CourseAndSOPExport_Click(object sender, EventArgs e)
        {
            

            string fileName = "CompareCourseAndSOP.xls";
            string path = this.MapPath(fileName);
            download(path, fileName);
        }
        #endregion

        #region 关系
        protected ISheet createSheetJobFunction(HSSFWorkbook workBook, string sheetName)
        {
            string strForExcel = "<table border='1'><tr><td>Initial</td><td>Job Function Code</td><td>Name </td><td>Status</td></tr>";
            StringBuilder sbSel = new StringBuilder();
            sbSel.Append("     select * from ( ");
            sbSel.Append("  SELECT  T4.code,T4.name,1 as Falg FROM    ( ");
            sbSel.Append("  select T1b.*  from (SELECT    a.code FROM    ISOTrain_Relation a ");
            sbSel.Append(" EXCEPT SELECT   b.code FROM      Jobfunction b where b.Status='0') T1a ");
            sbSel.Append(" inner  join ISOTrain_Relation T1b on T1a.code=T1b.code) AS T4 ");
            sbSel.Append(" union   SELECT   T5.code,T5.name,2 as Falg FROM    (  ");
            sbSel.Append(" select T1b.*  from ( SELECT   c.code FROM  Jobfunction c where c.Status='0' ");
            sbSel.Append(" EXCEPT SELECT     d.code FROM      ISOTrain_Relation d) T1a ");
            sbSel.Append(" inner  join Jobfunction T1b on T1a.code=T1b.code  and T1b.Status='0') AS T5 ");
            sbSel.Append(" union SELECT  *,3 as Falg FROM   (  ");
            sbSel.Append(" SELECT   c.code,c.name  FROM      Jobfunction c ");
            sbSel.Append(" inner join ISOTrain_Relation d on c.code=d.code and c.Status='0' and  c.name<>d.name ) as T6 ");
            sbSel.Append(" ) as T ");


            DataSet dsISOTrainNum = DBUtility.DbHelperSQL.Query(sbSel.ToString());
            ISheet sheet = workBook.CreateSheet(sheetName);
            sheet.AutoSizeColumn(0);
            sheet.AutoSizeColumn(1);
            sheet.AutoSizeColumn(2);
            IRow RowHead = sheet.CreateRow(0);
            RowHead.CreateCell(0).SetCellValue("code");
            RowHead.CreateCell(1).SetCellValue("name");
            RowHead.CreateCell(2).SetCellValue("Falg");
            for (int i = 0; i < dsISOTrainNum.Tables[0].Rows.Count; i++)
            {
                // string ISOTrainNum = dsISOTrainNum.Tables[0].Rows[i]["ISOTrainNum"].ToString();//ISOTrainNum
                //string EasyTrainingNum = dsISOTrainNum.Tables[0].Rows[i]["EasyTrainingNum"].ToString();//EasyTrainingNum

                string code = dsISOTrainNum.Tables[0].Rows[i]["code"].ToString();//CourseCode
                string name = dsISOTrainNum.Tables[0].Rows[i]["name"].ToString();//CourseRevision
                string Flag = dsISOTrainNum.Tables[0].Rows[i]["Falg"].ToString();//Falg
                string strFlag = "";
                if (Flag == "1")
                {
                    strFlag = "missing in Easy Training";
                }
                else if (Flag == "2")
                {
                    strFlag = "missing in ISOtrain";
                }
                else if (Flag == "3")
                {
                    strFlag = "differs between ISOtrain and Easy Training(in Easy Training)";
                }
                IRow RowBody = sheet.CreateRow(i + 1);
                RowBody.CreateCell(0).SetCellValue(code);
                RowBody.CreateCell(1).SetCellValue(name);
                RowBody.CreateCell(2).SetCellValue(strFlag);
            }
            return sheet;

        }
        protected ISheet createSheetModule(HSSFWorkbook workBook, string sheetName)
        {

            StringBuilder sbSel = new StringBuilder();
            sbSel.Append("    select * from ( ");
            sbSel.Append("  SELECT  T4.ModuleCode,T4.Description,1 as Falg FROM    ( ");
            sbSel.Append(" select T1b.*  from (SELECT    a.ModuleCode FROM    ISOTrain_Relation a ");
            sbSel.Append(" EXCEPT SELECT   b.ModuleCode FROM      Module b where b.Status='0') T1a ");
            sbSel.Append(" inner  join ISOTrain_Relation T1b on T1a.ModuleCode=T1b.ModuleCode) AS T4 ");
            sbSel.Append(" union   SELECT   T5.ModuleCode,T5.Description,2 as Falg FROM    (  ");
            sbSel.Append(" select T1b.*  from ( SELECT   c.ModuleCode FROM  Module c where c.Status='0' ");
            sbSel.Append(" EXCEPT SELECT     d.ModuleCode FROM      ISOTrain_Relation d) T1a ");
            sbSel.Append(" inner  join Module T1b on T1a.ModuleCode=T1b.ModuleCode  and T1b.Status='0') AS T5 ");
            //sbSel.Append(" union SELECT  *,3 as Falg FROM   (  ");
           // sbSel.Append(" SELECT   c.ModuleCode,c.Description  FROM      Module c ");
           // sbSel.Append(" inner join ISOTrain_Relation d on c.ModuleCode=d.ModuleCode and c.Status='0' and  c.Description<>d.Description ) as T6 ");
            sbSel.Append(" ) as T ");


            DataSet dsISOTrainNum = DBUtility.DbHelperSQL.Query(sbSel.ToString());
            ISheet sheet = workBook.CreateSheet(sheetName);
            sheet.AutoSizeColumn(0);
            sheet.AutoSizeColumn(1);
            sheet.AutoSizeColumn(2);
            IRow RowHead = sheet.CreateRow(0);
            RowHead.CreateCell(0).SetCellValue("ModuleCode");
            RowHead.CreateCell(1).SetCellValue("Description");
            RowHead.CreateCell(2).SetCellValue("Status");
            for (int i = 0; i < dsISOTrainNum.Tables[0].Rows.Count; i++)
            {
                // string ISOTrainNum = dsISOTrainNum.Tables[0].Rows[i]["ISOTrainNum"].ToString();//ISOTrainNum
                //string EasyTrainingNum = dsISOTrainNum.Tables[0].Rows[i]["EasyTrainingNum"].ToString();//EasyTrainingNum

                string ModuleCode = dsISOTrainNum.Tables[0].Rows[i]["ModuleCode"].ToString();//CourseCode
                string Description = dsISOTrainNum.Tables[0].Rows[i]["Description"].ToString();//CourseRevision
                string Flag = dsISOTrainNum.Tables[0].Rows[i]["Falg"].ToString();//Falg
                string strFlag = "";
                if (Flag == "1")
                {
                    strFlag = "missing in Easy Training";
                }
                else if (Flag == "2")
                {
                    strFlag = "missing in ISOtrain";
                }
                else if (Flag == "3")
                {
                    strFlag = "differs between ISOtrain and Easy Training(in Easy Training)";
                }
                //strForExcel += "<td><div class=\"datagrid-cell\">" + EasyTrainingNum + "</div></td>";
                //strForExcel += "<td><div class=\"datagrid-cell\">" + ISOTrainNum + "</div></td>";
                IRow RowBody = sheet.CreateRow(i + 1);
                RowBody.CreateCell(0).SetCellValue(ModuleCode);
                RowBody.CreateCell(1).SetCellValue(Description);
                RowBody.CreateCell(2).SetCellValue(strFlag);
            }
            return sheet;


        }
        protected ISheet createSheetCourseDetail(HSSFWorkbook workBook, string sheetName)
        {
            string strForExcel = "<table border='1'><tr><td></td><td>CourseTitleDescription</td><td>TaskLevel</td><td>GraceDays</td><td>NeedRetraining</td><td>Frequency</td><td>Status</td></tr>";
            StringBuilder sbSel = new StringBuilder();
            sbSel.Append("   select T.*,h.userID,h.dept,h.costCenterArea from ( ");
            sbSel.Append("  SELECT  T1.CourseCode,T1.CourseTitleDescription,T1.TaskLevel,T1.NotifyDays,T1.Frequency,1 as Falg FROM    ( ");
    sbSel.Append(" select T1b.*  from (SELECT    a.CourseCode FROM    ISOTrain_Relation a where substring(a.CourseCode,1,1)<>'Q'");
	sbSel.Append(" EXCEPT SELECT   b.CourseCode FROM      form_Course b where b.Status='0') T1a ");
	sbSel.Append(" inner  join ISOTrain_Relation T1b on T1a.CourseCode=T1b.CourseCode ) AS T1 ");
    sbSel.Append("  union   SELECT   T2.CourseCode,T2.CourseTitleDescription,T2.TaskLevel,T2.NotifyDays,T2.Frequency,2 as Falg FROM    (  ");  
  sbSel.Append("  select T1b.*  from ( SELECT   c.CourseCode FROM      form_Course c where c.Status='0' ");
  sbSel.Append(" EXCEPT SELECT     d.CourseCode FROM      ISOTrain_Relation d where substring(d.CourseCode,1,1)<>'Q') T1a ");
  sbSel.Append(" inner  join form_Course T1b on T1a.CourseCode=T1b.CourseCode  and T1b.Status='0') AS T2 ");
sbSel.Append(" union SELECT  *,3 as Falg FROM   ( ");  
sbSel.Append(" SELECT   c.CourseCode,c.CourseTitleDescription,'' as TaskLevel,'' as NotifyDays,'' as Frequency FROM      form_Course c ");
sbSel.Append(" inner join ISOTrain_Relation d on c.CourseCode=d.CourseCode and c.Status='0' and  substring(d.CourseCode,1,1)<>'Q' and c.CourseTitleDescription<>d.CourseTitleDescription) as T3 ");
sbSel.Append(" union SELECT  distinct *,3 as Falg FROM   ( "); 
sbSel.Append(" SELECT   c.CourseCode,'' as CourseTitleDescription,'' as TaskLevel,c.NotifyDays,'' as Frequency FROM      form_Course c ");
sbSel.Append(" inner join ISOTrain_Relation d on c.CourseCode=d.CourseCode and c.Status='0' and  c.NotifyDays<>d.NotifyDays ) as T5 ");
sbSel.Append(" union SELECT distinct *,3 as Falg FROM   (  ");
sbSel.Append(" SELECT   cT.CourseCode,'' as CourseTitleDescription,cT.TaskLevel,'' as NotifyDays,'' as Frequency FROM  ");    
sbSel.Append(" (SELECT   c.CourseCode,(case  when c.TaskLevel like '%R-U%'  then  'R-U' when c.TaskLevel like '%Job%'  then  'JOB'   end ) as TaskLevel2,Status,TaskLevel FROM      form_Course c   )as cT ");
sbSel.Append(" inner join ISOTrain_Relation d on cT.CourseCode=d.CourseCode and cT.Status='0' and  cT.TaskLevel2<>d.TaskLevel  ) as T4 ");
sbSel.Append(" union SELECT distinct *,3 as Falg FROM   (  ");
sbSel.Append(" SELECT   cT.CourseCode,'' as CourseTitleDescription,'' as TaskLevel,'' as NotifyDays,cT.Frequency as Frequency FROM ");
sbSel.Append(" (SELECT   c.CourseCode,(case  when c.Frequency=''  then  '0' when c.Frequency!='' then  Frequency end ) as Frequency2,Status,Frequency FROM      form_Course c   )as cT ");
  sbSel.Append("    inner join ISOTrain_Relation d on cT.CourseCode=d.CourseCode and cT.Status='0' and cT.Frequency2!=d.Frequency  ) as T6 ");
     
     
sbSel.Append(" ) as T ");
sbSel.Append("  left  join form_Course  cc on  cc.CourseCode= T.CourseCode  and cc.Status='0' ");
sbSel.Append("  left  join (select  userID,dept,costCenterArea from hr_info) h on  h.userID= cc.initial  ");


            DataSet dsISOTrainNum = DBUtility.DbHelperSQL.Query(sbSel.ToString());
            ISheet sheet = workBook.CreateSheet(sheetName);
            sheet.AutoSizeColumn(0);
            sheet.AutoSizeColumn(1);
            sheet.AutoSizeColumn(2);
            IRow RowHead = sheet.CreateRow(0);
            RowHead.CreateCell(0).SetCellValue("CourseCode");
            RowHead.CreateCell(1).SetCellValue("Owner");
            RowHead.CreateCell(2).SetCellValue("Department");
            RowHead.CreateCell(3).SetCellValue("Area");
            RowHead.CreateCell(4).SetCellValue("CourseTitleDescription");
            RowHead.CreateCell(5).SetCellValue("TaskLevel");
            RowHead.CreateCell(6).SetCellValue("GraceDays");
            RowHead.CreateCell(7).SetCellValue("Frequency");
            RowHead.CreateCell(8).SetCellValue("Falg");
            for (int i = 0; i < dsISOTrainNum.Tables[0].Rows.Count; i++)
            {
                // string ISOTrainNum = dsISOTrainNum.Tables[0].Rows[i]["ISOTrainNum"].ToString();//ISOTrainNum
                //string EasyTrainingNum = dsISOTrainNum.Tables[0].Rows[i]["EasyTrainingNum"].ToString();//EasyTrainingNum

                string CourseCode = dsISOTrainNum.Tables[0].Rows[i]["CourseCode"].ToString();//CourseCode
                string userID = dsISOTrainNum.Tables[0].Rows[i]["userID"].ToString();//userID
                string dept = dsISOTrainNum.Tables[0].Rows[i]["dept"].ToString();//dept
                string costCenterArea = dsISOTrainNum.Tables[0].Rows[i]["costCenterArea"].ToString();//costCenterArea
                string CourseTitleDescription = dsISOTrainNum.Tables[0].Rows[i]["CourseTitleDescription"].ToString();//CourseRevision
                string TaskLevel = dsISOTrainNum.Tables[0].Rows[i]["TaskLevel"].ToString();//EffectiveDate
                string NotifyDays = dsISOTrainNum.Tables[0].Rows[i]["NotifyDays"].ToString();//EffectiveDate
                //string NeedRetraining = dsISOTrainNum.Tables[0].Rows[i]["NeedRetraining"].ToString();//EffectiveDate
                string Frequency = dsISOTrainNum.Tables[0].Rows[i]["Frequency"].ToString();//EffectiveDate
                string Flag = dsISOTrainNum.Tables[0].Rows[i]["Falg"].ToString();//Falg
                string strFlag = "";
                if (Flag == "1")
                {
                    strFlag = "missing in Easy Training";
                }
                else if (Flag == "2")
                {
                    strFlag = "missing in ISOtrain";
                }
                else if (Flag == "3")
                {
                    strFlag = "differs between ISOtrain and Easy Training(in Easy Training)";
                }
                IRow RowBody = sheet.CreateRow(i + 1);
                RowBody.CreateCell(0).SetCellValue(CourseCode);
                RowBody.CreateCell(1).SetCellValue(userID);
                RowBody.CreateCell(2).SetCellValue(dept);
                RowBody.CreateCell(3).SetCellValue(costCenterArea);
                RowBody.CreateCell(4).SetCellValue(CourseTitleDescription);
                RowBody.CreateCell(5).SetCellValue(TaskLevel);
                RowBody.CreateCell(6).SetCellValue(NotifyDays);
                //RowBody.CreateCell(7).SetCellValue(NeedRetraining);
                RowBody.CreateCell(7).SetCellValue(Frequency);
                RowBody.CreateCell(8).SetCellValue(strFlag);
                strForExcel += "<tr>";
                //strForExcel += "<td><div class=\"datagrid-cell\">" + EasyTrainingNum + "</div></td>";
                //strForExcel += "<td><div class=\"datagrid-cell\">" + ISOTrainNum + "</div></td>";
            }
            return sheet;



        }
        protected ISheet createSheetModule2JF(HSSFWorkbook workBook, string sheetName)
        {
            StringBuilder sbSel = new StringBuilder();
            sbSel.Append("   select T.*,h.userID,h.dept,h.costCenterArea from ( ");
            sbSel.Append("  SELECT  T4.code,T4.ModuleCode,1 as Falg FROM    ( ");
            sbSel.Append("  SELECT    a.code, a.ModuleCode FROM    ISOTrain_Relation a ");
            sbSel.Append(" EXCEPT SELECT   j.code,m.ModuleCode FROM      Module2JF mj ,Module m ,Jobfunction j where ");
            sbSel.Append("  m.Status='0' and j.Status='0' and mj.Satatus='0' and mj.JFID=j.ID and mj.JFCode=j.code and mj.ModuleID=m.ID ");
            sbSel.Append(" ) AS T4 ");
            sbSel.Append(" union   SELECT   T5.code,T5.ModuleCode,2 as Falg FROM    (  ");
            sbSel.Append(" SELECT   j.code,m.ModuleCode FROM      Module2JF mj ,Module m ,Jobfunction j where ");
            sbSel.Append("  m.Status='0' and j.Status='0' and mj.Satatus='0' and mj.JFID=j.ID and mj.JFCode=j.code and mj.ModuleID=m.ID ");
            sbSel.Append(" EXCEPT SELECT   d.code,  d.ModuleCode FROM      ISOTrain_Relation d ");
            sbSel.Append(" ) AS T5 ) as T ");
            sbSel.Append(" left join Jobfunction j on j.code=T.code ");
            sbSel.Append(" left join (select  userID,dept,costCenterArea from hr_info) h on  h.userID= j.owner ");


            DataSet dsISOTrainNum = DBUtility.DbHelperSQL.Query(sbSel.ToString());
            ISheet sheet = workBook.CreateSheet(sheetName);
            sheet.AutoSizeColumn(0);
            sheet.AutoSizeColumn(1);
            sheet.AutoSizeColumn(2);
            sheet.AutoSizeColumn(3);
            sheet.AutoSizeColumn(4);
            sheet.AutoSizeColumn(5);
            IRow RowHead = sheet.CreateRow(0);
            RowHead.CreateCell(0).SetCellValue("Job Function Code");
            RowHead.CreateCell(1).SetCellValue("Owner");
            RowHead.CreateCell(2).SetCellValue("Dept");
            RowHead.CreateCell(3).SetCellValue("Area");
            RowHead.CreateCell(4).SetCellValue("ModuleCode");
            RowHead.CreateCell(5).SetCellValue("Status");

            for (int i = 0; i < dsISOTrainNum.Tables[0].Rows.Count; i++)
            {
                // string ISOTrainNum = dsISOTrainNum.Tables[0].Rows[i]["ISOTrainNum"].ToString();//ISOTrainNum
                //string EasyTrainingNum = dsISOTrainNum.Tables[0].Rows[i]["EasyTrainingNum"].ToString();//EasyTrainingNum

                string code = dsISOTrainNum.Tables[0].Rows[i]["code"].ToString();//CourseCode
                string userID = dsISOTrainNum.Tables[0].Rows[i]["userID"].ToString();//userID
                string dept = dsISOTrainNum.Tables[0].Rows[i]["dept"].ToString();//dept
                string costCenterArea = dsISOTrainNum.Tables[0].Rows[i]["costCenterArea"].ToString();//costCenterArea
                string ModuleCode = dsISOTrainNum.Tables[0].Rows[i]["ModuleCode"].ToString();//CourseRevision
                string Flag = dsISOTrainNum.Tables[0].Rows[i]["Falg"].ToString();//Falg
                string strFlag = "";
                if (Flag == "1")
                {
                    strFlag = "missing in Easy Training";
                }
                else if (Flag == "2")
                {
                    strFlag = "missing in ISOtrain";
                }
                else if (Flag == "3")
                {
                    strFlag = "differs between ISOtrain and Easy Training";
                }
                IRow RowBody = sheet.CreateRow(i + 1);
                RowBody.CreateCell(0).SetCellValue(code);
                RowBody.CreateCell(1).SetCellValue(userID);
                RowBody.CreateCell(2).SetCellValue(dept);
                RowBody.CreateCell(3).SetCellValue(costCenterArea);
                RowBody.CreateCell(4).SetCellValue(ModuleCode);
                RowBody.CreateCell(5).SetCellValue(strFlag);

            }
            return sheet;
        }
        protected ISheet createSheetCourse2Module(HSSFWorkbook workBook, string sheetName)
        {
            StringBuilder sbSel = new StringBuilder();
            sbSel.Append("      select T.*,cc.initial,sop.initial,h.userID,h.dept,h.costCenterArea from ( ");
 sbSel.Append(" SELECT  T4.ModuleCode,T4.CourseCode,1 as Falg FROM    ( ");
  sbSel.Append("  SELECT    a.CourseCode, a.ModuleCode FROM    ISOTrain_Relation a where  substring(a.CourseCode,1,1)<>'Q' ");
	sbSel.Append(" EXCEPT SELECT   c.CourseCode,m.ModuleCode FROM      Course2Module cm ,Module m ,form_Course c where ");
    sbSel.Append(" m.Status='0' and c.Status='0' and cm.Sataus='0' and cm.CourseID=c.ID and cm.ModuleID=m.ID ");
	sbSel.Append(" ) AS T4 ");
sbSel.Append(" union   SELECT   T5.ModuleCode,T5.CourseCode,2 as Falg FROM    (  ");  
 sbSel.Append(" SELECT   c.CourseCode,m.ModuleCode FROM      Course2Module cm ,Module m ,form_Course c where ");
  sbSel.Append("  m.Status='0' and c.Status='0' and cm.Sataus='0' and cm.CourseID=c.ID and cm.ModuleID=m.ID ");
  sbSel.Append(" EXCEPT SELECT   d.CourseCode,  d.ModuleCode FROM      ISOTrain_Relation d where  substring(d.CourseCode,1,1)<>'Q' ");
sbSel.Append(" ) AS T5 ");
sbSel.Append(" union  SELECT  T6.ModuleCode,T6.CourseCode,1 as Falg FROM    ( ");
   sbSel.Append(" SELECT    a.CourseCode, a.ModuleCode FROM    ISOTrain_Relation a where  substring(a.CourseCode,1,1)='Q' ");
	sbSel.Append(" EXCEPT SELECT   c.SOPNo as CourseCode,m.ModuleCode FROM      Course2Module cm ,Module m ,form_SOP c where ");
    sbSel.Append(" m.Status='0' and c.Status='0' and cm.Sataus='0' and cm.CourseID=c.ID and cm.ModuleID=m.ID ");
	sbSel.Append(" ) AS T6 ");
sbSel.Append(" union   SELECT   T7.ModuleCode,T7.CourseCode,2 as Falg FROM    (   "); 
 sbSel.Append(" SELECT   c.SOPNo as CourseCode,m.ModuleCode FROM      Course2Module cm ,Module m ,form_SOP c where ");
    sbSel.Append(" m.Status='0' and c.Status='0' and cm.Sataus='0' and cm.CourseID=c.ID and cm.ModuleID=m.ID ");
  sbSel.Append(" EXCEPT SELECT   d.CourseCode,  d.ModuleCode FROM      ISOTrain_Relation d where  substring(d.CourseCode,1,1)='Q'");
sbSel.Append(" ) AS T7) as T ");
  sbSel.Append("          left  join form_Course  cc on  cc.CourseCode= T.CourseCode  ");
 sbSel.Append(" left  join form_sop  sop on  sop.SOPNo = T.CourseCode  ");
 sbSel.Append("     left  join (select  userID,dept,costCenterArea from hr_info) h on  h.userID= cc.initial or h.userID= sop.initial ");


            DataSet dsISOTrainNum = DBUtility.DbHelperSQL.Query(sbSel.ToString());
            ISheet sheet = workBook.CreateSheet(sheetName);
            IRow RowHead = sheet.CreateRow(0);
            RowHead.CreateCell(0).SetCellValue("ModuleCode");
            RowHead.CreateCell(1).SetCellValue("CourseCode");
            RowHead.CreateCell(2).SetCellValue("Owner");
            RowHead.CreateCell(3).SetCellValue("Dept");
            RowHead.CreateCell(4).SetCellValue("Area");
            RowHead.CreateCell(5).SetCellValue("Status");

            for (int i = 0; i < dsISOTrainNum.Tables[0].Rows.Count; i++)
            {
                IRow RowBody = sheet.CreateRow(i + 1);
                string ModuleCode = dsISOTrainNum.Tables[0].Rows[i]["ModuleCode"].ToString();//CourseCode
                string CourseCode = dsISOTrainNum.Tables[0].Rows[i]["CourseCode"].ToString();//CourseRevision
                string userID = dsISOTrainNum.Tables[0].Rows[i]["userID"].ToString();//userID
                string dept = dsISOTrainNum.Tables[0].Rows[i]["dept"].ToString();//dept
                string costCenterArea = dsISOTrainNum.Tables[0].Rows[i]["costCenterArea"].ToString();//costCenterArea
                string Flag = dsISOTrainNum.Tables[0].Rows[i]["Falg"].ToString();//Falg
                string strFlag = "";
                if (Flag == "1")
                {
                    strFlag = "missing in Easy Training";
                }
                else if (Flag == "2")
                {
                    strFlag = "missing in ISOtrain";
                }
                else if (Flag == "3")
                {
                    strFlag = "differs between ISOtrain and Easy Training";
                }
                RowBody.CreateCell(0).SetCellValue(ModuleCode);
                RowBody.CreateCell(1).SetCellValue(CourseCode);
                RowBody.CreateCell(2).SetCellValue(userID);
                RowBody.CreateCell(3).SetCellValue(dept);
                RowBody.CreateCell(4).SetCellValue(costCenterArea);
                RowBody.CreateCell(5).SetCellValue(strFlag);
                
            }
            return sheet;
        }


        protected void Btn_CompareCourse2Module_Click(object sender, EventArgs e)
        {
            string fileName = "CompareRelation.xls";
            string path = this.MapPath(fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            HSSFWorkbook workBook = new HSSFWorkbook();
            createSheetJobFunction(workBook, "CompareJobFunction"); //JobFunction
            createSheetModule(workBook, "CompareModule"); //Module
            createSheetCourseDetail(workBook, "CompareCourseDetail"); //CourseDetail
            createSheetModule2JF(workBook, "CompareModule2JF"); //Module2JF
            createSheetCourse2Module(workBook, "CompareCourse2Module"); //Course2Module
            
            using (FileStream file = new FileStream(path, FileMode.Create))
            {
                workBook.Write(file);　　//创建Excel文件。
                file.Close();
                MessageBox.Show(this, "比对成功，可以导出Excel!/The comparison is successful, you can export Excel!");
            }
        }
        protected void btn_Course2ModuleExport_Click(object sender, EventArgs e)
        {
            string fileName = "CompareRelation.xls";
            string path = this.MapPath(fileName);
            download(path, fileName);
        }
        #endregion

        private void download(String strInstallFile, String fileName)
        {

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(strInstallFile);

            Page.Response.ContentType = "APPLICATION/OCTET-STREAM";

            Page.Response.AddHeader("Content-length", fileInfo.Length.ToString());



            /*IE浏览器*/

            if (Request.Browser.IsBrowser("IE"))
            {



                Page.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.GetEncoding("GBK")));

            }

            else
            {

                Page.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);

            }

            Page.Response.WriteFile(strInstallFile);

            Response.Flush();



        }
        protected void Export(string fileName, Label lab)
        {
            Response.Charset = "GB2312";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            string styleRG = "<meta http-equiv=\"content-type\' content=\"application/ms-excel; charset=gb2312\"/>";
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            lab.RenderControl(htw);
            Response.Write(styleRG);
            Response.Write(@"<style>.tb.td{border-right:solid 1px red;}</style> ");
            Response.Write(sw.ToString());
            Response.End();
        }
    }
}