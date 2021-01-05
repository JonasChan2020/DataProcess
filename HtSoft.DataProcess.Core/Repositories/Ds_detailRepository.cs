using Yuebon.Commons.Repositories;
using Yuebon.Commons.IDbContext;
using HtSoft.DataProcess.IRepositories;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.Repositories
{
    /// <summary>
    /// 仓储接口的实现
    /// </summary>
    public class Ds_detailRepository : BaseRepository<Ds_detail, string>, IDs_detailRepository
    {
		public Ds_detailRepository()
        {
        }

        public Ds_detailRepository(IDbContextCore context) : base(context)
        {
        }
    }
}