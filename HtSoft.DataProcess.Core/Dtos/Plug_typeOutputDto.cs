using System;
using System.ComponentModel.DataAnnotations;

namespace HtSoft.DataProcess.Dtos
{
    /// <summary>
    /// 插件类型表输出对象模型
    /// </summary>
    [Serializable]
    public class Plug_typeOutputDto
    {
        /// <summary>
        /// 设置或获取唯一键值
        /// </summary>
        [MaxLength(300)]
        public string Id { get; set; }
        /// <summary>
        /// 设置或获取类型编码
        /// </summary>
        [MaxLength(900)]
        public string Ptcode { get; set; }
        /// <summary>
        /// 设置或获取类型名称
        /// </summary>
        [MaxLength(900)]
        public string Ptname { get; set; }
        /// <summary>
        /// 设置或获取类型描述
        /// </summary>
        [MaxLength(3000)]
        public string Ptdesc { get; set; }
        /// <summary>
        /// 设置或获取类型接口名称
        /// </summary>
        [MaxLength(3000)]
        public string Ptiname { get; set; }
        /// <summary>
        /// 设置或获取父ID
        /// </summary>
        [MaxLength(300)]
        public string Parentid { get; set; }
        /// <summary>
        /// 设置或获取层级路径
        /// </summary>
        [MaxLength(12000)]
        public string Levelpath { get; set; }
        /// <summary>
        /// 设置或获取排序
        /// </summary>
        public int? Dsort { get; set; }
        /// <summary>
        /// 设置或获取状态
        /// </summary>
        [MaxLength(60)]
        public string State { get; set; }
        /// <summary>
        /// 设置或获取创建人
        /// </summary>
        [MaxLength(300)]
        public string Cuid { get; set; }
        /// <summary>
        /// 设置或获取创建时间
        /// </summary>
        public DateTime? Ctime { get; set; }
        /// <summary>
        /// 设置或获取更新人
        /// </summary>
        [MaxLength(300)]
        public string Uuid { get; set; }
        /// <summary>
        /// 设置或获取更新时间
        /// </summary>
        public DateTime? Utime { get; set; }

    }
}
