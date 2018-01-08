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
using System.Collections.Generic;
using Maticsoft.Common;
using Eday.Model;
namespace Eday.BLL
{
	/// <summary>
	/// map_Jobfunction
	/// </summary>
	public partial class map_Jobfunction
	{
		private readonly Eday.DAL.map_Jobfunction dal=new Eday.DAL.map_Jobfunction();
		public map_Jobfunction()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string ID)
		{
			return dal.Exists(ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Eday.Model.map_Jobfunction model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Eday.Model.map_Jobfunction model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string ID)
		{
			
			return dal.Delete(ID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			return dal.DeleteList(IDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Eday.Model.map_Jobfunction GetModel(string ID)
		{
			
			return dal.GetModel(ID);
		}

        /// <summary>
        /// @杜秋菊
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="JFID"></param>
        /// <param name="JFCode"></param>
        /// <returns></returns>
        public Eday.Model.map_Jobfunction GetModel(string userID, string JFID, string JFCode)
        {

            return dal.GetModel(userID, JFID, JFCode);
        }
		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Eday.Model.map_Jobfunction GetModelByCache(string ID)
		{
			
			string CacheKey = "map_JobfunctionModel-" + ID;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ID);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Eday.Model.map_Jobfunction)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Eday.Model.map_Jobfunction> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Eday.Model.map_Jobfunction> DataTableToList(DataTable dt)
		{
			List<Eday.Model.map_Jobfunction> modelList = new List<Eday.Model.map_Jobfunction>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Eday.Model.map_Jobfunction model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  BasicMethod
		#region  ExtensionMethod
        public string GetCheckCode(Eday.Model.map_Jobfunction mapJob)
        {
            return dal.GetCheckCode(mapJob);
        }
        public string GetAuditStr(Eday.Model.map_Jobfunction mapJob)
        {
            return dal.GetAuditStr(mapJob);
        }

        /// <summary>
        /// @杜秋菊
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="JFID"></param>
        /// <param name="JFCode"></param>
        /// <returns></returns>
        public Eday.Model.map_Jobfunction GetModelByUserIDJFCode(string userID, string JFCode)
        {

            return dal.GetModelByUserIDJFCode(userID, JFCode);
        }
        public Eday.Model.map_Jobfunction GetModelByUserIDJFCodeActiveDate(string userID, string JFCode,DateTime activeDate)
        {

            return dal.GetModelByUserIDJFCodeActiveDate(userID, JFCode, activeDate);
        }
		#endregion  ExtensionMethod
	}
}

