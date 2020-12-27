using System;
using Yuebon.Commons.IServices;
using HtSoft.DataProcess.Dtos;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.IServices
{
    /// <summary>
    /// 定义目标库配置信息表服务接口
    /// </summary>
    public interface ISd_sdconfigService:IService<Sd_sdconfig,Sd_sdconfigOutputDto, string>
    {
    }
}
