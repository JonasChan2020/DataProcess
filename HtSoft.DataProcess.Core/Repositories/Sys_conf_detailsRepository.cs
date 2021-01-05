using Yuebon.Commons.Repositories;
using Yuebon.Commons.IDbContext;
using HtSoft.DataProcess.IRepositories;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.Repositories
{
    /// <summary>
    /// 仓储接口的实现
    /// </summary>
    public class Sys_conf_detailsRepository : BaseRepository<Sys_conf_details, string>, ISys_conf_detailsRepository
    {
		public Sys_conf_detailsRepository()
        {
        }

        public Sys_conf_detailsRepository(IDbContextCore context) : base(context)
        {
        }
    }
}