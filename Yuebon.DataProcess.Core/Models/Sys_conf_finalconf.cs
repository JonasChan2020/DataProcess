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
    [Table("dp_sys_conf_finalconf")]
    [Serializable]
    public class Sys_conf_finalconf : BaseEntity<string>
    {
        /// <summary>
        /// 设置或获取数据模型ID
        /// </summary>
        public string Sys_Conf_Id { get; set; }
        /// <summary>
        /// 设置或获取系统ID
        /// </summary>
        public string Sys_Id { get; set; }
        /// <summary>
        /// 设置或获取配置json串
        /// </summary>
        public string ConfJson { get; set; }
        

    }
}
