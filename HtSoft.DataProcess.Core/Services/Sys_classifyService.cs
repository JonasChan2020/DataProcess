using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Services;
using Yuebon.Security.IServices;
using HtSoft.DataProcess.IRepositories;
using HtSoft.DataProcess.IServices;
using HtSoft.DataProcess.Dtos;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.Services
{
    /// <summary>
    /// 系统分类表服务接口实现
    /// </summary>
    public class Sys_classifyService: BaseService<Sys_classify,Sys_classifyOutputDto, string>, ISys_classifyService
    {
		private readonly ISys_classifyRepository _repository;
        private readonly ILogService _logService;
        public Sys_classifyService(ISys_classifyRepository repository,ILogService logService) : base(repository)
        {
			_repository=repository;
			_logService=logService;
            //_repository.OnOperationLog += _logService.OnOperationLog;
        }
    }
}