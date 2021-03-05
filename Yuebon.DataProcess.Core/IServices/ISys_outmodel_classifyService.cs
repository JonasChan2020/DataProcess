using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.IServices;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.IServices
{
    /// <summary>
    /// 定义数据输出模型分类表服务接口
    /// </summary>
    public interface ISys_outmodel_classifyService:IService<Sys_outmodel_classify,Sys_outmodel_classifyOutputDto, string>
    {
        /// <summary>
        /// 获取分类适用于Vue 树形列表
        /// </summary>
        /// <returns></returns>
        Task<List<Sys_outmodel_classifyOutputDto>> GetAllClassifyTreeTable(SearchInputDto<Sys_outmodel_classify> search);
    }
}
