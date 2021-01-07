using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yuebon.DataProcess.Dtos
{
    /// <summary>
    /// 输出对象模型
    /// </summary>
    [Serializable]
    public class Sys_conf_objOutputDto
    {
        /// <summary>
        /// 设置或获取唯一键值
        /// </summary>
        [MaxLength(0)]
        public string Id { get; set; }

        /// <summary>
        /// 设置或获取系统配置详情ID
        /// </summary>
        [MaxLength(0)]
        public string Sys_conf_detail_id { get; set; }

        /// <summary>
        /// 设置或获取配置json串
        /// </summary>
        [MaxLength(0)]
        public string Configjson { get; set; }


    }
}
