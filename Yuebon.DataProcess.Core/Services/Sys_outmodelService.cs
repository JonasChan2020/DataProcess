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
    /// 数据输出模型服务接口实现
    /// </summary>
    public class Sys_outmodelService: BaseService<Sys_outmodel,Sys_outmodelOutputDto, string>, ISys_outmodelService
    {
		private readonly ISys_outmodelRepository _repository;
        public Sys_outmodelService(ISys_outmodelRepository repository) : base(repository)
        {
			_repository=repository;
        }
    }
}