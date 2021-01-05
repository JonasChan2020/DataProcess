using Yuebon.Commons.Repositories;
using Yuebon.Commons.IDbContext;
using HtSoft.DataProcess.IRepositories;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.Repositories
{
    /// <summary>
    /// 仓储接口的实现
    /// </summary>
    public class Plug_typeRepository : BaseRepository<Plug_type, string>, IPlug_typeRepository
    {
		public Plug_typeRepository()
        {
        }

        public Plug_typeRepository(IDbContextCore context) : base(context)
        {
        }
    }
}