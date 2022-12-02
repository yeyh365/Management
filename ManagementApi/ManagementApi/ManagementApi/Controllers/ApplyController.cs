using Management.Application.Dto;
using Management.Application.Services.Impl;
using ManagementApi.Filters;
using ManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ManagementApi.Controllers
{
    [RoutePrefix("api/Apply")]
    [ApiAuthorize]
    public class ApplyController : ApiController
    {
        /// <summary>
        /// 获取当前登录的的分页的申请流程
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserApply")]
        public ResultModel GetUserApply([FromUri] ApplyProcessDto applyProcessDto)
        {
            ApplyService applyService = new ApplyService();
            ResultModel resultModel = new ResultModel();//需要返回的口令信息

            try
            {
                List<ApplyProcessDto> list = applyService.GetAllApply(applyProcessDto);

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
        /// <summary>
        /// 增加一条流程
        /// </summary>
        /// <param name="EmployeeDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddApply")]
        public ResultModel AddApply(ApplyProcessDto applyProcessDto)
        {
            ApplyService applyService = new ApplyService();
            ResultModel resultModel = new ResultModel();//需要返回的口令信息

            try
            {
                ApplyProcessDto list = applyService.AddApply(applyProcessDto);
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
        /// <summary>
        /// 删除一个流程 假删除
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DeletApply")]
        public ResultModel DeletApply([FromUri] ApplyProcessDto applyProcessDto)
        {
            ApplyService applyInfo = new ApplyService();
            ResultModel resultModel = new ResultModel();//需要返回的口令信息

            try
            {
                ApplyProcessDto list = applyInfo.DeleteApply(applyProcessDto);
                if (list.Count >= 1)
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
    }
}
