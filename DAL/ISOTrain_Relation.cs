/**  版本信息模板在安装目录下，可自行修改。
* ISOTrain_Relation.cs
*
* 功 能： N/A
* 类 名： ISOTrain_Relation
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017/11/23 9:42:53   N/A    初版
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
	/// 数据访问类:ISOTrain_Relation
	/// </summary>
	public partial class ISOTrain_Relation
	{
		public ISOTrain_Relation()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Eday.Model.ISOTrain_Relation model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ISOTrain_Relation(");
			strSql.Append("ID,code,name,ModuleCode,Description,CourseCode,CourseTitleDescription,TaskLevel,NotifyDays,NeedRetraining,Frequency)");
			strSql.Append(" values (");
			strSql.Append("@ID,@code,@name,@ModuleCode,@Description,@CourseCode,@CourseTitleDescription,@TaskLevel,@NotifyDays,@NeedRetraining,@Frequency)");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.NVarChar,50),
					new SqlParameter("@code", SqlDbType.NVarChar,50),
					new SqlParameter("@name", SqlDbType.NVarChar,200),
					new SqlParameter("@ModuleCode", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@CourseCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CourseTitleDescription", SqlDbType.NVarChar,-1),
					new SqlParameter("@TaskLevel", SqlDbType.NVarChar,50),
					new SqlParameter("@NotifyDays", SqlDbType.NVarChar,50),
					new SqlParameter("@NeedRetraining", SqlDbType.NVarChar,50),
					new SqlParameter("@Frequency", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.code;
			parameters[2].Value = model.name;
			parameters[3].Value = model.ModuleCode;
			parameters[4].Value = model.Description;
			parameters[5].Value = model.CourseCode;
			parameters[6].Value = model.CourseTitleDescription;
			parameters[7].Value = model.TaskLevel;
			parameters[8].Value = model.NotifyDays;
			parameters[9].Value = model.NeedRetraining;
			parameters[10].Value = model.Frequency;

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
		public bool Update(Eday.Model.ISOTrain_Relation model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ISOTrain_Relation set ");
			strSql.Append("ID=@ID,");
			strSql.Append("code=@code,");
			strSql.Append("name=@name,");
			strSql.Append("ModuleCode=@ModuleCode,");
			strSql.Append("Description=@Description,");
			strSql.Append("CourseCode=@CourseCode,");
			strSql.Append("CourseTitleDescription=@CourseTitleDescription,");
			strSql.Append("TaskLevel=@TaskLevel,");
			strSql.Append("NotifyDays=@NotifyDays,");
			strSql.Append("NeedRetraining=@NeedRetraining,");
			strSql.Append("Frequency=@Frequency");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.NVarChar,50),
					new SqlParameter("@code", SqlDbType.NVarChar,50),
					new SqlParameter("@name", SqlDbType.NVarChar,200),
					new SqlParameter("@ModuleCode", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@CourseCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CourseTitleDescription", SqlDbType.NVarChar,-1),
					new SqlParameter("@TaskLevel", SqlDbType.NVarChar,50),
					new SqlParameter("@NotifyDays", SqlDbType.NVarChar,50),
					new SqlParameter("@NeedRetraining", SqlDbType.NVarChar,50),
					new SqlParameter("@Frequency", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.code;
			parameters[2].Value = model.name;
			parameters[3].Value = model.ModuleCode;
			parameters[4].Value = model.Description;
			parameters[5].Value = model.CourseCode;
			parameters[6].Value = model.CourseTitleDescription;
			parameters[7].Value = model.TaskLevel;
			parameters[8].Value = model.NotifyDays;
			parameters[9].Value = model.NeedRetraining;
			parameters[10].Value = model.Frequency;

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
			strSql.Append("delete from ISOTrain_Relation ");
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
		public Eday.Model.ISOTrain_Relation GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,code,name,ModuleCode,Description,CourseCode,CourseTitleDescription,TaskLevel,NotifyDays,NeedRetraining,Frequency from ISOTrain_Relation ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
			};

			Eday.Model.ISOTrain_Relation model=new Eday.Model.ISOTrain_Relation();
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
		public Eday.Model.ISOTrain_Relation DataRowToModel(DataRow row)
		{
			Eday.Model.ISOTrain_Relation model=new Eday.Model.ISOTrain_Relation();
			if (row != null)
			{
				if(row["ID"]!=null)
				{
					model.ID=row["ID"].ToString();
				}
				if(row["code"]!=null)
				{
					model.code=row["code"].ToString();
				}
				if(row["name"]!=null)
				{
					model.name=row["name"].ToString();
				}
				if(row["ModuleCode"]!=null)
				{
					model.ModuleCode=row["ModuleCode"].ToString();
				}
				if(row["Description"]!=null)
				{
					model.Description=row["Description"].ToString();
				}
				if(row["CourseCode"]!=null)
				{
					model.CourseCode=row["CourseCode"].ToString();
				}
				if(row["CourseTitleDescription"]!=null)
				{
					model.CourseTitleDescription=row["CourseTitleDescription"].ToString();
				}
				if(row["TaskLevel"]!=null)
				{
					model.TaskLevel=row["TaskLevel"].ToString();
				}
				if(row["NotifyDays"]!=null)
				{
					model.NotifyDays=row["NotifyDays"].ToString();
				}
				if(row["NeedRetraining"]!=null)
				{
					model.NeedRetraining=row["NeedRetraining"].ToString();
				}
				if(row["Frequency"]!=null)
				{
					model.Frequency=row["Frequency"].ToString();
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
			strSql.Append("select ID,code,name,ModuleCode,Description,CourseCode,CourseTitleDescription,TaskLevel,NotifyDays,NeedRetraining,Frequency ");
			strSql.Append(" FROM ISOTrain_Relation ");
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
			strSql.Append(" ID,code,name,ModuleCode,Description,CourseCode,CourseTitleDescription,TaskLevel,NotifyDays,NeedRetraining,Frequency ");
			strSql.Append(" FROM ISOTrain_Relation ");
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
			strSql.Append("select count(1) FROM ISOTrain_Relation ");
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
			strSql.Append(")AS Row, T.*  from ISOTrain_Relation T ");
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
			parameters[0].Value = "ISOTrain_Relation";
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

