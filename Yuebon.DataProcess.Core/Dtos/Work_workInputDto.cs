using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Dtos
{
    /// <summary>
    /// 输入对象模型
    /// </summary>
    [AutoMap(typeof(Work_work))]
    [Serializable]
    public class Work_workInputDto: IInputDto<string>
    {
        /// <summary>
        /// 设置或获取配置信息ID
        /// </summary>
        public string Conf_conf_id { get; set; }
        /// <summary>
        /// 设置或获取配置信息详情IDs
        /// </summary>
        public string Conf_detail_ids { get; set; }
        /// <summary>
        /// 设置或获取数据路径
        /// </summary>
        public string Datapath { get; set; }
        /// <summary>
        /// 设置或获取描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 设置或获取启用标记
        /// </summary>
        public bool? EnabledMark { get; set; }
        /// <summary>
        /// 设置或获取唯一键值
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 设置或获取目标ID
        /// </summary>
        public string Sdid { get; set; }
        /// <summary>
        /// 设置或获取排序字段
        /// </summary>
        public int? SortCode { get; set; }
        /// <summary>
        /// 设置或获取状态
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 设置或获取工作编码
        /// </summary>
        public string Wcode { get; set; }

    }
}
