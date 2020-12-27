using AutoMapper;
using System;
using HtSoft.DataProcess.Models;
using Yuebon.Commons.Dtos;

namespace HtSoft.DataProcess.Dtos
{
    /// <summary>
    /// 目标库配置信息表输入对象模型
    /// </summary>
    [AutoMap(typeof(Sd_sdconfig))]
    [Serializable]
    public class Sd_sdconfigInputDto: IInputDto<string>
    {
        /// <summary>
        /// 设置或获取所属目标库ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 设置或获取目标库配置信息
        /// </summary>
        public string Dsconfig { get; set; }
        /// <summary>
        /// 设置或获取创建人
        /// </summary>
        public string Cuid { get; set; }
        /// <summary>
        /// 设置或获取创建时间
        /// </summary>
        public DateTime? Ctime { get; set; }
        /// <summary>
        /// 设置或获取更新人
        /// </summary>
        public string Uuid { get; set; }
        /// <summary>
        /// 设置或获取更新时间
        /// </summary>
        public DateTime? Utime { get; set; }

    }
}
