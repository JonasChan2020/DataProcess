using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Models;
using Yuebon.Commons.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Dtos
{
    /// <summary>
    /// 数据输出模型分类表输入对象模型
    /// </summary>
    [AutoMap(typeof(Sys_outmodel_classify))]
    [Serializable]
    public class Sys_outmodel_classifyInputDto: IInputDto<string>
    {
        /// <summary>
        /// 设置或获取唯一键值
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 设置或获取类型编码
        /// </summary>
        public string ClassCode { get; set; }
        /// <summary>
        /// 设置或获取类型名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 设置或获取父ID
        /// </summary>
        public string Parentid { get; set; }
        /// <summary>
        /// 设置或获取层级路径
        /// </summary>
        public string Levelpath { get; set; }
        /// <summary>
        /// 设置或获取所属系统ID
        /// </summary>
        public string Sysid { get; set; }
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
