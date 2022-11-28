using JWT.Algorithms;
using JWT.Serializers;
using JWT;
using ManagementApi.Filters;
using ManagementApi.Helper;
using ManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Management.Domain.Entityes;
using Management.EntityFramework.Entities;
using Management.EntityFramework.Repositories;
using System.Data.Entity;
using Management.Application.Services.Impl;
using Management.Application.Dto;

namespace ManagementApi.Controllers
{
    [RoutePrefix("api/Logins")]
    public class LoginController : ApiController
    {
        [HttpGet]
        [Route("Login")]
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
                   string AccessToken =  JWTHelper.EncodeAccessToken(UserName, isAdmin);
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
