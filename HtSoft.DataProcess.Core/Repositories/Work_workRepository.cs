using Yuebon.Commons.Repositories;
using Yuebon.Commons.IDbContext;
using HtSoft.DataProcess.IRepositories;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.Repositories
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