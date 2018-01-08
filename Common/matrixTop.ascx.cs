using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//using wflow.Security;
using System.Security.Principal;
using System.IO;
using DBUtility;

namespace MatrixTool.Common
{
    public partial class matrixTop : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DbHelperSQL.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["GoverConnectionString"].ToString();

            if (!IsPostBack)
            {
                string purl = Request.Url.GetLeftPart(UriPartial.Authority);
                string purl2 = Request.FilePath.ToString();
                string fileName = Server.MapPath("~/english/" + purl2);
                if (purl2.Contains("transnew") && Request.Params["linetype"] != null)
                {
                    purl2 = purl2 + "?linetype=" + Request.Params["linetype"].ToString();
                }
                
                if (!File.Exists(fileName)) this.LEN2.Text = "";

                else if ((purl2.Contains("confirm")) || (purl2.Contains("detail")) || (purl2.Contains("update")))
                    this.LEN2.Text = "";  //<a href=#>[English]</a>
                else
                    this.LEN2.Text = "<a href=" + purl + "/english" + purl2 + "><span class=\"name\">[English]</span></a>";
                /**/
                this.HLeasyOrder.NavigateUrl = purl + "/onePage/FrontPage.aspx";
            }
        }


        protected override void OnInit(EventArgs e)
        {
            if (Session["user"] != null)
            {

            }
            else
            {
                string strLoginName =  WindowsIdentity.GetCurrent().Name;
                 Eday.BLL.hr_info  db= new  Eday.BLL.hr_info();
                 string username = "";
                  string userid = "";
                  try
                  {
                            DataSet ds = db.GetList(" userid='"+strLoginName+"'");	
                            username = ds.Tables[0].Rows[0]["username"].ToString();
                            userid = ds.Tables[0].Rows[0]["userid"].ToString();

                            Session.Remove("username");
                            Session.Add("username", username);
                            Session.Remove("userid");
                            Session.Add("userid", userid);
                  }
                    catch (Exception e1)
                    {
                        username = strLoginName;
                        userid = strLoginName;
                        Response.Redirect("../Error.aspx");
                    }   
                }
            try
            {
                login.Text = Session["username"].ToString();
            }
            catch (Exception e1)
            {
                Response.Redirect("../Error.aspx");

            }
        }    
    }
}