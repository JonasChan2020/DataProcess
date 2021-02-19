using Yuebon.Commons.Repositories;
using Yuebon.Commons.IDbContext;
using Yuebon.DataProcess.IRepositories;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Repositories
{
    /// <summary>
    /// 仓储接口的实现
    /// </summary>
    public class Plug_ConfDetailRepository : BaseRepository<Plug_ConfDetail, string>, IPlug_ConfDetailRepository
    {
		public Plug_ConfDetailRepository()
        {
        }

        public Plug_ConfDetailRepository(IDbContextCore context) : base(context)
        {
        }
    }
}