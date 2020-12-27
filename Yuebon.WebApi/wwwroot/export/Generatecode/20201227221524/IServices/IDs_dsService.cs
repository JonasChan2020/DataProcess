using System;
using Yuebon.Commons.IServices;
using HtSoft.DataProcess.Dtos;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.IServices
{
    /// <summary>
    /// 定义数据源表服务接口
    /// </summary>
    public interface IDs_dsService:IService<Ds_ds,Ds_dsOutputDto, string>
    {
    }
}
