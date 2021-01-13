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
    public interface IPlug_typeService:IService<Plug_type,Plug_typeOutputDto, string>
    {
        /// <summary>
        /// 获取分类适用于Vue 树形列表
        /// </summary>
        /// <returns></returns>
        Task<List<Plug_typeOutputDto>> GetAllClassifyTreeTable();
    }
}
