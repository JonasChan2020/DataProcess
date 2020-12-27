using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Models;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.Dtos
{
    /// <summary>
    /// 数据源配置信息表输入对象模型
    /// </summary>
    [AutoMap(typeof(Ds_dsconfig))]
    [Serializable]
    public class Ds_dsconfigInputDto: IInputDto<string>
    {
        /// <summary>
        /// 设置或获取所属数据源ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 设置或获取数据源配置信息
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
