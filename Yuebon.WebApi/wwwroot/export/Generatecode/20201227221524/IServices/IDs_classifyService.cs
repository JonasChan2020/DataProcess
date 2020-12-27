using System;
using Yuebon.Commons.IServices;
using HtSoft.DataProcess.Dtos;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.IServices
{
    /// <summary>
    /// 定义数据源分类表服务接口
    /// </summary>
    public interface IDs_classifyService:IService<Ds_classify,Ds_classifyOutputDto, string>
    {
    }
}
