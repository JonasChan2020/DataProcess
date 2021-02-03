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
using System.Data;

namespace Yuebon.DataProcess.Services
{
    /// <summary>
    /// 服务接口实现
    /// </summary>
    public class Sys_sysService: BaseService<Sys_sys,Sys_sysOutputDto, string>, ISys_sysService
    {
		private readonly ISys_sysRepository _repository;
        private readonly ISys_classifyService _classservice;
        private readonly ISd_sysdbService _sysdbservice;
        private readonly ILogService _logService;
        public Sys_sysService(ISys_sysRepository repository, ISys_classifyService classService, ISd_sysdbService sysdbservice, ILogService logService) : base(repository)
        {
			_repository=repository;
            _classservice = classService;
            _sysdbservice = sysdbservice;
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
        public override async Task<PageResult<Sys_sysOutputDto>> FindWithPagerAsync(SearchInputDto<Sys_sys> search)
        {

            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege();
            PagerInfo pagerInfo = new PagerInfo
            {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<Sys_sys> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            List<Sys_sysOutputDto> resultList = list.MapTo<Sys_sysOutputDto>();
            List<Sys_sysOutputDto> listResult = new List<Sys_sysOutputDto>();
            foreach (Sys_sysOutputDto item in resultList)
            {
                if (!string.IsNullOrEmpty(item.Classify_id))
                {
                    item.Classify_Name = _classservice.Get(item.Classify_id).Stname;
                }
                if (!string.IsNullOrEmpty(item.MdbId))
                {
                    item.MdbName = _sysdbservice.Get(item.MdbId).SdName;
                }
                listResult.Add(item);
            }
            PageResult<Sys_sysOutputDto> pageResult = new PageResult<Sys_sysOutputDto>
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