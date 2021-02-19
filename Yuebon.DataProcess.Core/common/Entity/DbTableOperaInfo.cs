using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuebon.DataProcess.Core.common.Entity
{
    /// <summary>
    /// 表操作实体类
    /// </summary>
    public class DbTableOperaInfo
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 操作方式
        /// </summary>
        public string OperaType { get; set; }
        /// <summary>
        /// 操作参数字符串
        /// </summary>
        public string OperaParamers { get; set; }
    }
}
