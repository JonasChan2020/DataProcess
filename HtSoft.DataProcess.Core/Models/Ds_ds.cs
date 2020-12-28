using System;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace HtSoft.DataProcess.Models
{
    /// <summary>
    /// 数据源表，数据实体对象
    /// </summary>
    [Table("dp_ds_ds")]
    [Serializable]
    public class Ds_ds:BaseEntity<string>
    {
        /// <summary>
        /// 设置或获取数据源编码
        /// </summary>
        public string Dscode { get; set; }

        /// <summary>
        /// 设置或获取数据源名称
        /// </summary>
        public string Dsname { get; set; }

        /// <summary>
        /// 设置或获取数据源描述
        /// </summary>
        public string Dsdesc { get; set; }

        /// <summary>
        /// 设置或获取数据源分类
        /// </summary>
        public string Dsclassify { get; set; }

        /// <summary>
        /// 设置或获取数据源类型
        /// </summary>
        public string Dstype { get; set; }

        /// <summary>
        /// 设置或获取所属组织ID
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
