using System;
using Yuebon.Commons.IServices;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.IServices
{
    /// <summary>
    /// 定义数据输出模型分类表服务接口
    /// </summary>
    public interface ISys_outmodel_classifyService:IService<Sys_outmodel_classify,Sys_outmodel_classifyOutputDto, string>
    {
    }
}
