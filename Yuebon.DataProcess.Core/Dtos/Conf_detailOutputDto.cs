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
    public class Conf_detailOutputDto
    {        /// <summary>
        /// 设置或获取唯一键值
        /// </summary>
        [MaxLength(0)]
        public string Id { get; set; }
        /// <summary>
        /// 设置或获取配置ID
        /// </summary>
        [MaxLength(0)]
        public string Conf_id { get; set; }
        /// <summary>
        /// 设置或获取配置类别 0：验证，1：导入
        /// </summary>
        [MaxLength(0)]
        public int? Conf_type { get; set; }

        /// <summary>
        /// 设置或获取表名
        /// </summary>
        [MaxLength(0)]
        public string TbName { get; set; }

        /// <summary>
        /// 设置或获取系统配置ID
        /// </summary>
        [MaxLength(0)]
        public string Sys_conf_id { get; set; }

        /// <summary>
        /// 设置或获取配置详细内容
        /// </summary>
        [MaxLength(0)]
        public string ConfStr { get; set; }

    }
}
