using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DBUtility;
using System.IO;
using System.Security.Principal;

namespace GoverProject
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //string strLoginName = WindowsIdentity.GetCurrent().Name; // WindowsIdentity.GetCurrent().Name;
                string strLoginName = "CXZH"; // WindowsIdentity.GetCurrent().Name;
                string name = strLoginName;

                int index=strLoginName.IndexOf("\\");
                if (index > -1)
                {
                    name=strLoginName.Substring(index + 1);
                }
                tbname.Text = name;
            }
        }
        protected void blogin_Click(object sender, EventArgs e)
        {
            String uname = tbname.Text;
            //string sessiontimeout = System.Configuration.ConfigurationManager.AppSettings.GetValues("sessiontimeout")[0].ToString();
            DbHelperSQL.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["GoverConnectionString"].ToString();

            Eday.BLL.hr_info bll = new Eday.BLL.hr_info();
            string where = " userid = '";
            where += uname;
            where += "' ";
            DataSet ds = bll.GetList(where); ;
            if (ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show(this, "用户名不存在！/Username does not exist!");
            }
            else
            {
                if (ds.Tables[0].Rows[0]["status"].ToString().ToUpper() == "DESERT")
                {
                    MessageBox.Show(this, "该用户已离职!/The user has left!");
                }
                else
                {
                    if (RadioType.SelectedIndex == 0) //直线经理登陆
                    {
                        string whereD = " director = '";
                        whereD += uname;
                        whereD += "' ";
                        DataSet dsD = bll.GetList(whereD); ;
                        if (dsD.Tables[0].Rows.Count == 0)
                        {

                            MessageBox.Show(this, "您不是直线经理，不能用直线经理登陆!/You are not a line manager,you can not use the line manager permission!");
                            return; 
                        }
                    }
                    HttpContext.Current.Session["id"] = ds.Tables[0].Rows[0]["id"].ToString();
                    HttpContext.Current.Session["userid"] = ds.Tables[0].Rows[0]["userid"].ToString();
                    HttpContext.Current.Session["username"] = ds.Tables[0].Rows[0]["username"].ToString();
                    HttpContext.Current.Session["logType"] = RadioType.SelectedIndex;
                    //HttpContext.Current.Session.Timeout = int.Parse(sessiontimeout);
                    string url = "guide.aspx";
                    Response.Redirect(url, true);
                }
            }
            
        }

        protected void LocationBlogin(string uname)
        {

            string url = "";
            url = "guide.aspx";
            Response.Redirect(url, true);
        
        }


        protected void btnReset_Click(object sender, EventArgs e)
        {
            tbname.Text = "USER_ID";
        } 
    }
}
