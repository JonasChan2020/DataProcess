using System;
using Yuebon.Commons.IServices;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.IServices
{
    /// <summary>
    /// 定义目标库详情服务接口
    /// </summary>
    public interface ISd_detailService:IService<Sd_detail,Sd_detailOutputDto, string>
    {
    }
}
