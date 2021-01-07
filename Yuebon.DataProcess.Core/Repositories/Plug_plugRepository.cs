using Yuebon.Commons.Repositories;
using Yuebon.Commons.IDbContext;
using Yuebon.DataProcess.IRepositories;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Repositories
{
    /// <summary>
    /// 仓储接口的实现
    /// </summary>
    public class Plug_plugRepository : BaseRepository<Plug_plug, string>, IPlug_plugRepository
    {
		public Plug_plugRepository()
        {
        }

        public Plug_plugRepository(IDbContextCore context) : base(context)
        {
        }
    }
}