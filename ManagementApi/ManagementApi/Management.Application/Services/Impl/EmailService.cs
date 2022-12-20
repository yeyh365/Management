using Management.Core.Helper;
using Management.Core.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Management.Application.Services.Impl
{
    public class EmailService
    {
        /// <summary>
        /// 发送通知邮箱通知登陆人
        /// </summary>
        public void LoginSendEmail()
        {
            EmailHelper email = new EmailHelper();
            EmailModel emailModel = new EmailModel();
            emailModel.From = "yeyh365@163.com";
            emailModel.To = new List<string>()
            {
                "yeyh365@163.com"
            };
            emailModel.Subject = "测试.net";
            emailModel.Body = "hello word!";
            emailModel.IsBodyHtml = false;
            emailModel.Host = ConfigurationManager.AppSettings["MailHost"].ToString();
            emailModel.Port =Convert.ToInt32(ConfigurationManager.AppSettings["MailPort"])  ;
            emailModel.UserName = ConfigurationManager.AppSettings["UserName"].ToString();
            emailModel.Password = ConfigurationManager.AppSettings["Password"].ToString();
            emailModel.MailPriority=System.Net.Mail.MailPriority.Normal;
            emailModel.Encoding = Encoding.UTF8;
            email.Send(emailModel);



        }
    }
}
