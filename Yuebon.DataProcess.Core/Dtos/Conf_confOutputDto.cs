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
    public class Conf_confOutputDto
    {
        /// <summary>
        /// 源上级ID
        /// </summary>
        [MaxLength(0)]
        public string FromParentId { get; set; }
         /// <summary>
        /// 目标上级ID
        /// </summary>
        [MaxLength(0)]
        public string FromParentName { get; set; }
        /// <summary>
        /// 目标上级ID
        /// </summary>
        [MaxLength(0)]
        public string FromParentDescription { get; set; }
        /// <summary>
        /// 源表ID
        /// </summary>
        [MaxLength(0)]
        public string FromId { get; set; }
        /// <summary>
        /// 源表名
        /// </summary>
        [MaxLength(0)]
        public string FromTbName { get; set; }
        /// <summary>
        /// 源表名
        /// </summary>
        [MaxLength(0)]
        public string FromDescription { get; set; }
        /// <summary>
        /// 源配置类型0：系统，1：数据库
        /// </summary>
        [MaxLength(0)]
        public int? ConfFromType { get; set; }
        /// <summary>
        /// 目标上级ID
        /// </summary>
        [MaxLength(0)]
        public string ToParentId { get; set; }
        /// <summary>
        /// 目标上级ID
        /// </summary>
        [MaxLength(0)]
        public string ToParentName { get; set; }
        /// <summary>
        /// 目标上级ID
        /// </summary>
        [MaxLength(0)]
        public string ToParentDescription { get; set; }
        /// <summary>
        /// 目标表ID
        /// </summary>
        [MaxLength(0)]
        public string ToId { get; set; }
        /// <summary>
        /// 目标表名
        /// </summary>
        [MaxLength(0)]
        public string ToTbName { get; set; }
        /// <summary>
        /// 目标表名
        /// </summary>
        [MaxLength(0)]
        public string ToDescription { get; set; }
        /// <summary>
        /// 目标配置类型0：系统，1：数据库
        /// </summary>
        [MaxLength(0)]
        public int? ConfToType { get; set; }
        /// <summary>
        /// 设置或获取创建时间
        /// </summary>
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 设置或获取创建人
        /// </summary>
        [MaxLength(0)]
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
        [MaxLength(0)]
        public string DeleteUserId { get; set; }
        /// <summary>
        /// 设置或获取描述
        /// </summary>
        [MaxLength(0)]
        public string Description { get; set; }
        /// <summary>
        /// 设置或获取启用标记
        /// </summary>
        public bool? EnabledMark { get; set; }
        /// <summary>
        /// 设置或获取唯一键值
        /// </summary>
        [MaxLength(0)]
        public string Id { get; set; }
        /// <summary>
        /// 设置或获取最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 设置或获取最后修改人
        /// </summary>
        [MaxLength(0)]
        public string LastModifyUserId { get; set; }
        /// <summary>
        /// 设置或获取排序字段
        /// </summary>
        public int? SortCode { get; set; }
        /// <summary>
        /// 设置或获取状态
        /// </summary>
        [MaxLength(0)]
        public string State { get; set; }

    }
}
