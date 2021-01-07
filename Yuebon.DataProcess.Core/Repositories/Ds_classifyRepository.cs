using Yuebon.Commons.Repositories;
using Yuebon.Commons.IDbContext;
using Yuebon.DataProcess.IRepositories;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Repositories
{
    /// <summary>
    /// 仓储接口的实现
    /// </summary>
    public class Ds_classifyRepository : BaseRepository<Ds_classify, string>, IDs_classifyRepository
    {
		public Ds_classifyRepository()
        {
        }

        public Ds_classifyRepository(IDbContextCore context) : base(context)
        {
        }
    }
}