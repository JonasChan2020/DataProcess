using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Dtos
{
    /// <summary>
    /// 输入对象模型
    /// </summary>
    [AutoMap(typeof(Sys_conf))]
    [Serializable]
    public class Sys_confInputDto: IInputDto<string>
    {
        /// <summary>
        /// 设置或获取唯一键值
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 设置或获取系统ID
        /// </summary>
        public string Sysid { get; set; }

        /// <summary>
        /// 设置或获取配置信息编码
        /// </summary>
        public string Confcode { get; set; }

        /// <summary>
        /// 设置或获取配置信息名称
        /// </summary>
        public string Confname { get; set; }

        /// <summary>
        /// 设置或获取配置信息描述
        /// </summary>
        public string Confdes { get; set; }

        /// <summary>
        /// 设置或获取配置分类ID
        /// </summary>
        public string Classify_id { get; set; }

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
