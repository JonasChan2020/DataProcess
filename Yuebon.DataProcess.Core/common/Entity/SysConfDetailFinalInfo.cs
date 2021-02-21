using System.Collections.Generic;
using Yuebon.DataProcess.Core.OutSideDbService.Entity;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Core.common.Entity
{
    public class SysConfDetailFinalInfo
    {
        /// <summary>
        /// 数据模型详情配置ID
        /// </summary>
        public string SysConfDetail_Id { get; set; }

        /// <summary>
        /// 执行顺序
        /// </summary>
        public int LevelNum { get; set; }

        /// <summary>
        /// 数据模型详情配置信息
        /// </summary>
        public Sys_conf_details SysConfDetailInfo { get; set; }

        /// <summary>
        /// 字段配置信息
        /// </summary>
        public List<DbFieldInfo> FieldList { get; set; }

    }
}
