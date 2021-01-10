using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Dtos
{
    /// <summary>
    /// 输入对象模型
    /// </summary>
    [AutoMap(typeof(Conf_detail))]
    [Serializable]
    public class Conf_detailInputDto: IInputDto<string>
    {
        /// <summary>
        /// 设置或获取配置ID
        /// </summary>
        public string Conf_id { get; set; }
        /// <summary>
        /// 设置或获取配置类别 0：验证，1：导入
        /// </summary>
        public bool? Conf_type { get; set; }
        /// <summary>
        /// 设置或获取描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 设置或获取数据源详情ID
        /// </summary>
        public string Ds_detail_id { get; set; }
        /// <summary>
        /// 设置或获取启用标记
        /// </summary>
        public bool? EnabledMark { get; set; }
        /// <summary>
        /// 设置或获取唯一键值
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 设置或获取排序字段
        /// </summary>
        public int? SortCode { get; set; }
        /// <summary>
        /// 设置或获取状态
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 设置或获取系统配置ID
        /// </summary>
        public string Sys_conf_id { get; set; }

    }
}
