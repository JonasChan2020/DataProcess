using Yuebon.Commons.Repositories;
using Yuebon.Commons.IDbContext;
using HtSoft.DataProcess.IRepositories;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.Repositories
{
    /// <summary>
    /// 仓储接口的实现
    /// </summary>
    public class Sd_classifyRepository : BaseRepository<Sd_classify, string>, ISd_classifyRepository
    {
		public Sd_classifyRepository()
        {
        }

        public Sd_classifyRepository(IDbContextCore context) : base(context)
        {
        }
    }
}