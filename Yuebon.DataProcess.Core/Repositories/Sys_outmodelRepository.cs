using System;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;
using Yuebon.DataProcess.IRepositories;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Repositories
{
    /// <summary>
    /// 数据输出模型仓储接口的实现
    /// </summary>
    public class Sys_outmodelRepository : BaseRepository<Sys_outmodel, string>, ISys_outmodelRepository
    {
		public Sys_outmodelRepository()
        {
        }

        public Sys_outmodelRepository(IDbContextCore context) : base(context)
        {
        }
    }
}