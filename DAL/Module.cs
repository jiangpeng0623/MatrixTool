using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references

namespace MatrixTool.DAL
{
    public partial class Module
    {
        public Module()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Module");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.NVarChar,50)          };
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(MatrixTool.Model.Module model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Module(");
            strSql.Append("ID,initital,ModuleCode,Description,CreateTime,CreateUser,UpdateTime,UpdateUser,Status,Checkcode,bak1,bak2,bak3,bak4,bak5)");
            strSql.Append(" values (");
            strSql.Append("@ID,@initital,@ModuleCode,@Description,@CreateTime,@CreateUser,@UpdateTime,@UpdateUser,@Status,@Checkcode,@bak1,@bak2,@bak3,@bak4,@bak5)");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.NVarChar,50),
                    new SqlParameter("@initital", SqlDbType.NVarChar,50),
                    new SqlParameter("@ModuleCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@Description", SqlDbType.NVarChar,500),
                    new SqlParameter("@CreateTime", SqlDbType.DateTime),
                    new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@UpdateTime", SqlDbType.DateTime),
                    new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@Status", SqlDbType.NVarChar,50),
                    new SqlParameter("@Checkcode", SqlDbType.NVarChar,-1),
                    new SqlParameter("@bak1", SqlDbType.NVarChar,50),
                    new SqlParameter("@bak2", SqlDbType.NVarChar,50),
                    new SqlParameter("@bak3", SqlDbType.NVarChar,50),
                    new SqlParameter("@bak4", SqlDbType.NVarChar,50),
                    new SqlParameter("@bak5", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.initital;
            parameters[2].Value = model.ModuleCode;
            parameters[3].Value = model.Description;
            parameters[4].Value = model.CreateTime;
            parameters[5].Value = model.CreateUser;
            parameters[6].Value = model.UpdateTime;
            parameters[7].Value = model.UpdateUser;
            parameters[8].Value = model.Status;
            parameters[9].Value = model.Checkcode;
            parameters[10].Value = model.bak1;
            parameters[11].Value = model.bak2;
            parameters[12].Value = model.bak3;
            parameters[13].Value = model.bak4;
            parameters[14].Value = model.bak5;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.Module model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Module set ");
            strSql.Append("initital=@initital,");
            strSql.Append("ModuleCode=@ModuleCode,");
            strSql.Append("Description=@Description,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("CreateUser=@CreateUser,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("UpdateUser=@UpdateUser,");
            strSql.Append("Status=@Status,");
            strSql.Append("Checkcode=@Checkcode,");
            strSql.Append("bak1=@bak1,");
            strSql.Append("bak2=@bak2,");
            strSql.Append("bak3=@bak3,");
            strSql.Append("bak4=@bak4,");
            strSql.Append("bak5=@bak5");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@initital", SqlDbType.NVarChar,50),
                    new SqlParameter("@ModuleCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@Description", SqlDbType.NVarChar,500),
                    new SqlParameter("@CreateTime", SqlDbType.DateTime),
                    new SqlParameter("@CreateUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@UpdateTime", SqlDbType.DateTime),
                    new SqlParameter("@UpdateUser", SqlDbType.NVarChar,50),
                    new SqlParameter("@Status", SqlDbType.NVarChar,50),
                    new SqlParameter("@Checkcode", SqlDbType.NVarChar,-1),
                    new SqlParameter("@bak1", SqlDbType.NVarChar,50),
                    new SqlParameter("@bak2", SqlDbType.NVarChar,50),
                    new SqlParameter("@bak3", SqlDbType.NVarChar,50),
                    new SqlParameter("@bak4", SqlDbType.NVarChar,50),
                    new SqlParameter("@bak5", SqlDbType.NVarChar,50),
                    new SqlParameter("@ID", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.initital;
            parameters[1].Value = model.ModuleCode;
            parameters[2].Value = model.Description;
            parameters[3].Value = model.CreateTime;
            parameters[4].Value = model.CreateUser;
            parameters[5].Value = model.UpdateTime;
            parameters[6].Value = model.UpdateUser;
            parameters[7].Value = model.Status;
            parameters[8].Value = model.Checkcode;
            parameters[9].Value = model.bak1;
            parameters[10].Value = model.bak2;
            parameters[11].Value = model.bak3;
            parameters[12].Value = model.bak4;
            parameters[13].Value = model.bak5;
            parameters[14].Value = model.ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Module ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.NVarChar,50)          };
            parameters[0].Value = ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Module ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Module GetModel(string ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,initital,ModuleCode,Description,CreateTime,CreateUser,UpdateTime,UpdateUser,Status,Checkcode,bak1,bak2,bak3,bak4,bak5 from Module ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.NVarChar,50)          };
            parameters[0].Value = ID;

            Model.Module model = new Model.Module();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象实体  @杜秋菊  
        /// </summary>
        public Model.Module GetModelByCode(string ModuleCode)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,initital,ModuleCode,Description,CreateTime,CreateUser,UpdateTime,UpdateUser,Status,Checkcode,bak1,bak2,bak3,bak4,bak5 from Module ");
            strSql.Append(" where ModuleCode=@ModuleCode ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ModuleCode", SqlDbType.NVarChar,50)          };
            parameters[0].Value = ModuleCode;

