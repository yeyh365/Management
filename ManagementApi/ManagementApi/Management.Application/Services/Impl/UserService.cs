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
using static Management.Application.Common.testfanshehuoqu;

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
                employeeDto.CardId = employee.CardId;
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
            List<User> users = this._eFRepository.GetAll<User>().Where(e=>e.IsDeleted != true).ToList();
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
        ///分页查询员工
        /// </summary>
        /// <param name="searchEmployeeDto"></param>
        /// <returns></returns>
        public List<EmployeeDto> GetEmployeeLimitDto(SearchEmployeeDto searchEmployeeDto)
        {
            List<EmployeeDto> list = new List<EmployeeDto>();
            IEnumerable<Employee> employeesTime = this._eFRepository.GetAll<Employee>().OrderBy(a => a.Id);
            if (!string.IsNullOrEmpty(searchEmployeeDto.EmployeeName))
            {
                employeesTime = employeesTime.Where(e => e.EmployeeName== searchEmployeeDto.EmployeeName);
            }
            if (searchEmployeeDto.EmployeeId>1)
            {
                employeesTime = employeesTime.Where(e => e.EmployeeId == searchEmployeeDto.EmployeeId);
            }
            if (!string.IsNullOrEmpty(searchEmployeeDto.DepartmentNumber))
            {
                employeesTime = employeesTime.Where(e => e.DepartmentNumber == searchEmployeeDto.DepartmentNumber);
            }
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
                employeeDto.CardId = employee.CardId;
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
                employeeDto.CardId = delEmployee.CardId;
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
        /// 删除一个用户的实体 假删除
        /// </summary>
        /// <param name="DelUser"></param>
        /// <returns></returns>
        public UserDto DeleteUser(UserDto DelUser)
        {
            UserDto userDto = new UserDto();
            //List<Employee> employeesNow = this._eFRepository.GetAll<Employee>().OrderBy(a => a.Id).ToList();
            User delEmployee = this._eFRepository.GetAll<User>().FirstOrDefault(e => e.Id == DelUser.Id);
            if (delEmployee != null)
            {
                delEmployee.IsDeleted=true;
                bool result = this._eFRepository.Update<User>(delEmployee);
                userDto.Count = Convert.ToInt32(result);
            }
            else
            {
                userDto.Count = 0;
            }

            return userDto;
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
                employee.CardId = AddEmployee.CardId;
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
        /// 修改一个用户信息
        /// </summary>
        /// <param name="DelUser"></param>
        /// <returns></returns>
        public UserDto UpdateUser(UserDto UpdateUser)
        {
            UserDto userDto = new UserDto();
            //List<Employee> employeesNow = this._eFRepository.GetAll<Employee>().OrderBy(a => a.Id).ToList();
            User user = this._eFRepository.GetAll<User>().FirstOrDefault(e => e.Id == UpdateUser.Id);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(UpdateUser.Account))
                {
                    user.Account = UpdateUser.Account;
                }
                if (!string.IsNullOrEmpty(UpdateUser.UserName))
                {
                    user.UserName = UpdateUser.UserName;
                }
                if (!string.IsNullOrEmpty(UpdateUser.Password))
                {
                    user.Password = UpdateUser.Password;
                }
                if (!string.IsNullOrEmpty(UpdateUser.Mobile))
                {
                    user.Mobile = UpdateUser.Mobile;
                }
                if (!string.IsNullOrEmpty(UpdateUser.Email))
                {
                    user.Email = UpdateUser.Email;
                }
                if (!string.IsNullOrEmpty(UpdateUser.Picture))
                {
                    user.Picture = UpdateUser.Picture;
                }
                bool result = this._eFRepository.Update<User>(user);
                userDto.Count = Convert.ToInt32(result);
            }
            else
            {
                userDto.Count = 0;
            }

            return userDto;
        }
        /// <summary>
        /// 修改一个员工信息
        /// </summary>
        /// <param name="DelUser"></param>
        /// <returns></returns>
        public EmployeeDto UpdateEmployee(EmployeeDto UpdateEmployee)
        {
            EmployeeDto userDto = new EmployeeDto();
            //List<Employee> employeesNow = this._eFRepository.GetAll<Employee>().OrderBy(a => a.Id).ToList();
            Employee employee = this._eFRepository.GetAll<Employee>().FirstOrDefault(e => e.Id == UpdateEmployee.Id);
            if (employee != null)
            {
                if (UpdateEmployee.EmployeeId>1)
                {
                    employee.EmployeeId = UpdateEmployee.EmployeeId;
                }
                if (!string.IsNullOrEmpty(UpdateEmployee.EmployeeName))
                {
                    employee.EmployeeName = UpdateEmployee.EmployeeName;
                }
                if (!string.IsNullOrEmpty(UpdateEmployee.DepartmentNumber))
                {
                    employee.DepartmentNumber = UpdateEmployee.DepartmentNumber;
                }
                if (!string.IsNullOrEmpty(UpdateEmployee.PositionNumber))
                {
                    employee.PositionNumber = UpdateEmployee.PositionNumber;
                }
                if (!string.IsNullOrEmpty(UpdateEmployee.CardId))
                {
                    employee.CardId = UpdateEmployee.CardId;
                }
                if (!string.IsNullOrEmpty(UpdateEmployee.Sex))
                {
                    employee.Sex = UpdateEmployee.Sex;
                }
                if (!string.IsNullOrEmpty(UpdateEmployee.Mobile))
                {
                    employee.Mobile = UpdateEmployee.Mobile;
                }
                if (!string.IsNullOrEmpty(UpdateEmployee.Email))
                {
                    employee.Email = UpdateEmployee.Email;
                }
                bool result = this._eFRepository.Update<Employee>(employee);
                userDto.Count = Convert.ToInt32(result);
            }
            else
            {
                userDto.Count = 0;
            }

            return userDto;
        }
        /// <summary>
        /// 导出员工信息
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
        /// 导出用户信息
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage ExportUserList()
        {
            var Query = this._eFRepository.GetAll<User>().OrderBy(a => a.Id).ToList();
            var Qualificationcolumn = Initcolumn(2);

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
        /// 增加一个用户
        /// </summary>
        /// <param name="AddUser"></param>
        /// <returns></returns>
        public EmployeeDto AddUser(UserDto addUser)
        {
            EmployeeDto employeeDto = new EmployeeDto();
            User user = new User();
            //List<Employee> employeesNow = this._eFRepository.GetAll<Employee>().OrderBy(a => a.Id).ToList();
            if (!string.IsNullOrEmpty(addUser.Account)&& !string.IsNullOrEmpty(addUser.Password)&& !string.IsNullOrEmpty(addUser.UserName))
            {
                user.Account = addUser.Account;
                user.UserName = addUser.UserName;
                user.Password = addUser.Password;
                user.Mobile = addUser.Mobile;
                user.Email = addUser.Email;
                user.Picture = addUser.Picture;
                user.Last_LoginTime = addUser.Last_LoginTime;
                user.Count = addUser.Count;
                user.IsDeleted = addUser.IsDeleted;

                bool result = this._eFRepository.Add<User>(user);
                employeeDto.Count = Convert.ToInt32(result);
            }
            else
            {
                employeeDto.Count = 0;
            }

            return employeeDto;
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

            }else if (Type == 2)
            {
                adminApplyColumn.Add("Id", "Id");
                adminApplyColumn.Add("Account", "用户账号");
                adminApplyColumn.Add("UserName", "用户姓名");
                adminApplyColumn.Add("Password", "用户密码");
                adminApplyColumn.Add("Mobile", "电话");
                adminApplyColumn.Add("Email", "邮箱");
                adminApplyColumn.Add("Picture", "头像地址");
                adminApplyColumn.Add("Last_LoginTime", "最后登录时间");
                adminApplyColumn.Add("Count", "登录次数");
                adminApplyColumn.Add("IsDeleted", "是否删除");

            }

            return adminApplyColumn;
        }
    }
}
