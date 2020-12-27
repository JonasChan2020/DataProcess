using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace HtSoft.DataProcess.Models
{
    /// <summary>
    /// 插件类型表，数据实体对象
    /// </summary>
    [Table("dp_plug_type")]
    [Serializable]
    public class Plug_type:BaseEntity<string>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        /// <summary>
        /// 设置或获取类型编码
        /// </summary>
        public string Ptcode { get; set; }
        /// <summary>
        /// 设置或获取类型名称
        /// </summary>
        public string Ptname { get; set; }
        /// <summary>
        /// 设置或获取类型描述
        /// </summary>
        public string Ptdesc { get; set; }
        /// <summary>
        /// 设置或获取类型接口名称
        /// </summary>
        public string Ptiname { get; set; }
        /// <summary>
        /// 设置或获取父ID
        /// </summary>
        public string Parentid { get; set; }
        /// <summary>
        /// 设置或获取层级路径
        /// </summary>
        public string Levelpath { get; set; }
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
