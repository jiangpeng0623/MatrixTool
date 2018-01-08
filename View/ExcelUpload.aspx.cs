using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using DBUtility;
using System.Data.OleDb;
using DBUtility;
using System.Data.SqlClient;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using Eday;
using System.Net;
using System.Text;
using System.Diagnostics;
using System.Diagnostics;
namespace MatrixTool.View
{
    public partial class ExcelUpload : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        void DeleteAllExcel(string uploadpath)
        {
                //获取文件夹中所有图片
            if (Directory.GetFileSystemEntries(uploadpath).Length > 0)
            {
                //遍历文件夹中所有文件
                foreach (string file in Directory.GetFiles(uploadpath))
                {
                    //文件己存在
                    if (File.Exists(file))
                    {
                        FileInfo fi = new FileInfo(file);

                        //if (fi.CreationTime < DateTime.Now)
                        //{
                        //判断当前文件属性是否是只读
                        if (fi.Attributes.ToString().IndexOf("ReadyOnly") >= 0)
                        {
                            fi.Attributes = FileAttributes.Normal;
                        }
                        //删除文件
                        File.Delete(file);
                        //}
                    }
                }
            }
        }
        protected void Btn_upload_Click(object sender, EventArgs e)
        {
            string deleMissing = " delete from EmployeebySupervisor";
            int n = DbHelperSQL.ExecuteSql(deleMissing);
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
                    if (FileUpload1.PostedFile.ContentLength > 10240 * 1000)
                    {
                        string strErr = "所选文件超过10M，导入失败!!";
                        //  MessageBox.Show(this, strErr);
                        return;
                    }


                    DataSet ds = new DataSet();
                    String fname = "MissingReport" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    string filename = fname + fileExtension;
                    this.FileUpload1.PostedFile.SaveAs(path + filename);
                    ds = ExcelToDS(path + filename);
                    DataTable dt = ds.Tables[0];

                    int nRealDate = 0;
                    int dtNum = dt.Rows.Count;
                    if (dtNum > 0)
                    {
                        for (int i = 1; i < dtNum; i++)
                        {
                            string AREA = dt.Rows[i][0].ToString();
                            string MODULE_CODE = dt.Rows[i][1].ToString();
                            string MODULE_DESC = dt.Rows[i][2].ToString();
                            string COURSE_CODE = dt.Rows[i][3].ToString();
                            string TASK_CODE = dt.Rows[i][4].ToString();
                            string COURSE_REV = dt.Rows[i][5].ToString();
                            string DESCRIPTION = dt.Rows[i][6].ToString();
                            string DUE_REVISION = dt.Rows[i][7].ToString();
                            string STATUS = dt.Rows[i][8].ToString();
                            string STATUS_MEANING = dt.Rows[i][9].ToString();
                            string ST_QUALIFIER = dt.Rows[i][10].ToString();
                            string QUALIFIER_MEANING = dt.Rows[i][11].ToString();
                            string PENDINGQUAL_INFO = dt.Rows[i][12].ToString();
                            string COMPLIANCE_FLAG = dt.Rows[i][13].ToString();
                            string EMP_ID = dt.Rows[i][14].ToString();
                            string EMP_NAME = dt.Rows[i][15].ToString();
                            string DEPT_CODE = dt.Rows[i][16].ToString();
                            if (AREA == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行DEPT_CODE不能为空！/Employee initial in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }

                            if (MODULE_CODE == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行DESCRIPTION不能为空！/Supervisor in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (MODULE_DESC == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行EMP_ID不能为空！/Course code in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (COURSE_CODE == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行EMPLOYEE_NAME不能为空！/TASK_CODE in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (TASK_CODE == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "EMP_STATUS不能为空！/Course description in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }

                            if (COURSE_REV == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "CLASS_CODE不能为空！/Status in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (DESCRIPTION == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行EMP_SHIFT不能为空！DUE_DATE in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (DUE_REVISION == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行SUP_NAME不能为空！DUE_DATE in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (STATUS == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行SERVER_TIME不能为空！DUE_DATE in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (STATUS_MEANING == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行SERVER_TIME不能为空！DUE_DATE in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (ST_QUALIFIER == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行SERVER_TIME不能为空！DUE_DATE in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (QUALIFIER_MEANING == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行SERVER_TIME不能为空！DUE_DATE in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (PENDINGQUAL_INFO == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行SERVER_TIME不能为空！DUE_DATE in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (COMPLIANCE_FLAG == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行SERVER_TIME不能为空！DUE_DATE in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (EMP_ID == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行SERVER_TIME不能为空！DUE_DATE in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (EMP_NAME == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行SERVER_TIME不能为空！DUE_DATE in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                            if (DEPT_CODE == "")
                            {
                                MessageBox.Show(this, "第" + (i + 1).ToString() + "行SERVER_TIME不能为空！DUE_DATE in row " + (i + 1).ToString() + " can not be empty");
                                return;
                            }
                        }

                        int insertNumber = 0;
                        //批量插入数据
                        for (int i = 1; i < dtNum; i++)
                        {
                            string AREA = dt.Rows[i][0].ToString();
                            string MODULE_CODE = dt.Rows[i][1].ToString();
                            string MODULE_DESC = dt.Rows[i][2].ToString();
                            string COURSE_CODE = dt.Rows[i][3].ToString();
                            string TASK_CODE = dt.Rows[i][4].ToString();
                            string COURSE_REV = dt.Rows[i][5].ToString();
                            string DESCRIPTION = dt.Rows[i][6].ToString();
                            string DUE_REVISION = dt.Rows[i][7].ToString();
                            string STATUS = dt.Rows[i][8].ToString();
                            string STATUS_MEANING = dt.Rows[i][9].ToString();
                            string ST_QUALIFIER = dt.Rows[i][10].ToString();
                            string QUALIFIER_MEANING = dt.Rows[i][11].ToString();
                            string PENDINGQUAL_INFO = dt.Rows[i][12].ToString();
                            string COMPLIANCE_FLAG = dt.Rows[i][13].ToString();
                            string EMP_ID = dt.Rows[i][14].ToString();
                            string EMP_NAME = dt.Rows[i][15].ToString();
                            string DEPT_CODE = dt.Rows[i][16].ToString();
                            string Stime = DateTime.Now.ToString();
                            string sqlStr = "insert into Weekly Dept ITP(AREA，MODULE_CODE，MODULE_DESC，COURSE_CODE，TASK_CODE，COURSE_REV，DESCRIPTION，DUE_REVISION，STATUS，STATUS_MEANING，ST_QUALIFIER，QUALIFIER_MEANING，PENDINGQUAL_INFO，COMPLIANCE_FLAG，EMP_ID，EMP_NAME，DEPT_CODE，Stime)values('" + AREA + "','" + MODULE_CODE + "','" + MODULE_DESC + "','" + COURSE_CODE + "','" + TASK_CODE + "','" + COURSE_REV + "','" + DESCRIPTION + "','" + DUE_REVISION + "','" + STATUS + "','" + STATUS_MEANING + "','" + ST_QUALIFIER + "','" + QUALIFIER_MEANING + "','" + PENDINGQUAL_INFO + "','" + COMPLIANCE_FLAG + "','" + EMP_ID + "','" + EMP_NAME + "','" + DEPT_CODE + "'，'" + Stime + "')";
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

        private DataSet ExcelToDS(string p)
        {
            throw new NotImplementedException();
        }

        private DataSet ExcelToDS(string postFileName, string Path)
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
                strExcel = "select * from  [Employee by Supervisor$]";
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


        protected void ID_Back_Click(object sender, EventArgs e)
        {
            Response.Write(" <script> top.location.href= '/guide.aspx '; </script> ");
            return;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Process p = Process.Start("E:/bg/研发中心/开发项目/杜秋菊/maxtrixtool(2)1020/10月31/MatrixTool/MatrixTool.sln");
            p.WaitForExit();//关键，等待外部程序退出后才能往下执行
        }
    }
}