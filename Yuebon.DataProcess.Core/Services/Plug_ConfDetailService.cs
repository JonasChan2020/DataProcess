using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Services;
using Yuebon.Security.IServices;
using Yuebon.DataProcess.IRepositories;
using Yuebon.DataProcess.IServices;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Services
{
    /// <summary>
    /// 服务接口实现
    /// </summary>
    public class Plug_ConfDetailService : BaseService<Plug_ConfDetail, Plug_ConfDetailOutputDto, string>, IPlug_ConfDetailService
    {
		private readonly IPlug_ConfDetailRepository _repository;
        private readonly ILogService _logService;
        public Plug_ConfDetailService(IPlug_ConfDetailRepository repository,ILogService logService) : base(repository)
        {
			_repository=repository;
			_logService=logService;
            //_repository.OnOperationLog += _logService.OnOperationLog;
        }
    }
}