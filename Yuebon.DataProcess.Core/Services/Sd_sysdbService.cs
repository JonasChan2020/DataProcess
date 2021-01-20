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
    public class Sd_sysdbService: BaseService<Sd_sysdb,Sd_sysdbOutputDto, string>, ISd_sysdbService
    {
		private readonly ISd_sysdbRepository _repository;
        private readonly ISd_classifyService _classservice;
        private readonly ISys_sysRepository _sysrepository;
        private readonly ILogService _logService;
        public Sd_sysdbService(ISd_sysdbRepository repository, ISys_sysRepository sysrepository, ISd_classifyService classService, ILogService logService) : base(repository)
        {
			_repository=repository;
            _classservice = classService;
            _sysrepository = sysrepository;
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
        public override async Task<PageResult<Sd_sysdbOutputDto>> FindWithPagerAsync(SearchInputDto<Sd_sysdb> search)
        {

            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege();
            PagerInfo pagerInfo = new PagerInfo
            {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            if (!string.IsNullOrEmpty(search.Filter.Sys_id))
            {
                where += string.Format(" and sys_id = '{0}'", search.Filter.Sys_id);
            }
            List<Sd_sysdb> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            List<Sd_sysdbOutputDto> resultList = list.MapTo<Sd_sysdbOutputDto>();
            List<Sd_sysdbOutputDto> listResult = new List<Sd_sysdbOutputDto>();
            foreach (Sd_sysdbOutputDto item in resultList)
            {
                if (!string.IsNullOrEmpty(item.Classify_id))
                {
                    item.Classify_Name = _classservice.Get(item.Classify_id).Dtname;
                }
                if (!string.IsNullOrEmpty(item.Sys_id))
                {
                    item.Sys_Name = _sysrepository.Get(item.Sys_id).Sysname;
                }
                listResult.Add(item);
            }
            PageResult<Sd_sysdbOutputDto> pageResult = new PageResult<Sd_sysdbOutputDto>
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