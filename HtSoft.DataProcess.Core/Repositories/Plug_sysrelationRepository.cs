using Yuebon.Commons.Repositories;
using Yuebon.Commons.IDbContext;
using HtSoft.DataProcess.IRepositories;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.Repositories
{
    /// <summary>
    /// 仓储接口的实现
    /// </summary>
    public class Plug_sysrelationRepository : BaseRepository<Plug_sysrelation, string>, IPlug_sysrelationRepository
    {
		public Plug_sysrelationRepository()
        {
        }

        public Plug_sysrelationRepository(IDbContextCore context) : base(context)
        {
        }
    }
}