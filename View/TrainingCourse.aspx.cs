using DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;

namespace MatrixTool.View
{
    public partial class TrainingCourse : System.Web.UI.Page
    {
        /// <summary>    
        /// 页数（从1开始）
        /// </summary>
        int pageNo = 0;
        /// <summary>
        /// 内容条数
        /// </summary>
        int pageSize = 20;

        int newpageNo;
        int num;
        DataTable dtExcel;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                lblCurrentPage.Text = 1.ToString();
                BindData();
                AddShowTrueOrfalse();
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        protected void BindData()
        {
            if (newpageNo == 0)
            {
                pageNo = Convert.ToInt32(lblCurrentPage.Text.Trim());
            }
            else
            {
                pageNo = newpageNo;
            }
            string SerachForm_Course = txtSerachForm_Course.Text.Trim();
            string SerachInitial = txtSerachInitial.Text.Trim();
            string SerachTime = txtSerachTime.Value;

            if (string.IsNullOrEmpty(SerachForm_Course))
            {
                SerachForm_Course = " 1=1 ";
            }
            else
            {
                SerachForm_Course = " a.CourseCode = '" + SerachForm_Course + "' ";
            }
            if (string.IsNullOrEmpty(SerachInitial))
            {
                SerachInitial = " 1=1 ";
            }
            else
            {
                SerachInitial = " a.Trainer = '" + SerachInitial + "' ";
            }
            if (string.IsNullOrEmpty(SerachTime))
            {
                SerachTime = " 1=1 ";
            }
            else
            {
                SerachTime = " StartTime < '" + SerachTime + "' and EndTime > '" + SerachTime + "'";
            }
            string str = "select a.ID,a.CourseCode,a.Trainer,b.CourseTitleDescription,StartTime,EndTime,Address,CreateUser,CreateTime from TrainingCourse a left join form_Course b on a.CourseCode = b.CourseCode where " + SerachForm_Course + " and " + SerachInitial + " and " + SerachTime + "order by 'StartTime'";
            DataSet ds = DbHelperSQL.Query(str);
            DataTable dt = ds.Tables[0];
            dtExcel = dt;
            num = dt.Rows.Count;
            //dt.Columns.Add("Time", typeof(string));
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    dt.Rows[i]["Time"] = dt.Rows[i]["StartTime"].ToString() + " -- " + dt.Rows[i]["EndTime"].ToString();
            //}
            DataTable dataTableSerach;
            dataTableSerach = getOnePageTable(dt, pageNo, pageSize);
            GridView1.DataSource = dataTableSerach;
            GridView1.DataBind();
            if (num % pageSize == 0)
            {
                labAll.Text = (num / pageSize).ToString();
            }
            else
            {
                labAll.Text = ((num / pageSize) + 1).ToString();
            }

            this.ddlCurrentPage.Items.Clear();
            for (int i = 1; i <= Convert.ToInt32(labAll.Text); i++)
            {
                this.ddlCurrentPage.Items.Add(i.ToString());
            }
        }

