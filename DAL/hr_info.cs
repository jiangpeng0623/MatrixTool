/**  版本信息模板在安装目录下，可自行修改。
* hr_info.cs
*
* 功 能： N/A
* 类 名： hr_info
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2016/10/9 10:02:44   N/A    初版
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
	/// 数据访问类:hr_info
	/// </summary>
	public partial class hr_info
	{
		public hr_info()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from hr_info");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.NVarChar,50)			};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Eday.Model.hr_info model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into hr_info(");
			strSql.Append("ID,userID,userPwd,roleNames,roleIDs,userName,IDcard,gender,nation,bloodType,birthdate,nativePlace,maritalStatus,Political,mobilephone,phoneCall,workPhone,Email,firstEducation,Topeducation,Worktime,entryTime,status,ContractTime,dept,deptID,address,AccountLocation,FileLocation,addDate,remark,photo,director,costCenterArea,position,postTitle,flag,bak1,bak2,bak3,bak4,bak5)");
			strSql.Append(" values (");
			strSql.Append("@ID,@userID,@userPwd,@roleNames,@roleIDs,@userName,@IDcard,@gender,@nation,@bloodType,@birthdate,@nativePlace,@maritalStatus,@Political,@mobilephone,@phoneCall,@workPhone,@Email,@firstEducation,@Topeducation,@Worktime,@entryTime,@status,@ContractTime,@dept,@deptID,@address,@AccountLocation,@FileLocation,@addDate,@remark,@photo,@director,@costCenterArea,@position,@postTitle,@flag,@bak1,@bak2,@bak3,@bak4,@bak5)");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.NVarChar,50),
					new SqlParameter("@userID", SqlDbType.NVarChar,-1),
					new SqlParameter("@userPwd", SqlDbType.NVarChar,-1),
					new SqlParameter("@roleNames", SqlDbType.NVarChar,-1),
					new SqlParameter("@roleIDs", SqlDbType.NVarChar,-1),
					new SqlParameter("@userName", SqlDbType.NVarChar,-1),
					new SqlParameter("@IDcard", SqlDbType.NVarChar,-1),
					new SqlParameter("@gender", SqlDbType.NVarChar,-1),
					new SqlParameter("@nation", SqlDbType.NVarChar,-1),
					new SqlParameter("@bloodType", SqlDbType.NVarChar,-1),
					new SqlParameter("@birthdate", SqlDbType.NVarChar,-1),
					new SqlParameter("@nativePlace", SqlDbType.NVarChar,-1),
					new SqlParameter("@maritalStatus", SqlDbType.NVarChar,-1),
					new SqlParameter("@Political", SqlDbType.NVarChar,-1),
					new SqlParameter("@mobilephone", SqlDbType.NVarChar,-1),
					new SqlParameter("@phoneCall", SqlDbType.NVarChar,-1),
					new SqlParameter("@workPhone", SqlDbType.NVarChar,-1),
					new SqlParameter("@Email", SqlDbType.NVarChar,-1),
					new SqlParameter("@firstEducation", SqlDbType.NVarChar,-1),
					new SqlParameter("@Topeducation", SqlDbType.NVarChar,-1),
					new SqlParameter("@Worktime", SqlDbType.NVarChar,-1),
					new SqlParameter("@entryTime", SqlDbType.NVarChar,-1),
					new SqlParameter("@status", SqlDbType.NVarChar,-1),
					new SqlParameter("@ContractTime", SqlDbType.NVarChar,-1),
					new SqlParameter("@dept", SqlDbType.NVarChar,-1),
					new SqlParameter("@deptID", SqlDbType.NVarChar,-1),
					new SqlParameter("@address", SqlDbType.NVarChar,-1),
					new SqlParameter("@AccountLocation", SqlDbType.NVarChar,-1),
					new SqlParameter("@FileLocation", SqlDbType.NVarChar,-1),
					new SqlParameter("@addDate", SqlDbType.DateTime),
					new SqlParameter("@remark", SqlDbType.NVarChar,-1),
					new SqlParameter("@photo", SqlDbType.NVarChar,-1),
					new SqlParameter("@director", SqlDbType.NVarChar,50),
					new SqlParameter("@costCenterArea", SqlDbType.NVarChar,50),
					new SqlParameter("@position", SqlDbType.NVarChar,50),
					new SqlParameter("@postTitle", SqlDbType.NVarChar,50),
					new SqlParameter("@flag", SqlDbType.VarChar,50),
					new SqlParameter("@bak1", SqlDbType.NVarChar,50),
					new SqlParameter("@bak2", SqlDbType.NVarChar,50),
					new SqlParameter("@bak3", SqlDbType.NVarChar,50),
					new SqlParameter("@bak4", SqlDbType.NVarChar,50),
					new SqlParameter("@bak5", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.userID;
			parameters[2].Value = model.userPwd;
			parameters[3].Value = model.roleNames;
			parameters[4].Value = model.roleIDs;
			parameters[5].Value = model.userName;
			parameters[6].Value = model.IDcard;
			parameters[7].Value = model.gender;
			parameters[8].Value = model.nation;
			parameters[9].Value = model.bloodType;
			parameters[10].Value = model.birthdate;
			parameters[11].Value = model.nativePlace;
			parameters[12].Value = model.maritalStatus;
			parameters[13].Value = model.Political;
			parameters[14].Value = model.mobilephone;
			parameters[15].Value = model.phoneCall;
			parameters[16].Value = model.workPhone;
			parameters[17].Value = model.Email;
			parameters[18].Value = model.firstEducation;
			parameters[19].Value = model.Topeducation;
			parameters[20].Value = model.Worktime;
			parameters[21].Value = model.entryTime;
			parameters[22].Value = model.status;
			parameters[23].Value = model.ContractTime;
			parameters[24].Value = model.dept;
			parameters[25].Value = model.deptID;
			parameters[26].Value = model.address;
			parameters[27].Value = model.AccountLocation;
			parameters[28].Value = model.FileLocation;
			parameters[29].Value = model.addDate;
			parameters[30].Value = model.remark;
			parameters[31].Value = model.photo;
			parameters[32].Value = model.director;
			parameters[33].Value = model.costCenterArea;
			parameters[34].Value = model.position;
			parameters[35].Value = model.postTitle;
			parameters[36].Value = model.flag;
			parameters[37].Value = model.bak1;
			parameters[38].Value = model.bak2;
			parameters[39].Value = model.bak3;
			parameters[40].Value = model.bak4;
			parameters[41].Value = model.bak5;

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
		public bool Update(Eday.Model.hr_info model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update hr_info set ");
			strSql.Append("userID=@userID,");
			strSql.Append("userPwd=@userPwd,");
			strSql.Append("roleNames=@roleNames,");
			strSql.Append("roleIDs=@roleIDs,");
			strSql.Append("userName=@userName,");
			strSql.Append("IDcard=@IDcard,");
			strSql.Append("gender=@gender,");
			strSql.Append("nation=@nation,");
			strSql.Append("bloodType=@bloodType,");
			strSql.Append("birthdate=@birthdate,");
			strSql.Append("nativePlace=@nativePlace,");
			strSql.Append("maritalStatus=@maritalStatus,");
			strSql.Append("Political=@Political,");
			strSql.Append("mobilephone=@mobilephone,");
			strSql.Append("phoneCall=@phoneCall,");
			strSql.Append("workPhone=@workPhone,");
			strSql.Append("Email=@Email,");
			strSql.Append("firstEducation=@firstEducation,");
			strSql.Append("Topeducation=@Topeducation,");
			strSql.Append("Worktime=@Worktime,");
			strSql.Append("entryTime=@entryTime,");
			strSql.Append("status=@status,");
			strSql.Append("ContractTime=@ContractTime,");
			strSql.Append("dept=@dept,");
			strSql.Append("deptID=@deptID,");
			strSql.Append("address=@address,");
			strSql.Append("AccountLocation=@AccountLocation,");
			strSql.Append("FileLocation=@FileLocation,");
			strSql.Append("addDate=@addDate,");
			strSql.Append("remark=@remark,");
			strSql.Append("photo=@photo,");
			strSql.Append("director=@director,");
			strSql.Append("costCenterArea=@costCenterArea,");
			strSql.Append("position=@position,");
			strSql.Append("postTitle=@postTitle,");
			strSql.Append("flag=@flag,");
			strSql.Append("bak1=@bak1,");
			strSql.Append("bak2=@bak2,");
			strSql.Append("bak3=@bak3,");
			strSql.Append("bak4=@bak4,");
			strSql.Append("bak5=@bak5");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@userID", SqlDbType.NVarChar,-1),
					new SqlParameter("@userPwd", SqlDbType.NVarChar,-1),
					new SqlParameter("@roleNames", SqlDbType.NVarChar,-1),
					new SqlParameter("@roleIDs", SqlDbType.NVarChar,-1),
					new SqlParameter("@userName", SqlDbType.NVarChar,-1),
					new SqlParameter("@IDcard", SqlDbType.NVarChar,-1),
					new SqlParameter("@gender", SqlDbType.NVarChar,-1),
					new SqlParameter("@nation", SqlDbType.NVarChar,-1),
					new SqlParameter("@bloodType", SqlDbType.NVarChar,-1),
					new SqlParameter("@birthdate", SqlDbType.NVarChar,-1),
					new SqlParameter("@nativePlace", SqlDbType.NVarChar,-1),
					new SqlParameter("@maritalStatus", SqlDbType.NVarChar,-1),
					new SqlParameter("@Political", SqlDbType.NVarChar,-1),
					new SqlParameter("@mobilephone", SqlDbType.NVarChar,-1),
					new SqlParameter("@phoneCall", SqlDbType.NVarChar,-1),
					new SqlParameter("@workPhone", SqlDbType.NVarChar,-1),
					new SqlParameter("@Email", SqlDbType.NVarChar,-1),
					new SqlParameter("@firstEducation", SqlDbType.NVarChar,-1),
					new SqlParameter("@Topeducation", SqlDbType.NVarChar,-1),
					new SqlParameter("@Worktime", SqlDbType.NVarChar,-1),
					new SqlParameter("@entryTime", SqlDbType.NVarChar,-1),
					new SqlParameter("@status", SqlDbType.NVarChar,-1),
					new SqlParameter("@ContractTime", SqlDbType.NVarChar,-1),
					new SqlParameter("@dept", SqlDbType.NVarChar,-1),
					new SqlParameter("@deptID", SqlDbType.NVarChar,-1),
					new SqlParameter("@address", SqlDbType.NVarChar,-1),
					new SqlParameter("@AccountLocation", SqlDbType.NVarChar,-1),
					new SqlParameter("@FileLocation", SqlDbType.NVarChar,-1),
					new SqlParameter("@addDate", SqlDbType.DateTime),
					new SqlParameter("@remark", SqlDbType.NVarChar,-1),
					new SqlParameter("@photo", SqlDbType.NVarChar,-1),
					new SqlParameter("@director", SqlDbType.NVarChar,50),
					new SqlParameter("@costCenterArea", SqlDbType.NVarChar,50),
					new SqlParameter("@position", SqlDbType.NVarChar,50),
					new SqlParameter("@postTitle", SqlDbType.NVarChar,50),
					new SqlParameter("@flag", SqlDbType.VarChar,50),
					new SqlParameter("@bak1", SqlDbType.NVarChar,50),
					new SqlParameter("@bak2", SqlDbType.NVarChar,50),
					new SqlParameter("@bak3", SqlDbType.NVarChar,50),
					new SqlParameter("@bak4", SqlDbType.NVarChar,50),
					new SqlParameter("@bak5", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.userID;
			parameters[1].Value = model.userPwd;
			parameters[2].Value = model.roleNames;
			parameters[3].Value = model.roleIDs;
			parameters[4].Value = model.userName;
			parameters[5].Value = model.IDcard;
			parameters[6].Value = model.gender;
			parameters[7].Value = model.nation;
			parameters[8].Value = model.bloodType;
			parameters[9].Value = model.birthdate;
			parameters[10].Value = model.nativePlace;
			parameters[11].Value = model.maritalStatus;
			parameters[12].Value = model.Political;
			parameters[13].Value = model.mobilephone;
			parameters[14].Value = model.phoneCall;
			parameters[15].Value = model.workPhone;
			parameters[16].Value = model.Email;
			parameters[17].Value = model.firstEducation;
			parameters[18].Value = model.Topeducation;
			parameters[19].Value = model.Worktime;
			parameters[20].Value = model.entryTime;
			parameters[21].Value = model.status;
			parameters[22].Value = model.ContractTime;
			parameters[23].Value = model.dept;
			parameters[24].Value = model.deptID;
			parameters[25].Value = model.address;
			parameters[26].Value = model.AccountLocation;
			parameters[27].Value = model.FileLocation;
			parameters[28].Value = model.addDate;
			parameters[29].Value = model.remark;
			parameters[30].Value = model.photo;
			parameters[31].Value = model.director;
			parameters[32].Value = model.costCenterArea;
			parameters[33].Value = model.position;
			parameters[34].Value = model.postTitle;
			parameters[35].Value = model.flag;
			parameters[36].Value = model.bak1;
			parameters[37].Value = model.bak2;
			parameters[38].Value = model.bak3;
			parameters[39].Value = model.bak4;
			parameters[40].Value = model.bak5;
			parameters[41].Value = model.ID;

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
			strSql.Append("delete from hr_info ");
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
			strSql.Append("delete from hr_info ");
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
		public Eday.Model.hr_info GetModel(string ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,userID,userPwd,roleNames,roleIDs,userName,IDcard,gender,nation,bloodType,birthdate,nativePlace,maritalStatus,Political,mobilephone,phoneCall,workPhone,Email,firstEducation,Topeducation,Worktime,entryTime,status,ContractTime,dept,deptID,address,AccountLocation,FileLocation,addDate,remark,photo,director,costCenterArea,position,postTitle,flag,bak1,bak2,bak3,bak4,bak5 from hr_info ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.NVarChar,50)			};
			parameters[0].Value = ID;

			Eday.Model.hr_info model=new Eday.Model.hr_info();
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
		public Eday.Model.hr_info DataRowToModel(DataRow row)
		{
			Eday.Model.hr_info model=new Eday.Model.hr_info();
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
				if(row["userPwd"]!=null)
				{
					model.userPwd=row["userPwd"].ToString();
				}
				if(row["roleNames"]!=null)
				{
					model.roleNames=row["roleNames"].ToString();
				}
				if(row["roleIDs"]!=null)
				{
					model.roleIDs=row["roleIDs"].ToString();
				}
				if(row["userName"]!=null)
				{
					model.userName=row["userName"].ToString();
				}
				if(row["IDcard"]!=null)
				{
					model.IDcard=row["IDcard"].ToString();
				}
				if(row["gender"]!=null)
				{
					model.gender=row["gender"].ToString();
				}
				if(row["nation"]!=null)
				{
					model.nation=row["nation"].ToString();
				}
				if(row["bloodType"]!=null)
				{
					model.bloodType=row["bloodType"].ToString();
				}
				if(row["birthdate"]!=null)
				{
					model.birthdate=row["birthdate"].ToString();
				}
				if(row["nativePlace"]!=null)
				{
					model.nativePlace=row["nativePlace"].ToString();
				}
				if(row["maritalStatus"]!=null)
				{
					model.maritalStatus=row["maritalStatus"].ToString();
				}
				if(row["Political"]!=null)
				{
					model.Political=row["Political"].ToString();
				}
				if(row["mobilephone"]!=null)
				{
					model.mobilephone=row["mobilephone"].ToString();
				}
				if(row["phoneCall"]!=null)
				{
					model.phoneCall=row["phoneCall"].ToString();
				}
				if(row["workPhone"]!=null)
				{
					model.workPhone=row["workPhone"].ToString();
				}
				if(row["Email"]!=null)
				{
					model.Email=row["Email"].ToString();
				}
				if(row["firstEducation"]!=null)
				{
					model.firstEducation=row["firstEducation"].ToString();
				}
				if(row["Topeducation"]!=null)
				{
					model.Topeducation=row["Topeducation"].ToString();
				}
				if(row["Worktime"]!=null)
				{
					model.Worktime=row["Worktime"].ToString();
				}
				if(row["entryTime"]!=null)
				{
					model.entryTime=row["entryTime"].ToString();
				}
				if(row["status"]!=null)
				{
					model.status=row["status"].ToString();
				}
				if(row["ContractTime"]!=null)
				{
					model.ContractTime=row["ContractTime"].ToString();
				}
				if(row["dept"]!=null)
				{
					model.dept=row["dept"].ToString();
				}
				if(row["deptID"]!=null)
				{
					model.deptID=row["deptID"].ToString();
				}
				if(row["address"]!=null)
				{
					model.address=row["address"].ToString();
				}
				if(row["AccountLocation"]!=null)
				{
					model.AccountLocation=row["AccountLocation"].ToString();
				}
				if(row["FileLocation"]!=null)
				{
					model.FileLocation=row["FileLocation"].ToString();
				}
				if(row["addDate"]!=null && row["addDate"].ToString()!="")
				{
					model.addDate=DateTime.Parse(row["addDate"].ToString());
				}
				if(row["remark"]!=null)
				{
					model.remark=row["remark"].ToString();
				}
				if(row["photo"]!=null)
				{
					model.photo=row["photo"].ToString();
				}
				if(row["director"]!=null)
				{
					model.director=row["director"].ToString();
				}
				if(row["costCenterArea"]!=null)
				{
					model.costCenterArea=row["costCenterArea"].ToString();
				}
				if(row["position"]!=null)
				{
					model.position=row["position"].ToString();
				}
				if(row["postTitle"]!=null)
				{
					model.postTitle=row["postTitle"].ToString();
				}
				if(row["flag"]!=null)
				{
					model.flag=row["flag"].ToString();
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
			strSql.Append("select ID,userID,userPwd,roleNames,roleIDs,userName,IDcard,gender,nation,bloodType,birthdate,nativePlace,maritalStatus,Political,mobilephone,phoneCall,workPhone,Email,firstEducation,Topeducation,Worktime,entryTime,status,ContractTime,dept,deptID,address,AccountLocation,FileLocation,addDate,remark,photo,director,costCenterArea,position,postTitle,flag,bak1,bak2,bak3,bak4,bak5 ");
			strSql.Append(" FROM hr_info ");
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
			strSql.Append(" ID,userID,userPwd,roleNames,roleIDs,userName,IDcard,gender,nation,bloodType,birthdate,nativePlace,maritalStatus,Political,mobilephone,phoneCall,workPhone,Email,firstEducation,Topeducation,Worktime,entryTime,status,ContractTime,dept,deptID,address,AccountLocation,FileLocation,addDate,remark,photo,director,costCenterArea,position,postTitle,flag,bak1,bak2,bak3,bak4,bak5 ");
			strSql.Append(" FROM hr_info ");
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
			strSql.Append("select count(1) FROM hr_info ");
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
			strSql.Append(")AS Row, T.*  from hr_info T ");
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
			parameters[0].Value = "hr_info";
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

		#endregion  ExtensionMethod
	}
}

