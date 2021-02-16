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
    public class Conf_confService: BaseService<Conf_conf,Conf_confOutputDto, string>, IConf_confService
    {
		private readonly IConf_confRepository _repository;
        private readonly ISys_sysRepository _sysrepository;
        private readonly ILogService _logService;
        public Conf_confService(IConf_confRepository repository, ISys_sysRepository sysrepository, ILogService logService) : base(repository)
        {
			_repository=repository;
            _sysrepository = sysrepository;
            _logService =logService;
            //_repository.OnOperationLog += _logService.OnOperationLog;
        }
    }
}