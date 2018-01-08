/**  版本信息模板在安装目录下，可自行修改。
* map_Jobfunction.cs
*
* 功 能： N/A
* 类 名： map_Jobfunction
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2016/10/11 15:31:41   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace Eday.DAL
{
	/// <summary>
	/// 数据访问类:map_Jobfunction
	/// </summary>
	public partial class map_Jobfunction
	{
		public map_Jobfunction()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from map_Jobfunction");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.NVarChar,50)			};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Eday.Model.map_Jobfunction model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into map_Jobfunction(");
			strSql.Append("ID,userID,JobfunctionID,JFCode,date,createDate,createUser,UpDateUser,UpDateTime,Status,Version,Checkcode,bak1,bak2,bak3,bak4,bak5)");
			strSql.Append(" values (");
			strSql.Append("@ID,@userID,@JobfunctionID,@JFCode,@date,@createDate,@createUser,@UpDateUser,@UpDateTime,@Status,@Version,@Checkcode,@bak1,@bak2,@bak3,@bak4,@bak5)");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.NVarChar,50),
					new SqlParameter("@userID", SqlDbType.NVarChar,50),
					new SqlParameter("@JobfunctionID", SqlDbType.NVarChar,50),
					new SqlParameter("@JFCode", SqlDbType.NVarChar,50),
					new SqlParameter("@date", SqlDbType.DateTime),
					new SqlParameter("@createDate", SqlDbType.DateTime),
					new SqlParameter("@createUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpDateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpDateTime", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.NVarChar,50),
					new SqlParameter("@Version", SqlDbType.NVarChar,50),
					new SqlParameter("@Checkcode", SqlDbType.NVarChar,-1),
					new SqlParameter("@bak1", SqlDbType.NVarChar,50),
					new SqlParameter("@bak2", SqlDbType.NVarChar,50),
					new SqlParameter("@bak3", SqlDbType.NVarChar,50),
					new SqlParameter("@bak4", SqlDbType.NVarChar,50),
					new SqlParameter("@bak5", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.userID;
			parameters[2].Value = model.JobfunctionID;
			parameters[3].Value = model.JFCode;
			parameters[4].Value = model.date;
			parameters[5].Value = model.createDate;
			parameters[6].Value = model.createUser;
			parameters[7].Value = model.UpDateUser;
			parameters[8].Value = model.UpDateTime;
			parameters[9].Value = model.Status;
			parameters[10].Value = model.Version;
			parameters[11].Value = model.Checkcode;
			parameters[12].Value = model.bak1;
			parameters[13].Value = model.bak2;
			parameters[14].Value = model.bak3;
			parameters[15].Value = model.bak4;
			parameters[16].Value = model.bak5;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Update(Eday.Model.map_Jobfunction model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update map_Jobfunction set ");
			strSql.Append("userID=@userID,");
			strSql.Append("JobfunctionID=@JobfunctionID,");
			strSql.Append("JFCode=@JFCode,");
			strSql.Append("date=@date,");
			strSql.Append("createDate=@createDate,");
			strSql.Append("createUser=@createUser,");
			strSql.Append("UpDateUser=@UpDateUser,");
			strSql.Append("UpDateTime=@UpDateTime,");
			strSql.Append("Status=@Status,");
			strSql.Append("Version=@Version,");
			strSql.Append("Checkcode=@Checkcode,");
			strSql.Append("bak1=@bak1,");
			strSql.Append("bak2=@bak2,");
			strSql.Append("bak3=@bak3,");
			strSql.Append("bak4=@bak4,");
			strSql.Append("bak5=@bak5");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@userID", SqlDbType.NVarChar,50),
					new SqlParameter("@JobfunctionID", SqlDbType.NVarChar,50),
					new SqlParameter("@JFCode", SqlDbType.NVarChar,50),
					new SqlParameter("@date", SqlDbType.DateTime),
					new SqlParameter("@createDate", SqlDbType.DateTime),
					new SqlParameter("@createUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpDateUser", SqlDbType.NVarChar,50),
					new SqlParameter("@UpDateTime", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.NVarChar,50),
					new SqlParameter("@Version", SqlDbType.NVarChar,50),
					new SqlParameter("@Checkcode", SqlDbType.NVarChar,-1),
					new SqlParameter("@bak1", SqlDbType.NVarChar,50),
					new SqlParameter("@bak2", SqlDbType.NVarChar,50),
					new SqlParameter("@bak3", SqlDbType.NVarChar,50),
					new SqlParameter("@bak4", SqlDbType.NVarChar,50),
					new SqlParameter("@bak5", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.userID;
			parameters[1].Value = model.JobfunctionID;
			parameters[2].Value = model.JFCode;
			parameters[3].Value = model.date;
			parameters[4].Value = model.createDate;
			parameters[5].Value = model.createUser;
			parameters[6].Value = model.UpDateUser;
			parameters[7].Value = model.UpDateTime;
			parameters[8].Value = model.Status;
			parameters[9].Value = model.Version;
			parameters[10].Value = model.Checkcode;
			parameters[11].Value = model.bak1;
			parameters[12].Value = model.bak2;
			parameters[13].Value = model.bak3;
			parameters[14].Value = model.bak4;
			parameters[15].Value = model.bak5;
			parameters[16].Value = model.ID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from map_Jobfunction ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.NVarChar,50)			};
			parameters[0].Value = ID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from map_Jobfunction ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public Eday.Model.map_Jobfunction GetModel(string ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,userID,JobfunctionID,JFCode,date,createDate,createUser,UpDateUser,UpDateTime,Status,Version,Checkcode,bak1,bak2,bak3,bak4,bak5 from map_Jobfunction ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.NVarChar,50)			};
			parameters[0].Value = ID;

			Eday.Model.map_Jobfunction model=new Eday.Model.map_Jobfunction();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}

        /// <summary>
        /// @杜秋菊  得到一个对象实体
        /// </summary>
        public Eday.Model.map_Jobfunction GetModel(string userID, string JFID, string JFCode)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,userID,JobfunctionID,JFCode,date,createDate,createUser,UpDateUser,UpDateTime,Status,Version,Checkcode,bak1,bak2,bak3,bak4,bak5 from map_Jobfunction  ");
            strSql.Append(" where userID=@userID ");
            strSql.Append(" and  JobfunctionID=@JFID ");
            strSql.Append(" and JFCode=@JFCode ");
            strSql.Append(" and Status='0' ");
            SqlParameter[] parameters = {
					new SqlParameter("@userID", SqlDbType.NVarChar,50),
                    new SqlParameter("@JFID", SqlDbType.NVarChar,50),
                    new SqlParameter("@JFCode", SqlDbType.NVarChar,50)
                                        };
            parameters[0].Value = userID;
            parameters[1].Value = JFID;
            parameters[2].Value = JFCode;

            Eday.Model.map_Jobfunction model = new Eday.Model.map_Jobfunction();
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
		public Eday.Model.map_Jobfunction DataRowToModel(DataRow row)
		{
			Eday.Model.map_Jobfunction model=new Eday.Model.map_Jobfunction();
			if (row != null)
			{
				if(row["ID"]!=null)
				{
					model.ID=row["ID"].ToString();
				}
				if(row["userID"]!=null)
				{
					model.userID=row["userID"].ToString();
				}
				if(row["JobfunctionID"]!=null)
				{
					model.JobfunctionID=row["JobfunctionID"].ToString();
				}
				if(row["JFCode"]!=null)
				{
					model.JFCode=row["JFCode"].ToString();
				}
				if(row["date"]!=null && row["date"].ToString()!="")
				{
					model.date=DateTime.Parse(row["date"].ToString());
				}
				if(row["createDate"]!=null && row["createDate"].ToString()!="")
				{
					model.createDate=DateTime.Parse(row["createDate"].ToString());
				}
				if(row["createUser"]!=null)
				{
					model.createUser=row["createUser"].ToString();
				}
				if(row["UpDateUser"]!=null)
				{
					model.UpDateUser=row["UpDateUser"].ToString();
				}
				if(row["UpDateTime"]!=null && row["UpDateTime"].ToString()!="")
				{
					model.UpDateTime=DateTime.Parse(row["UpDateTime"].ToString());
				}
				if(row["Status"]!=null)
				{
					model.Status=row["Status"].ToString();
				}
				if(row["Version"]!=null)
				{
					model.Version=row["Version"].ToString();
				}
				if(row["Checkcode"]!=null)
				{
					model.Checkcode=row["Checkcode"].ToString();
				}
				if(row["bak1"]!=null)
				{
					model.bak1=row["bak1"].ToString();
				}
				if(row["bak2"]!=null)
				{
					model.bak2=row["bak2"].ToString();
				}
				if(row["bak3"]!=null)
				{
					model.bak3=row["bak3"].ToString();
				}
				if(row["bak4"]!=null)
				{
					model.bak4=row["bak4"].ToString();
				}
				if(row["bak5"]!=null)
				{
					model.bak5=row["bak5"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,userID,JobfunctionID,JFCode,date,createDate,createUser,UpDateUser,UpDateTime,Status,Version,Checkcode,bak1,bak2,bak3,bak4,bak5 ");
			strSql.Append(" FROM map_Jobfunction ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" ID,userID,JobfunctionID,JFCode,date,createDate,createUser,UpDateUser,UpDateTime,Status,Version,Checkcode,bak1,bak2,bak3,bak4,bak5 ");
			strSql.Append(" FROM map_Jobfunction ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM map_Jobfunction ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from map_Jobfunction T ");
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
			parameters[0].Value = "map_Jobfunction";
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
        //杜秋菊 验证过2017/04/03
        public string GetCheckCode(Eday.Model.map_Jobfunction mapJob)
        { 
            string str=mapJob.userID + mapJob.JobfunctionID + mapJob.JFCode + mapJob.date 
                + mapJob.createDate +mapJob.createUser+ mapJob.UpDateUser + mapJob.UpDateTime + mapJob.Status;
            return str;
        }
        public string GetAuditStr(Eday.Model.map_Jobfunction mapJob)
        {
            string Status = "";
            if (mapJob.Status.Trim() == "0")
            {
                Status = "Active";
            }
            else if (mapJob.Status.Trim() == "1")
            {
                Status = "Inactive";
            }
            else if (mapJob.Status.Trim() == "2")
            {
                Status = "Keep relation";
            }
            string str = "userID&" + mapJob.userID + "$JobfunctionID&" + mapJob.JobfunctionID + "$JFCode&" + mapJob.JFCode + "$date&" + mapJob.date + "$createDate&" + mapJob.createDate +
                         "$createUser&" + mapJob.createUser + "$UpDateUser&" + mapJob.UpDateUser + "$UpDateTime&" + mapJob.UpDateTime + "$Status&" + Status;
            return str;
        }

        /// <summary>
        /// @杜秋菊  得到一个对象实体
        /// </summary>
        public Eday.Model.map_Jobfunction GetModelByUserIDJFCode(string userID, string JFCode)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from map_Jobfunction  ");
            strSql.Append(" where userID=@userID ");
            strSql.Append(" and JFCode=@JFCode ");
            strSql.Append(" and Status='0' ");
            SqlParameter[] parameters = {
					new SqlParameter("@userID", SqlDbType.NVarChar,50),
                    new SqlParameter("@JFCode", SqlDbType.NVarChar,50)
                                        };
            parameters[0].Value = userID;
            parameters[1].Value = JFCode;

            Eday.Model.map_Jobfunction model = new Eday.Model.map_Jobfunction();
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
        /// @杜秋菊  得到一个对象实体
        /// </summary>
        public Eday.Model.map_Jobfunction GetModelByUserIDJFCodeActiveDate(string userID, string JFCode, DateTime activeDate)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 *  from map_Jobfunction  ");
            strSql.Append(" where userID=@userID ");
            strSql.Append(" and JFCode=@JFCode ");
            strSql.Append(" and date=@activeDate ");
            strSql.Append(" and Status='0' ");
            SqlParameter[] parameters = {
					new SqlParameter("@userID", SqlDbType.NVarChar,50),
                     new SqlParameter("@JFCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@activeDate", SqlDbType.DateTime)
                                        };
            parameters[0].Value = userID;
            parameters[1].Value = JFCode;
            parameters[2].Value = activeDate;

            Eday.Model.map_Jobfunction model = new Eday.Model.map_Jobfunction();
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
		#endregion  ExtensionMethod
	}
}

