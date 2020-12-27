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
    /// 目标库配置信息表服务接口实现
    /// </summary>
    public class Sd_sdconfigService: BaseService<Sd_sdconfig,Sd_sdconfigOutputDto, string>, ISd_sdconfigService
    {
		private readonly ISd_sdconfigRepository _repository;
        private readonly ILogService _logService;
        public Sd_sdconfigService(ISd_sdconfigRepository repository,ILogService logService) : base(repository)
        {
			_repository=repository;
			_logService=logService;
            //_repository.OnOperationLog += _logService.OnOperationLog;
        }
    }
}