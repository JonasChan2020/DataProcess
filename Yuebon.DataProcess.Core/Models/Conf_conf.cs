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
    [Table("dp_conf_conf")]
    [Serializable]
    public class Conf_conf:BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        /// <summary>
        /// 源上级ID
        /// </summary>
        public string FromParentId { get; set; }
        /// <summary>
        /// 源表ID
        /// </summary>
        public string FromId { get; set; }
        /// <summary>
        /// 源配置类型0：系统，1：数据库
        /// </summary>
        public int? ConfFromType { get; set; }
        /// <summary>
        /// 目标上级ID
        /// </summary>
        public string ToParentId { get; set; }
        /// <summary>
        /// 目标表ID
        /// </summary>
        public string ToId { get; set; }

        /// <summary>
        /// 目标配置类型0：系统，1：数据库
        /// </summary>
        public int? ConfToType { get; set; }
        /// <summary>
        /// 设置或获取创建时间
        /// </summary>
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 设置或获取创建人
        /// </summary>
        public string CreatorUserId { get; set; }
        /// <summary>
        /// 设置或获取删除标记
        /// </summary>
        public bool? DeleteMark { get; set; }
        /// <summary>
        /// 设置或获取删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }
        /// <summary>
        /// 设置或获取删除人
        /// </summary>
        public string DeleteUserId { get; set; }
        /// <summary>
        /// 设置或获取描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 设置或获取启用标记
        /// </summary>
        public bool? EnabledMark { get; set; }
        /// <summary>
        /// 设置或获取最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 设置或获取最后修改人
        /// </summary>
        public string LastModifyUserId { get; set; }
        /// <summary>
        /// 设置或获取排序字段
        /// </summary>
        public int? SortCode { get; set; }
        /// <summary>
        /// 设置或获取状态
        /// </summary>
        public string State { get; set; }

    }
}
