using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yuebon.DataProcess.Dtos
{
    /// <summary>
    /// 数据输出模型最终查询语句输出对象模型
    /// </summary>
    [Serializable]
    public class Sys_outmodel_sqlOutputDto
    {
        /// <summary>
        /// 设置或获取唯一键值
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }
        /// <summary>
        /// 设置或获取数据输出模型ID
        /// </summary>
        [MaxLength(100)]
        public string Sys_outmodel_id { get; set; }
        /// <summary>
        /// 设置或获取最终sql语句
        /// </summary>
        [MaxLength(16777215)]
        public string Sqlstr { get; set; }

    }
}
