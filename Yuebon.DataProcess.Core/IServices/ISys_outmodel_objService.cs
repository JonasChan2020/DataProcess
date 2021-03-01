using System;
using Yuebon.Commons.IServices;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.IServices
{
    /// <summary>
    /// 定义数据输出模型列字段信息服务接口
    /// </summary>
    public interface ISys_outmodel_objService:IService<Sys_outmodel_obj,Sys_outmodel_objOutputDto, string>
    {
    }
}
