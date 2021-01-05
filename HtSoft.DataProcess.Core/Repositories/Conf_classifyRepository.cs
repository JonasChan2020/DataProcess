using Yuebon.Commons.Repositories;
using Yuebon.Commons.IDbContext;
using HtSoft.DataProcess.IRepositories;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.Repositories
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