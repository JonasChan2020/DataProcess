using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Dtos
{
    /// <summary>
    /// 输入对象模型
    /// </summary>
    [AutoMap(typeof(Ds_detail))]
    [Serializable]
    public class Ds_detailInputDto: IInputDto<string>
    {
        /// <summary>
        /// 设置或获取列名所在行
        /// </summary>
        public int? Colnameindex { get; set; }

        /// 设置或获取列名
        /// </summary>
        public string Columns { get; set; }

        /// 设置或获取描述
        /// </summary>
        public string Description { get; set; }

        /// 设置或获取数据源ID
        /// </summary>
        public string Ds_id { get; set; }

        /// 设置或获取启用标记
        /// </summary>
        public bool? EnabledMark { get; set; }

        /// 设置或获取唯一键值
        /// </summary>
        public string Id { get; set; }

        /// 设置或获取优先级
        /// </summary>
        public int? Levelnum { get; set; }

        /// 设置或获取排序字段
        /// </summary>
        public int? SortCode { get; set; }

        /// 设置或获取状态
        /// </summary>
        public string State { get; set; }

        /// 设置或获取表名
        /// </summary>
        public string Tbname { get; set; }

        /// 设置或获取数据起始行
        /// </summary>
        public int? Valueindex { get; set; }

    }
}