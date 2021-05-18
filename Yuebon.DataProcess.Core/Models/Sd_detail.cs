using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.DataProcess.Models
{
    /// <summary>
    /// 目标库详情，数据实体对象
    /// </summary>
    [Table("dp_sd_detail")]
    [Serializable]
    public class Sd_detail:BaseEntity<string>
    {
        /// <summary>
        /// 设置或获取目标库ID
        /// </summary>
        public string Sd_id { get; set; }
        /// <summary>
        /// 设置或获取表集合信息
        /// </summary>
        public string Tbs { get; set; }

        /// <summary>
        /// excel表信息
        /// </summary>
        public string ExcelTbs { get; set; }

    }
}
