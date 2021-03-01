using System;
using Yuebon.Commons.IRepositories;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.IRepositories
{
    /// <summary>
    /// 定义数据输出模型最终查询语句仓储接口
    /// </summary>
    public interface ISys_outmodel_sqlRepository:IRepository<Sys_outmodel_sql, string>
    {
    }
}