using Yuebon.Commons.Repositories;
using Yuebon.Commons.IDbContext;
using HtSoft.DataProcess.IRepositories;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.Repositories
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