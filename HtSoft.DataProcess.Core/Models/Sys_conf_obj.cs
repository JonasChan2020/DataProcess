using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace HtSoft.DataProcess.Models
{
    /// <summary>
    /// ，数据实体对象
    /// </summary>
    [Table("dp_sys_conf_obj")]
    [Serializable]
    public class Sys_conf_obj:BaseEntity<string>
    {
        /// <summary>
        /// 设置或获取系统配置详情ID
        /// </summary>
        public string Sys_conf_detail_id { get; set; }
        /// <summary>
        /// 设置或获取配置json串
        /// </summary>
        public string Configjson { get; set; }

    }
}
