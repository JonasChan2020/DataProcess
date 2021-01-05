using Yuebon.Commons.Repositories;
using Yuebon.Commons.IDbContext;
using HtSoft.DataProcess.IRepositories;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.Repositories
{
    /// <summary>
    /// 仓储接口的实现
    /// </summary>
    public class Sys_classifyRepository : BaseRepository<Sys_classify, string>, ISys_classifyRepository
    {
		public Sys_classifyRepository()
        {
        }

        public Sys_classifyRepository(IDbContextCore context) : base(context)
        {
        }
    }
}