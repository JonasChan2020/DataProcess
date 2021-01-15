using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Dtos
{
    /// <summary>
    /// 输入对象模型
    /// </summary>
    [AutoMap(typeof(Sd_sysdb))]
    [Serializable]
    public class Sd_sysdbInputDto: IInputDto<string>
    {
        /// <summary>
        /// 设置或获取目标库分类
        /// </summary>
        public string Classify_id { get; set; }

        /// 设置或获取描述
        /// </summary>
        public string Description { get; set; }

        /// 设置或获取启用标记
        /// </summary>
        public bool? EnabledMark { get; set; }

        /// 设置或获取唯一键值
        /// </summary>
        public string Id { get; set; }

        /// 设置或获取连接字符串
        /// </summary>
        public string Sdconnectionstr { get; set; }

        /// 设置或获取目标库描述
        /// </summary>
        public string Sddesc { get; set; }

        /// 设置或获取目标库名称
        /// </summary>
        public string SdName { get; set; }

        /// 设置或获取目标库类型
        /// </summary>
        public string Sdtype { get; set; }

        /// 设置或获取排序字段
        /// </summary>
        public int? SortCode { get; set; }

        /// 设置或获取状态
        /// </summary>
        public string State { get; set; }

        /// 设置或获取所属系统ID
        /// </summary>
        public string Sys_id { get; set; }

    }
}