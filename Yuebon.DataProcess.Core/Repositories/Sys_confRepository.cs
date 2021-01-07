using Yuebon.Commons.Repositories;
using Yuebon.Commons.IDbContext;
using Yuebon.DataProcess.IRepositories;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Repositories
{
    /// <summary>
    /// 仓储接口的实现
    /// </summary>
    public class Sys_confRepository : BaseRepository<Sys_conf, string>, ISys_confRepository
    {
		public Sys_confRepository()
        {
        }

        public Sys_confRepository(IDbContextCore context) : base(context)
        {
        }
    }
}