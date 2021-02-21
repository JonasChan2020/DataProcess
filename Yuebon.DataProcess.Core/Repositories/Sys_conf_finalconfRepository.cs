using Yuebon.Commons.Repositories;
using Yuebon.Commons.IDbContext;
using Yuebon.DataProcess.IRepositories;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Repositories
{
    /// <summary>
    /// 仓储接口的实现
    /// </summary>
    public class Sys_conf_finalconfRepository : BaseRepository<Sys_conf_finalconf, string>, ISys_conf_finalconfRepository
    {
		public Sys_conf_finalconfRepository()
        {
        }

        public Sys_conf_finalconfRepository(IDbContextCore context) : base(context)
        {
        }
    }
}