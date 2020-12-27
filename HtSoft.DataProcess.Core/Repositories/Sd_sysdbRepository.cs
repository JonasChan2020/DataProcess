using System;

using Yuebon.Commons.Repositories;
using HtSoft.DataProcess.IRepositories;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.Repositories
{
    /// <summary>
    /// 目标库表仓储接口的实现
    /// </summary>
    public class Sd_sysdbRepository : BaseRepository<Sd_sysdb, string>, ISd_sysdbRepository
    {
		public Sd_sysdbRepository()
        {
        }
    }
}