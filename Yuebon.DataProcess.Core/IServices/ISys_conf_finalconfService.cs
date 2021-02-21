using System;
using System.Threading.Tasks;
using Yuebon.Commons.IServices;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.IServices
{
    /// <summary>
    /// 定义服务接口
    /// </summary>
    public interface ISys_conf_finalconfService : IService<Sys_conf_finalconf, Sys_conf_finalconfOutputDto, string>
    {
        /// <summary>
        /// 更新数据模型配置
        /// </summary>
        /// <param name="sysConfId">数据模型ID</param>
        /// <returns></returns>
        Task<bool> UpdateDetailConfig(string sysConfId);
    }
}
