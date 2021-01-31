using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Yuebon.Commons.IRepositories;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.IRepositories
{
    /// <summary>
    /// 定义仓储接口
    /// </summary>
    public interface IPlug_plugRepository:IRepository<Plug_plug, string>
    {
        /// <summary>
        /// 根据查询条件获取数据集合
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        Task<IEnumerable<Plug_plug>> GetEnableListWithSysIdAsync(string sysId, IDbTransaction trans = null);
    }
}