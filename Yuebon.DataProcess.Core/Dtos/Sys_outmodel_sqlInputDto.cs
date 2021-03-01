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
    /// 数据输出模型最终查询语句输入对象模型
    /// </summary>
    [AutoMap(typeof(Sys_outmodel_sql))]
    [Serializable]
    public class Sys_outmodel_sqlInputDto: IInputDto<string>
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
        /// 设置或获取最终sql语句
        /// </summary>
        public string Sqlstr { get; set; }

    }
}
