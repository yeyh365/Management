using log4net;
using Management.Application.Services.Impl;
using Management.Core.Helper;
using ManagementApi.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using static System.Net.Mime.MediaTypeNames;

namespace ManagementApi.Controllers
{
    /// <summary>
    /// 测试用
    /// </summary>
    [RoutePrefix("api/AATestStart")]
    public class AATestStartController : ApiController
    {
/*        private static readonly ILog logger = log4net.LogManager.GetLogger("loginfo"); */  //选择<logger name="loginfo">的配置 
        [HttpGet]
        [Route("AATestStart")]
        public string GetPicturesSteam(string pwt)
        {

            EmailService email = new EmailService();
            email.LoginSendEmail();
            string pUrl = ConfigurationManager.AppSettings["reslturl"].ToString();
            //logger.Debug("this is debuger");
            //logger.Info("this is info");
            //logger.Warn("this is warm");
            //logger.Error("this is Error");
            string result = pwt;

            string password = MD5Helper.MD5Encrypt16(result);


            string ACD = "YYH";
            LogHelper.Log(ACD);
            HttpResponseMessage resp = new HttpResponseMessage();


            ///测试redis链接
            string test = "Name";
            string value = "Yeyh";
            string time = "1000";
            RedisHelper redis = new RedisHelper();
            redis.SetStringKey(test, value, int.Parse(time));

            var acd = redis.GetStringValue(test);




            string str = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
                string str1 = System.Environment.CurrentDirectory;
                //string pictures = @"Files\Pictures\abcd.png";
            try
            {
            }
            catch (Exception)
            {

                LogHelper.Log("Exception");
                
            }
            return ACD;
        }
        /// <summary>
        /// 16位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt16(string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(password)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }
    }
}
