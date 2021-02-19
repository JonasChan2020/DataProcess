using System;
using System.ComponentModel.DataAnnotations.Schema;
using Yuebon.Commons.Models;

namespace Yuebon.DataProcess.Models
{
    /// <summary>
    /// 使用插件的配置信息
    /// </summary>
    [Table("dp_plug_confdetail")]
    [Serializable]
    public class Plug_ConfDetail : BaseEntity<string>
    {
        /// <summary>
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
