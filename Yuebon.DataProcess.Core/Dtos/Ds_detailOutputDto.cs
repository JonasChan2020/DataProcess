using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yuebon.DataProcess.Dtos
{
    /// <summary>
    /// 输出对象模型
    /// </summary>
    [Serializable]
    public class Ds_detailOutputDto
    {
        /// <summary>
        /// 设置或获取列名所在行
        /// </summary>
        public int? Colnameindex { get; set; }

        /// 设置或获取列名
        /// </summary>
        [MaxLength(0)]
        public string Columns { get; set; }

        /// 设置或获取创建时间
        /// </summary>
        public DateTime? CreatorTime { get; set; }

        /// 设置或获取创建人
        /// </summary>
        [MaxLength(0)]
        public string CreatorUserId { get; set; }

        /// 设置或获取删除标记
        /// </summary>
        public bool? DeleteMark { get; set; }

        /// 设置或获取删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        /// 设置或获取删除人
        /// </summary>
        [MaxLength(0)]
        public string DeleteUserId { get; set; }

        /// 设置或获取描述
        /// </summary>
        [MaxLength(0)]
        public string Description { get; set; }

        /// 设置或获取数据源ID
        /// </summary>
        [MaxLength(0)]
        public string Ds_id { get; set; }

        /// 设置或获取启用标记
        /// </summary>
        public bool? EnabledMark { get; set; }

        /// 设置或获取唯一键值
        /// </summary>
        [MaxLength(0)]
        public string Id { get; set; }

        /// 设置或获取最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// 设置或获取最后修改人
        /// </summary>
        [MaxLength(0)]
        public string LastModifyUserId { get; set; }

        /// 设置或获取优先级
        /// </summary>
        public int? Levelnum { get; set; }

        /// 设置或获取排序字段
        /// </summary>
        public int? SortCode { get; set; }

        /// 设置或获取状态
        /// </summary>
        [MaxLength(0)]
        public string State { get; set; }

        /// 设置或获取表名
        /// </summary>
        [MaxLength(0)]
        public string Tbname { get; set; }

        /// 设置或获取数据起始行
        /// </summary>
        public int? Valueindex { get; set; }

    }
}