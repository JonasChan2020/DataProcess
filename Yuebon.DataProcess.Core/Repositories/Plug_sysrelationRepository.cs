using Yuebon.Commons.Repositories;
using Yuebon.Commons.IDbContext;
using Yuebon.DataProcess.IRepositories;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Repositories
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