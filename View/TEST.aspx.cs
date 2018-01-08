using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DBUtility;

namespace MatrixTool.View
{
    public partial class TEST : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string SQL = "select bianhao,bname from bumen where zhi='a'";
                DataSet dt = DbHelperSQL.Query(SQL);
                this.DropDownList3.DataSource = dt;
                this.DropDownList3.DataTextField = "bname";
                this.DropDownList3.DataValueField = "bianhao";
                this.DropDownList3.DataBind();
            }
        }

        public void xiaobumen(string xiao)
        {
            string sql = "select bianhao,bname from bumen where zhi='" + xiao + "'";
            DataSet dt = DbHelperSQL.Query(sql);
            this.DropDownList2.DataSource = dt;
            this.DropDownList2.DataTextField = "bname";
            this.DropDownList2.DataValueField = "bianhao";
            this.DropDownList2.DataBind();
        }

        protected void DropDownList1_SelectedIndexChanged1(object sender, EventArgs e)
        {

            string xiao = this.DropDownList1.Text;
            if (xiao == "AP")
            {
                xiaobumen("a1");
            }
            if (xiao == "BS")
            {
                xiaobumen("a2");
            }
            if (xiao == "CVP")
            {
                xiaobumen("a3");
            }
            if (xiao == "DD")
            {
                xiaobumen("a4");
            }
            if (xiao == "FP")
            {
                xiaobumen("a5");
            }
            if (xiao == "PO&cLean")
            {
                xiaobumen("a6");
            }
            if (xiao == "PS")
            {
                xiaobumen("a7");
            }
            if (xiao == "QC")
            {
                xiaobumen("a8");
            }
            if (xiao == "Quality")
            {
                xiaobumen("a9");
            }
            if (xiao == "costcntrtarea")
            {
                xiaobumen("a10");
            }
            if (xiao == "DEPT_DESC")
            {
                xiaobumen("a11");
            }
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {

            string bianhao = this.DropDownList2.SelectedValue;
            //rs(bianhao);
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}