using System;

using Yuebon.Commons.Repositories;
using HtSoft.DataProcess.IRepositories;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.Repositories
{
    /// <summary>
    /// 目标库配置信息表仓储接口的实现
    /// </summary>
    public class Sd_sdconfigRepository : BaseRepository<Sd_sdconfig, string>, ISd_sdconfigRepository
    {
		public Sd_sdconfigRepository()
        {
        }
    }
}