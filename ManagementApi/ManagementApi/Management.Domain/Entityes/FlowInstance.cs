using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Domain.Entityes
{
    /// <summary>
    /// 流程实例表
    /// </summary>
    [Table("WF_FlowInstance")]
    public class FlowInstance
    {
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// 当前节点
        /// </summary>
        public string NodeSN { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get; set; }

        /// <summary>
        /// 流状态
        /// </summary>
        public string WFStatus { get; set; }


        /// <summary>
        /// 流程发起人userId
        /// </summary>
        public Guid? StarterId { get; set; }

        /// <summary>
        /// 流程发起人姓名
        /// </summary>
        public string Starter { get; set; }

        /// <summary>
        /// 当前操作人userId
        /// </summary>
        public Guid? OperatorId { get; set; }

        /// <summary>
        /// 当前操作人姓名
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 待办人Id
        /// </summary>
        public Guid? ToDoerId { get; set; }

        /// <summary>
        /// 待办人名称
        /// </summary>
        public string ToDoer { get; set; }


        /// <summary>
        /// 已操作人
        /// </summary>
        public string Operated { get; set; }

        /// <summary>
        /// 流程创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 流程更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 申请单Id
        /// </summary>
        public Guid? RequisitionId { get; set; }


    }
}
