using Management.Application.Dto;
using Management.Domain.Entityes;
using Management.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Application.Services.Impl
{
    public class ApplyService :IApplyService
    {
        private EFRepository _eFRepository;
        public ApplyService()
        {
            _eFRepository=new EFRepository();
        }
        /// <summary>
        /// 根据用户账户信息分页查询所有流程信息
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<ApplyProcessDto> GetAllApply(ApplyProcessDto applySearch)
        {
            List<ApplyProcessDto> list = new List<ApplyProcessDto>();
            List<ApplyProcess> applyProcess;
            if (!string.IsNullOrEmpty(applySearch.Account) && applySearch.Account == "admin")
            {
                applyProcess = this._eFRepository.GetAll<ApplyProcess>().Where(a => a.IsDelete != true).ToList();
            }
            else
            {
                applyProcess = this._eFRepository.GetAll<ApplyProcess>().Where(a => a.IsDelete != true && a.Account == applySearch.Account).ToList();
            }
            int count = applyProcess.Count;
            foreach (var apply in applyProcess)
            {
                ApplyProcessDto applyProcessDto = new ApplyProcessDto();
                applyProcessDto.Id = apply.Id;
                applyProcessDto.UserId = apply.UserId;
                applyProcessDto.Account = apply.Account;
                applyProcessDto.ApplyTitle = apply.ApplyTitle;
                applyProcessDto.ApplyBoby = apply.ApplyBoby;
                applyProcessDto.ApplyDate = apply.ApplyDate;
                applyProcessDto.ApproverDepartmentId = apply.ApproverDepartmentId;
                applyProcessDto.ApproverrDepartmenResult = apply.ApproverrDepartmenResult;
                applyProcessDto.ApproverDepartmentMeg = apply.ApproverDepartmentMeg;
                applyProcessDto.ApproverrDepartmentDate = apply.ApproverrDepartmentDate;
                applyProcessDto.GeneralManagerId = apply.GeneralManagerId;
                applyProcessDto.GeneralManagerResult = apply.GeneralManagerResult;
                applyProcessDto.GeneralManagerMeg = apply.GeneralManagerMeg;
                applyProcessDto.GeneralManagerDate = apply.GeneralManagerDate;
                applyProcessDto.ExpiredDate = apply.ExpiredDate;
                applyProcessDto.ApplyState = apply.ApplyState;
                applyProcessDto.ApplyResult = apply.ApplyResult;
                applyProcessDto.ApplyResult = apply.ApplyResult;
                applyProcessDto.Count = count;

                list.Add(applyProcessDto);
            }
            var countList = list.Count();
            if (countList > applySearch.Limit)
            {

                list = list.Skip((applySearch.Page - 1) * applySearch.Limit).Take(applySearch.Limit).ToList();

            }
            return list;
        }
        /// <summary>
        /// 获取属于自己审批的流程
        /// </summary>
        /// <param name="applySearch"></param>
        /// <returns></returns>
        public List<ApplyProcessDto> GetApprovalApply(ApplyProcessDto applySearch)
        {
            List<ApplyProcessDto> list = new List<ApplyProcessDto>();
            List<ApplyProcess> applyProcess;
            if (!string.IsNullOrEmpty(applySearch.Account)&&applySearch.Account=="admin")
            {
                applyProcess = this._eFRepository.GetAll<ApplyProcess>().Where(a => a.IsDelete != true).ToList();
            }
            else
            {
                applyProcess = this._eFRepository.GetAll<ApplyProcess>().Where(a => a.IsDelete != true && a.ApproverDepartmentId == applySearch.Account).ToList();
            }
           
            int count = applyProcess.Count;
            foreach (var apply in applyProcess)
            {
                ApplyProcessDto applyProcessDto = new ApplyProcessDto();
                applyProcessDto.Id = apply.Id;
                applyProcessDto.UserId = apply.UserId;
                applyProcessDto.Account = apply.Account;
                applyProcessDto.ApplyTitle = apply.ApplyTitle;
                applyProcessDto.ApplyBoby = apply.ApplyBoby;
                applyProcessDto.ApplyDate = apply.ApplyDate;
                applyProcessDto.ApproverDepartmentId = apply.ApproverDepartmentId;
                applyProcessDto.ApproverrDepartmenResult = apply.ApproverrDepartmenResult;
                applyProcessDto.ApproverDepartmentMeg = apply.ApproverDepartmentMeg;
                applyProcessDto.ApproverrDepartmentDate = apply.ApproverrDepartmentDate;
                applyProcessDto.GeneralManagerId = apply.GeneralManagerId;
                applyProcessDto.GeneralManagerResult = apply.GeneralManagerResult;
                applyProcessDto.GeneralManagerMeg = apply.GeneralManagerMeg;
                applyProcessDto.GeneralManagerDate = apply.GeneralManagerDate;
                applyProcessDto.ExpiredDate = apply.ExpiredDate;
                applyProcessDto.ApplyState = apply.ApplyState;
                applyProcessDto.ApplyResult = apply.ApplyResult;
                applyProcessDto.ApplyResult = apply.ApplyResult;
                applyProcessDto.Count = count;

                list.Add(applyProcessDto);
            }
            var countList = list.Count();
            if (countList > applySearch.Limit)
            {

                list = list.Skip((applySearch.Page - 1) * applySearch.Limit).Take(applySearch.Limit).ToList();

            }
            return list;
        }
        /// <summary>
        /// 增加一个申请流程
        /// </summary>
        /// <param name="applyProcessDto"></param>
        /// <returns></returns>
        public ApplyProcessDto AddApply(ApplyProcessDto applyProcessDto)
        {
            
            //List<Employee> employeesNow = this._eFRepository.GetAll<Employee>().OrderBy(a => a.Id).ToList();
            if (!string.IsNullOrEmpty(applyProcessDto.Account)&&!string.IsNullOrEmpty(applyProcessDto.ApplyTitle)&&!string.IsNullOrEmpty(applyProcessDto.ApplyBoby))
            {
                ApplyProcess applyProcess = new ApplyProcess();
                var employee = this._eFRepository.GetAll<Employee>();
                var departmentName = from epm in employee.Where(e => e.EmployeeId == applyProcessDto.Account)
                                     join dep in this._eFRepository.GetAll<Departemnt>() on epm.DepartmentNumber equals dep.Id
                                     select new ApplyProcessDto
                                     {

                                        ApproverDepartmentName = dep.EmployeeId,
                                     };
                
                applyProcess.UserId = applyProcessDto.UserId;
                applyProcess.Account = applyProcessDto.Account;
                applyProcess.ApplyTitle = applyProcessDto.ApplyTitle;
                applyProcess.ApplyBoby = applyProcessDto.ApplyBoby;
                applyProcess.ApplyDate = applyProcessDto.ApplyDate;
                applyProcess.ApproverDepartmentId = departmentName.FirstOrDefault().ApproverDepartmentName;
                applyProcess.GeneralManagerId = "admin";
                bool result = this._eFRepository.Add<ApplyProcess>(applyProcess);
                applyProcessDto.Count = Convert.ToInt32(result);
            }
            else
            {
                applyProcessDto.Count = 0;
            }

            return applyProcessDto;
        }
        /// <summary>
        /// 删除一个申请流程 假删除
        /// </summary>
        /// <param name="applyProcessDto"></param>
        /// <returns></returns>
        public ApplyProcessDto DeleteApply(ApplyProcessDto applyProcessDto)
        {
            ApplyProcessDto applyProcessDto1 = new ApplyProcessDto();
            ApplyProcess delapplyProcess = this._eFRepository.GetAll<ApplyProcess>().FirstOrDefault(a => a.Id == applyProcessDto.Id);
            if (delapplyProcess != null)
            {
                delapplyProcess.ApplyState = false;
                delapplyProcess.IsDelete = true;
                delapplyProcess.ApplyResult = "已取消";
                bool result = this._eFRepository.Update<ApplyProcess>(delapplyProcess);
                applyProcessDto1.Count = Convert.ToInt32(result);
            }
            else
            {
                applyProcessDto1.Count = 0;
            }

            return applyProcessDto1;
            //UserDto userDto = new UserDto();
            ////List<Employee> employeesNow = this._eFRepository.GetAll<Employee>().OrderBy(a => a.Id).ToList();
            //User delEmployee = this._eFRepository.GetAll<User>().FirstOrDefault(e => e.Id == DelUser.Id);
            //if (delEmployee != null)
            //{
            //    delEmployee.IsDeleted = true;
            //    bool result = this._eFRepository.Update<User>(delEmployee);
            //    userDto.Count = Convert.ToInt32(result);
            //}
            //else
            //{
            //    userDto.Count = 0;
            //}

            //return userDto;
        }
        /// <summary>
        /// 取消一个申请流程
        /// </summary>
        /// <param name="applyProcessDto"></param>
        /// <returns></returns>
        public ApplyProcessDto RejectApply(ApplyProcessDto applyProcessDto)
        {
            ApplyProcessDto applyProcessDto1 = new ApplyProcessDto();
            ApplyProcess delapplyProcess = this._eFRepository.GetAll<ApplyProcess>().FirstOrDefault(a => a.Id == applyProcessDto.Id);
            if (delapplyProcess != null)
            {
                delapplyProcess.ApplyState = false;
                delapplyProcess.IsDelete = false;
                delapplyProcess.ApplyResult = "已取消";
                bool result = this._eFRepository.Update<ApplyProcess>(delapplyProcess);
                applyProcessDto1.Count = Convert.ToInt32(result);
            }
            else
            {
                applyProcessDto1.Count = 0;
            }

            return applyProcessDto1;
            //UserDto userDto = new UserDto();
            ////List<Employee> employeesNow = this._eFRepository.GetAll<Employee>().OrderBy(a => a.Id).ToList();
            //User delEmployee = this._eFRepository.GetAll<User>().FirstOrDefault(e => e.Id == DelUser.Id);
            //if (delEmployee != null)
            //{
            //    delEmployee.IsDeleted = true;
            //    bool result = this._eFRepository.Update<User>(delEmployee);
            //    userDto.Count = Convert.ToInt32(result);
            //}
            //else
            //{
            //    userDto.Count = 0;
            //}

            //return userDto;
        }
        /// <summary>
        /// 审批项目经理流程
        /// </summary>
        /// <param name="applyProcessDto"></param>
        /// <returns></returns>
        public ApplyProcessDto ProcessApply(ApplyProcessDto applyProcessDto)
        {
            ApplyProcessDto applyProcessDto1 = new ApplyProcessDto();
            ApplyProcess UpDateapplyProcess = this._eFRepository.GetAll<ApplyProcess>().FirstOrDefault(a => a.Id == applyProcessDto.Id);
            if (UpDateapplyProcess != null)
            {
                if (applyProcessDto.ApproverrDepartmenResult!=null)
                {
                    UpDateapplyProcess.ApproverrDepartmenResult = applyProcessDto.ApproverrDepartmenResult;
                    if (string.IsNullOrEmpty(applyProcessDto.ApproverDepartmentMeg))
                    {
                        UpDateapplyProcess.ApproverDepartmentMeg = applyProcessDto.ApproverDepartmentMeg;
                    }
                }
                if (applyProcessDto.GeneralManagerResult!=null)
                {
                    UpDateapplyProcess.GeneralManagerResult = applyProcessDto.GeneralManagerResult;
                    UpDateapplyProcess.ApplyState = false;

                    if (string.IsNullOrEmpty(applyProcessDto.GeneralManagerMeg))
                {
                    UpDateapplyProcess.GeneralManagerMeg = applyProcessDto.GeneralManagerMeg;
                }
                    

                    if (applyProcessDto.GeneralManagerResult==false)
                    {
                        UpDateapplyProcess.ApplyResult = "被驳回";
                    }
                    if (applyProcessDto.GeneralManagerResult == true)
                    {
                        UpDateapplyProcess.ApplyResult = "申请成功";
                    }

                }
                bool result = this._eFRepository.Update<ApplyProcess>(UpDateapplyProcess);
                applyProcessDto1.Count = Convert.ToInt32(result);
            }
            else
            {
                applyProcessDto1.Count = 0;
            }

            return applyProcessDto1;
            //UserDto userDto = new UserDto();
            ////List<Employee> employeesNow = this._eFRepository.GetAll<Employee>().OrderBy(a => a.Id).ToList();
            //User delEmployee = this._eFRepository.GetAll<User>().FirstOrDefault(e => e.Id == DelUser.Id);
            //if (delEmployee != null)
            //{
            //    delEmployee.IsDeleted = true;
            //    bool result = this._eFRepository.Update<User>(delEmployee);
            //    userDto.Count = Convert.ToInt32(result);
            //}
            //else
            //{
            //    userDto.Count = 0;
            //}

            //return userDto;
        }
    }
}
