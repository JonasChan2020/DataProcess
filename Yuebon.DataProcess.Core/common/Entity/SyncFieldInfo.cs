using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuebon.DataProcess.Core.common.Entity
{
    /// <summary>
    /// 同步数据配置时用的记录类
    /// </summary>
    public class SyncFieldInfo
    {
        /// <summary>
        /// 表执行步骤
        /// </summary>
        public int TableLevelNum { get; set; }
        /// <summary>
        /// 写入表名称
        /// </summary>
        public string WriteTableName { get; set; }
        /// <summary>
        /// 写入字段
        /// </summary>
        public string WriteFieldName { get; set; }
        /// <summary>
        /// 写入字段描述
        /// </summary>
        public string WriteDescription { get; set; }
        /// <summary>
        /// 读取表名称
        /// </summary>
        public string ReadTableName { get; set; }
        /// <summary>
        /// 读取字段
        /// </summary>
        public string ReadFieldName { get; set; }
        /// <summary>
        /// 读取字段描述
        /// </summary>
        public string ReadDescription { get; set; }
        /// <summary>
        ///  对应字段
        /// </summary>
        public string CorField { get; set; }
        /// <summary>
        ///  默认值
        /// </summary>
        public string DefaultValue { get; set; }
        /// <summary>
        ///  动态表唯一判定字段
        /// </summary>
        public string Is_DynamicSingle { get; set; }
    }
}
