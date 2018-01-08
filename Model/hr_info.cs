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
namespace Eday.Model
{
	/// <summary>
	/// hr_info:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class hr_info
	{
		public hr_info()
		{}
		#region Model
		private string _id;
		private string _userid;
		private string _userpwd;
		private string _rolenames;
		private string _roleids;
		private string _username;
		private string _idcard;
		private string _gender;
		private string _nation;
		private string _bloodtype;
		private string _birthdate;
		private string _nativeplace;
		private string _maritalstatus;
		private string _political;
		private string _mobilephone;
		private string _phonecall;
		private string _workphone;
		private string _email;
		private string _firsteducation;
		private string _topeducation;
		private string _worktime;
		private string _entrytime;
		private string _status;
		private string _contracttime;
		private string _dept;
		private string _deptid;
		private string _address;
		private string _accountlocation;
		private string _filelocation;
		private DateTime? _adddate;
		private string _remark;
		private string _photo;
		private string _director;
		private string _costcenterarea;
		private string _position;
		private string _posttitle;
		private string _flag;
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
		public string userID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string userPwd
		{
			set{ _userpwd=value;}
			get{return _userpwd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string roleNames
		{
			set{ _rolenames=value;}
			get{return _rolenames;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string roleIDs
		{
			set{ _roleids=value;}
			get{return _roleids;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string userName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IDcard
		{
			set{ _idcard=value;}
			get{return _idcard;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string gender
		{
			set{ _gender=value;}
			get{return _gender;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string nation
		{
			set{ _nation=value;}
			get{return _nation;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bloodType
		{
			set{ _bloodtype=value;}
			get{return _bloodtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string birthdate
		{
			set{ _birthdate=value;}
			get{return _birthdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string nativePlace
		{
			set{ _nativeplace=value;}
			get{return _nativeplace;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string maritalStatus
		{
			set{ _maritalstatus=value;}
			get{return _maritalstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Political
		{
			set{ _political=value;}
			get{return _political;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string mobilephone
		{
			set{ _mobilephone=value;}
			get{return _mobilephone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string phoneCall
		{
			set{ _phonecall=value;}
			get{return _phonecall;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string workPhone
		{
			set{ _workphone=value;}
			get{return _workphone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string firstEducation
		{
			set{ _firsteducation=value;}
			get{return _firsteducation;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Topeducation
		{
			set{ _topeducation=value;}
			get{return _topeducation;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Worktime
		{
			set{ _worktime=value;}
			get{return _worktime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string entryTime
		{
			set{ _entrytime=value;}
			get{return _entrytime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ContractTime
		{
			set{ _contracttime=value;}
			get{return _contracttime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string dept
		{
			set{ _dept=value;}
			get{return _dept;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string deptID
		{
			set{ _deptid=value;}
			get{return _deptid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AccountLocation
		{
			set{ _accountlocation=value;}
			get{return _accountlocation;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FileLocation
		{
			set{ _filelocation=value;}
			get{return _filelocation;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? addDate
		{
			set{ _adddate=value;}
			get{return _adddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string photo
		{
			set{ _photo=value;}
			get{return _photo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string director
		{
			set{ _director=value;}
			get{return _director;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string costCenterArea
		{
			set{ _costcenterarea=value;}
			get{return _costcenterarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string position
		{
			set{ _position=value;}
			get{return _position;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string postTitle
		{
			set{ _posttitle=value;}
			get{return _posttitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string flag
		{
			set{ _flag=value;}
			get{return _flag;}
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

