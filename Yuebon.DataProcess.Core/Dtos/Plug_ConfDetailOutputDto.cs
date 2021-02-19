using System;
using System.ComponentModel.DataAnnotations;

namespace Yuebon.DataProcess.Dtos
{
    /// <summary>
    /// 输出对象模型
    /// </summary>
    [Serializable]
    public class Plug_ConfDetailOutputDto
    {        /// <summary>
        /// 设置或获取唯一键值
        /// </summary>
        [MaxLength(100)]
        public string Id { get; set; }
        // <summary>
        /// 设置或获取主项ID
        /// </summary>
        [MaxLength(100)]
        public string Obj_Id { get; set; }

        /// <summary>
        /// 设置或获取配置类型
        /// </summary>
        [MaxLength(1)]
        public int? ConfType { get; set; }


        /// <summary>
        /// 设置或获取配置字符串
        /// </summary>
        [MaxLength(0)]
        public string ConfJson { get; set; }

    }
}
