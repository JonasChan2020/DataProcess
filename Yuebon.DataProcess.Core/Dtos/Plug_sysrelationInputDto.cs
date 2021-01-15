using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Dtos
{
    /// <summary>
    /// 输入对象模型
    /// </summary>
    [AutoMap(typeof(Plug_sysrelation))]
    [Serializable]
    public class Plug_sysrelationInputDto: IInputDto<string>
    {
        /// <summary>
        /// 设置或获取描述
        /// </summary>
        public string Description { get; set; }

        /// 设置或获取启用标记
        /// </summary>
        public bool? EnabledMark { get; set; }

        /// 设置或获取唯一键值
        /// </summary>
        public string Id { get; set; }

        /// 设置或获取插件ID
        /// </summary>
        public string Plug_id { get; set; }

        /// 设置或获取排序字段
        /// </summary>
        public int? SortCode { get; set; }

        /// 设置或获取状态
        /// </summary>
        public string State { get; set; }

        /// 设置或获取系统ID
        /// </summary>
        public string Sys_id { get; set; }

    }
}