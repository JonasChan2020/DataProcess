using System;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;
using Yuebon.DataProcess.IRepositories;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Repositories
{
    /// <summary>
    /// 数据输出模型分类表仓储接口的实现
    /// </summary>
    public class Sys_outmodel_classifyRepository : BaseRepository<Sys_outmodel_classify, string>, ISys_outmodel_classifyRepository
    {
		public Sys_outmodel_classifyRepository()
        {
        }

        public Sys_outmodel_classifyRepository(IDbContextCore context) : base(context)
        {
        }
    }
}