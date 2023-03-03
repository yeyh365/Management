using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagementApi.Models
{
    public class ResultModel
    {

            /// <summary>
            /// 是否成功
            /// </summary>
            public bool Success { get; set; }
            /// <summary>
            /// 令牌
            /// </summary>
            public object Data { get; set; }
            /// <summary>
            /// 错误信息
            /// </summary>
            public string Message { get; set; }
        
    }
    public class ResultModel2
    {
        public object Count { get; set; }
        public object List { get; set; }

    }
}