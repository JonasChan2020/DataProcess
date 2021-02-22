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
    public class Sys_conf_finalconfOutputDto
    {        /// <summary>
        /// 设置或获取唯一键值
        /// </summary>
        [MaxLength(0)]
        public string Id { get; set; }
        /// <summary>
        /// 设置或获取数据模型ID
        /// </summary>
        [MaxLength(0)]
        public string Sys_Conf_Id { get; set; }
        /// <summary>
        /// 设置或获取系统ID
        /// </summary>
        [MaxLength(0)]
        public string Sys_Id { get; set; }
        /// <summary>
        /// 记录类型：0：完整记录；1：用于同步对应时的列表
        /// </summary>
        [MaxLength(1)]
        public int? ConfType { get; set; }
        /// <summary>
        /// 设置或获取配置json串
        /// </summary>
        [MaxLength(0)]
        public string ConfJson { get; set; }

    }
}
