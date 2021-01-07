using Yuebon.Commons.Repositories;
using Yuebon.Commons.IDbContext;
using Yuebon.DataProcess.IRepositories;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Repositories
{
    /// <summary>
    /// 仓储接口的实现
    /// </summary>
    public class Sys_conf_objRepository : BaseRepository<Sys_conf_obj, string>, ISys_conf_objRepository
    {
		public Sys_conf_objRepository()
        {
        }

        public Sys_conf_objRepository(IDbContextCore context) : base(context)
        {
        }
    }
}