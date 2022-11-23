using ManagementApi.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ManagementApi.Controllers
{

        [RoutePrefix("api/UserInfo")]
        [ApiAuthorize]
        public class UserInfoController : ApiController
        {
            /// <summary>
            /// 获取用户信息
            /// </summary>
            /// <returns></returns>
            [HttpGet]
            [Route("GetUserInfo")]
            public string GetUserInfo()
            {
                var userInfo = new
                {
                    UserName = "test",
                    Tel = "123456789",
                    Address = "testddd"
                };
                return JsonConvert.SerializeObject(userInfo);
            }
        }

}
