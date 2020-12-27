using System;

using Yuebon.Commons.Repositories;
using HtSoft.DataProcess.IRepositories;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.Repositories
{
    /// <summary>
    /// 插件表仓储接口的实现
    /// </summary>
    public class Plug_plugRepository : BaseRepository<Plug_plug, string>, IPlug_plugRepository
    {
		public Plug_plugRepository()
        {
        }
    }
}