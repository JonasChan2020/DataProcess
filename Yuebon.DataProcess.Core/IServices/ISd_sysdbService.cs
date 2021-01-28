using System;
using System.Data;
using System.Threading.Tasks;
using Yuebon.Commons.IServices;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.IServices
{
    /// <summary>
    /// 定义服务接口
    /// </summary>
    public interface ISd_sysdbService:IService<Sd_sysdb,Sd_sysdbOutputDto, string>
    {
        /// <summary>
        /// 异步步新增实体。
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        Task<bool> UpdateDbContents(Sd_sysdb entity, IDbTransaction trans = null);
    }
}
