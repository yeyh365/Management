using Management.Application.Dto;
using Management.Domain.Entityes;
using Management.EntityFramework.Repositories;
using ManagementApi.Models;
using NPOI.POIFS.FileSystem;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Application.Services.Impl
{
    /// <summary>
    /// 工作流服务
    /// </summary>
    public class WorkflowService : IApplyService
    {
        private EFRepository _eFRepository;
        public WorkflowService()
        {
            _eFRepository = new EFRepository();
        }
        /// <summary>
        /// 查询工作流菜单
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<List<WorkflowDto>>  GetWorkflowMenu()
        {

            List<WorkflowDto> list = new List<WorkflowDto>();
            var employee = await  this._eFRepository.GetAllAsync<WorkflowConfiguration>();
            foreach (var item in employee)
            {
                WorkflowDto workflowDto = new WorkflowDto();
                workflowDto.Id = item.WorkflowCode;
                workflowDto.ApplyTitle = item.WorkflowName;
                list.Add(workflowDto);
            }

            return list;
        }
        /// <summary>
        /// 增加一个工作流程
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public WorkflowDto AddWorkflow(WorkflowDto workflowDto)
        {

            //List<Employee> employeesNow = this._eFRepository.GetAll<Employee>().OrderBy(a => a.Id).ToList();
            if (!string.IsNullOrEmpty(workflowDto.Account) && !string.IsNullOrEmpty(workflowDto.ApplyTitle) && !string.IsNullOrEmpty(workflowDto.ApplyBoby))
            {
                ApplyProcess applyProcess = new ApplyProcess();
                var employee = this._eFRepository.GetAll<Employee>();
                var departmentName = from epm in employee.Where(e => e.EmployeeId == workflowDto.Account)
                                     join dep in this._eFRepository.GetAll<Departemnt>() on epm.DepartmentNumber equals dep.Id
                                     select new ApplyProcessDto
                                     {
                                         ApproverDepartmentName = dep.EmployeeId,
                                     };

                applyProcess.UserId = workflowDto.UserId;
                applyProcess.Account = workflowDto.Account;
                applyProcess.ApplyTitle = workflowDto.ApplyTitle;
                applyProcess.ApplyBoby = workflowDto.ApplyBoby;
                //applyProcess.ApplyDate = workflowDto.ApplyDate;
                applyProcess.ApproverDepartmentId = departmentName.FirstOrDefault().ApproverDepartmentName;
                applyProcess.GeneralManagerId = "admin";
                bool result = this._eFRepository.Add<ApplyProcess>(applyProcess);
                //workflowDto.Count = Convert.ToInt32(result);
            }
            else
            {
                //workflowDto.Count = 0;
            }

            return workflowDto;
        }
        /// <summary>
        /// 查询登陆人待办流程
        /// </summary>
        /// <param name="applyProcessDto"></param>
        /// <returns></returns>
        public ResultModel2 QueryPendingWorkFlow(WorkflowDto workflowDto)
        {
            List<WorkflowDto> list = new List<WorkflowDto>();
            ResultModel2 resultModel2 = new ResultModel2();
            //List<Employee> employeesNow = this._eFRepository.GetAll<Employee>().OrderBy(a => a.Id).ToList();
            if (!string.IsNullOrEmpty(workflowDto.Account))
            {
                ApplyProcess applyProcess = new ApplyProcess();
                var workflowOrder = this._eFRepository.GetAll<WorkflowOrder>();
                var workflowRecords = this._eFRepository.GetAll<WorkflowRecords>();
                var workflowRecordsPending = from w in workflowRecords
                                             where w.AuditStatus == 0 && w.IsAudit == 0
                                             group w by w.DocumentCode
                           into gw
                                             select new
                                             {
                                                 FlowSerialnunber = gw.Min(g => g.FlowSerialnunber),
                                                 DocumentCode = gw.Key
                                             };
                var pending = from p in workflowRecordsPending
                              join o in workflowOrder on p.DocumentCode equals o.DocumentCode
                              join r in workflowRecords.Where(w => w.AssigneeId == workflowDto.Account) on new { p.DocumentCode, p.FlowSerialnunber } equals new { r.DocumentCode, r.FlowSerialnunber }
                              select new WorkflowDto
                              {
                                  Account = "",
                                  ApplyTitle = r.WorkflowName,
                                  ApplyBoby = o.Mark,
                                  WorkflowId = o.WorkflowId,
                                  DocumentCode = p.DocumentCode,
                                  DocumentTime = o.DocumentTime,
                                  Status = o.Status,
                                  CreateUser = o.CreateUser,
                                  IsDelete = o.IsDelete,
                                  AssigneeId = r.AssigneeId,
                                  HandleDate = r.HandleDate,
                                  Remarks = r.Remarks,
                                  AuditStatus = r.AuditStatus,
                                  IsAudit = r.IsAudit,
                                  FlowSerialnunber = r.FlowSerialnunber
                              };
                var count = pending.Count();
                //if (count > workflowdto.limit)
                //{

                //    pending = pending.skip((workflowdto.page - 1) * workflowdto.limit).take(workflowdto.limit);

                //}
                //var departmentname = from epm in employee.where(e => e.employeeid == workflowdto.account)
                //                     join dep in this._efrepository.getall<departemnt>() on epm.departmentnumber equals dep.id 
                //                     select new applyprocessdto
                //                     {

                //                         approverdepartmentname = dep.employeeid,
                //                     };
                if (workflowDto.Limit > 0 && workflowDto.Limit > 0)
                {
                    list = pending.Skip((workflowDto.Page - 1) * workflowDto.Limit).Take(workflowDto.Limit).ToList();
                }


                resultModel2.List = list;
                resultModel2.Count = Convert.ToInt32(count);
            }
            else
            {
                //workflowDto.Count = 0;
            }

            return resultModel2;
        }
        /// <summary>
        /// 查询登陆已经办理的流程
        /// </summary>
        /// <param name="applyProcessDto"></param>
        /// <returns></returns>
        public ResultModel2 QueryEndWorkFlow(WorkflowDto workflowDto)
        {
            List<WorkflowDto> list = new List<WorkflowDto>();
            ResultModel2 resultModel2 = new ResultModel2();
            //List<Employee> employeesNow = this._eFRepository.GetAll<Employee>().OrderBy(a => a.Id).ToList();
            if (!string.IsNullOrEmpty(workflowDto.Account))
            {
                ApplyProcess applyProcess = new ApplyProcess();
                var workflowOrder = this._eFRepository.GetAll<WorkflowOrder>();
                var workflowRecords = this._eFRepository.GetAll<WorkflowRecords>();
                var pending = from
                               o in workflowOrder
                              join r in workflowRecords.Where(w => w.AssigneeId == workflowDto.Account).Where(w => w.IsAudit == 1) on o.DocumentCode equals r.DocumentCode
                              select new WorkflowDto
                              {
                                  Account = "",
                                  ApplyTitle = r.WorkflowName,
                                  ApplyBoby = o.Mark,
                                  WorkflowId = o.WorkflowId,
                                  DocumentCode = o.DocumentCode,
                                  DocumentTime = o.DocumentTime,
                                  Status = o.Status,
                                  CreateUser = o.CreateUser,
                                  IsDelete = o.IsDelete,
                                  AssigneeId = r.AssigneeId,
                                  HandleDate = r.HandleDate,
                                  Remarks = r.Remarks,
                                  AuditStatus = r.AuditStatus,
                                  IsAudit = r.IsAudit,
                                  FlowSerialnunber = r.FlowSerialnunber
                              };
                var count = pending.Count();
                //if (count > workflowdto.limit)
                //{

                //    pending = pending.skip((workflowdto.page - 1) * workflowdto.limit).take(workflowdto.limit);

                //}
                //var departmentname = from epm in employee.where(e => e.employeeid == workflowdto.account)
                //                     join dep in this._efrepository.getall<departemnt>() on epm.departmentnumber equals dep.id 
                //                     select new applyprocessdto
                //                     {

                //                         approverdepartmentname = dep.employeeid,
                //                     };
                if (workflowDto.Limit > 0 && workflowDto.Limit > 0)
                {
                    list = pending.OrderBy(w => w.UserId).Skip((workflowDto.Page - 1) * workflowDto.Limit).Take(workflowDto.Limit).ToList();
                }
                else
                {
                    list = pending.ToList();
                }


                resultModel2.List = list;
                resultModel2.Count = Convert.ToInt32(count);
            }
            else
            {
                //workflowDto.Count = 0;
            }

            return resultModel2;
        }
        /// <summary>
        /// 更新流程节点
        /// </summary>
        /// <param name="applyProcessDto"></param>
        /// <returns></returns>
        public int UpdateWorkFlow(WorkflowDto workflowDto)
        {
            List<WorkflowDto> list = new List<WorkflowDto>();
            //List<Employee> employeesNow = this._eFRepository.GetAll<Employee>().OrderBy(a => a.Id).ToList();
            if (!string.IsNullOrEmpty(workflowDto.Account))
            {
                WorkflowOrder workflowOrder = this._eFRepository.GetAll<WorkflowOrder>().Where(w => w.DocumentCode == workflowDto.DocumentCode).FirstOrDefault();
                ApplyProcess applyProcess = new ApplyProcess();
                WorkflowRecords process = this._eFRepository.GetAll<WorkflowRecords>().Where(w => w.DocumentCode == workflowDto.DocumentCode).Where(w => w.AssigneeId == workflowDto.Account).Where(w => w.FlowSerialnunber == workflowDto.FlowSerialnunber).FirstOrDefault();
                if (workflowDto.AuditStatus == 2)
                {
                    workflowOrder.Status = "3";
                    bool result1 = this._eFRepository.Update<WorkflowOrder>(workflowOrder);
                }
                else
                {
                    if (LastOneProcess(workflowDto.DocumentCode, workflowDto.FlowSerialnunber))
                    {
                       workflowOrder.Status = "3";
                       bool result2 = this._eFRepository.Update<WorkflowOrder>(workflowOrder);
                    }
                }
                process.AuditStatus = workflowDto.AuditStatus;
                process.IsAudit = 1;

                bool result3 = this._eFRepository.Update<WorkflowRecords>(process);

                workflowDto.Count = Convert.ToInt32(result3);

            }
            else
            {
                //workflowDto.Count = 0;
            }

            return workflowDto.Count;
        }
        /// <summary>
        /// 查询登陆人待办消息数
        /// </summary>
        /// <param name="applyProcessDto"></param>
        /// <returns></returns>
        public async  Task<int>  QueryPendingWorkFlowCount(WorkflowDto workflowDto)
        {
            int count = 0;
            //List<Employee> employeesNow = this._eFRepository.GetAll<Employee>().OrderBy(a => a.Id).ToList();
            if (!string.IsNullOrEmpty(workflowDto.Account))
            {
                ApplyProcess applyProcess = new ApplyProcess();
                var workflowOrder = await this._eFRepository.GetAllAsync<WorkflowOrder>();
                var workflowRecords = await this._eFRepository.GetAllAsync<WorkflowRecords>();
                var pending = from o in workflowOrder
                              join r in workflowRecords.Where(w => w.AuditStatus == 0).Where(w => w.IsAudit == 0).Where(w => w.AssigneeId == workflowDto.Account) on o.DocumentCode equals r.DocumentCode
                              select new WorkflowDto
                              {
                                  Account = "",
                                  ApplyTitle = r.WorkflowName,
                                  ApplyBoby = o.WorkflowId,
                                  WorkflowId = o.WorkflowId,
                                  DocumentCode = o.DocumentCode,
                                  DocumentTime = o.DocumentTime,
                                  Status = o.Status,
                                  CreateUser = o.CreateUser,
                                  IsDelete = o.IsDelete,
                                  Mark = o.Mark,
                                  WorkflowName = r.WorkflowName,
                                  AssigneeId = r.AssigneeId,
                                  HandleDate = r.HandleDate,
                                  Remarks = r.Remarks,
                                  AuditStatus = r.AuditStatus,
                                  IsAudit = r.IsAudit,
                                  FlowSerialnunber = r.FlowSerialnunber
                              };
                count = pending.Count();
                //if (count > workflowDto.Limit)
                //{

                //    pending = pending.Skip((workflowDto.Page - 1) * workflowDto.Limit).Take(workflowDto.Limit);

                //}
                //var departmentName = from epm in employee.Where(e => e.EmployeeId == workflowDto.Account)
                //                     join dep in this._eFRepository.GetAll<Departemnt>() on epm.DepartmentNumber equals dep.Id 
                //                     select new ApplyProcessDto
                //                     {

                //                         ApproverDepartmentName = dep.EmployeeId,
                //                     };
                //list = pending.ToList();
                workflowDto.Count = Convert.ToInt32(count);
            }
            else
            {
                //workflowDto.Count = 0;
            }

            return count;
        }
        /// <summary>
        /// 增加一个申请流程
        /// </summary>
        /// <param name="workflowMenuDto"></param>
        /// <returns></returns>
        public async Task<ResultModel> AddPendingWorkFlow(WorkflowMenuDto workflowMenuDto)
        {
            ResultModel resultModel = new ResultModel();
            int count = 0;
            //List<Employee> employeesNow = this._eFRepository.GetAll<Employee>().OrderBy(a => a.Id).ToList();
            if (!string.IsNullOrEmpty(workflowMenuDto.Account))
            {
                ApplyProcess applyProcess = new ApplyProcess();
                var workflowOrder = await this._eFRepository.GetAllAsync<WorkflowOrder>();
                var workflowRecords = await this._eFRepository.GetAllAsync<WorkflowRecords>();
                var pending = from o in workflowOrder
                              join r in workflowRecords.Where(w => w.AuditStatus == 0).Where(w => w.IsAudit == 0).Where(w => w.AssigneeId == workflowMenuDto.Account) on o.DocumentCode equals r.DocumentCode
                              select new WorkflowDto
                              {
                                  Account = "",
                                  ApplyTitle = r.WorkflowName,
                                  ApplyBoby = o.WorkflowId,
                                  WorkflowId = o.WorkflowId,
                                  DocumentCode = o.DocumentCode,
                                  DocumentTime = o.DocumentTime,
                                  Status = o.Status,
                                  CreateUser = o.CreateUser,
                                  IsDelete = o.IsDelete,
                                  Mark = o.Mark,
                                  WorkflowName = r.WorkflowName,
                                  AssigneeId = r.AssigneeId,
                                  HandleDate = r.HandleDate,
                                  Remarks = r.Remarks,
                                  AuditStatus = r.AuditStatus,
                                  IsAudit = r.IsAudit,
                                  FlowSerialnunber = r.FlowSerialnunber
                              };
                count = pending.Count();
                //if (count > workflowDto.Limit)
                //{

                //    pending = pending.Skip((workflowDto.Page - 1) * workflowDto.Limit).Take(workflowDto.Limit);

                //}
                //var departmentName = from epm in employee.Where(e => e.EmployeeId == workflowDto.Account)
                //                     join dep in this._eFRepository.GetAll<Departemnt>() on epm.DepartmentNumber equals dep.Id 
                //                     select new ApplyProcessDto
                //                     {

                //                         ApproverDepartmentName = dep.EmployeeId,
                //                     };
                //list = pending.ToList();
                workflowMenuDto.Count = Convert.ToInt32(count);
            }
            else
            {
                //workflowDto.Count = 0;
            }

            return resultModel;
        }
        /// <summary>
        /// 判断当前节点是否未最后一个节点
        /// </summary>
        /// <returns></returns>
        public bool LastOneProcess(string documentCode, string Size)
        {
            bool result = false;
            var workflowRecords = this._eFRepository.GetAll<WorkflowRecords>();
            var workflowRecordsPending = from w in workflowRecords
                                         where w.DocumentCode == documentCode /*w.AuditStatus == 0 && w.IsAudit == 0 &&*/
                                         group w by w.DocumentCode
                       into gw
                                         select new
                                         {
                                             FlowSerialnunber = gw.Max(g => g.FlowSerialnunber),
                                             DocumentCode = gw.Key
                                         };
            if (workflowRecordsPending.FirstOrDefault().FlowSerialnunber == Size)
            {
                result = true;
            }
            return result;
        }

    }
}
