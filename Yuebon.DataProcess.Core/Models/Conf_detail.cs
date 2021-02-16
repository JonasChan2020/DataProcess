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
    [Table("dp_conf_detail")]
    [Serializable]
    public class Conf_detail:BaseEntity<string>
    {
        /// <summary>
        /// 设置或获取配置ID
        /// </summary>
        public string Conf_id { get; set; }
        /// <summary>
        /// 设置或获取配置类别 0：验证，1：导入
        /// </summary>
        public int? Conf_type { get; set; }

        /// <summary>
        /// 设置或获取表名
        /// </summary>
        public string TbName { get; set; }

        /// <summary>
        /// 设置或获取系统配置ID
        /// </summary>
        public string Sys_conf_id { get; set; }

        /// <summary>
        /// 设置或获取配置详细内容
        /// </summary>
        public string ConfStr { get; set; }


    }
}
