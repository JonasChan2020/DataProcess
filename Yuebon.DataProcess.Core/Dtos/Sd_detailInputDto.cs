using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Dtos
{
    /// <summary>
    /// 目标库详情输入对象模型
    /// </summary>
    [AutoMap(typeof(Sd_detail))]
    [Serializable]
    public class Sd_detailInputDto: IInputDto<string>
    {
        /// <summary>
        /// 设置或获取唯一主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 设置或获取目标库ID
        /// </summary>
        public string Sd_id { get; set; }
        /// <summary>
        /// 设置或获取表集合信息
        /// </summary>
        public string Tbs { get; set; }

    }
}
