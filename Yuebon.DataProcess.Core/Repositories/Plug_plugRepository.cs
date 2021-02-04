using Yuebon.Commons.Repositories;
using Yuebon.Commons.IDbContext;
using Yuebon.DataProcess.IRepositories;
using Yuebon.DataProcess.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data;
using Yuebon.Commons.Log;
using System;
using Dapper;

namespace Yuebon.DataProcess.Repositories
{
    /// <summary>
    /// 仓储接口的实现
    /// </summary>
    public class Plug_plugRepository : BaseRepository<Plug_plug, string>, IPlug_plugRepository
    {
        public Plug_plugRepository()
        {
        }

        public Plug_plugRepository(IDbContextCore context) : base(context)
        {
        }

        /// <summary>
        /// 根据查询条件获取数据集合
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public async Task<IEnumerable<Plug_plug>> GetEnableListWithSysIdAsync(string sysId, IDbTransaction trans = null)
        {
            string sql = $"select pg.*  from dp_plug_plug pg "
                + " left join dp_plug_sysrelation pgrela on pg.id=pgrela.plug_id"
                + " where pgrela.sys_id='" + sysId + "' or is_public='1' ";
            return await DapperConn.QueryAsync<Plug_plug>(sql, trans);
        }
    }
}