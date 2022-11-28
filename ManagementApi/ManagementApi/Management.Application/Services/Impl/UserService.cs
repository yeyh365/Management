using Management.Application.Dto;
using Management.Domain.Entityes;
using Management.EntityFramework.Repositories;
using ManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Management.Application.Services.Impl
{
    public class UserService : IUserService
    {
        private  EFRepository _eFRepository;
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
            User user = this._eFRepository.GetAll<User>().Where(u=>u.Account == Account).ToList().FirstOrDefault();
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
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<ResultModel> GetUserInfo(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
