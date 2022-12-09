using ManagementApi.Helper;
using ManagementApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ManagementApi.Controllers
{
    [RoutePrefix("api/Files")]
    public class FilesController : ApiController
    {
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("ImgUpload")]
        public ResultModel ImgUpload()
        {
            var result = new ResultModel();
            //添加操作日志
           
            try
            {
                var FileInfo = HttpContext.Current.Request.Files[0];
                string FileName = Path.GetFileName(FileInfo.FileName);
                string Name = Path.GetFileNameWithoutExtension(FileInfo.FileName);
                string FileExt = Path.GetExtension(FileInfo.FileName);
                string NewName = Guid.NewGuid().ToString();

                //文件大小，单位字节
                int fileContentLength = FileInfo.ContentLength;
                //二进制数组
                byte[] fileBytes = new byte[fileContentLength];
                //创建Stream对象，并指向上传文件
                Stream inputStream = FileInfo.InputStream;
                //从当前流中读取字节，读入字节数组中
                inputStream.Read(fileBytes, 0, fileContentLength);

                // 文件保存目录路径
                string saveTempPath = "/Files/Pictures/";
                var dirTempPath = HttpContext.Current.Server.MapPath("~" + saveTempPath);
                DirectoryInfo di = new DirectoryInfo(dirTempPath);
                if (!di.Exists) { di.Create(); }
                string FilePath = dirTempPath + "\\" + NewName + FileExt;
        
                using (var fileStream = File.Create(FilePath))
                {
                    inputStream.Seek(0, SeekOrigin.Begin);
                    inputStream.CopyTo(fileStream);
                }

                //Attachment hp = new Attachment();
                //hp.FileName = Name;
                //hp.FilePath = TheURL + saveTempPath + NewName + FileExt;
                //hp.index = index;
                ////绑定数据对象
                //{
                //    vprReturn.RepairImage = TheURL + saveTempPath + NewName + FileExt;
                //}
                string ServerUrl =saveTempPath+ NewName + FileExt;
                result.Data = ServerUrl;
                result.Message = "上传成功!";
                return result;
            }
            catch (Exception ee)
            {
               
                result.Message = ee.Message;
                return result;
            }
        }
    }
}
