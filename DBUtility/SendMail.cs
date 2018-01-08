using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Data;

namespace DBUtility
{
    public static class SendMail
    {

        /// <summary>
        /// 发送邮件方法
        /// </summary>
        /// <param name="Sysemail">系统邮箱</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件格式、主要内容</param>
        /// <param name="sever">邮件服务</param>
        /// <param name="to">收件人</param>
        /// <param name="bcc">抄送人</param>
        public static void Send(string Sysemail, string SyseSubject, string subject, string body, string sever, string to, string cc)
        {
            MailMessage mailObj = new MailMessage();

            mailObj.From = new MailAddress(Sysemail, SyseSubject, System.Text.Encoding.UTF8);

            string[] Emails = to.Split(';');
            for (int i = 0; i < Emails.Length; i++)
            {
                if (Emails[i] != "")
                {
                    mailObj.To.Add(Emails[i]);
                }
            }


            string[] CcEmails = cc.Split(';');
            for (int i = 0; i < CcEmails.Length; i++)
            {
                if (CcEmails[i] != "")
                {
                    mailObj.CC.Add(CcEmails[i]);
                }
            }
                

            //mailObj.CC.Add(cc);

            mailObj.Subject = subject;
            mailObj.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码  

            mailObj.Priority = MailPriority.Low;

            mailObj.IsBodyHtml = true;

            mailObj.Body = body.ToString();//邮件内容
            mailObj.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码  

            SmtpClient client = new SmtpClient();
            client.Host = sever;
            //object userState = mailObj;
            try
            {
                //client.SendAsync(mailObj, userState);
                client.Send(mailObj);  
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                //DBUtility.MessageBox.Show("发送邮件出错");
               // ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>alert('发送邮件出错');</script>");
                //MessageBox.Show(this, "请为新建课程填写课号");
                throw new Exception(ex.Message);
            }  
        }


        /// <summary>
        /// 发送邮件带附件的方法
        /// </summary>
        /// <param name="Sysemail">系统邮箱</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件格式、主要内容</param>
        /// <param name="sever">邮件服务</param>
        /// <param name="to">收件人</param>
        /// <param name="cc">抄送人</param>
        /// <param name="file">附件</param>
        public static void Send(string Sysemail, string SyseSubject, string subject, string body, string sever, string to, string cc, string file)
        {
            MailMessage mailObj = new MailMessage();

            mailObj.From = new MailAddress(Sysemail, SyseSubject, System.Text.Encoding.UTF8);

            string[] Emails = to.Split(';');
            for (int i = 0; i < Emails.Length; i++)
            {
                if (Emails[i] != "")
                {
                    mailObj.To.Add(Emails[i]);
                }
            }

            string[] CcEmails = cc.Split(';');
            for (int i = 0; i < CcEmails.Length; i++)
            {
                if (CcEmails[i] != "")
                {
                    mailObj.CC.Add(CcEmails[i]);
                }
            }

            Attachment objMailAttachment;
            //创建一个附件对象  
            objMailAttachment = new Attachment(file);//发送邮件的附件
            mailObj.Attachments.Add(objMailAttachment);//将附件附加到邮件消息对象中  

            mailObj.Subject = subject;
            mailObj.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码  

            mailObj.Priority = MailPriority.Low;

            mailObj.IsBodyHtml = true;

            mailObj.Body = body.ToString();//邮件内容
            mailObj.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码  

            SmtpClient client = new SmtpClient();
            client.Host = sever;
            //object userState = mailObj;
            try
            {
                //client.SendAsync(mailObj, userState);
                client.Send(mailObj);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// 查询人事信息
        /// </summary>
        /// <param name="Initial">人员账号</param>
        /// <returns>string</returns>
        public static string GetUserInfor(string Initial, string flage)
        {
            string reStr = "";
            try
            {
                string sql = "select * from hr_info where userID = '" + Initial + "'";
                DataTable dt = DbHelperSQL.Query(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    reStr = dt.Rows[0][flage].ToString();
                }

            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return reStr;
        }
    }
}
