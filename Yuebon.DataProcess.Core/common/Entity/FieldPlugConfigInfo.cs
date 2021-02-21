namespace Yuebon.DataProcess.Core.common.Entity
{
    /// <summary>
    /// 字段的插件配置信息
    /// </summary>
    public class FieldPlugConfigInfo
    {
        /// <summary>
        /// 执行顺序
        /// </summary>
        public int? levelNum { get; set; }
        /// <summary>
        /// 插件方式
        /// </summary>
        public DataGetType PlugType { get; set; }
        /// <summary>
        /// 配置json串
        /// </summary>
        public string confJson { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? EnableMark { get; set; }
        /// <summary>
        /// 是否具有页面
        /// </summary>
        public bool? HasPage { get; set; }
    }
}
