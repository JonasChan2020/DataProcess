using System;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;
using Yuebon.DataProcess.IRepositories;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Repositories
{
    /// <summary>
    /// 数据输出模型详情表仓储接口的实现
    /// </summary>
    public class Sys_outmodel_detailsRepository : BaseRepository<Sys_outmodel_details, string>, ISys_outmodel_detailsRepository
    {
		public Sys_outmodel_detailsRepository()
        {
        }

        public Sys_outmodel_detailsRepository(IDbContextCore context) : base(context)
        {
        }
    }
}