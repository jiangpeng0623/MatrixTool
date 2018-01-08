using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Collections;
using DBUtility;

namespace GoverProject
{
    public partial class index1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DbHelperSQL.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["GoverConnectionString"].ToString();

            if (!IsPostBack)
            {
                if (Session["userid"] == null || Session["userid"].ToString() == "")
                {
                    Response.Write(" <script> top.location.href= '/login.aspx '; </script> ");
                    return;
                }
                else
                {
                    if (!(Request.QueryString["url"] == null || Request.QueryString["url"].ToString() == ""))
                    {
                        this.hidurl.Value = Request.QueryString["url"].ToString();
                    }
                }
            }
        }

        protected void imbTC_Click(object sender, ImageClickEventArgs e)
        {
            Response.Write(" <script> top.location.href= '/login.aspx '; </script> ");
            Session.Clear();

        }

    }
}
