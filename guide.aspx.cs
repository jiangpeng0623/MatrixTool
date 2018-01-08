using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace MatrixTool
{
    public partial class guide : System.Web.UI.Page
    {
        public String importData = String.Empty;
        public String DownLoadData = String.Empty;
        public String importDataTop = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["logType"] == null || Session["logType"].ToString() == "")
                {
                    Response.Write(" <script> top.location.href= '/login.aspx '; </script> ");
                    return;
                }
                else
                {
                    PanelUp01.Style["margin-left"] = "15%";
                    string Type = "Line Manager";
                    if (Session["logType"].ToString().Trim() == "0")
                    {
                        Type = "Line Manager";
                    }
                    else if (Session["logType"].ToString().Trim() == "1")
                    {
                        Type = "Employee";
                    }
                    //labType.Text = Type;
                    //Label1.Text = Type;

                    string userID = Session["userid"].ToString();
                    string rolesName = System.Configuration.ConfigurationManager.AppSettings.GetValues("ImportRole")[0].ToString();
                    if (rolesName.ToUpper().Contains("," + userID.ToUpper() + ",")) //该登录人有导入数据的权限
                    {
                        importData = " <li><span class=\"bull\">&bull;</span> &nbsp; <a href=\"index.aspx?url=View/QualificationUpload.aspx\">数据导入data import</a></li>";
                        importData += " <li><span class=\"bull\">&bull;</span> &nbsp; <a href=\"index.aspx?url=View/CompareDataImport.aspx\">数据对比data compare</a></li>";
                    }
                    else
                    {
                        importDataTop = "<br />";
                    }


                    DownLoadData = " <li><span class=\"bull\">&bull;</span> &nbsp;  <a href=\"index.aspx?url=View/tableone.aspx\">员工过期培训查询ISOtrain Tracker</a></li>";
   
                }
            }
        }
    }
}