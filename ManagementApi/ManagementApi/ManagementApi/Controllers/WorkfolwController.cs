using Management.Application.Dto;
using Management.Application.Services.Impl;
using ManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ManagementApi.Controllers
{
    /// <summary>
    /// 工作流申请
    /// </summary>
     [RoutePrefix("api/Workflow")]
    public class WorkfolwController : ApiController 
    {
        /// <summary>
        /// 获取工作流菜单
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetWorkflowMenu")]
        public ResultModel GetWorkflowMenu()
        {
            WorkflowService workflowService = new WorkflowService();
            ResultModel resultModel = new ResultModel();//需要返回的口令信息

            try
            {
                List<WorkflowDto> list = workflowService.GetWorkflowMenu();

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
        /// 获取当前登录的代办申请流程
        /// </summary>
        /// <param name="workfolwDto"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserPendingWorkflow")]
        public ResultModel GetUserPendingWorkflow([FromUri] WorkflowDto workfolwDto)
        {
            WorkflowService workflowService = new WorkflowService();
            ResultModel resultModel = new ResultModel();//需要返回的口令信息

            try
            {
                var data = workflowService.QueryPendingWorkFlow(workfolwDto);

                resultModel.Success = true;
                //resultModel.Data =JsonConvert.SerializeObject(list);
                resultModel.Data = data;

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
        /// 获取当前登录人已经办理的申请流程
        /// </summary>
        /// <param name="workfolwDto"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserEndWorkflow")]
        public ResultModel GetUserEndWorkflow([FromUri] WorkflowDto workfolwDto)
        {
            WorkflowService workflowService = new WorkflowService();
            ResultModel resultModel = new ResultModel();//需要返回的口令信息

            try
            {
                var data = workflowService.QueryEndWorkFlow(workfolwDto);

                resultModel.Success = true;
                //resultModel.Data =JsonConvert.SerializeObject(list);
                resultModel.Data = data;

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
        /// 获取当前登录的代办消息数
        /// </summary>
        /// <param name="workfolwDto"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserPendingWorkflowCount")]
        public ResultModel GetUserPendingWorkflowCount([FromUri] WorkflowDto workfolwDto)
        {
            WorkflowService workflowService = new WorkflowService();
            ResultModel resultModel = new ResultModel();//需要返回的口令信息

            try
            {
               int list = workflowService.QueryPendingWorkFlowCount(workfolwDto);

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
        /// 更新流程节点审批信息
        /// </summary>
        /// <param name="workfolwDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateWorkFlow")]
        public ResultModel UpdateWorkFlow([FromUri] WorkflowDto workfolwDto)
        {
            WorkflowService workflowService = new WorkflowService();
            ResultModel resultModel = new ResultModel();//需要返回的口令信息

            try
            {
                int list = workflowService.UpdateWorkFlow(workfolwDto);

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
        /// <param name="workflowDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddWorkflow")]
        public ResultModel AddWorkflow(WorkflowDto workflowDto)
        {
            WorkflowService workflowService = new WorkflowService();
            ResultModel resultModel = new ResultModel();//需要返回的口令信息

            try
            {
                WorkflowDto list = workflowService.AddWorkflow(workflowDto);
                if (list!=null)
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
        // GET: api/WorkFolw
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/WorkFolw/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/WorkFolw
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/WorkFolw/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/WorkFolw/5
        public void Delete(int id)
        {
        }
    }
}
