using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.Commons.IServices;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.IServices
{
    /// <summary>
    /// 定义服务接口
    /// </summary>
    public interface IDs_classifyService:IService<Ds_classify,Ds_classifyOutputDto, string>
    {
        /// <summary>
        /// 获取分类适用于Vue 树形列表
        /// </summary>
        /// <returns></returns>
        Task<List<Ds_classifyOutputDto>> GetAllClassifyTreeTable();
    }
}
