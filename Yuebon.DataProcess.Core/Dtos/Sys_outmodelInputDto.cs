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
    /// 数据输出模型输入对象模型
    /// </summary>
    [AutoMap(typeof(Sys_outmodel))]
    [Serializable]
    public class Sys_outmodelInputDto: IInputDto<string>
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
        /// 设置或获取模型编码
        /// </summary>
        public string Modelcode { get; set; }
        /// <summary>
        /// 设置或获取模型名称
        /// </summary>
        public string Modelname { get; set; }
        /// <summary>
        /// 设置或获取模型分类ID
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
