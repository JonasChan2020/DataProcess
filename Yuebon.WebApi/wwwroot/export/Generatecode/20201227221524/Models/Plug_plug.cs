using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace HtSoft.DataProcess.Models
{
    /// <summary>
    /// 插件表，数据实体对象
    /// </summary>
    [Table("dp_plug_plug")]
    [Serializable]
    public class Plug_plug:BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        /// <summary>
        /// 设置或获取插件编码
        /// </summary>
        public string Pcode { get; set; }
        /// <summary>
        /// 设置或获取插件名称
        /// </summary>
        public string Pname { get; set; }
        /// <summary>
        /// 设置或获取插件描述
        /// </summary>
        public string Pdesc { get; set; }
        /// <summary>
        /// 设置或获取所属目标库ID
        /// </summary>
        public string Dbid { get; set; }
        /// <summary>
        /// 设置或获取插件类型
        /// </summary>
        public string Ptype { get; set; }
        /// <summary>
        /// 设置或获取插件路径
        /// </summary>
        public string Ppath { get; set; }
        /// <summary>
        /// 设置或获取标签
        /// </summary>
        public string Ptag { get; set; }
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
