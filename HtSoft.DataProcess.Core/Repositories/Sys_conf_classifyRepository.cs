using Yuebon.Commons.Repositories;
using Yuebon.Commons.IDbContext;
using HtSoft.DataProcess.IRepositories;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.Repositories
{
    /// <summary>
    /// 仓储接口的实现
    /// </summary>
    public class Sys_conf_classifyRepository : BaseRepository<Sys_conf_classify, string>, ISys_conf_classifyRepository
    {
		public Sys_conf_classifyRepository()
        {
        }

        public Sys_conf_classifyRepository(IDbContextCore context) : base(context)
        {
        }
    }
}