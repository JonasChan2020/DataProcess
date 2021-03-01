using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.DataProcess.Models
{
    /// <summary>
    /// 数据输出模型最终查询语句，数据实体对象
    /// </summary>
    [Table("dp_sys_outmodel_sql")]
    [Serializable]
    public class Sys_outmodel_sql:BaseEntity<string>
    {
        /// <summary>
        /// 设置或获取数据输出模型ID
        /// </summary>
        public string Sys_outmodel_id { get; set; }
        /// <summary>
        /// 设置或获取最终sql语句
        /// </summary>
        public string Sqlstr { get; set; }

    }
}
