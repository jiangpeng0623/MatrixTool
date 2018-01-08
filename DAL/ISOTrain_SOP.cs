/**  版本信息模板在安装目录下，可自行修改。
* ISOTrain_SOP.cs
*
* 功 能： N/A
* 类 名： ISOTrain_SOP
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017/11/22 9:52:10   N/A    初版
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
	/// 数据访问类:ISOTrain_SOP
	/// </summary>
	public partial class ISOTrain_SOP
	{
		public ISOTrain_SOP()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Eday.Model.ISOTrain_SOP model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ISOTrain_SOP(");
			strSql.Append("ID,SOPNo,Edition,bak1,bak2,bak3,bak4,bak5)");
			strSql.Append(" values (");
			strSql.Append("@ID,@SOPNo,@Edition,@bak1,@bak2,@bak3,@bak4,@bak5)");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.NVarChar,50),
					new SqlParameter("@SOPNo", SqlDbType.NVarChar,50),
					new SqlParameter("@Edition", SqlDbType.NVarChar,50),
					new SqlParameter("@bak1", SqlDbType.NVarChar,50),
					new SqlParameter("@bak2", SqlDbType.NVarChar,50),
					new SqlParameter("@bak3", SqlDbType.NVarChar,50),
					new SqlParameter("@bak4", SqlDbType.NVarChar,50),
					new SqlParameter("@bak5", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.SOPNo;
			parameters[2].Value = model.Edition;
			parameters[3].Value = model.bak1;
			parameters[4].Value = model.bak2;
			parameters[5].Value = model.bak3;
			parameters[6].Value = model.bak4;
			parameters[7].Value = model.bak5;

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
		public bool Update(Eday.Model.ISOTrain_SOP model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ISOTrain_SOP set ");
			strSql.Append("ID=@ID,");
			strSql.Append("SOPNo=@SOPNo,");
			strSql.Append("Edition=@Edition,");
			strSql.Append("bak1=@bak1,");
			strSql.Append("bak2=@bak2,");
			strSql.Append("bak3=@bak3,");
			strSql.Append("bak4=@bak4,");
			strSql.Append("bak5=@bak5");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.NVarChar,50),
					new SqlParameter("@SOPNo", SqlDbType.NVarChar,50),
					new SqlParameter("@Edition", SqlDbType.NVarChar,50),
					new SqlParameter("@bak1", SqlDbType.NVarChar,50),
					new SqlParameter("@bak2", SqlDbType.NVarChar,50),
					new SqlParameter("@bak3", SqlDbType.NVarChar,50),
					new SqlParameter("@bak4", SqlDbType.NVarChar,50),
					new SqlParameter("@bak5", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.SOPNo;
			parameters[2].Value = model.Edition;
			parameters[3].Value = model.bak1;
			parameters[4].Value = model.bak2;
			parameters[5].Value = model.bak3;
			parameters[6].Value = model.bak4;
			parameters[7].Value = model.bak5;

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
		public bool Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ISOTrain_SOP ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
			};

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
		/// 得到一个对象实体
		/// </summary>
		public Eday.Model.ISOTrain_SOP GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,SOPNo,Edition,bak1,bak2,bak3,bak4,bak5 from ISOTrain_SOP ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
			};

			Eday.Model.ISOTrain_SOP model=new Eday.Model.ISOTrain_SOP();
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
		public Eday.Model.ISOTrain_SOP DataRowToModel(DataRow row)
		{
			Eday.Model.ISOTrain_SOP model=new Eday.Model.ISOTrain_SOP();
			if (row != null)
			{
				if(row["ID"]!=null)
				{
					model.ID=row["ID"].ToString();
				}
				if(row["SOPNo"]!=null)
				{
					model.SOPNo=row["SOPNo"].ToString();
				}
				if(row["Edition"]!=null)
				{
					model.Edition=row["Edition"].ToString();
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
			strSql.Append("select ID,SOPNo,Edition,bak1,bak2,bak3,bak4,bak5 ");
			strSql.Append(" FROM ISOTrain_SOP ");
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
			strSql.Append(" ID,SOPNo,Edition,bak1,bak2,bak3,bak4,bak5 ");
			strSql.Append(" FROM ISOTrain_SOP ");
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
			strSql.Append("select count(1) FROM ISOTrain_SOP ");
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
				strSql.Append("order by T. desc");
			}
			strSql.Append(")AS Row, T.*  from ISOTrain_SOP T ");
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
			parameters[0].Value = "ISOTrain_SOP";
			parameters[1].Value = "";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

