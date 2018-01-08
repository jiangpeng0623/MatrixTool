using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DBUtility
{
    public class workFlow
    {
        protected string GetFormValue(string rowName, string tableName, string formid)
        {
            string str = "";
            string strSql = "select " + rowName + " from " + tableName + " where id='" + formid + "'";
            DataTable dt = DbHelperSQL.Query(strSql).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    for (int columns = 0; columns < dt.Columns.Count; columns++)
                    {
                        string form_value = dt.Rows[i][columns] == null ? "" : dt.Rows[i][columns].ToString();
                        if (form_value != "")
                        {
                            str += form_value + ";";
                        }
                    }
                }
            }
            if (str == "")
            {
                return str;
            }
            else
            {
                return str.Substring(0, str.Length - 1);
            }
        }
        /// <summary>
        /// 返回该节点特殊关联值
        /// </summary>
        /// <param name="rowName">表单列</param>
        /// <param name="tableName">表单表</param>
        /// <param name="formid">表单申请ID</param>
        /// <returns>特殊关联值</returns>
        public string ReturnValue(string rowName, string tableName, string formid)
        {
            string values = "";
            string strVallue = GetFormValue(rowName, tableName, formid);

            if (strVallue != "")
            {
                string strSql = "select name from wf_baseType where valueID='" + strVallue + "'";
                DataTable dt=DbHelperSQL.Query(strSql).Tables[0];
                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["name"] != null)
                {
                    values = dt.Rows[0]["name"].ToString();
                }
            }

            return values;
        }

        public string ReturnValueNew(string parameter)
        {
            string values = "";
            string strSql = "select value from wf_baseType where name='" + parameter + "'";
            DataSet ds = DbHelperSQL.Query(strSql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                values = ds.Tables[0].Rows[0]["value"] == null ? "0" : ds.Tables[0].Rows[0]["value"].ToString();
            }
            else
            {
                values = "0";
            }
            return values;
        }
        /// <summary>
        /// 判断表单申请金额与特殊金额的比对
        /// </summary>
        /// <param name="money">表单金额</param>
        /// <returns>返回金额</returns>
        public string ReturnMoney(string money)
        {
            string retMoney = "";
            double moneyInt = 500;

            double moneyStr = double.Parse(money);
            if (moneyStr > moneyInt)
            {
                retMoney = "500";
            }
            else
            {
                retMoney = "0";
            }

            return retMoney;
        }
        /// <summary>
        /// 判断表单申请金额与特殊金额的比对
        /// </summary>
        /// <param name="money">表单金额</param>
        /// <param name="compareMoney">比对金额</param>
        /// <returns>返回金额</returns>
        public string ReturnMoney(string money, double compareMoney)
        {
            string retMoney = "";

            double moneyStr = double.Parse(money);
            if (moneyStr > compareMoney)
            {
                retMoney = compareMoney.ToString();
            }
            else
            {
                retMoney = "0";
            }

            return retMoney;
        }
        /// <summary>
        /// 返回表单指定列的值
        /// </summary>
        /// <param name="rowName">表单列</param>
        /// <param name="tableName">表单</param>
        /// <param name="formid">申请的表单ID</param>
        /// <returns>返回值</returns>
        public string ReturnRoleValues(string rowName, string tableName, string formid)
        {
            string values = "";
            string strSql = "select " + rowName + " from " + tableName + " where id='" + formid + "'";
            DataTable dt = DbHelperSQL.Query(strSql).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                values = dt.Rows[0][0].ToString();
            }
            return values;
        }
    }
}
