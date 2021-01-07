using Yuebon.Commons.Repositories;
using Yuebon.Commons.IDbContext;
using Yuebon.DataProcess.IRepositories;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Repositories
{
    /// <summary>
    /// 仓储接口的实现
    /// </summary>
    public class Conf_classifyRepository : BaseRepository<Conf_classify, string>, IConf_classifyRepository
    {
		public Conf_classifyRepository()
        {
        }

        public Conf_classifyRepository(IDbContextCore context) : base(context)
        {
        }
    }
}