using System;

using Yuebon.Commons.Repositories;
using HtSoft.DataProcess.IRepositories;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.Repositories
{
    /// <summary>
    /// 数据源配置信息表仓储接口的实现
    /// </summary>
    public class Ds_dsconfigRepository : BaseRepository<Ds_dsconfig, string>, IDs_dsconfigRepository
    {
		public Ds_dsconfigRepository()
        {
        }
    }
}