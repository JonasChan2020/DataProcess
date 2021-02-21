using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Dtos
{
    /// <summary>
    /// 输入对象模型
    /// </summary>
    [AutoMap(typeof(Sys_conf_finalconf))]
    [Serializable]
    public class Sys_conf_finalconfInputDto : IInputDto<string>
    {        /// <summary>
        /// 设置或获取唯一键值
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 设置或获取数据模型ID
        /// </summary>
        public string Sys_Conf_Id { get; set; }
        /// <summary>
        /// 设置或获取系统ID
        /// </summary>
        public string Sys_Id { get; set; }
        /// <summary>
        /// 设置或获取配置json串
        /// </summary>
        public string ConfJson { get; set; }

    }
}
