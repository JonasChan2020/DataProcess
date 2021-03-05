using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Yuebon.DataProcess.Core.OutSideDbService.Entity;

namespace Yuebon.DataProcess.Dtos
{
    /// <summary>
    /// 数据输出模型输出对象模型
    /// </summary>
    [Serializable]
    public class Sys_outmodelOutputDto
    {
        /// <summary>
        /// 设置或获取唯一键值
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }
        /// <summary>
        /// 设置或获取系统ID
        /// </summary>
        [MaxLength(100)]
        public string Sysid { get; set; }
        /// <summary>
        /// 设置或获取模型编码
        /// </summary>
        [MaxLength(300)]
        public string Modelcode { get; set; }
        /// <summary>
        /// 设置或获取模型名称
        /// </summary>
        [MaxLength(300)]
        public string Modelname { get; set; }
        /// <summary>
        /// 设置或获取模型分类ID
        /// </summary>
        [MaxLength(100)]
        public string Classify_id { get; set; }
        /// <summary>
        /// 设置或获取状态
        /// </summary>
        [MaxLength(20)]
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
        [MaxLength(500)]
        public string Description { get; set; }
        /// <summary>
        /// 设置或获取创建时间
        /// </summary>
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 设置或获取创建人
        /// </summary>
        [MaxLength(50)]
        public string CreatorUserId { get; set; }
        /// <summary>
        /// 设置或获取最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 设置或获取最后修改人
        /// </summary>
        [MaxLength(50)]
        public string LastModifyUserId { get; set; }
        /// <summary>
        /// 设置或获取删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }
        /// <summary>
        /// 设置或获取删除人
        /// </summary>
        [MaxLength(254)]
        public string DeleteUserId { get; set; }

        /// <summary>
        /// 设置或获取所属系统名称
        /// </summary>
        [MaxLength(0)]
        public string Sys_Name { get; set; }

        /// <summary>
        /// 设置或获取分类名称
        /// </summary>
        [MaxLength(0)]
        public string Classify_Name { get; set; }
        /// <summary>
        /// 设置或获取字段集合
        /// </summary>
        [MaxLength(0)]
        public List<DbFieldInfo> Fileds { get; set; }

    }
}
