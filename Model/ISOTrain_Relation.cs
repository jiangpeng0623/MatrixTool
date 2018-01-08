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
namespace Eday.Model
{
	/// <summary>
	/// ISOTrain_Relation:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ISOTrain_Relation
	{
		public ISOTrain_Relation()
		{}
		#region Model
		private string _id;
		private string _code;
		private string _name;
		private string _modulecode;
		private string _description;
		private string _coursecode;
		private string _coursetitledescription;
		private string _tasklevel;
		private string _notifydays;
		private string _needretraining;
		private string _frequency;
		/// <summary>
		/// 
		/// </summary>
		public string ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ModuleCode
		{
			set{ _modulecode=value;}
			get{return _modulecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CourseCode
		{
			set{ _coursecode=value;}
			get{return _coursecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CourseTitleDescription
		{
			set{ _coursetitledescription=value;}
			get{return _coursetitledescription;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TaskLevel
		{
			set{ _tasklevel=value;}
			get{return _tasklevel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NotifyDays
		{
			set{ _notifydays=value;}
			get{return _notifydays;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NeedRetraining
		{
			set{ _needretraining=value;}
			get{return _needretraining;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Frequency
		{
			set{ _frequency=value;}
			get{return _frequency;}
		}
		#endregion Model

	}
}

