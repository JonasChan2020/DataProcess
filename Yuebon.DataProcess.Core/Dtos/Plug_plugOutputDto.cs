using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yuebon.DataProcess.Dtos
{
    /// <summary>
    /// 输出对象模型
    /// </summary>
    [Serializable]
    public class Plug_plugOutputDto
    {
        /// <summary>
        /// 设置或获取创建时间
        /// </summary>
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 设置或获取创建人
        /// </summary>
        [MaxLength(0)]
        public string CreatorUserId { get; set; }
        /// <summary>
        /// 设置或获取删除标记
        /// </summary>
        public bool? DeleteMark { get; set; }
        /// <summary>
        /// 设置或获取删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }
        /// <summary>
        /// 设置或获取删除人
        /// </summary>
        [MaxLength(0)]
        public string DeleteUserId { get; set; }
        /// <summary>
        /// 设置或获取描述
        /// </summary>
        [MaxLength(0)]
        public string Description { get; set; }
        /// <summary>
        /// 设置或获取启用标记
        /// </summary>
        public bool? EnabledMark { get; set; }
        /// <summary>
        /// 设置或获取唯一键值
        /// </summary>
        [MaxLength(0)]
        public string Id { get; set; }
        /// <summary>
        /// 设置或获取是否为公共插件 0否，1是
        /// </summary>
        public bool? Is_public { get; set; }

        /// <summary>
        /// 设置或获取是否具有编辑页面
        /// </summary>
        public bool? HasPage { get; set; }

        /// <summary>
        /// 设置或获取最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 设置或获取最后修改人
        /// </summary>
        [MaxLength(0)]
        public string LastModifyUserId { get; set; }
        /// <summary>
        /// 设置或获取插件编码
        /// </summary>
        [MaxLength(0)]
        public string Pcode { get; set; }
        /// <summary>
        /// 设置或获取插件描述
        /// </summary>
        [MaxLength(0)]
        public string Pdesc { get; set; }
        /// <summary>
        /// 设置或获取插件名称
        /// </summary>
        [MaxLength(0)]
        public string Pname { get; set; }
        /// <summary>
        /// 设置或获取插件路径
        /// </summary>
        [MaxLength(0)]
        public string Ppath { get; set; }
        /// <summary>
        /// 设置或获取标签
        /// </summary>
        [MaxLength(0)]
        public string Ptag { get; set; }
        /// <summary>
        /// 设置或获取插件类型
        /// </summary>
        [MaxLength(0)]
        public string Ptype { get; set; }

        /// <summary>
        /// 设置或获取分类名称
        /// </summary>
        [MaxLength(0)]
        public string Classify_Name { get; set; }
        /// <summary>
        /// 设置或获取排序字段
        /// </summary>
        public int? SortCode { get; set; }
        /// <summary>
        /// 设置或获取状态
        /// </summary>
        [MaxLength(0)]
        public string State { get; set; }

    }
}