            Model.Module model = new Model.Module();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Module DataRowToModel(DataRow row)
        {
            Model.Module model = new Model.Module();
            if (row != null)
            {
                if (row["ID"] != null)
                {
                    model.ID = row["ID"].ToString();
                }
                if (row["initital"] != null)
                {
                    model.initital = row["initital"].ToString();
                }
                if (row["ModuleCode"] != null)
                {
                    model.ModuleCode = row["ModuleCode"].ToString();
                }
                if (row["Description"] != null)
                {
                    model.Description = row["Description"].ToString();
                }
                if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
                }
                if (row["CreateUser"] != null)
                {
                    model.CreateUser = row["CreateUser"].ToString();
                }
                if (row["UpdateTime"] != null && row["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(row["UpdateTime"].ToString());
                }
                if (row["UpdateUser"] != null)
                {
                    model.UpdateUser = row["UpdateUser"].ToString();
                }
                if (row["Status"] != null)
                {
                    model.Status = row["Status"].ToString();
                }
                if (row["Checkcode"] != null)
                {
                    model.Checkcode = row["Checkcode"].ToString();
                }
                if (row["bak1"] != null)
                {
                    model.bak1 = row["bak1"].ToString();
                }
                if (row["bak2"] != null)
                {
                    model.bak2 = row["bak2"].ToString();
                }
                if (row["bak3"] != null)
                {
                    model.bak3 = row["bak3"].ToString();
                }
                if (row["bak4"] != null)
                {
                    model.bak4 = row["bak4"].ToString();
                }
                if (row["bak5"] != null)
                {
                    model.bak5 = row["bak5"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,initital,ModuleCode,Description,CreateTime,CreateUser,UpdateTime,UpdateUser,Status,Checkcode,bak1,bak2,bak3,bak4,bak5 ");
            strSql.Append(" FROM Module ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ID,initital,ModuleCode,Description,CreateTime,CreateUser,UpdateTime,UpdateUser,Status,Checkcode,bak1,bak2,bak3,bak4,bak5 ");
            strSql.Append(" FROM Module ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Module ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from Module T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "Module";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

        #endregion  BasicMethod
        #region  ExtensionMethod
        /// <summary>
        /// 设置加密字段
        /// </summary>
        /// <param name="course2JF"></param>
        /// <returns></returns>
        public string GetCheckCode(Model.Module Modelcourse)
        {
            string forMD5 = Modelcourse.initital + Modelcourse.ModuleCode + Modelcourse.Description +
                                  Modelcourse.CreateTime + Modelcourse.CreateUser + Modelcourse.UpdateTime + Modelcourse.UpdateUser
                                  + Modelcourse.Status;
            return forMD5;
        }
        /// <summary>
        /// 设置加密字段
        /// </summary>
        /// <param name="course2JF"></param>
        /// <returns></returns>
        public string GetAuditStr(Model.Module module)
        {
            string Status = "";
            if (module.Status.Trim() == "0")
            {
                Status = "Active";
            }
            else if (module.Status.Trim() == "1")
            {
                Status = "Inactive";
            }
            string str = "initital&" + module.initital + "$ModuleCode&" + module.ModuleCode + "$Description&" + module.Description + "$CreateTime&" + module.CreateTime +
                "$CreateUser&" + module.CreateUser + "$UpdateTime&" + module.UpdateTime + "$UpdateUser&" + module.UpdateUser + "$Status&" + Status;
            return str;
        }
        #endregion  ExtensionMethod
    }
}