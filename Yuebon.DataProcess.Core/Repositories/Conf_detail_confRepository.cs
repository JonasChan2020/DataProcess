using Yuebon.Commons.Repositories;
using Yuebon.Commons.IDbContext;
using Yuebon.DataProcess.IRepositories;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Repositories
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