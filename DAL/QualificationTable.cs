/**  版本信息模板在安装目录下，可自行修改。
* QualificationTable.cs
*
* 功 能： N/A
* 类 名： QualificationTable
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017/9/16 11:07:35   N/A    初版
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
	/// 数据访问类:QualificationTable
	/// </summary>
	public partial class QualificationTable
	{
		public QualificationTable()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from QualificationTable");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.NVarChar,50)			};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Eday.Model.QualificationTable model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into QualificationTable(");
			strSql.Append("ID,CourseCode,EmployeeID,CourseTime,Status,CreateTime,CreateUserID,UpdateTime,UpdateUserID,CheckCode,bak1,bak2,bak3,bak4,bak5)");
			strSql.Append(" values (");
			strSql.Append("@ID,@CourseCode,@EmployeeID,@CourseTime,@Status,@CreateTime,@CreateUserID,@UpdateTime,@UpdateUserID,@CheckCode,@bak1,@bak2,@bak3,@bak4,@bak5)");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.NVarChar,50),
					new SqlParameter("@CourseCode", SqlDbType.NVarChar,50),
					new SqlParameter("@EmployeeID", SqlDbType.NVarChar,50),
					new SqlParameter("@CourseTime", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUserID", SqlDbType.NVarChar,50),
					new SqlParameter("@CheckCode", SqlDbType.NVarChar,-1),
					new SqlParameter("@bak1", SqlDbType.NVarChar,50),
					new SqlParameter("@bak2", SqlDbType.NVarChar,50),
					new SqlParameter("@bak3", SqlDbType.NVarChar,50),
					new SqlParameter("@bak4", SqlDbType.NVarChar,50),
					new SqlParameter("@bak5", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.CourseCode;
			parameters[2].Value = model.EmployeeID;
			parameters[3].Value = model.CourseTime;
			parameters[4].Value = model.Status;
			parameters[5].Value = model.CreateTime;
			parameters[6].Value = model.CreateUserID;
			parameters[7].Value = model.UpdateTime;
			parameters[8].Value = model.UpdateUserID;
			parameters[9].Value = model.CheckCode;
			parameters[10].Value = model.bak1;
			parameters[11].Value = model.bak2;
			parameters[12].Value = model.bak3;
			parameters[13].Value = model.bak4;
			parameters[14].Value = model.bak5;

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
		public bool Update(Eday.Model.QualificationTable model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update QualificationTable set ");
			strSql.Append("CourseCode=@CourseCode,");
			strSql.Append("EmployeeID=@EmployeeID,");
			strSql.Append("CourseTime=@CourseTime,");
			strSql.Append("Status=@Status,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("CreateUserID=@CreateUserID,");
			strSql.Append("UpdateTime=@UpdateTime,");
			strSql.Append("UpdateUserID=@UpdateUserID,");
			strSql.Append("CheckCode=@CheckCode,");
			strSql.Append("bak1=@bak1,");
			strSql.Append("bak2=@bak2,");
			strSql.Append("bak3=@bak3,");
			strSql.Append("bak4=@bak4,");
			strSql.Append("bak5=@bak5");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@CourseCode", SqlDbType.NVarChar,50),
					new SqlParameter("@EmployeeID", SqlDbType.NVarChar,50),
					new SqlParameter("@CourseTime", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUserID", SqlDbType.NVarChar,50),
					new SqlParameter("@CheckCode", SqlDbType.NVarChar,-1),
					new SqlParameter("@bak1", SqlDbType.NVarChar,50),
					new SqlParameter("@bak2", SqlDbType.NVarChar,50),
					new SqlParameter("@bak3", SqlDbType.NVarChar,50),
					new SqlParameter("@bak4", SqlDbType.NVarChar,50),
					new SqlParameter("@bak5", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.CourseCode;
			parameters[1].Value = model.EmployeeID;
			parameters[2].Value = model.CourseTime;
			parameters[3].Value = model.Status;
			parameters[4].Value = model.CreateTime;
			parameters[5].Value = model.CreateUserID;
			parameters[6].Value = model.UpdateTime;
			parameters[7].Value = model.UpdateUserID;
			parameters[8].Value = model.CheckCode;
			parameters[9].Value = model.bak1;
			parameters[10].Value = model.bak2;
			parameters[11].Value = model.bak3;
			parameters[12].Value = model.bak4;
			parameters[13].Value = model.bak5;
			parameters[14].Value = model.ID;

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
			strSql.Append("delete from QualificationTable ");
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
			strSql.Append("delete from QualificationTable ");
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
		public Eday.Model.QualificationTable GetModel(string ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,CourseCode,EmployeeID,CourseTime,Status,CreateTime,CreateUserID,UpdateTime,UpdateUserID,CheckCode,bak1,bak2,bak3,bak4,bak5 from QualificationTable ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.NVarChar,50)			};
			parameters[0].Value = ID;

			Eday.Model.QualificationTable model=new Eday.Model.QualificationTable();
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
		public Eday.Model.QualificationTable DataRowToModel(DataRow row)
		{
			Eday.Model.QualificationTable model=new Eday.Model.QualificationTable();
			if (row != null)
			{
				if(row["ID"]!=null)
				{
					model.ID=row["ID"].ToString();
				}
				if(row["CourseCode"]!=null)
				{
					model.CourseCode=row["CourseCode"].ToString();
				}
				if(row["EmployeeID"]!=null)
				{
					model.EmployeeID=row["EmployeeID"].ToString();
				}
				if(row["CourseTime"]!=null && row["CourseTime"].ToString()!="")
				{
					model.CourseTime=DateTime.Parse(row["CourseTime"].ToString());
				}
				if(row["Status"]!=null)
				{
					model.Status=row["Status"].ToString();
				}
				if(row["CreateTime"]!=null && row["CreateTime"].ToString()!="")
				{
					model.CreateTime=DateTime.Parse(row["CreateTime"].ToString());
				}
				if(row["CreateUserID"]!=null)
				{
					model.CreateUserID=row["CreateUserID"].ToString();
				}
				if(row["UpdateTime"]!=null && row["UpdateTime"].ToString()!="")
				{
					model.UpdateTime=DateTime.Parse(row["UpdateTime"].ToString());
				}
				if(row["UpdateUserID"]!=null)
				{
					model.UpdateUserID=row["UpdateUserID"].ToString();
				}
				if(row["CheckCode"]!=null)
				{
					model.CheckCode=row["CheckCode"].ToString();
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
			strSql.Append("select ID,CourseCode,EmployeeID,CourseTime,Status,CreateTime,CreateUserID,UpdateTime,UpdateUserID,CheckCode,bak1,bak2,bak3,bak4,bak5 ");
			strSql.Append(" FROM QualificationTable ");
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
			strSql.Append(" ID,CourseCode,EmployeeID,CourseTime,Status,CreateTime,CreateUserID,UpdateTime,UpdateUserID,CheckCode,bak1,bak2,bak3,bak4,bak5 ");
			strSql.Append(" FROM QualificationTable ");
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
			strSql.Append("select count(1) FROM QualificationTable ");
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
			strSql.Append(")AS Row, T.*  from QualificationTable T ");
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
			parameters[0].Value = "QualificationTable";
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
        public Eday.Model.QualificationTable GetModelByCourseAndEmploeeID(string CourseCode, string initialID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,CourseCode,EmployeeID,CourseTime,Status,CreateTime,CreateUserID,UpdateTime,UpdateUserID,CheckCode,bak1,bak2,bak3,bak4,bak5 from QualificationTable ");
            strSql.Append(" where  CourseCode=@CourseCode and EmployeeID=@EmployeeID ");
            SqlParameter[] parameters = {
					new SqlParameter("@CourseCode", SqlDbType.NVarChar,50),		
                    new SqlParameter("@EmployeeID", SqlDbType.NVarChar,50),		
                                        };
            parameters[0].Value = CourseCode;
            parameters[1].Value = initialID;

            Eday.Model.QualificationTable model = new Eday.Model.QualificationTable();
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

