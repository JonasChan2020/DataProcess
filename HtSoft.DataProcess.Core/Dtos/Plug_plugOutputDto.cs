using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HtSoft.DataProcess.Dtos
{
    /// <summary>
    /// 插件表输出对象模型
    /// </summary>
    [Serializable]
    public class Plug_plugOutputDto
    {
        /// <summary>
        /// 设置或获取唯一键值
        /// </summary>
        [MaxLength(300)]
        public string Id { get; set; }
        /// <summary>
        /// 设置或获取插件编码
        /// </summary>
        [MaxLength(900)]
        public string Pcode { get; set; }
        /// <summary>
        /// 设置或获取插件名称
        /// </summary>
        [MaxLength(900)]
        public string Pname { get; set; }
        /// <summary>
        /// 设置或获取插件描述
        /// </summary>
        [MaxLength(6000)]
        public string Pdesc { get; set; }
        /// <summary>
        /// 设置或获取所属目标库ID
        /// </summary>
        [MaxLength(300)]
        public string Dbid { get; set; }
        /// <summary>
        /// 设置或获取插件类型
        /// </summary>
        [MaxLength(300)]
        public string Ptype { get; set; }
        /// <summary>
        /// 设置或获取插件路径
        /// </summary>
        [MaxLength(6000)]
        public string Ppath { get; set; }
        /// <summary>
        /// 设置或获取标签
        /// </summary>
        [MaxLength(900)]
        public string Ptag { get; set; }
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
