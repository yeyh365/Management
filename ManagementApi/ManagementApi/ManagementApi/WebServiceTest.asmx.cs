using Management.Application.Dto;
using Management.Application.Services.Impl;
using ManagementApi.Helper;
using ManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ManagementApi
{
    /// <summary>
    /// WebServiceTest 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceTest : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {

            return "Hello World";
        }
        [WebMethod]
        public ResultModel Login(string UserName, string Password)
        {
            ResultModel resultModel = new ResultModel();//需要返回的口令信息
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
            {
                //User user = new User();
                //EFDBContext eFDBContext = new EFDBContext();
                //var asdf = (from i in eFDBContext.User  //使用LINQ查询
                //                                        //where i.User_Name =="yyh"
                //            select i).ToList();
                //List<User> result;
                //EFRepository eFRepository = new EFRepository();
                ////List<User> a = eFRepository.GetAll<User>();
                //List<User> b = eFRepository.GetAll<User>();
                //using (var db = new EFDBContext())
                //{
                //    result = (from i in db.User  //使用LINQ查询
                //                                 //where i.User_Name =="yyh"
                //              select i).ToList();
                //}
                string userName = UserName;
                string passWord = Password;
                UserService user = new UserService();
                LoginUserDto loginUserDto = user.FindLoginUser(UserName);
                if (loginUserDto.Password == Password)
                {


                    bool isAdmin = (userName == "admin") ? true : false;
                    ////模拟数据库数据，真正的数据应该从数据库读取
                    ////身份验证信息
                    try
                    {
                        string AccessToken = JWTHelper.EncodeAccessToken(UserName, isAdmin);
                        //    AuthInfo authInfo = new AuthInfo { UserName = userName, Roles = new List<string> { "admin", "commonrole" }, IsAdmin = isAdmin, ExpiryDateTime = DateTime.Now.AddHours(2) };
                        //string a = ConfigurationManager.AppSettings[AppSettingKeys.JWTSecret].ToString();
                        //string secretKey = ConfigurationManager.AppSettings[AppSettingKeys.JWTSecret].ToString();//口令加密秘钥

                        //    byte[] key = Encoding.UTF8.GetBytes(secretKey);
                        //    IJwtAlgorithm algorithm = new HMACSHA256Algorithm();//加密方式
                        //    IJsonSerializer serializer = new JsonNetSerializer();//序列化Json
                        //    IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();//base64加解密
                        //    IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);//JWT编码
                        //    var token = encoder.Encode(authInfo, key);//生成令牌
                        //                                              //口令信息
                        LogonUserDto logonUserDto = new LogonUserDto();
                        logonUserDto.UserName = UserName;
                        logonUserDto.Password = Password;


                        logonUserDto.AccessToken = AccessToken;
                        resultModel.Success = true;
                        resultModel.Data = logonUserDto;
                        resultModel.Message = "OK";
                    }
                    catch (Exception ex)
                    {
                        resultModel.Success = false;
                        resultModel.Message = ex.Message.ToString();
                    }
                }
                else
                {
                    resultModel.Success = false;
                    resultModel.Message = "用户信息为空";
                }
            }
            else
            {
                resultModel.Success = false;
                resultModel.Message = "用户信息为空";
            }
            return resultModel;
        }
    }
}
