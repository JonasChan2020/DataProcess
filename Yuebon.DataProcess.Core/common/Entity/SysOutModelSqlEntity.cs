using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuebon.DataProcess.Core.common.Entity
{
    /// <summary>
    /// 用于数据输出模型中的字段配置
    /// </summary>
    public class SysOutModelSqlEntity
    {
        /// <summary>
        /// 数据集名称
        /// </summary>
        public string Tbname { get; set; }
        /// <summary>
        /// 字段编码
        /// </summary>
        public string ColumnCode { get; set; }
        /// <summary>
        /// 字段描述
        /// </summary>
        public string ColumnDescription { get; set; }
        /// <summary>
        /// 字段名称
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 数据集字段集合
        /// </summary>
        public object columnList { get; set; }
    }
}
