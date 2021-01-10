using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Dtos
{
    /// <summary>
    /// 输入对象模型
    /// </summary>
    [AutoMap(typeof(Plug_type))]
    [Serializable]
    public class Plug_typeInputDto: IInputDto<string>
    {
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
        /// 设置或获取层级路径
        /// </summary>
        public string Levelpath { get; set; }
        /// <summary>
        /// 设置或获取父ID
        /// </summary>
        public string Parentid { get; set; }
        /// <summary>
        /// 设置或获取类型编码
        /// </summary>
        public string Ptcode { get; set; }
        /// <summary>
        /// 设置或获取类型描述
        /// </summary>
        public string Ptdesc { get; set; }
        /// <summary>
        /// 设置或获取类型接口名称
        /// </summary>
        public string Ptiname { get; set; }
        /// <summary>
        /// 设置或获取类型名称
        /// </summary>
        public string Ptname { get; set; }
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
