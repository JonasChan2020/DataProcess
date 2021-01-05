using Yuebon.Commons.Repositories;
using Yuebon.Commons.IDbContext;
using HtSoft.DataProcess.IRepositories;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.Repositories
{
    /// <summary>
    /// 仓储接口的实现
    /// </summary>
    public class Conf_detailRepository : BaseRepository<Conf_detail, string>, IConf_detailRepository
    {
		public Conf_detailRepository()
        {
        }

        public Conf_detailRepository(IDbContextCore context) : base(context)
        {
        }
    }
}