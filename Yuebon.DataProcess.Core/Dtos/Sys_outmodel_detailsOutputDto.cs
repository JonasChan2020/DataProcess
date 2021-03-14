using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Yuebon.DataProcess.Core.OutSideDbService.Entity;

namespace Yuebon.DataProcess.Dtos
{
    /// <summary>
    /// 数据输出模型详情表输出对象模型
    /// </summary>
    [Serializable]
    public class Sys_outmodel_detailsOutputDto
    {
        /// <summary>
        /// 设置或获取唯一键值
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }
        /// <summary>
        /// 设置或获取输出模型ID
        /// </summary>
        [MaxLength(100)]
        public string Sys_outmodel_id { get; set; }
        /// <summary>
        /// 设置或获取表名
        /// </summary>
        [MaxLength(500)]
        public string Tbname { get; set; }
        /// <summary>
        /// 设置或获取条件
        /// </summary>
        [MaxLength(2000)]
        public string Fiterstr { get; set; }
        /// <summary>
        /// 设置或获取执行顺序
        /// </summary>
        public int? Levelnum { get; set; }
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
        /// 字段列表
        /// </summary>
        [MaxLength(0)]
        public List<DbFieldInfo> Fileds { get; set; }

    }
}
