using System;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;
using Yuebon.DataProcess.IRepositories;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Repositories
{
    /// <summary>
    /// 数据输出模型列字段信息仓储接口的实现
    /// </summary>
    public class Sys_outmodel_objRepository : BaseRepository<Sys_outmodel_obj, string>, ISys_outmodel_objRepository
    {
		public Sys_outmodel_objRepository()
        {
        }

        public Sys_outmodel_objRepository(IDbContextCore context) : base(context)
        {
        }
    }
}