using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yuebon.DataProcess.Dtos
{
    /// <summary>
    /// 输出对象模型
    /// </summary>
    [Serializable]
    public class Ds_dsOutputDto
    {
        /// <summary>
        /// 设置或获取数据源分类
        /// </summary>
        [MaxLength(0)]
        public string Classify_id { get; set; }

        /// <summary>
        /// 设置或获取分类名称
        /// </summary>
        [MaxLength(0)]
        public string Classify_Name { get; set; }
        /// <summary>
        /// 设置或获取连接信息
        /// </summary>
        [MaxLength(0)]
        public string Connectionstr { get; set; }
        /// <summary>
        /// 设置或获取创建时间
        /// </summary>
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 设置或获取创建人
        /// </summary>
        [MaxLength(0)]
        public string CreatorUserId { get; set; }
        /// <summary>
        /// 设置或获取删除标记
        /// </summary>
        public bool? DeleteMark { get; set; }
        /// <summary>
        /// 设置或获取删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }
        /// <summary>
        /// 设置或获取删除人
        /// </summary>
        [MaxLength(0)]
        public string DeleteUserId { get; set; }
        /// <summary>
        /// 设置或获取描述
        /// </summary>
        [MaxLength(0)]
        public string Description { get; set; }
        /// <summary>
        /// 设置或获取数据源编码
        /// </summary>
        [MaxLength(0)]
        public string Dscode { get; set; }
        /// <summary>
        /// 设置或获取数据源名称
        /// </summary>
        [MaxLength(0)]
        public string Dsname { get; set; }
        /// <summary>
        /// 设置或获取数据源类型
        /// </summary>
        [MaxLength(0)]
        public string Dstype { get; set; }
        /// <summary>
        /// 设置或获取启用标记
        /// </summary>
        public bool? EnabledMark { get; set; }
        /// <summary>
        /// 设置或获取唯一键值
        /// </summary>
        [MaxLength(0)]
        public string Id { get; set; }
        /// <summary>
        /// 设置或获取最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 设置或获取最后修改人
        /// </summary>
        [MaxLength(0)]
        public string LastModifyUserId { get; set; }
        /// <summary>
        /// 设置或获取排序字段
        /// </summary>
        public int? SortCode { get; set; }
        /// <summary>
        /// 设置或获取状态
        /// </summary>
        [MaxLength(0)]
        public string State { get; set; }

    }
}
