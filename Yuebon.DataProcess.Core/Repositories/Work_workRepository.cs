using Yuebon.Commons.Repositories;
using Yuebon.Commons.IDbContext;
using Yuebon.DataProcess.IRepositories;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Repositories
{
    /// <summary>
    /// 仓储接口的实现
    /// </summary>
    public class Work_workRepository : BaseRepository<Work_work, string>, IWork_workRepository
    {
		public Work_workRepository()
        {
        }

        public Work_workRepository(IDbContextCore context) : base(context)
        {
        }
    }
}