using System;
using Yuebon.Commons.IServices;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.IServices
{
    /// <summary>
    /// 定义数据输出模型服务接口
    /// </summary>
    public interface ISys_outmodelService:IService<Sys_outmodel,Sys_outmodelOutputDto, string>
    {
    }
}
