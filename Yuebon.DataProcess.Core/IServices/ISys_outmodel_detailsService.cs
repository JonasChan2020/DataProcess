using System;
using Yuebon.Commons.IServices;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.IServices
{
    /// <summary>
    /// 定义数据输出模型详情表服务接口
    /// </summary>
    public interface ISys_outmodel_detailsService:IService<Sys_outmodel_details,Sys_outmodel_detailsOutputDto, string>
    {
    }
}
