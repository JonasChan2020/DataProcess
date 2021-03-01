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
    /// 数据输出模型详情表服务接口实现
    /// </summary>
    public class Sys_outmodel_detailsService: BaseService<Sys_outmodel_details,Sys_outmodel_detailsOutputDto, string>, ISys_outmodel_detailsService
    {
		private readonly ISys_outmodel_detailsRepository _repository;
        public Sys_outmodel_detailsService(ISys_outmodel_detailsRepository repository) : base(repository)
        {
			_repository=repository;
        }
    }
}