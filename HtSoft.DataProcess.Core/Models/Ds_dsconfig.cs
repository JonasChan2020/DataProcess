using System;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace HtSoft.DataProcess.Models
{
    /// <summary>
    /// 数据源配置信息表，数据实体对象
    /// </summary>
    [Table("dp_ds_dsconfig")]
    [Serializable]
    public class Ds_dsconfig:BaseEntity<string>
    {
        /// <summary>
        /// 设置或获取数据源配置信息
        /// </summary>
        public string Dsconfig { get; set; }
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
