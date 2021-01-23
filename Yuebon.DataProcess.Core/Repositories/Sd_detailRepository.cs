using System;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;
using Yuebon.DataProcess.IRepositories;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Repositories
{
    /// <summary>
    /// 目标库详情仓储接口的实现
    /// </summary>
    public class Sd_detailRepository : BaseRepository<Sd_detail, string>, ISd_detailRepository
    {
		public Sd_detailRepository()
        {
        }

        public Sd_detailRepository(IDbContextCore context) : base(context)
        {
        }
    }
}