using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Dtos
{
    /// <summary>
    /// 输入对象模型
    /// </summary>
    [AutoMap(typeof(Conf_conf))]
    [Serializable]
    public class Conf_confInputDto: IInputDto<string>
    {
        /// <summary>
        /// 起始方配置类型0：系统，1：数据库
        /// </summary>
        public int? ConfFromType { get; set; }

        /// <summary>
        /// 终止方配置类型0：系统，1：数据库
        /// </summary>
        public int? ConfToType { get; set; }
        /// <summary>
        /// 设置或获取描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 设置或获取关联库或系统ID
        /// </summary>
        public string FromId { get; set; }

        /// <summary>
        /// 设置或获取被关联库或系统ID
        /// </summary>
        public string ToId { get; set; }
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
      

    }
}
