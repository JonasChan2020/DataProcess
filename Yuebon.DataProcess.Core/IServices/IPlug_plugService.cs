using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.Commons.IServices;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.IServices
{
    /// <summary>
    /// 定义服务接口
    /// </summary>
    public interface IPlug_plugService:IService<Plug_plug,Plug_plugOutputDto, string>
    {
        Task<List<Plug_plugOutputDto>> GetEnableListWithSys(string SysId, string ptype);
    }
}
