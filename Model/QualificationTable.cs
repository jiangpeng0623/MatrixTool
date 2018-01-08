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
namespace Eday.Model
{
	/// <summary>
	/// QualificationTable:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class QualificationTable
	{
		public QualificationTable()
		{}
		#region Model
		private string _id;
		private string _coursecode;
		private string _employeeid;
		private DateTime? _coursetime;
		private string _status;
		private DateTime? _createtime;
		private string _createuserid;
		private DateTime? _updatetime;
		private string _updateuserid;
		private string _checkcode;
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
		public string CourseCode
		{
			set{ _coursecode=value;}
			get{return _coursecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EmployeeID
		{
			set{ _employeeid=value;}
			get{return _employeeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CourseTime
		{
			set{ _coursetime=value;}
			get{return _coursetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CreateUserID
		{
			set{ _createuserid=value;}
			get{return _createuserid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UpdateUserID
		{
			set{ _updateuserid=value;}
			get{return _updateuserid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CheckCode
		{
			set{ _checkcode=value;}
			get{return _checkcode;}
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

