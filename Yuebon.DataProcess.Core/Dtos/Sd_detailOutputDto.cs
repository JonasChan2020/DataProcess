using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yuebon.DataProcess.Dtos
{
    /// <summary>
    /// 目标库详情输出对象模型
    /// </summary>
    [Serializable]
    public class Sd_detailOutputDto
    {
        /// <summary>
        /// 设置或获取唯一主键
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }
        /// <summary>
        /// 设置或获取目标库ID
        /// </summary>
        [MaxLength(100)]
        public string Sd_id { get; set; }
        /// <summary>
        /// 设置或获取表集合信息
        /// </summary>
        [MaxLength(16777215)]
        public string Tbs { get; set; }

    }
}
