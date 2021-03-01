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
    /// 数据输出模型分类表服务接口实现
    /// </summary>
    public class Sys_outmodel_classifyService: BaseService<Sys_outmodel_classify,Sys_outmodel_classifyOutputDto, string>, ISys_outmodel_classifyService
    {
		private readonly ISys_outmodel_classifyRepository _repository;
        public Sys_outmodel_classifyService(ISys_outmodel_classifyRepository repository) : base(repository)
        {
			_repository=repository;
        }
    }
}