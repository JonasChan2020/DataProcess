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
    public class Sys_confService: BaseService<Sys_conf,Sys_confOutputDto, string>, ISys_confService
    {
		private readonly ISys_confRepository _repository;
        private readonly ISys_conf_classifyService _classservice;
        private readonly ISys_sysRepository _sysrepository;
        private readonly ILogService _logService;
        public Sys_confService(ISys_confRepository repository, ISys_sysRepository sysrepository, ISys_conf_classifyService classService, ILogService logService) : base(repository)
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
        public override async Task<PageResult<Sys_confOutputDto>> FindWithPagerAsync(SearchInputDto<Sys_conf> search)
        {

            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege();
            PagerInfo pagerInfo = new PagerInfo
            {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            if (!string.IsNullOrEmpty(search.Filter.Sysid))
            {
                where += string.Format(" and sysid = '{0}'", search.Filter.Sysid);
            }
            List<Sys_conf> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            List<Sys_confOutputDto> resultList = list.MapTo<Sys_confOutputDto>();
            List<Sys_confOutputDto> listResult = new List<Sys_confOutputDto>();
            foreach (Sys_confOutputDto item in resultList)
            {
                if (!string.IsNullOrEmpty(item.Classify_id))
                {
                    item.Classify_Name = _classservice.Get(item.Classify_id).Stname;
                }
                if (!string.IsNullOrEmpty(item.Sysid))
                {
                    item.Sys_Name = _sysrepository.Get(item.Sysid).Sysname;
                }
                listResult.Add(item);
            }
            PageResult<Sys_confOutputDto> pageResult = new PageResult<Sys_confOutputDto>
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