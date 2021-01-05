using Yuebon.Commons.Repositories;
using Yuebon.Commons.IDbContext;
using HtSoft.DataProcess.IRepositories;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.Repositories
{
    /// <summary>
    /// 仓储接口的实现
    /// </summary>
    public class Conf_confRepository : BaseRepository<Conf_conf, string>, IConf_confRepository
    {
		public Conf_confRepository()
        {
        }

        public Conf_confRepository(IDbContextCore context) : base(context)
        {
        }
    }
}