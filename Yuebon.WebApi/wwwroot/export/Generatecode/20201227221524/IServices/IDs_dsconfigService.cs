using System;
using Yuebon.Commons.IServices;
using HtSoft.DataProcess.Dtos;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.IServices
{
    /// <summary>
    /// 定义数据源配置信息表服务接口
    /// </summary>
    public interface IDs_dsconfigService:IService<Ds_dsconfig,Ds_dsconfigOutputDto, string>
    {
    }
}
