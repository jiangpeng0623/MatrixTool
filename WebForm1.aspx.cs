using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.OleDb;
//using LTP.Common;
using Eday;
using DBUtility;
using System.Data.SqlClient;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace MatrixTool
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DbHelperSQL.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["GoverConnectionString"].ToString();
        }
        protected void Btn_upload_Click(object sender, EventArgs e)
        {
            //string path = Server.MapPath("../upload/");
            string path = System.Configuration.ConfigurationManager.AppSettings.GetValues("AttachPath")[0].ToString();
            bool fileOk = false;
            string fileExtension = "";
            if (FileUpload1.HasFile)
            {
                fileExtension = Path.GetExtension(this.FileUpload1.FileName).ToLower();
                if (fileExtension == ".xls")
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
                            string DEPT_CODE = dt.Rows[i][0].ToString();//userID
                            string DESCRIPTION = dt.Rows[i][1].ToString();//userID
                            string EMP_ID = dt.Rows[i][2].ToString();//userID
                            string EMPLOYEE_NAME = dt.Rows[i][3].ToString();//userID
                            string EMP_STATUS = dt.Rows[i][4].ToString();//userID
                            string CLASS_CODE = dt.Rows[i][5].ToString();//userID
                            string EMP_SHIFT = dt.Rows[i][6].ToString();//userID
                            string SUP_NAME = dt.Rows[i][7].ToString();//userID
                            string SERVER_TIME = dt.Rows[i][8].ToString();//userID


                            //if (userID == "" && coursecode == "" && status == "")
                            //{
                            //    nRealDate = i;
                            //    break;
                            //}
                            ////判断非空
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

                            string sqlStr = "insert into EmployeebySupervisor(DEPT_CODE,DESCRIPTION,EMP_ID,EMPLOYEE_NAME,EMP_STATUS,CLASS_CODE,EMP_SHIFT,SUP_NAME,SERVER_TIME,Stime)values('" + DEPT_CODE + "','" + DESCRIPTION + "','" + EMP_ID + "','" + EMPLOYEE_NAME + "','" + EMP_STATUS + "','" + CLASS_CODE + "','" + EMP_SHIFT + "','" + SUP_NAME + "','" + SERVER_TIME + "','" + Stime + "')";
                            DbHelperSQL.ExecuteSql(sqlStr);
                            insertNumber++;
                        }

                        if (insertNumber > 0)
                        {
                            string aa = "数据添加成功!  共添加了" + insertNumber + "条Missing数据; ";
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




                    //}
                    //catch (Exception ee)
                    //{
                    //    string strErr = "上传失败,请与管理员联系!!";
                    //    Console.WriteLine(ee.Message);
                    //    Console.WriteLine(ee.StackTrace);
                    //    MessageBox.Show(this, strErr);
                    //}
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
        //获取excel中的数据
        public DataSet ExcelToDS(string Path)
        {
            Console.WriteLine(Path);
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Path + ";" + "Extended Properties='Excel 8.0;hdr=no;imex=1;'";
            //string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;HDR=Yes;IMEX=1;'";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            string strExcel = "";
            OleDbDataAdapter myCommand = null;
            DataSet ds = null;
            strExcel = "select * from  [Sheet$]";
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            ds = new DataSet();
            myCommand.Fill(ds);
            conn.Close();
            return ds;

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            HSSFWorkbook workBook = new HSSFWorkbook();
            //ISheet sheetA = workBook.CreateSheet("sheetA");
            //ISheet sheetB = workBook.CreateSheet("sheetB");

            createSheet(workBook, "SheetA", getds("EmployeebySupervisor"));
            createSheet(workBook, "SheetB", getds("missingReport"));
            createSheet(workBook, "SheetC", getds("hr_info"));
            string path = Server.MapPath(@"\test.xls");//用这个试试

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (FileStream file = new FileStream(path, FileMode.Create))
            {
                workBook.Write(file);　　//创建Excel文件。
                file.Close();
            }
            //MessageBox.Show(this, "ok");
            //Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            //excel.Application.Workbooks.Add(true);
            //excel.Visible = true;
            //excel.Cells[1, 1] = "参数配置基本表";
            //int rowCount;
            //string sql = "SELECT * from projectInfo";
            //DataSet ds = DbHelperSQL.Query(sql);
            //rowCount = this.dataSet_Config1.config.Rows.Count;//要导入Excel的dataset

            //excel.Cells[3, 1] = "系统编号";
            //excel.Cells[3, 2] = "分系统编号";
            //excel.Cells[3, 3] = "站ID";
            //excel.Cells[3, 4] = "IO模块ID";
            //excel.Cells[3, 5] = "IO模块类型";

            /////添加数据
            //for (int i = 0; i < rowCount; i++) ///每一行
            //{
            //    excel.Cells[i + 4, 1] = this.dataSet_Config1.config.Rows[i]["系统编号"];
            //    excel.Cells[i + 4, 2] = this.dataSet_Config1.config.Rows[i]["分系统编号"];
            //    excel.Cells[i + 4, 3] = this.dataSet_Config1.config.Rows[i]["站ID"];
            //    excel.Cells[i + 4, 4] = this.dataSet_Config1.config.Rows[i]["IO模块ID"];
            //    excel.Cells[i + 4, 5] = this.dataSet_Config1.config.Rows[i]["IO模块类型"];

            //}
        }


        private DataSet  getds(string tablename)
        {
           
            string sql = "SELECT * from  " + tablename + "";
            DataSet dss = DbHelperSQL.Query(sql);
            return dss;
        }
        private ISheet createSheet(HSSFWorkbook workBook, string sheetName,DataSet dss)
        {
            //string sql = "SELECT * from EmployeebySupervisor";
            //DataSet dss = DbHelperSQL.Query(sql);
            //for (int i = 0; i < dss.Tables[0].Columns.Count; i++)//插入标题
            //{
            //    DataRow dr = dss.Tables[0].NewRow();
            //    dr[dss.Tables[0].Columns[i].ColumnName] = dss.Tables[0].Columns[i].ColumnName;
            //    dss.Tables[0].Rows.InsertAt(dr, 0);
            //}
            ISheet sheet = workBook.CreateSheet(sheetName);//
            IRow RowHead = sheet.CreateRow(0);
            for (int iColumnIndex = 0; iColumnIndex < dss.Tables[0].Columns.Count; iColumnIndex++)// 0 , dataset.table[0]row[0].counts,i++
            {

                string aa = dss.Tables[0].Columns[iColumnIndex].ColumnName.ToString();
                RowHead.CreateCell(iColumnIndex).SetCellValue(aa);  //dataset.table[0]row[0][i].value .tostring
            }

            for (int iRowIndex = 0; iRowIndex < 3; iRowIndex++)//3的位置是dataset总行数dss.Tables[0].Rows.Count
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


    }
}