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
        private readonly ISd_sysdbService sdService;
        private readonly ISys_sysService sysService;
        private readonly ILogService _logService;
        public Conf_confService(IConf_confRepository repository, ISd_sysdbService _sdService, ISys_sysService _sysService, ILogService logService) : base(repository)
        {
			_repository=repository;
            sdService = _sdService;
            sysService = _sysService;
            _logService =logService;
            //_repository.OnOperationLog += _logService.OnOperationLog;
        }

        /// <summary>
        /// 分页查询，自行封装sql语句
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="info">分页信息</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="desc">排序方式 true为desc，false为asc</param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public override async Task<PageResult<Conf_confOutputDto>> FindWithPagerAsync(SearchInputDto<Conf_conf> search)
        {
            string where = GetDataPrivilege();
            PagerInfo pagerInfo = new PagerInfo
            {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            if (search.Filter != null && !string.IsNullOrEmpty(search.Filter.FromId))
            {
                where += string.Format(" and fromid = '{0}'", search.Filter.FromId);
            }
            List<Conf_conf> list = await repository.FindWithPagerAsync(where, pagerInfo);
            List<Conf_confOutputDto> resultList = list.MapTo<Conf_confOutputDto>();
            List<Conf_confOutputDto> listResult = new List<Conf_confOutputDto>();
            foreach (Conf_confOutputDto item in resultList)
            {
                if (!string.IsNullOrEmpty(item.ToId))
                {
                    if (item.ConfToType == 0)
                    {
                        item.ToName = sysService.Get(item.ToId).Sysname;
                    }
                    else if (item.ConfToType == 1)
                    {
                        item.ToName = sdService.Get(item.ToId).SdName;
                    }

                }
               
                listResult.Add(item);
            }
            PageResult<Conf_confOutputDto> pageResult = new PageResult<Conf_confOutputDto>
            {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = listResult,
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }
    }
}