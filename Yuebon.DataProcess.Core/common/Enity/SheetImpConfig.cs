using System;
using System.Collections.Generic;

namespace Yuebon.DataProcess.Core.common.Enity
{
    #region sheet导入配置类
    /// <summary>
    /// 导入配置类
    /// </summary>
    public class SheetImpConfig : CustomEntityBase
    {
        /// <summary>
        /// 执行序号
        /// </summary>
        public string tbName { get; set; }
        /// <summary>
        /// 执行序号
        /// </summary>
        public int stepNum { get; set; }
        /// <summary>
        /// 关联表ID
        /// </summary>
        public string cgsID { get; set; }
        /// <summary>
        /// 导入配置
        /// </summary>
        public List<DbImportTemplet> dbImportTempletList { get; set; }
    }

    #endregion

    #region 导入配置类
    /// <summary>
    /// 导入配置类
    /// </summary>
    public class DbImportTemplet : CustomEntityBase
    {
        /// <summary>
        /// 执行序号
        /// </summary>
        public int stepNum { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string tbName { get; set; }
        /// <summary>
        /// 表类型，0：横表；1：动态表；
        /// </summary>
        public int tbType { get; set; }
        /// <summary>
        /// 列配置
        /// </summary>
        public List<TempletColumn> templeColList { get; set; }
        /// <summary>
        /// 表模型
        /// </summary>
        public tableML tb { get; set; }
    }
    #endregion

    #region 数据库字段实体
    /// <summary>
    /// 数据库字段实体
    /// </summary>
    public class TempletColumn : CustomEntityBase
    {
        /// <summary>
        /// 行号（动态表合并用）
        /// </summary>
        public int rowIndex { get; set; }
        /// <summary>
        /// 列号
        /// </summary>
        public int columnIndex { get; set; }
        /// <summary>
        /// 字段名称
        /// </summary>
        public string columnName { get; set; }
        /// <summary>
        /// 字段值
        /// </summary>
        public string columnValue { get; set; }
        /// <summary>
        /// json键
        /// </summary>
        public string KeyDataType { get; set; }
        /// <summary>
        /// json值
        /// </summary>
        public string ValueDataCovertType { get; set; }
        /// <summary>
        /// 属性分类查询
        /// </summary>
        public string ClassifySearch { get; set; }
        /// <summary>
        /// 上级获取方式查询字段
        /// </summary>
        public string ParentSearChCol { get; set; }
        /// <summary>
        /// 上级获取方式辅助相等字段
        /// </summary>
        public string ParentSameCol { get; set; }
        /// <summary>
        /// 上级获取方式转换字段
        /// </summary>
        public string ParenSaveCol { get; set; }
        /// <summary>
        /// 数据获取类型
        /// </summary>
        public string dataType { get; set; }
        /// <summary>
        /// 字段数据类型
        /// </summary>
        public string columnType { get; set; }
        /// <summary>
        /// 字段数据长度
        /// </summary>
        public string columnLenght { get; set; }
        /// <summary>
        /// 是否为唯一判定条件
        /// </summary>
        public string isKey { get; set; }
        /// <summary>
        /// 是否为唯一关键字段
        /// </summary>
        public string isSingleKey { get; set; }
        /// <summary>
        /// 更新时不更新
        /// </summary>
        public string noUpdates { get; set; }
        /// <summary>
        /// 不能为空
        /// </summary>
        public string notNulls { get; set; }
        /// <summary>
        /// 对应导入模板列
        /// </summary>
        public string modelColIndex { get; set; }
        /// <summary>
        /// 导入模板配置值
        /// </summary>
        public string modelValue { get; set; }
        /// <summary>
        /// 导入模板配置是否为唯一判定项（动态表，竖表）
        /// </summary>
        public string modelIsSingle { get; set; }
        /// <summary>
        /// 是否配置（0：未配合；1：已配置）
        /// </summary>
        public int isConfig { get; set; }
        public string PropertyTbName { get; set; }
    }
    #endregion

