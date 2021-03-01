using System;
using Yuebon.Commons.IServices;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.IServices
{
    /// <summary>
    /// 定义数据输出模型最终查询语句服务接口
    /// </summary>
    public interface ISys_outmodel_sqlService:IService<Sys_outmodel_sql,Sys_outmodel_sqlOutputDto, string>
    {
    }
}
