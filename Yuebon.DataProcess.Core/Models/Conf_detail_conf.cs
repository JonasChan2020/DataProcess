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
    [Table("dp_conf_detail_conf")]
    [Serializable]
    public class Conf_detail_conf:BaseEntity<string>
    {
        /// <summary>
        /// 设置或获取配置详情ID
        /// </summary>
        public string Conf_detail_id { get; set; }

        /// <summary>
        /// 设置或获取配置json串
        /// </summary>
        public string Confstr { get; set; }


    }
}
