using System;
using System.ComponentModel.DataAnnotations;

namespace HtSoft.DataProcess.Dtos
{
    /// <summary>
    /// 目标库配置信息表输出对象模型
    /// </summary>
    [Serializable]
    public class Sd_sdconfigOutputDto
    {
        /// <summary>
        /// 设置或获取所属目标库ID
        /// </summary>
        [MaxLength(300)]
        public string Id { get; set; }
        /// <summary>
        /// 设置或获取目标库配置信息
        /// </summary>
        [MaxLength(16777215)]
        public string Dsconfig { get; set; }
        /// <summary>
        /// 设置或获取创建人
        /// </summary>
        [MaxLength(300)]
        public string Cuid { get; set; }
        /// <summary>
        /// 设置或获取创建时间
        /// </summary>
        public DateTime? Ctime { get; set; }
        /// <summary>
        /// 设置或获取更新人
        /// </summary>
        [MaxLength(300)]
        public string Uuid { get; set; }
        /// <summary>
        /// 设置或获取更新时间
        /// </summary>
        public DateTime? Utime { get; set; }

    }
}
