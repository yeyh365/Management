﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Application.Dto
{
    public class ApplyProcessDto : SearchDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 申请人ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 申请人账户
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 申请标题
        /// </summary>
        public string ApplyTitle { get; set; }
        /// <summary>
        /// 申请内容
        /// </summary>
        public string ApplyBoby { get; set; }
        /// <summary>
        /// 发起时间
        /// </summary>
        public DateTime? ApplyDate { get; set; }
        /// <summary>
        /// 经理Id
        /// </summary>
        public string ApproverDepartmentId { get; set; }
        /// <summary>
        /// 项目经理审批结果
        /// </summary>
        public bool? ApproverrDepartmenResult { get; set; }
        /// <summary>
        /// 项目经理审批信息
        /// </summary>
        public string ApproverDepartmentMeg { get; set; }
        /// <summary>
        /// 项目经理审批时间
        /// </summary>
        public DateTime? ApproverrDepartmentDate { get; set; }
        /// <summary>
        /// 总经理ID
        /// </summary>
        public string GeneralManagerId { get; set; }
        /// <summary>
        /// 总经理审批结果
        /// </summary>
        public bool? GeneralManagerResult { get; set; }
        /// <summary>
        /// 总经理审批信息
        /// </summary>
        public string GeneralManagerMeg { get; set; }
        public DateTime? GeneralManagerDate { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? ExpiredDate { get; set; }
        /// <summary>
        /// 流程状态
        /// </summary>
        public bool? ApplyState { get; set; }
        /// <summary>
        /// 流程结果
        /// </summary>
        public string ApplyResult { get; set; }
        public int Count { get; set; }
        /// <summary>
        /// 部门经理账号
        /// </summary>
        public string ApproverDepartmentName { get; set; }
        /// <summary>
        /// 总经理账号
        /// </summary>
        public string GeneralManagerName { get; set; }

    }
}
