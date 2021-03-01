using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yuebon.DataProcess.Dtos
{
    /// <summary>
    /// 数据输出模型列字段信息输出对象模型
    /// </summary>
    [Serializable]
    public class Sys_outmodel_objOutputDto
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
        /// 设置或获取列字段json
        /// </summary>
        [MaxLength(16777215)]
        public string Confstr { get; set; }

    }
}
