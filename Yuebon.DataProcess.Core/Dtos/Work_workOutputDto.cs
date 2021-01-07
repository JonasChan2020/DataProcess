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
    public class Work_workOutputDto
    {
        /// <summary>
        /// 设置或获取唯一键值
        /// </summary>
        [MaxLength(0)]
        public string Id { get; set; }

        /// <summary>
        /// 设置或获取工作编码
        /// </summary>
        [MaxLength(0)]
        public string Wcode { get; set; }

        /// <summary>
        /// 设置或获取配置信息ID
        /// </summary>
        [MaxLength(0)]
        public string Conf_conf_id { get; set; }

        /// <summary>
        /// 设置或获取配置信息详情IDs
        /// </summary>
        [MaxLength(0)]
        public string Conf_detail_ids { get; set; }

        /// <summary>
        /// 设置或获取目标ID
        /// </summary>
        [MaxLength(0)]
        public string Sdid { get; set; }

        /// <summary>
        /// 设置或获取数据路径
        /// </summary>
        [MaxLength(0)]
        public string Datapath { get; set; }

        /// <summary>
        /// 设置或获取状态
        /// </summary>
        [MaxLength(0)]
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
        [MaxLength(0)]
        public string Description { get; set; }

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
        /// 设置或获取最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 设置或获取最后修改人
        /// </summary>
        [MaxLength(0)]
        public string LastModifyUserId { get; set; }

        /// <summary>
        /// 设置或获取删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 设置或获取删除人
        /// </summary>
        [MaxLength(0)]
        public string DeleteUserId { get; set; }


    }
}
