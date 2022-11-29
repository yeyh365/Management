using Management.Application.Common;
using Management.Application.Dto;
using Management.Domain.Entityes;
using Management.EntityFramework.Repositories;
using ManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Management.Core.Helper;

namespace Management.Application.Services.Impl
{
    public class UserService : IUserService
    {

        private EFRepository _eFRepository;

        public UserService()
        {

            this._eFRepository = new EFRepository();
        }
        /// <summary>
        /// 根据用户账户信息查询用户信息
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public LoginUserDto FindLoginUser(string Account)
        {
            User user = this._eFRepository.GetAll<User>().Where(u => u.Account == Account).ToList().FirstOrDefault();
            LoginUserDto loginUsers = new LoginUserDto();
            if (user != null)
            {
                loginUsers.Id = user.Id;
                loginUsers.Account = user.Account;
                loginUsers.UserName = user.UserName;
                loginUsers.Password = user.Password;
                loginUsers.Mobile = user.Mobile;
                loginUsers.Email = user.Email;
                loginUsers.Picture = user.Picture;
                loginUsers.Last_LoginTime = user.Last_LoginTime;
                loginUsers.Count = user.Count;
                loginUsers.IsDeleted = user.IsDeleted;
            }
            return loginUsers;
        }

        public Task<LoginUserDto> FindLoginUserAsync(string Account)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取所有的员工
        /// </summary>
        /// <returns></returns>
        public List<EmployeeDto> GetAllEmployeeDto()
        {
            List<EmployeeDto> list = new List<EmployeeDto>();
            List<Employee> employees = this._eFRepository.GetAll<Employee>().ToList();
            foreach (var employee in employees)
            {
                EmployeeDto employeeDto = new EmployeeDto();
                employeeDto.Id = employee.Id;
                employeeDto.EmployeeId = employee.EmployeeId;
                employeeDto.EmployeeName = employee.EmployeeName;
                employeeDto.DepartmentNumber = employee.DepartmentNumber;
                employeeDto.PositionNumber = employee.PositionNumber;
                employeeDto.CredId = employee.CredId;
                employeeDto.Sex = employee.Sex;
                employeeDto.Mobile = employee.Mobile;
                employeeDto.Email = employee.Email;
                list.Add(employeeDto);
            }
            return list;
        }
        /// <summary>
        /// 获取所有的用户
        /// </summary>
        /// <returns></returns>
        public List<UserDto> GetAllUser()
        {
            List<UserDto> list = new List<UserDto>();
            List<User> users = this._eFRepository.GetAll<User>().ToList();
            foreach (var user in users)
            {
                UserDto userDto = new UserDto();
                userDto.Id = user.Id;
                userDto.Account = user.Account;
                userDto.UserName = user.UserName;
                userDto.Password = user.Password;
                userDto.Mobile = user.Mobile;
                userDto.Email = user.Email;
                userDto.Picture = user.Picture;
                userDto.Last_LoginTime = user.Last_LoginTime;
                userDto.Count = user.Count;
                userDto.IsDeleted = user.IsDeleted;
                list.Add(userDto);
            }
            return list;
        }
        /// <summary>
        ///分页查询
        /// </summary>
        /// <param name="searchEmployeeDto"></param>
        /// <returns></returns>
        public List<EmployeeDto> GetEmployeeLimitDto(SearchEmployeeDto searchEmployeeDto)
        {
            List<EmployeeDto> list = new List<EmployeeDto>();
            List<Employee> employeesTime = this._eFRepository.GetAll<Employee>().OrderBy(a => a.Id).ToList();
            var count = employeesTime.Count();
            var employees = employeesTime.Skip((searchEmployeeDto.Page - 1) * searchEmployeeDto.Limit).Take(searchEmployeeDto.Limit);
            foreach (var employee in employees)
            {
                EmployeeDto employeeDto = new EmployeeDto();
                employeeDto.Id = employee.Id;
                employeeDto.EmployeeId = employee.EmployeeId;
                employeeDto.EmployeeName = employee.EmployeeName;
                employeeDto.DepartmentNumber = employee.DepartmentNumber;
                employeeDto.PositionNumber = employee.PositionNumber;
                employeeDto.CredId = employee.CredId;
                employeeDto.Sex = employee.Sex;
                employeeDto.Mobile = employee.Mobile;
                employeeDto.Email = employee.Email;
                employeeDto.Count = count;

                list.Add(employeeDto);
            }
            return list;
        }
        /// <summary>
        /// 删除一个员工的实体
        /// </summary>
        /// <param name="DelEmployee"></param>
        /// <returns></returns>
        public EmployeeDto DeleteEmployee(SearchEmployeeDto DelEmployee)
        {
            EmployeeDto employeeDto = new EmployeeDto();
            //List<Employee> employeesNow = this._eFRepository.GetAll<Employee>().OrderBy(a => a.Id).ToList();
            Employee delEmployee = this._eFRepository.GetAll<Employee>().FirstOrDefault(e => e.Id == DelEmployee.Id);
            if (delEmployee != null)
            {
                employeeDto.Id = delEmployee.Id;
                employeeDto.EmployeeId = delEmployee.EmployeeId;
                employeeDto.EmployeeName = delEmployee.EmployeeName;
                employeeDto.DepartmentNumber = delEmployee.DepartmentNumber;
                employeeDto.PositionNumber = delEmployee.PositionNumber;
                employeeDto.CredId = delEmployee.CredId;
                employeeDto.Sex = delEmployee.Sex;
                employeeDto.Mobile = delEmployee.Mobile;
                employeeDto.Email = delEmployee.Email;
                bool result = this._eFRepository.Delete<Employee>(delEmployee);
                employeeDto.Count = Convert.ToInt32(result);
            }
            else
            {
                employeeDto.Count = 0;
            }

            return employeeDto;
        }
        /// <summary>
        /// 增加一个员工
        /// </summary>
        /// <param name="AddEmployee"></param>
        /// <returns></returns>
        public EmployeeDto AddEmployee(SearchEmployeeDto AddEmployee)
        {
            EmployeeDto employeeDto = new EmployeeDto();
            Employee employee = new Employee();
            //List<Employee> employeesNow = this._eFRepository.GetAll<Employee>().OrderBy(a => a.Id).ToList();
            if (AddEmployee.EmployeeId >= 2)
            {
                employee.EmployeeId = AddEmployee.EmployeeId;
                employee.EmployeeName = AddEmployee.EmployeeName;
                employee.DepartmentNumber = AddEmployee.DepartmentNumber;
                employee.PositionNumber = AddEmployee.PositionNumber;
                employee.CredId = AddEmployee.CardId;
                employee.Sex = AddEmployee.Sex;
                employee.Mobile = AddEmployee.Mobile;
                employee.Email = AddEmployee.Email;
                bool result = this._eFRepository.Add<Employee>(employee);
                employeeDto.Count = Convert.ToInt32(result);
            }
            else
            {
                employeeDto.Count = 0;
            }

            return employeeDto;
        }
        /// <summary>
        /// 导出用户和员工信息
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage ExportEmployeeList()
        {
            var Query = this._eFRepository.GetAll<Employee>().OrderBy(a => a.Id).ToList();
            var Qualificationcolumn = Initcolumn(1);

            var stream = new StreamHelper();
            var _excelHelper = new NPOIHelper();
            var openStream = stream.BytesToStream(_excelHelper.Export(Query, Qualificationcolumn, "员工信息"));

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(openStream);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = string.Format("{0}.xlsx", "test");
            return response;
        }
        /// <summary> 
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<ResultModel> GetUserInfo(int Id)
        {
            throw new NotImplementedException();
        }
        private Dictionary<string, string> Initcolumn(int Type, bool IsAdmin = false)
        {
            Dictionary<string, string> adminApplyColumn = new Dictionary<string, string>();
            if (Type == 1)//学员等级汇总
            {
                adminApplyColumn.Add("Id", "Id");
                adminApplyColumn.Add("EmployeeId", "员工账号");
                adminApplyColumn.Add("EmployeeName", "员工姓名");
                adminApplyColumn.Add("DepartmentNumber", "部门");
                adminApplyColumn.Add("PositionNumber", "职位");
                adminApplyColumn.Add("CredId", "身份证号");
                adminApplyColumn.Add("Sex", "性别");
                adminApplyColumn.Add("Mobile", "部门");
                adminApplyColumn.Add("Email", "邮箱");

            }

            return adminApplyColumn;
        }
    }
}