        /// <summary>
        /// 添加按钮的显示和隐藏
        /// </summary>
        protected void AddShowTrueOrfalse()
        {
            string User = Session["userid"].ToString();
            string str = @"select count(0) from hr_info a 
            inner join Trainer b on a.userID = b.TrainerInitial
            inner join form_Course c  on b.CourseID = c.ID  and c.Status = '0'
            where a.userID = '" + User + "' and a.Status != 'Desert'  and b.Status = '0'";
            bool b1 = DbHelperSQL.Exists(str);

            if (b1)
            {
                this.Btn_Add.Style["display"] = "True";
            }
            else
            {
                this.Btn_Add.Style["display"] = "none";
            }
        }

        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_Search_Click(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// 添加按钮初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_Add_Click(object sender, EventArgs e)
        {
            this.windowbg.Style["display"] = "block";
            this.divAddUser.Style["display"] = "block";
            txtForm_Course.Text = null;
            txtInitial.Text = null;
            StartCalDate.Value = null;
            EndCalDate.Value = null;
            txtAddress.Text = null;
        }

        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_Export_Click(object sender, EventArgs e)
        {
            BindData();
            dtExcel.Columns.Remove("ID");
            dtExcel.Columns.Remove("CreateUser");
            dtExcel.Columns.Remove("CreateTime");
            //for (int i = 0; i < dtExcel.Rows.Count; i++)
            //{
            //    //dtExcel.Rows[i]["StartTime"] = dtExcel.Rows[i]["StartTime"].ToString();
            //    dtExcel.Rows[i]["StartTime"] = string.Format("{0:G}", dtExcel.Rows[i]["StartTime"]);
            //    dtExcel.Rows[i]["EndTime"] = string.Format("{0:G}", dtExcel.Rows[i]["EndTime"]);
            //}
            Export("CourseExcel", dtExcel, "CourseExcel");
        }

        /// <summary>
        /// 课程的添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnFirst_Click(object sender, EventArgs e)
        {
            string ID = Guid.NewGuid().ToString();
            string CourseCode = txtForm_Course.Text.Trim();
            string Initial = txtInitial.Text.Trim();

            string StartTime = StartCalDate.Value;
            string EndTime = EndCalDate.Value;
            string Address = txtAddress.Text.Trim();
            string CreateUser = Session["userid"].ToString();
            DateTime CreateTime = DateTime.Now;

            string str1 = "select count(0) from form_Course where CourseCode = '" + CourseCode + "' and Status = 0";
            string str2 = @"select count(0) from hr_info a 
            inner join Trainer b on a.userID = b.TrainerInitial
            inner join form_Course c  on b.CourseID = c.ID  and c.Status = '0'
            where a.userID = '" + Initial + "' and a.Status != 'Desert'  and b.Status = '0' and c.CourseCode = '" + CourseCode + "'";
            List<Check> list = new List<Check>();
            bool a = DbHelperSQL.Exists(str1);
            bool b = DbHelperSQL.Exists(str2);
            Check check = new Check();
            check.s1 = a;
            check.s2 = b;
            string ErrorTimeEmpty;
            if (string.IsNullOrEmpty(StartTime) && string.IsNullOrEmpty(EndTime))
            {
                ErrorTimeEmpty = " 时间输入不能空";
                check.s3 = false;
            }
            else
            {
                ErrorTimeEmpty = null;
                check.s3 = Convert.ToDateTime(EndTime) > Convert.ToDateTime(StartTime);
            }
            check.s4 = !string.IsNullOrEmpty(Address);
            list.Add(check);
            string Error = "创建新课程失败，因为";
            string ErrorCourseCode = " 该课程非有效课程";
            string ErrorInitial = " 该讲师非有效讲师";
            string ErrorTime = " 输入时间有误";
            string ErrorAddress = " 地址不能为空";
            string EndError = "。";

            if (list[0].s1 & list[0].s2 & list[0].s3 & list[0].s4)
            {
                string str = "INSERT INTO TrainingCourse (ID, CourseCode,Trainer,StartTime,EndTime,Address,CreateUser,CreateTime) VALUES ('" + ID + "', '" + CourseCode + "','" + Initial + "','" + StartTime + "','" + EndTime + "','" + Address + "','" + CreateUser + "','" + CreateTime + "')";
                var s = DbHelperSQL.ExecuteSql(str);
                this.windowbg.Style["display"] = "none";
                this.divAddUser.Style["display"] = "none";
                BindData();
                Response.Write("<script>alert('成功添加新课程 ~~ ')</script>");
            }
            else
            {
                foreach (var item in list)
                {
                    if (item.s1 == false)
                    {
                        Error = Error + ErrorCourseCode;
                    }
                    if (item.s2 == false)
                    {
                        Error = Error + ErrorInitial;
                    }
                    if (item.s3 == false)
                    {
                        if (!string.IsNullOrEmpty(ErrorTimeEmpty))
                        {
                        }
                        else
                        {
                            Error = Error + ErrorTime;
                        }
                    }
                    if (item.s4 == false)
                    {
                        Error = Error + ErrorAddress;
                    }
                }
                Error = Error + ErrorTimeEmpty + EndError;
                Response.Write("<script>alert('" + Error + "')</script>");
            }

        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void lbtDeleteJF_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 添加框的隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            //图层隐藏
            this.windowbg.Style["display"] = "none";
            this.divAddUser.Style["display"] = "none";
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnFrist_Click(object sender, EventArgs e)
        {
            newpageNo = 1;
            lblCurrentPage.Text = 1.ToString();
            BindData();
        }

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnPre_Click(object sender, EventArgs e)
        {
            pageNo = Convert.ToInt32(lblCurrentPage.Text);
            if (pageNo == 1)
            {
                pageNo = 1;
                Response.Write("<script>alert('当前页已经是首页！')</script>");
            }
            else
            {
                pageNo = pageNo - 1;
                lblCurrentPage.Text = pageNo.ToString();
            }
            BindData();
        }

        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnNext_Click(object sender, EventArgs e)
        {
            pageNo = Convert.ToInt32(lblCurrentPage.Text);
            if (pageNo == Convert.ToInt32(labAll.Text))
            {
                pageNo = Convert.ToInt32(labAll.Text);
                Response.Write("<script>alert('当前页已经是末页！')</script>");
            }
            else
            {
                pageNo = pageNo + 1;
                lblCurrentPage.Text = pageNo.ToString();

            }
            BindData();
        }

        /// <summary>
        /// 末尾页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnLast_Click(object sender, EventArgs e)
        {
            newpageNo = Convert.ToInt32(labAll.Text);
            lblCurrentPage.Text = labAll.Text;
            BindData();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = this.ddlCurrentPage.SelectedIndex;
            BindData();
        }

        /// <summary>
        /// DataTable分页并取出指定页码的数据
        /// </summary>
        /// <param name="dtAll">DataTable</param>
        /// <param name="pageNo">页码,注意：从1开始</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns>指定页码的DataTable数据</returns>
        private DataTable getOnePageTable(DataTable dtAll, int pageNo, int pageSize)
        {
            var totalCount = dtAll.Rows.Count;
            var totalPage = getTotalPage(totalCount, pageSize);
            var currentPage = pageNo;
            currentPage = (currentPage > totalPage ? totalPage : currentPage);//如果PageNo过大，则较正PageNo=PageCount
            currentPage = (currentPage <= 0 ? 1 : currentPage);//如果PageNo<=0，则改为首页
            //----克隆表结构到新表
            var onePageTable = dtAll.Clone();
            //----取出1页数据到新表
            var rowBegin = (currentPage - 1) * pageSize;
            var rowEnd = currentPage * pageSize;
            rowEnd = (rowEnd > totalCount ? totalCount : rowEnd);
            for (var i = rowBegin; i <= rowEnd - 1; i++)
            {
                var newRow = onePageTable.NewRow();
                var oldRow = dtAll.Rows[i];
                foreach (DataColumn column in dtAll.Columns)
                {
                    newRow[column.ColumnName] = oldRow[column.ColumnName];
                }
                onePageTable.Rows.Add(newRow);
            }
            return onePageTable;
        }
        public int getTotalPage(int totalCount, int pageSize)
        {
            var totalPage = (totalCount / pageSize) + (totalCount % pageSize > 0 ? 1 : 0);
            return totalPage;
        }
        protected void CheckCourseCode(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="Exceltitle"></param>
        /// <param name="table"></param>
        /// <param name="title"></param>
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

        public class Check
        {
            public bool s1 { get; set; }
            public bool s2 { get; set; }
            public bool s3 { get; set; }
            public bool s4 { get; set; }

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Write(" <script> top.location.href= '/guide.aspx'; </script> ");
        }
    }
}