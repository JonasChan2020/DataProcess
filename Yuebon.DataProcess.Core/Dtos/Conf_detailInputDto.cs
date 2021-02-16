using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Dtos
{
    /// <summary>
    /// 输入对象模型
    /// </summary>
    [AutoMap(typeof(Conf_detail))]
    [Serializable]
    public class Conf_detailInputDto: IInputDto<string>
    {        /// <summary>
        /// 设置或获取唯一键值
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 设置或获取配置ID
        /// </summary>
        public string Conf_id { get; set; }
        /// <summary>
        /// 设置或获取配置类别 0：验证，1：导入
        /// </summary>
        public int? Conf_type { get; set; }

        /// <summary>
        /// 设置或获取表名
        /// </summary>
        public string TbName { get; set; }

        /// <summary>
        /// 设置或获取系统配置ID
        /// </summary>
        public string Sys_conf_id { get; set; }

        /// <summary>
        /// 设置或获取配置详细内容
        /// </summary>
        public string ConfStr { get; set; }

    }
}
