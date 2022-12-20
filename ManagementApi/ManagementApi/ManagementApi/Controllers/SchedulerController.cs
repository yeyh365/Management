using Management.Application.Dto;
using Management.Application.Services.Impl;
using Management.Core.Helper;
using ManagementApi.Helper;
using ManagementApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace ManagementApi.Controllers
{
    [RoutePrefix("api/FluentScheduler")]
    public class SchedulerController : ApiController
    {
        [HttpGet]
        [Route("SchedulerStartUp")]
        public ResultModel StartUp()
        {
            ResultModel resultModel = new ResultModel();//需要返回的口令信息
            MyFluentScheduler.StartUp();
                resultModel.Success = true;
                resultModel.Data = 1;
                resultModel.Message = "OK";
                return resultModel;
        }

        [HttpGet]
        [Route("ScheduleStop")]
        public ResultModel Stop()
        {
            ResultModel resultModel = new ResultModel();//需要返回的口令信息
            MyFluentScheduler.Stop();
            resultModel.Success = true;
            resultModel.Data = 1;
            resultModel.Message = "OK";
            return resultModel;
        }
    }

}
