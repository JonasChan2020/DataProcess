using Yuebon.Commons.Repositories;
using Yuebon.Commons.IDbContext;
using Yuebon.DataProcess.IRepositories;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Repositories
{
    /// <summary>
    /// 仓储接口的实现
    /// </summary>
    public class Sd_sysdbRepository : BaseRepository<Sd_sysdb, string>, ISd_sysdbRepository
    {
		public Sd_sysdbRepository()
        {
        }

        public Sd_sysdbRepository(IDbContextCore context) : base(context)
        {
        }
    }
}