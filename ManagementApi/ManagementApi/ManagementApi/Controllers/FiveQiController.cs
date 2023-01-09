using Management.Application.Services.Impl;
using Management.Core.Helper;
using ManagementApi.Helper;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace ManagementApi.Controllers
{
    /// <summary>
    /// 五子棋得接受
    /// </summary>
    [RoutePrefix("api/FiveQi")]
    public class FiveQiController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pwt"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetFive")]
        public string GetFiveQi()
        {
            string str = "";
            int[][] result = new int[15][];
            try
            {
                ///测试redis链接
                string key = "FiveQi";
                RedisHelper redis = new RedisHelper();
                 str = redis.GetStringValue(key);
                result = JsonConvert.DeserializeObject<int[][]>(str);

            }
            catch (Exception)
            {

                LogHelper.Log("Exception");

            }
            return str;
        }
        /// <summary>
        /// 更新五子棋
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("PostFive")]
        public string PostFiveQi(int[][] value1)
        {
            //int[][] arrs1 = new int[15][];
            //int[][][] arrs = new int[3][][];
            //arrs[0] = new int[2][]
            //{
            //    new int[3] { 1, 2, 3 },
            //    new int[1] { 4 }
            //};
            //arrs[1] = new int[][]
            //{
            //    new int[] { 5, 6 },
            //    new int[] { 7, 8, 9, 10 },
            //    new int[] { 11, 12, 13 }
            //};
            //arrs[2] = new int[][]
            //{
            //    new int[] { 14, 15, 16, 17, 18 }
            //};

            string str = JsonConvert.SerializeObject(value1);
            int[][] arrys = JsonConvert.DeserializeObject<int[][]>(str);
            string result = "";
            
            try
            {
              
                string key = "FiveQi";
                string value = str;
                string time = "10000";
                RedisHelper redis = new RedisHelper();
                redis.SetStringKey(key, value, int.Parse(time));
                result = "1";

            }
            catch (Exception)
            {

                LogHelper.Log("Exception");

            }
            return result;
        }
    }
}
