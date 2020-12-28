using System;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace HtSoft.DataProcess.Models
{
    /// <summary>
    /// 目标库分类表，数据实体对象
    /// </summary>
    [Table("dp_sd_classify")]
    [Serializable]
    public class Sd_classify:BaseEntity<string>
    {
        /// <summary>
        /// 设置或获取类型编码
        /// </summary>
        public string Dtcode { get; set; }

        /// <summary>
        /// 设置或获取类型名称
        /// </summary>
        public string Dtname { get; set; }

        /// <summary>
        /// 设置或获取类型描述
        /// </summary>
        public string Dtdesc { get; set; }

        /// <summary>
        /// 设置或获取父ID
        /// </summary>
        public string Parentid { get; set; }

        /// <summary>
        /// 设置或获取层级路径
        /// </summary>
        public string Levelpath { get; set; }

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
