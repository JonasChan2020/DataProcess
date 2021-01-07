using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Dtos
{
    /// <summary>
    /// 输入对象模型
    /// </summary>
    [AutoMap(typeof(Ds_ds))]
    [Serializable]
    public class Ds_dsInputDto: IInputDto<string>
    {
        /// <summary>
        /// 设置或获取唯一键值
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 设置或获取数据源编码
        /// </summary>
        public string Dscode { get; set; }

        /// <summary>
        /// 设置或获取数据源名称
        /// </summary>
        public string Dsname { get; set; }

        /// <summary>
        /// 设置或获取连接信息
        /// </summary>
        public string Connectionstr { get; set; }

        /// <summary>
        /// 设置或获取数据源分类
        /// </summary>
        public string Classify_id { get; set; }

        /// <summary>
        /// 设置或获取数据源类型
        /// </summary>
        public string Dstype { get; set; }

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
