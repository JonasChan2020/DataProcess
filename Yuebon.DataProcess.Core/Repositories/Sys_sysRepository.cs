using Yuebon.Commons.Repositories;
using Yuebon.Commons.IDbContext;
using Yuebon.DataProcess.IRepositories;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Repositories
{
    /// <summary>
    /// 仓储接口的实现
    /// </summary>
    public class Sys_sysRepository : BaseRepository<Sys_sys, string>, ISys_sysRepository
    {
		public Sys_sysRepository()
        {
        }

        public Sys_sysRepository(IDbContextCore context) : base(context)
        {
        }
    }
}