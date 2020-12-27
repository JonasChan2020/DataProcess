using System;
using Yuebon.Commons.IServices;
using HtSoft.DataProcess.Dtos;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.IServices
{
    /// <summary>
    /// 定义系统分类表服务接口
    /// </summary>
    public interface ISys_classifyService:IService<Sys_classify,Sys_classifyOutputDto, string>
    {
    }
}
