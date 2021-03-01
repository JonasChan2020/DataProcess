using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Services;
using Yuebon.DataProcess.IRepositories;
using Yuebon.DataProcess.IServices;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Services
{
    /// <summary>
    /// 数据输出模型列字段信息服务接口实现
    /// </summary>
    public class Sys_outmodel_objService: BaseService<Sys_outmodel_obj,Sys_outmodel_objOutputDto, string>, ISys_outmodel_objService
    {
		private readonly ISys_outmodel_objRepository _repository;
        public Sys_outmodel_objService(ISys_outmodel_objRepository repository) : base(repository)
        {
			_repository=repository;
        }
    }
}