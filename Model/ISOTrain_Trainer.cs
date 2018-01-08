/**  版本信息模板在安装目录下，可自行修改。
* ISOTrain_Trainer.cs
*
* 功 能： N/A
* 类 名： ISOTrain_Trainer
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017/11/17 16:38:43   N/A    初版
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
	/// ISOTrain_Trainer:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ISOTrain_Trainer
	{
		public ISOTrain_Trainer()
		{}
		#region Model
		private string _id;
		private string _trainerinitial;
		private string _coursecode;
		private string _tasklevel;
		private DateTime? _effectivedate;
		private DateTime? _createdate;
		private string _bak1;
		private string _bak2;
		private string _bak3;
		private string _bak4;
		private string _bak5;
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
		public string TrainerInitial
		{
			set{ _trainerinitial=value;}
			get{return _trainerinitial;}
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
		public string TaskLevel
		{
			set{ _tasklevel=value;}
			get{return _tasklevel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EffectiveDate
		{
			set{ _effectivedate=value;}
			get{return _effectivedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bak1
		{
			set{ _bak1=value;}
			get{return _bak1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bak2
		{
			set{ _bak2=value;}
			get{return _bak2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bak3
		{
			set{ _bak3=value;}
			get{return _bak3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bak4
		{
			set{ _bak4=value;}
			get{return _bak4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bak5
		{
			set{ _bak5=value;}
			get{return _bak5;}
		}
		#endregion Model

	}
}

