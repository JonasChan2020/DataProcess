using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.Commons.Models;
using Yuebon.Commons.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Dtos
{
    /// <summary>
    /// 数据输出模型列字段信息输入对象模型
    /// </summary>
    [AutoMap(typeof(Sys_outmodel_obj))]
    [Serializable]
    public class Sys_outmodel_objInputDto: IInputDto<string>
    {
        /// <summary>
        /// 设置或获取唯一键值
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 设置或获取数据输出模型ID
        /// </summary>
        public string Sys_outmodel_id { get; set; }
        /// <summary>
        /// 设置或获取列字段json
        /// </summary>
        public string Confstr { get; set; }

    }
}
