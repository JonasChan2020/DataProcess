using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HtSoft.DataProcess.Dtos
{
    /// <summary>
    /// 系统表输出对象模型
    /// </summary>
    [Serializable]
    public class Sys_sysOutputDto
    {
        /// <summary>
        /// 设置或获取唯一键值
        /// </summary>
        [MaxLength(300)]
        public string Id { get; set; }
        /// <summary>
        /// 设置或获取系统编码
        /// </summary>
        [MaxLength(900)]
        public string Syscode { get; set; }
        /// <summary>
        /// 设置或获取系统名称
        /// </summary>
        [MaxLength(900)]
        public string Sysname { get; set; }
        /// <summary>
        /// 设置或获取系统分类
        /// </summary>
        [MaxLength(300)]
        public string Sysclassify { get; set; }
        /// <summary>
        /// 设置或获取系统描述
        /// </summary>
        [MaxLength(6000)]
        public string Sysdesc { get; set; }
        /// <summary>
        /// 设置或获取所属组织
        /// </summary>
        [MaxLength(300)]
        public string Orgid { get; set; }
        /// <summary>
        /// 设置或获取排序
        /// </summary>
        public int? Dsort { get; set; }
        /// <summary>
        /// 设置或获取状态
        /// </summary>
        [MaxLength(60)]
        public string State { get; set; }
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
