﻿/**  版本信息模板在安装目录下，可自行修改。
* ISOTrain_Map_Jobfunction.cs
*
* 功 能： N/A
* 类 名： ISOTrain_Map_Jobfunction
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017/11/16 15:25:05   N/A    初版
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
	/// 数据访问类:ISOTrain_Map_Jobfunction
	/// </summary>
	public partial class ISOTrain_Map_Jobfunction
	{
		public ISOTrain_Map_Jobfunction()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ISOTrain_Map_Jobfunction");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.NVarChar,50)			};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Eday.Model.ISOTrain_Map_Jobfunction model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ISOTrain_Map_Jobfunction(");
			strSql.Append("ID,UserID,JFCode,ActiveDate,bak1,bak2,bak3,bak4,bak5)");
			strSql.Append(" values (");
			strSql.Append("@ID,@UserID,@JFCode,@ActiveDate,@bak1,@bak2,@bak3,@bak4,@bak5)");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.NVarChar,50),
					new SqlParameter("@UserID", SqlDbType.NVarChar,50),
					new SqlParameter("@JFCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ActiveDate", SqlDbType.DateTime),
					new SqlParameter("@bak1", SqlDbType.NVarChar,50),
					new SqlParameter("@bak2", SqlDbType.NVarChar,50),
					new SqlParameter("@bak3", SqlDbType.NVarChar,50),
					new SqlParameter("@bak4", SqlDbType.NVarChar,50),
					new SqlParameter("@bak5", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.UserID;
			parameters[2].Value = model.JFCode;
			parameters[3].Value = model.ActiveDate;
			parameters[4].Value = model.bak1;
			parameters[5].Value = model.bak2;
			parameters[6].Value = model.bak3;
			parameters[7].Value = model.bak4;
			parameters[8].Value = model.bak5;

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
		public bool Update(Eday.Model.ISOTrain_Map_Jobfunction model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ISOTrain_Map_Jobfunction set ");
			strSql.Append("UserID=@UserID,");
			strSql.Append("JFCode=@JFCode,");
			strSql.Append("ActiveDate=@ActiveDate,");
			strSql.Append("bak1=@bak1,");
			strSql.Append("bak2=@bak2,");
			strSql.Append("bak3=@bak3,");
			strSql.Append("bak4=@bak4,");
			strSql.Append("bak5=@bak5");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.NVarChar,50),
					new SqlParameter("@JFCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ActiveDate", SqlDbType.DateTime),
					new SqlParameter("@bak1", SqlDbType.NVarChar,50),
					new SqlParameter("@bak2", SqlDbType.NVarChar,50),
					new SqlParameter("@bak3", SqlDbType.NVarChar,50),
					new SqlParameter("@bak4", SqlDbType.NVarChar,50),
					new SqlParameter("@bak5", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.UserID;
			parameters[1].Value = model.JFCode;
			parameters[2].Value = model.ActiveDate;
			parameters[3].Value = model.bak1;
			parameters[4].Value = model.bak2;
			parameters[5].Value = model.bak3;
			parameters[6].Value = model.bak4;
			parameters[7].Value = model.bak5;
			parameters[8].Value = model.ID;

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
			strSql.Append("delete from ISOTrain_Map_Jobfunction ");
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
			strSql.Append("delete from ISOTrain_Map_Jobfunction ");
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
		public Eday.Model.ISOTrain_Map_Jobfunction GetModel(string ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,UserID,JFCode,ActiveDate,bak1,bak2,bak3,bak4,bak5 from ISOTrain_Map_Jobfunction ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.NVarChar,50)			};
			parameters[0].Value = ID;

			Eday.Model.ISOTrain_Map_Jobfunction model=new Eday.Model.ISOTrain_Map_Jobfunction();
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
		/// 得到一个对象实体
		/// </summary>
		public Eday.Model.ISOTrain_Map_Jobfunction DataRowToModel(DataRow row)
		{
			Eday.Model.ISOTrain_Map_Jobfunction model=new Eday.Model.ISOTrain_Map_Jobfunction();
			if (row != null)
			{
				if(row["ID"]!=null)
				{
					model.ID=row["ID"].ToString();
				}
				if(row["UserID"]!=null)
				{
					model.UserID=row["UserID"].ToString();
				}
				if(row["JFCode"]!=null)
				{
					model.JFCode=row["JFCode"].ToString();
				}
				if(row["ActiveDate"]!=null && row["ActiveDate"].ToString()!="")
				{
					model.ActiveDate=DateTime.Parse(row["ActiveDate"].ToString());
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
			strSql.Append("select ID,UserID,JFCode,ActiveDate,bak1,bak2,bak3,bak4,bak5 ");
			strSql.Append(" FROM ISOTrain_Map_Jobfunction ");
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
			strSql.Append(" ID,UserID,JFCode,ActiveDate,bak1,bak2,bak3,bak4,bak5 ");
			strSql.Append(" FROM ISOTrain_Map_Jobfunction ");
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
			strSql.Append("select count(1) FROM ISOTrain_Map_Jobfunction ");
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
			strSql.Append(")AS Row, T.*  from ISOTrain_Map_Jobfunction T ");
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
			parameters[0].Value = "ISOTrain_Map_Jobfunction";
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
        /// @杜秋菊  得到一个对象实体
        /// </summary>
        public Eday.Model.ISOTrain_Map_Jobfunction GetModelByJFAndEmploeeID(string userID, string JFCode)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 *  from ISOTrain_Map_Jobfunction  ");
            strSql.Append(" where userID=@userID ");
            strSql.Append(" and JFCode=@JFCode ");
            SqlParameter[] parameters = {
					new SqlParameter("@userID", SqlDbType.NVarChar,50),
                    new SqlParameter("@JFCode", SqlDbType.NVarChar,50)
                                        };
            parameters[0].Value = userID;
            parameters[1].Value = JFCode;

            Eday.Model.ISOTrain_Map_Jobfunction model = new Eday.Model.ISOTrain_Map_Jobfunction();
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
