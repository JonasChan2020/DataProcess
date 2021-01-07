using Yuebon.Commons.Repositories;
using Yuebon.Commons.IDbContext;
using Yuebon.DataProcess.IRepositories;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Repositories
{
    /// <summary>
    /// 仓储接口的实现
    /// </summary>
    public class Work_workdetailRepository : BaseRepository<Work_workdetail, string>, IWork_workdetailRepository
    {
		public Work_workdetailRepository()
        {
        }

        public Work_workdetailRepository(IDbContextCore context) : base(context)
        {
        }
    }
}