using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Dtos
{
    /// <summary>
    /// 输入对象模型
    /// </summary>
    [AutoMap(typeof(Conf_detail_conf))]
    [Serializable]
    public class Conf_detail_confInputDto: IInputDto<string>
    {
        /// <summary>
        /// 设置或获取唯一键值
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 设置或获取配置详情ID
        /// </summary>
        public string Conf_detail_id { get; set; }

        /// <summary>
        /// 设置或获取配置json串
        /// </summary>
        public string Confstr { get; set; }


    }
}
