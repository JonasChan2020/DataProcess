using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HtSoft.DataProcess.Dtos
{
    /// <summary>
    /// 数据源表输出对象模型
    /// </summary>
    [Serializable]
    public class Ds_dsOutputDto
    {
        /// <summary>
        /// 设置或获取唯一键值
        /// </summary>
        [MaxLength(300)]
        public string Id { get; set; }
        /// <summary>
        /// 设置或获取数据源编码
        /// </summary>
        [MaxLength(900)]
        public string Dscode { get; set; }
        /// <summary>
        /// 设置或获取数据源名称
        /// </summary>
        [MaxLength(900)]
        public string Dsname { get; set; }
        /// <summary>
        /// 设置或获取数据源描述
        /// </summary>
        [MaxLength(3000)]
        public string Dsdesc { get; set; }
        /// <summary>
        /// 设置或获取数据源分类
        /// </summary>
        [MaxLength(300)]
        public string Dsclassify { get; set; }
        /// <summary>
        /// 设置或获取数据源类型
        /// </summary>
        [MaxLength(300)]
        public string Dstype { get; set; }
        /// <summary>
        /// 设置或获取所属组织ID
        /// </summary>
        [MaxLength(300)]
        public string Orgid { get; set; }
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
