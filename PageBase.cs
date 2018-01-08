using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBUtility;
using System.Globalization;

namespace MatrixTool
{
    public class PageBase : System.Web.UI.Page
    {
        public PageBase()
        {

        }
        protected override void OnLoad(EventArgs e)
        {
            //在初始化子页面之前，判断用户的登录是否已登陆，以及访问页面权限
            if (Session["userID"] == null)
            {
                Response.Write(" <script> alert(\"页面已过期!\"); </script> ");
                Response.End();
            }
            else
            {
                //ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscriptBasePage", "<script> parent.loaddatainprocesshid(); </script>");
            }
            //初始化 DbHelperSQL 的数据库
            DbHelperSQL.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["GoverConnectionString"].ToString();
            //加载子页的onload。
            base.OnLoad(e);
        }
    }
}
