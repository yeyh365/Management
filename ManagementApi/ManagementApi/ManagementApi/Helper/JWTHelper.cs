using JWT.Algorithms;
using JWT.Serializers;
using JWT;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Text;
using ManagementApi.Models;

namespace ManagementApi.Helper
{
    public class JWTHelper
    {
        /// <summary>
        /// 传入一个AuthInfo 对象包含UserId Expires时间 生成token
        /// </summary>
        /// <param name="payload">封装号的一个获取</param>
        /// <returns></returns>
        public static string EncodeAccessToken(object payload)
        {
            //if (true)
            //{
            //    var key = ConfigurationManager.AppSettings[AppSettingKeys.JWTSecret].ToString();
            //}
            //else
            //{
            //AuthInfo authInfo = new AuthInfo()
            //{
            //    UserId = "Yerik.com",
            //    Expires = DateTime.Now.AddDays(1)
            //};
            const string secretKey = "Yerik.com";//口令加密秘钥（应该写到配置文件中）
            byte[] key = Encoding.UTF8.GetBytes(secretKey);
            //}


            IJwtAlgorithm algorithm = new HMACSHA256Algorithm(); //加密方式
            IJsonSerializer serializer = new JsonNetSerializer(); //序列化Json
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder(); //base64加解密
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder); //JWT编码

            return encoder.Encode(payload, key); //生成令牌  
        }
    }
}