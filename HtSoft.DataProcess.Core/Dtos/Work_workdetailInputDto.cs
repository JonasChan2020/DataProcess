using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.Dtos
{
    /// <summary>
    /// 输入对象模型
    /// </summary>
    [AutoMap(typeof(Work_workdetail))]
    [Serializable]
    public class Work_workdetailInputDto: IInputDto<string>
    {
        /// <summary>
        /// 设置或获取唯一键值
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 设置或获取作业ID
        /// </summary>
        public string Workid { get; set; }
        /// <summary>
        /// 设置或获取作业进行过程参数
        /// </summary>
        public string Workrunparm { get; set; }
        /// <summary>
        /// 设置或获取作业过程返回信息
        /// </summary>
        public string Resultcontent { get; set; }
        /// <summary>
        /// 设置或获取开始时间
        /// </summary>
        public DateTime? Starttime { get; set; }
        /// <summary>
        /// 设置或获取结束时间
        /// </summary>
        public DateTime? Endtime { get; set; }
        /// <summary>
        /// 设置或获取状态
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 设置或获取排序字段
        /// </summary>
        public int? SortCode { get; set; }
        /// <summary>
        /// 设置或获取启用标记
        /// </summary>
        public bool? EnabledMark { get; set; }
        /// <summary>
        /// 设置或获取描述
        /// </summary>
        public string Description { get; set; }

    }
}
