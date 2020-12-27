using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace HtSoft.DataProcess.Models
{
    /// <summary>
    /// 目标库表，数据实体对象
    /// </summary>
    [Table("dp_sd_sysdb")]
    [Serializable]
    public class Sd_sysdb:BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        /// <summary>
        /// 设置或获取目标库名称
        /// </summary>
        public string SdName { get; set; }
        /// <summary>
        /// 设置或获取目标库描述
        /// </summary>
        public string Sddesc { get; set; }
        /// <summary>
        /// 设置或获取目标库分类
        /// </summary>
        public string Sysclassify { get; set; }
        /// <summary>
        /// 设置或获取目标库类型
        /// </summary>
        public string Sdtype { get; set; }
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
