using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Dtos
{
    /// <summary>
    /// 输入对象模型
    /// </summary>
    [AutoMap(typeof(Plug_plug))]
    [Serializable]
    public class Plug_plugInputDto: IInputDto<string>
    {
        /// <summary>
        /// 设置或获取唯一键值
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 设置或获取插件编码
        /// </summary>
        public string Pcode { get; set; }

        /// <summary>
        /// 设置或获取插件名称
        /// </summary>
        public string Pname { get; set; }

        /// <summary>
        /// 设置或获取插件描述
        /// </summary>
        public string Pdesc { get; set; }

        /// <summary>
        /// 设置或获取插件类型
        /// </summary>
        public string Ptype { get; set; }

        /// <summary>
        /// 设置或获取插件路径
        /// </summary>
        public string Ppath { get; set; }

        /// <summary>
        /// 设置或获取标签
        /// </summary>
        public string Ptag { get; set; }

        /// <summary>
        /// 设置或获取是否为公共插件 0否，1是
        /// </summary>
        public bool? Is_public { get; set; }

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
