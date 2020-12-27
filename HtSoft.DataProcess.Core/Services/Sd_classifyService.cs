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
    /// 目标库分类表服务接口实现
    /// </summary>
    public class Sd_classifyService: BaseService<Sd_classify,Sd_classifyOutputDto, string>, ISd_classifyService
    {
		private readonly ISd_classifyRepository _repository;
        private readonly ILogService _logService;
        public Sd_classifyService(ISd_classifyRepository repository,ILogService logService) : base(repository)
        {
			_repository=repository;
			_logService=logService;
            //_repository.OnOperationLog += _logService.OnOperationLog;
        }
    }
}