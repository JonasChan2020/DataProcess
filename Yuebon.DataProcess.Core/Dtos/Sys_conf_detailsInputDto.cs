using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Dtos
{
    /// <summary>
    /// 输入对象模型
    /// </summary>
    [AutoMap(typeof(Sys_conf_details))]
    [Serializable]
    public class Sys_conf_detailsInputDto: IInputDto<string>
    {
        /// <summary>
        /// 设置或获取唯一键值
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 设置或获取配置ID
        /// </summary>
        public string Sys_conf_id { get; set; }

        /// <summary>
        /// 设置或获取表名
        /// </summary>
        public string Tbname { get; set; }

        /// <summary>
        /// 设置或获取执行顺序
        /// </summary>
        public int? Levelnum { get; set; }

        /// <summary>
        /// 设置或获取是否为动态表
        /// </summary>
        public bool? Is_dynamic { get; set; }

        /// <summary>
        /// 设置或获取是否为标识表
        /// </summary>
        public bool? Is_flag { get; set; }

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
