using System;
using Yuebon.Commons.IRepositories;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.IRepositories
{
    /// <summary>
    /// 定义数据源配置信息表仓储接口
    /// </summary>
    public interface IDs_dsconfigRepository:IRepository<Ds_dsconfig, string>
    {
    }
}