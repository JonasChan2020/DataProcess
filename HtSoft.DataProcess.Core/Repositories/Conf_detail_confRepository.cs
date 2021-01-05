using Yuebon.Commons.Repositories;
using Yuebon.Commons.IDbContext;
using HtSoft.DataProcess.IRepositories;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.Repositories
{
    /// <summary>
    /// 仓储接口的实现
    /// </summary>
    public class Conf_detail_confRepository : BaseRepository<Conf_detail_conf, string>, IConf_detail_confRepository
    {
		public Conf_detail_confRepository()
        {
        }

        public Conf_detail_confRepository(IDbContextCore context) : base(context)
        {
        }
    }
}