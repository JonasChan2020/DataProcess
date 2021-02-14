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
    public interface ISys_conf_detailsService:IService<Sys_conf_details,Sys_conf_detailsOutputDto, string>
    {
        /// <summary>
        /// 移动关联配置顺序
        /// </summary>
        /// <param name="cmId">自定义模版ID</param>
        /// <param name="sheetID">sheetID</param>
        /// <param name="model">移动的自定义模版的实体类</param>
        /// <param name="actionStr">动作：上、下、置顶，置底</param>
        /// <returns></returns>
        Task<bool> ChangeLevelNumAsync(string id, string actionStr);
    }
}
