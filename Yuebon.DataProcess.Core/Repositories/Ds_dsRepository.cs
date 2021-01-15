using Yuebon.Commons.Repositories;
using Yuebon.Commons.IDbContext;
using Yuebon.DataProcess.IRepositories;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Repositories
{
    /// <summary>
    /// 仓储接口的实现
    /// </summary>
    public class Ds_dsRepository : BaseRepository<Ds_ds, string>, IDs_dsRepository
    {
		public Ds_dsRepository()
        {
        }

        public Ds_dsRepository(IDbContextCore context) : base(context)
        {
        }
    }
}