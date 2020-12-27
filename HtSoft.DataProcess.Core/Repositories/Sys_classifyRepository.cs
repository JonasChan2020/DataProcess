using System;

using Yuebon.Commons.Repositories;
using HtSoft.DataProcess.IRepositories;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.Repositories
{
    /// <summary>
    /// 系统分类表仓储接口的实现
    /// </summary>
    public class Sys_classifyRepository : BaseRepository<Sys_classify, string>, ISys_classifyRepository
    {
		public Sys_classifyRepository()
        {
        }
    }
}