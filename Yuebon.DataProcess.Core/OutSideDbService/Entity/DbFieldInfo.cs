﻿using System.Collections.Generic;
using Yuebon.DataProcess.Core.common.Entity;

namespace Yuebon.DataProcess.Core.OutSideDbService.Entity
{
    /// <summary>
    /// 表的字段
    /// </summary>
    public class DbFieldInfo
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public DbFieldInfo()
        {
            FieldName = string.Empty;
            Description = string.Empty;
        }
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 系统数据类型，如 int
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// 数据库里面存放的类型。
        /// </summary>
        public string FieldType { get; set; }

        /// <summary>
        /// 代表小数位精度。
        /// </summary>
        public long? FieldScale { get; set; }
        /// <summary>
        /// 数据精度，仅数字类型有效，总共多少位数字（10进制）。
        /// 在MySql里面代表了字段长度
        /// </summary>
        public long? FieldPrecision { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? FieldMaxLength { get; set; }

        /// <summary>
        /// 可空
        /// </summary>
        public bool IsNullable { get; set; }
        /// <summary>
        /// 是否为主键字段
        /// </summary>
        public bool IsIdentity { get; set; }
        /// <summary>
        /// 【未用上】该字段是否自增
        /// </summary>
        public bool Increment { get; set; }


        /// <summary>
        /// 默认值
        /// </summary>
        public string FieldDefaultValue { get; set; }

        #region  数据处理工具用
        /// <summary>
        /// 字段值获取方式
        /// </summary>
        public DataGetType DataGetType { get; set; }

        /// <summary>
        /// 是否具有配置页面
        /// </summary>
        public bool HasPage { get; set; }

        /// <summary>
        /// 配置页面地址
        /// </summary>
        public string ConfigUri { get; set; }

        /// <summary>
        /// 唯一判定项
        /// </summary>
        public bool? Is_SingleDataKey { get; set; }

        /// <summary>
        /// 配置中显示
        /// </summary>
        public bool? Is_Visible { get; set; }

        /// <summary>
        /// 是否为ID字段
        /// </summary>
        public bool? Is_KeyColumn { get; set; }

        /// <summary>
        /// 是否不为空
        /// </summary>
        public bool? Is_NotNull { get; set; }

        /// <summary>
        /// 更新时保留
        /// </summary>
        public bool? Is_NoUpdate { get; set; }

        /// <summary>
        /// 查询不到是否为空
        /// </summary>
        public bool? Is_ChangeWhite { get; set; }
        /// <summary>
        /// 获取方法参数
        /// </summary>
        public string VerifyFunctionParamter { get; set; }
        /// <summary>
        /// 获取方法参数
        /// </summary>
        public string GetFunctionParamter { get; set; }

       
        /// <summary>
        /// 获取方法参数
        /// </summary>
        public List<FieldPlugConfigInfo> VerifyFunctionParamterList { get; set; }

        /// <summary>
        /// 获取方法参数
        /// </summary>
        public List<FieldPlugConfigInfo> GetFunctionParamterList { get; set; }



        #region 数据同步页面所用字段
        /// <summary>
        /// 表执行步骤
        /// </summary>
        public int? TableLevelNum { get; set; }
        /// <summary>
        /// 写入表名称
        /// </summary>
        public string WriteTableName { get; set; }
        /// <summary>
        /// 写入字段
        /// </summary>
        public string WriteFieldName { get; set; }
        /// <summary>
        /// 写入字段描述
        /// </summary>
        public string WriteDescription { get; set; }
        /// <summary>
        /// 读取字段信息
        /// </summary>
        public DbFieldInfo ReadFieldInfo { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }
        /// <summary>
        /// 动态表唯一判定字段
        /// </summary>
        public string Is_DynamicSingle { get; set; }
        /// <summary>
        /// 数据同步页面时其他方式的
        /// </summary>
        public string SyncDataConfParamter { get; set; }
        /// <summary>
        /// 数据同步页面时其他方式的
        /// </summary>
        public List<FieldPlugConfigInfo> SyncDataConfParamterList { get; set; }


        #endregion

        #endregion

    }
}
