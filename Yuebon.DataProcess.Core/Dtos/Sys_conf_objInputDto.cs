using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Dtos
{
    /// <summary>
    /// 输入对象模型
    /// </summary>
    [AutoMap(typeof(Sys_conf_obj))]
    [Serializable]
    public class Sys_conf_objInputDto: IInputDto<string>
    {
        /// <summary>
        /// 设置或获取配置json串
        /// </summary>
        public string Configjson { get; set; }
        /// <summary>
        /// 设置或获取唯一键值
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 设置或获取系统配置详情ID
        /// </summary>
        public string Sys_conf_detail_id { get; set; }

    }
}
