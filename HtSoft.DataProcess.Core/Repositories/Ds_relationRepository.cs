using Yuebon.Commons.Repositories;
using Yuebon.Commons.IDbContext;
using HtSoft.DataProcess.IRepositories;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.Repositories
{
    /// <summary>
    /// 仓储接口的实现
    /// </summary>
    public class Ds_relationRepository : BaseRepository<Ds_relation, string>, IDs_relationRepository
    {
		public Ds_relationRepository()
        {
        }

        public Ds_relationRepository(IDbContextCore context) : base(context)
        {
        }
    }
}