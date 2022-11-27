﻿using Management.Application.Dto;
using ManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Management.Application.Services
{
    public interface IUserService
    {
        /// <summary>
        /// 根据ID获取用户信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ResultModel> GetUserInfo(int Id );
        /// <summary>
        /// 根据userid 返回一个loginUser
        /// </summary>
        /// <param name="UserAD"></param>
        /// <returns></returns>
        LoginUserDto FindLoginUser(string Account);
        List<EmployeeDto> GetAllEmployeeDto();
    }
}
