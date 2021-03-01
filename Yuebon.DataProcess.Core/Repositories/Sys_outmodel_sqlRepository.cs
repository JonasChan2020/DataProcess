using System;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;
using Yuebon.DataProcess.IRepositories;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Repositories
{
    /// <summary>
    /// 数据输出模型最终查询语句仓储接口的实现
    /// </summary>
    public class Sys_outmodel_sqlRepository : BaseRepository<Sys_outmodel_sql, string>, ISys_outmodel_sqlRepository
    {
		public Sys_outmodel_sqlRepository()
        {
        }

        public Sys_outmodel_sqlRepository(IDbContextCore context) : base(context)
        {
        }
    }
}