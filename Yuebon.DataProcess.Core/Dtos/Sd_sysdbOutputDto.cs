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
    public class Sd_sysdbOutputDto
    {
        /// <summary>
        /// 设置或获取目标库分类
        /// </summary>
        [MaxLength(0)]
        public string Classify_id { get; set; }
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
        /// 设置或获取连接字符串
        /// </summary>
        [MaxLength(0)]
        public string Sdconnectionstr { get; set; }
        /// <summary>
        /// 设置或获取目标库描述
        /// </summary>
        [MaxLength(0)]
        public string Sddesc { get; set; }
        /// <summary>
        /// 设置或获取目标库名称
        /// </summary>
        [MaxLength(0)]
        public string SdName { get; set; }
        /// <summary>
        /// 设置或获取目标库类型
        /// </summary>
        [MaxLength(0)]
        public string Sdtype { get; set; }
        /// <summary>
        /// 设置或获取排序字段
        /// </summary>
        public int? SortCode { get; set; }
        /// <summary>
        /// 设置或获取状态
        /// </summary>
        [MaxLength(0)]
        public string State { get; set; }
        /// <summary>
        /// 设置或获取所属系统ID
        /// </summary>
        [MaxLength(0)]
        public string Sys_id { get; set; }

    }
}
