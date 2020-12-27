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
    /// 数据源配置信息表服务接口实现
    /// </summary>
    public class Ds_dsconfigService: BaseService<Ds_dsconfig,Ds_dsconfigOutputDto, string>, IDs_dsconfigService
    {
		private readonly IDs_dsconfigRepository _repository;
        private readonly ILogService _logService;
        public Ds_dsconfigService(IDs_dsconfigRepository repository,ILogService logService) : base(repository)
        {
			_repository=repository;
			_logService=logService;
            _repository.OnOperationLog += _logService.OnOperationLog;
        }
    }
}