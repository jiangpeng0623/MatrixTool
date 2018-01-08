using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DBUtility
{
    public static class Work
    {

        //查询工作流状态
        public static string ReturnStatus(string id)
        {
            string values = "";
            string sql = "select p.status from wf_proc p left join wf_node n on p.id = n.pid where n.formid = '" + id + "'";
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                values = dt.Rows[0]["status"].ToString();
            }
            return values;
        }

        //修改状态
        public static void UpdateBaseType(string id,string name)
        {

            string sql = "update baseType set nameId = '" + id + "',name = N'"+name+"' where valueID = 'Week'";
            DbHelperSQL.Query(sql);

        }


        //得到周数
        public static string  getZhou()
        {
            string values = "1";
            string sql = "select nameId from baseType where valueID = 'Week'";
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                values = dt.Rows[0]["nameId"].ToString();
            }
            return values;
        }


        //获取所有parameter
        public static DataTable getParameter(string colum)
        {
            string sql = "select value,name from baseType where valueID = '" + colum + "'";
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            return dt;
        }


        //查询附件
        public static bool AttchExit(string id)
        {
            bool vlaue = false;
            string sql = "select * from EasyTraining_Attachs where formid = '"+id+"'";
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                vlaue = true;
            }
            return vlaue;
        }
 
    }
}
