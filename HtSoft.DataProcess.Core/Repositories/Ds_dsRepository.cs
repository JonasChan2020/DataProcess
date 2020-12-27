using System;

using Yuebon.Commons.Repositories;
using HtSoft.DataProcess.IRepositories;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.Repositories
{
    /// <summary>
    /// 数据源表仓储接口的实现
    /// </summary>
    public class Ds_dsRepository : BaseRepository<Ds_ds, string>, IDs_dsRepository
    {
		public Ds_dsRepository()
        {
        }
    }
}