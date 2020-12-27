using System;

using Yuebon.Commons.Repositories;
using HtSoft.DataProcess.IRepositories;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.Repositories
{
    /// <summary>
    /// 系统表仓储接口的实现
    /// </summary>
    public class Sys_sysRepository : BaseRepository<Sys_sys, string>, ISys_sysRepository
    {
		public Sys_sysRepository()
        {
        }
    }
}