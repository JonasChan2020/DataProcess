using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.DataProcess.Models
{
    /// <summary>
    /// ，数据实体对象
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
        /// 设置或获取是否为公共插件 0否，1是
        /// </summary>
        public bool? Is_public { get; set; }

        /// <summary>
        /// 设置或获取状态
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 设置或获取排序字段
        /// </summary>
        public int? SortCode { get; set; }

        /// <summary>
        /// 设置或获取删除标记
        /// </summary>
        public bool? DeleteMark { get; set; }

        /// <summary>
        /// 设置或获取启用标记
        /// </summary>
        public bool? EnabledMark { get; set; }

        /// <summary>
        /// 设置或获取描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 设置或获取创建时间
        /// </summary>
        public DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 设置或获取创建人
        /// </summary>
        public string CreatorUserId { get; set; }

        /// <summary>
        /// 设置或获取最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 设置或获取最后修改人
        /// </summary>
        public string LastModifyUserId { get; set; }

        /// <summary>
        /// 设置或获取删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 设置或获取删除人
        /// </summary>
        public string DeleteUserId { get; set; }


    }
}
