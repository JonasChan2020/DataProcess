using AutoMapper;
using System;
using Yuebon.Commons.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Dtos
{
    /// <summary>
    /// 输入对象模型
    /// </summary>
    [AutoMap(typeof(Plug_ConfDetail))]
    [Serializable]
    public class Plug_ConfDetailInputDto : IInputDto<string>
    {        /// <summary>
        /// 设置或获取唯一键值
        /// </summary>
        public string Id { get; set; }
        // <summary>
        /// 设置或获取主项ID
        /// </summary>
        public string Obj_Id { get; set; }
        /// <summary>
        /// 设置或获取配置类型
        /// </summary>
        public int? ConfType { get; set; }


        /// <summary>
        /// 设置或获取配置字符串
        /// </summary>
        public string ConfJson { get; set; }

    }
}
