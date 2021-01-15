using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.DataProcess.Models
{
    /// <summary>
    /// ，数据实体对象
    /// </summary>
    [Table("dp_conf_classify")]
    [Serializable]
    public class Conf_classify:BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        /// <summary>
        /// 设置或获取分类编码
        /// </summary>
        public string Ccode { get; set; }

        /// 设置或获取分类描述
        /// </summary>
        public string Cdesc { get; set; }

        /// 设置或获取分类名称
        /// </summary>
        public string Cname { get; set; }

        /// 设置或获取创建时间
        /// </summary>
        public DateTime? CreatorTime { get; set; }

        /// 设置或获取创建人
        /// </summary>
        public string CreatorUserId { get; set; }

        /// 设置或获取删除标记
        /// </summary>
        public bool? DeleteMark { get; set; }

        /// 设置或获取删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        /// 设置或获取删除人
        /// </summary>
        public string DeleteUserId { get; set; }

        /// 设置或获取描述
        /// </summary>
        public string Description { get; set; }

        /// 设置或获取启用标记
        /// </summary>
        public bool? EnabledMark { get; set; }

        /// 设置或获取最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// 设置或获取最后修改人
        /// </summary>
        public string LastModifyUserId { get; set; }

        /// 设置或获取层级路径
        /// </summary>
        public string Levelpath { get; set; }

        /// 设置或获取父ID
        /// </summary>
        public string Parentid { get; set; }

        /// 设置或获取排序字段
        /// </summary>
        public int? SortCode { get; set; }

        /// 设置或获取状态
        /// </summary>
        public string State { get; set; }

        /// 设置或获取所属系统ID
        /// </summary>
        public string Sysid { get; set; }

    }
}