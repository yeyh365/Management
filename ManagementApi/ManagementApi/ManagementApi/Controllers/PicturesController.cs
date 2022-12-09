using ABC;
using Management.Application.Services.Impl;
using Management.Core.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ManagementApi.Controllers
{
    /// <summary>
    /// 图片上传
    /// </summary>
    [RoutePrefix("api/Pictures")]
    public class PicturesController : ApiController
    {
        /// <summary>
        /// 获取图片向前端返回一个字节流
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("UserFace1")]
        public HttpResponseMessage GetPicturesSteam()
        {
            LogHelper.Log("12344567");
            HttpResponseMessage resp = new HttpResponseMessage();
            
            try
            {
                string str = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
                string str1 = System.Environment.CurrentDirectory;
                //string pictures = @"Files\Pictures\abcd.png";
                UserService userService = new UserService();
                string picturesUrl = userService.FindLoginUser("admin").Picture;
                HttpResponseMessage result = null;
                string path = Path.Combine(str, picturesUrl);
                //var imgByte = File.ReadAllBytes(path);    //byte字节流
                var imgStream = new MemoryStream(File.ReadAllBytes(path));   //内存流stream
                resp = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    //Content = new ByteArrayContent(imgByte)
                    Content = new StreamContent(imgStream)
                };
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("Image/png");
            }
            catch (Exception)
            {

                LogHelper.Log("Exception"); 
            }
            return resp;
        }
        /// <summary>
        /// 获取图片向前端返回一个地址
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("UserFace")]
        public string GetPictures()
        {
            string str = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
            string str1 = System.Environment.CurrentDirectory;
            //string pictures = @"Files\Pictures\abcd.png";
            UserService userService = new UserService();
            string picturesUrl = userService.FindLoginUser("admin").Picture;
            //HttpResponseMessage result = null;
            //string path = Path.Combine(str, picturesUrl);
            ////var imgByte = File.ReadAllBytes(path);    //byte字节流
            //var imgStream = new MemoryStream(File.ReadAllBytes(path));   //内存流stream
            //var resp = new HttpResponseMessage(HttpStatusCode.OK)
            //{
            //    //Content = new ByteArrayContent(imgByte)
            //    Content = new StreamContent(imgStream)
            //};
            //resp.Content.Headers.ContentType = new MediaTypeHeaderValue("Image/png");


            return picturesUrl;
        }
        //public  IHttpActionResult PostPictures()
        //{
        //    return new IHttpActionResult(HttpStatusCode.OK);
        //}
    }
}
