using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MatrixTool.Model
{
    [Serializable]
    public partial class Module
    {
        public Module()
        { }
        #region Model
        private string _id;
        private string _initital;
        private string _modulecode;
        private string _description;
        private DateTime? _createtime;
        private string _createuser;
        private DateTime? _updatetime;
        private string _updateuser;
        private string _status;
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
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string initital
        {
            set { _initital = value; }
            get { return _initital; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModuleCode
        {
            set { _modulecode = value; }
            get { return _modulecode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreateUser
        {
            set { _createuser = value; }
            get { return _createuser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UpdateUser
        {
            set { _updateuser = value; }
            get { return _updateuser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Checkcode
        {
            set { _checkcode = value; }
            get { return _checkcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string bak1
        {
            set { _bak1 = value; }
            get { return _bak1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string bak2
        {
            set { _bak2 = value; }
            get { return _bak2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string bak3
        {
            set { _bak3 = value; }
            get { return _bak3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string bak4
        {
            set { _bak4 = value; }
            get { return _bak4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string bak5
        {
            set { _bak5 = value; }
            get { return _bak5; }
        }
        #endregion Model
    }
}