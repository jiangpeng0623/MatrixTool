using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DBUtility;//Please add references

namespace Eday
{
    /// <summary>
    /// 类hr_info。
    /// </summary>
    [Serializable]
    class missingReport
    {
        public missingReport()
        { }
        #region Model
        private string _id;
        private string _coursecode;
        private string _description;
        private string _taskcode;
        private string _supervisorcode;
        private string _userid;
        private string _status;
        private DateTime? _duedate;
        private string _createuser;
        private DateTime? _createtime;
        private DateTime? _isotraintime;
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
        public string CourseCode
        {
            set { _coursecode = value; }
            get { return _coursecode; }
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
        public string TaskCode
        {
            set { _taskcode = value; }
            get { return _taskcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SuperVisorCode
        {
            set { _supervisorcode = value; }
            get { return _supervisorcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string userID
        {
            set { _userid = value; }
            get { return _userid; }
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
        public DateTime? DueDate
        {
            set { _duedate = value; }
            get { return _duedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string createUser
        {
            set { _createuser = value; }
            get { return _createuser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? createTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ISOTrainTime
        {
            set { _isotraintime = value; }
            get { return _isotraintime; }
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

        #region Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from missingReport");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.NVarChar,50)			};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into missingReport(");
            strSql.Append("ID,CourseCode,Description,TaskCode,SuperVisorCode,userID,Status,DueDate,createUser,createTime,ISOTrainTime,bak1,bak2,bak3,bak4,bak5)");
            strSql.Append(" values (");
            strSql.Append("@ID,@CourseCode,@Description,@TaskCode,@SuperVisorCode,@userID,@Status,@DueDate,@createUser,@createTime,@ISOTrainTime,@bak1,@bak2,@bak3,@bak4,@bak5)");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.NVarChar,50),
					new SqlParameter("@CourseCode", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,-1),
					new SqlParameter("@TaskCode", SqlDbType.NVarChar,50),
					new SqlParameter("@SuperVisorCode", SqlDbType.NVarChar,50),
					new SqlParameter("@userID", SqlDbType.NVarChar,50),
					new SqlParameter("@Status", SqlDbType.NVarChar,50),
					new SqlParameter("@DueDate", SqlDbType.DateTime),
					new SqlParameter("@createUser", SqlDbType.NVarChar,50),
					new SqlParameter("@createTime", SqlDbType.DateTime),
					new SqlParameter("@ISOTrainTime", SqlDbType.DateTime),
					new SqlParameter("@bak1", SqlDbType.NVarChar,50),
					new SqlParameter("@bak2", SqlDbType.NVarChar,50),
					new SqlParameter("@bak3", SqlDbType.NVarChar,50),
					new SqlParameter("@bak4", SqlDbType.NVarChar,50),
					new SqlParameter("@bak5", SqlDbType.NVarChar,50)};
            parameters[0].Value = ID;
            parameters[1].Value = CourseCode;
            parameters[2].Value = Description;
            parameters[3].Value = TaskCode;
            parameters[4].Value = SuperVisorCode;
            parameters[5].Value = userID;
            parameters[6].Value = Status;
            parameters[7].Value = DueDate;
            parameters[8].Value = createUser;
            parameters[9].Value = createTime;
            parameters[10].Value = ISOTrainTime;
            parameters[11].Value = bak1;
            parameters[12].Value = bak2;
            parameters[13].Value = bak3;
            parameters[14].Value = bak4;
            parameters[15].Value = bak5;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from missingReport ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.NVarChar,50)			};
            parameters[0].Value = ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from missingReport ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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
        public  missingReport(string ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,CourseCode,Description,TaskCode,SuperVisorCode,userID,Status,DueDate,createUser,createTime,ISOTrainTime,bak1,bak2,bak3,bak4,bak5 from missingReport ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.NVarChar,50)			};
            parameters[0].Value = ID;


            DataSet ds = DbHelperSQL_wss.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                if (row != null)
                {
                    if (row["ID"] != null)
                    {
                        ID = row["ID"].ToString();
                    }
                    if (row["CourseCode"] != null)
                    {
                        CourseCode = row["CourseCode"].ToString();
                    }
                    if (row["Description"] != null)
                    {
                        Description = row["Description"].ToString();
                    }
                    if (row["TaskCode"] != null)
                    {
                        TaskCode = row["TaskCode"].ToString();
                    }
                    if (row["SuperVisorCode"] != null)
                    {
                        SuperVisorCode = row["SuperVisorCode"].ToString();
                    }
                    if (row["userID"] != null)
                    {
                        userID = row["userID"].ToString();
                    }
                    if (row["Status"] != null)
                    {
                        Status = row["Status"].ToString();
                    }
                    if (row["DueDate"] != null && row["DueDate"].ToString() != "")
                    {
                        DueDate = DateTime.Parse(row["DueDate"].ToString());
                    }
                    if (row["createUser"] != null)
                    {
                        createUser = row["createUser"].ToString();
                    }
                    if (row["createTime"] != null && row["createTime"].ToString() != "")
                    {
                        createTime = DateTime.Parse(row["createTime"].ToString());
                    }
                    if (row["ISOTrainTime"] != null && row["ISOTrainTime"].ToString() != "")
                    {
                        ISOTrainTime = DateTime.Parse(row["ISOTrainTime"].ToString());
                    }
                    if (row["bak1"] != null)
                    {
                        bak1 = row["bak1"].ToString();
                    }
                    if (row["bak2"] != null)
                    {
                        bak2 = row["bak2"].ToString();
                    }
                    if (row["bak3"] != null)
                    {
                        bak3 = row["bak3"].ToString();
                    }
                    if (row["bak4"] != null)
                    {
                        bak4 = row["bak4"].ToString();
                    }
                    if (row["bak5"] != null)
                    {
                        bak5 = row["bak5"].ToString();
                    }
                }
            }
           
        }
        #endregion
    }
}
