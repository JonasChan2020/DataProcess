using Yuebon.Commons.Repositories;
using Yuebon.Commons.IDbContext;
using Yuebon.DataProcess.IRepositories;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Repositories
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