    #region 自定义配置字段类
    /// <summary>
    /// 自定义配置字段类
    /// </summary>
    public class CustomColumnModel : CustomEntityBase
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string columnName { get; set; }
        /// <summary>
        /// 对应模版列号
        /// </summary>
        public string customModelIndex { get; set; }
        /// <summary>
        /// 字段值
        /// </summary>
        public string columnValue { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string defaultValue { get; set; }
        /// <summary>
        /// 是否为动态添加列唯一标识字段
        /// </summary>
        public string isDtSingle { get; set; }
        /// <summary>
        /// 是否为导入结果记录列
        /// </summary>
        public string isImportContent { get; set; }
        /// <summary>
        /// 动态添加列的行判断标识
        /// </summary>
        public string drowIndex { get; set; }
    }
    #endregion

    #region sheet表行实体类
    /// <summary>
    /// 表格实体类
    /// </summary>
    public class tableML : CustomEntityBase
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string tbName { get; set; }
        /// <summary>
        /// 表序号
        /// </summary>
        public int tbIndex { get; set; }
        /// <summary>
        /// 表类型
        /// </summary>
        public int tbType { get; set; }
        /// <summary>
        /// 表格内容
        /// </summary>
        public List<sheetRow> tbRows { get; set; }
    }


    /// <summary>
    /// 行信息
    /// </summary>
    [Serializable]
    public class sheetRow : CustomEntityBase
    {
        /// <summary>
        /// 实体类行序号（动态表合并用）
        /// </summary>
        public int listRowIndex { get; set; }
        /// <summary>
        /// 行序号
        /// </summary>
        public int rowIndex { get; set; }
        /// <summary>
        /// 行标识
        /// </summary>
        public string gpid { get; set; }
        /// <summary>
        /// s数据库内行标识
        /// </summary>
        public string dbGpid { get; set; }
        /// <summary>
        /// 列集合
        /// </summary>
        public List<sheetCol> colList { get; set; }

    }
    #endregion

    #region 列信息
    [Serializable]
    public class sheetCol : CustomEntityBase
    {
        /// <summary>
        /// 列序号
        /// </summary>
        public int colIndex { get; set; }
        /// <summary>
        /// 字段名称
        /// </summary>
        public string columnName { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string values { get; set; }
    }

    #endregion

    #region 系统配置字段类
    /// <summary>
    /// 系统配置字段类
    /// </summary>
    public class columnModel : CustomEntityBase
    {
        /// <summary>
        /// 列序号
        /// </summary>
        public int colIndex { get; set; }
        /// <summary>
        /// 字段名称
        /// </summary>
        public string columnName { get; set; }
        /// <summary>
        /// 字段别名
        /// </summary>
        public string asName { get; set; }
        /// <summary>
        /// 数据获取类型
        /// </summary>
        public string dataType { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string values { get; set; }
        /// <summary>
        /// 字段数据类型
        /// </summary>
        public string columnType { get; set; }
        /// <summary>
        /// 字段数据长度
        /// </summary>
        public string columnLenght { get; set; }
        /// <summary>
        /// 是否为唯一判定条件
        /// </summary>
        public bool isSingle { get; set; }
        /// <summary>
        /// 是否在配置中显示
        /// </summary>
        public bool visible { get; set; }
        /// <summary>
        /// 是否更新时保留
        /// </summary>
        public bool noUpdate { get; set; }
        /// <summary>
        /// 是否不能为空
        /// </summary>
        public bool notNull { get; set; }
        /// <summary>
        /// 对应导入模版的列
        /// </summary>
        public string modelColumnIndex { get; set; }
        /// <summary>
        /// 对应值
        /// </summary>
        public string modelColumnValue { get; set; }
        /// <summary>
        /// 最终值
        /// </summary>
        public string finalValue { get; set; }
        /// <summary>
        /// 是否为动态添加列唯一标识字段
        /// </summary>
        public string isDtSingle { get; set; }
    }
    #endregion

    #region sql查询参数类
    /// <summary>
    /// 结果缓存实体
    /// </summary>
    public class sqlParamters : CustomEntityBase
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string parName { get; set; }
        /// <summary>
        /// 参数值
        /// </summary>
        public List<string> parValues { get; set; }
        /// <summary>
        /// 参数类型
        /// </summary>
        public string parOts { get; set; }
        /// <summary>
        /// 参数长度
        /// </summary>
        public string parSize { get; set; }
    }
    #endregion
}
