using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.Dtos
{
    /// <summary>
    /// 输入对象模型
    /// </summary>
    [AutoMap(typeof(Conf_classify))]
    [Serializable]
    public class Conf_classifyInputDto: IInputDto<string>
    {
        /// <summary>
        /// 设置或获取唯一键值
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 设置或获取分类编码
        /// </summary>
        public string Ccode { get; set; }
        /// <summary>
        /// 设置或获取分类名称
        /// </summary>
        public string Cname { get; set; }
        /// <summary>
        /// 设置或获取分类描述
        /// </summary>
        public string Cdesc { get; set; }
        /// <summary>
        /// 设置或获取父ID
        /// </summary>
        public string Parentid { get; set; }
        /// <summary>
        /// 设置或获取层级路径
        /// </summary>
        public string Levelpath { get; set; }
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
