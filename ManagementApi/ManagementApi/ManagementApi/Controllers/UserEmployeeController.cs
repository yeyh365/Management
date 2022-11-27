using Management.Application.Dto;
using Management.Application.Services.Impl;
using ManagementApi.Filters;
using ManagementApi.Helper;
using ManagementApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ManagementApi.Controllers
{
    [RoutePrefix("api/UserEmployee")]
    [ApiAuthorize]
    public class UserEmployeeController : ApiController
    {

        [HttpGet]
        [Route("GetEmployeeInfo")]
        public ResultModel GetEmployeeInfo(string name = null)
        {
            UserService EmployeeInfo = new UserService();
            ResultModel resultModel = new ResultModel();//需要返回的口令信息

            try
            {
                List<EmployeeDto> list = EmployeeInfo.GetAllEmployeeDto();

                resultModel.Success = true;
                //resultModel.Data =JsonConvert.SerializeObject(list);
                resultModel.Data = list;

                resultModel.Message = "OK";
            }
            catch (Exception ex)
            {
                resultModel.Success = false;
                resultModel.Message = ex.Message.ToString();
            }
            return resultModel;
        }
        [HttpGet]
        [Route("GetUserInfo")]
        public ResultModel GetUserInfo(string name = null)
        {
            UserService user = new UserService();
            ResultModel resultModel = new ResultModel();//需要返回的口令信息

            try
            {
                List<UserDto> list = user.GetAllUser();

                resultModel.Success = true;
                //resultModel.Data =JsonConvert.SerializeObject(list);
                resultModel.Data = list;

                resultModel.Message = "OK";
            }
            catch (Exception ex)
            {
                resultModel.Success = false;
                resultModel.Message = ex.Message.ToString();
            }
            return resultModel;
        }
    }
}
