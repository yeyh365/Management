using Management.Application.Dto;
using Management.Application.Services.Impl;
using Management.EntityFramework.Entities;
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
        [HttpGet]
        [Route("GetEmployeeLimit")]
        public ResultModel GetEmployeeLimit([FromUri]SearchEmployeeDto searchEmployeeDto)
        {
            var page = searchEmployeeDto.Page;
            var limit = searchEmployeeDto.Limit;
            UserService EmployeeInfo = new UserService();
            ResultModel resultModel = new ResultModel();//需要返回的口令信息

            try
            {
                List<EmployeeDto> list = EmployeeInfo.GetEmployeeLimitDto(searchEmployeeDto);

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
        [Route("DeleteEmployee")]
        public ResultModel DeleteEmployee([FromUri] SearchEmployeeDto searchEmployeeDto)
        {
            UserService EmployeeInfo = new UserService();
            ResultModel resultModel = new ResultModel();//需要返回的口令信息

            try
            {
                EmployeeDto list = EmployeeInfo.DeleteEmployee(searchEmployeeDto);
                if (list.Count == 1)
                {
                    resultModel.Success = true;
                    //resultModel.Data =JsonConvert.SerializeObject(list);
                    resultModel.Data = list;

                    resultModel.Message = "OK";
                }
                else
                {
                    resultModel.Success = false;
                    //resultModel.Data =JsonConvert.SerializeObject(list);
                    resultModel.Data = 0;

                    resultModel.Message = "NOT OK";
                }
                //deltest deltest = new deltest();
                //var a = deltest.DeleteEmployee(searchEmployeeDto.Id);
                //resultModel.Success = true;
                //resultModel.Data = JsonConvert.SerializeObject(list);
                //resultModel.Data = a;

                //resultModel.Message = "OK";

            }
            catch (Exception ex)
            {
                resultModel.Success = false;
                resultModel.Message = ex.Message.ToString();
            }
            return resultModel;
        }

        [HttpPost]
        [Route("AddEmployee")]
        public ResultModel AddEmployee(SearchEmployeeDto EmployeeDto)
        {
            UserService EmployeeInfo = new UserService();
            ResultModel resultModel = new ResultModel();//需要返回的口令信息

            try
            {
                EmployeeDto list = EmployeeInfo.AddEmployee(EmployeeDto);
                if (list.Count == 1)
                {
                    resultModel.Success = true;
                    resultModel.Data = list;
                    resultModel.Message = "OK";
                }
                else
                {
                    resultModel.Success = false;
                    resultModel.Data = 0;
                    resultModel.Message = "NOT OK";
                }
            }
            catch (Exception ex)
            {
                resultModel.Success = false;
                resultModel.Message = ex.Message.ToString();
            }
            return resultModel;
        }
        [HttpGet]
        [Route("ExportEmployee")]
        public HttpResponseMessage ExportEmployee()
        {
            UserService EmployeeInfo = new UserService();
            HttpResponseMessage list = EmployeeInfo.ExportEmployeeList();

            return list;
        }
    }
}
