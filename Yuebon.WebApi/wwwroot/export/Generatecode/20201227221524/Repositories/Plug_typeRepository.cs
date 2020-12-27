using System;

using Yuebon.Commons.Repositories;
using HtSoft.DataProcess.IRepositories;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.Repositories
{
    /// <summary>
    /// 插件类型表仓储接口的实现
    /// </summary>
    public class Plug_typeRepository : BaseRepository<Plug_type, string>, IPlug_typeRepository
    {
		public Plug_typeRepository()
        {
        }
    }
}