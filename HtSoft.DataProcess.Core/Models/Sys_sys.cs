using System;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace HtSoft.DataProcess.Models
{
    /// <summary>
    /// 系统表，数据实体对象
    /// </summary>
    [Table("dp_sys_sys")]
    [Serializable]
    public class Sys_sys:BaseEntity<string>
    {
        /// <summary>
        /// 设置或获取系统编码
        /// </summary>
        public string Syscode { get; set; }

        /// <summary>
        /// 设置或获取系统名称
        /// </summary>
        public string Sysname { get; set; }

        /// <summary>
        /// 设置或获取系统分类
        /// </summary>
        public string Sysclassify { get; set; }

        /// <summary>
        /// 设置或获取系统描述
        /// </summary>
        public string Sysdesc { get; set; }

        /// <summary>
        /// 设置或获取所属组织
        /// </summary>
        public string Orgid { get; set; }

        /// <summary>
        /// 设置或获取排序
        /// </summary>
        public int? Dsort { get; set; }

        /// <summary>
        /// 设置或获取状态
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 设置或获取创建人
        /// </summary>
        public string Cuid { get; set; }

        /// <summary>
        /// 设置或获取创建时间
        /// </summary>
        public DateTime? Ctime { get; set; }

        /// <summary>
        /// 设置或获取更新人
        /// </summary>
        public string Uuid { get; set; }

        /// <summary>
        /// 设置或获取更新时间
        /// </summary>
        public DateTime? Utime { get; set; }


    }
}
