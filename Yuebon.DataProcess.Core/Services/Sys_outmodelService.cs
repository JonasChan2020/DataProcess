using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Services;
using Yuebon.DataProcess.IRepositories;
using Yuebon.DataProcess.IServices;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Services
{
    /// <summary>
    /// 数据输出模型服务接口实现
    /// </summary>
    public class Sys_outmodelService: BaseService<Sys_outmodel,Sys_outmodelOutputDto, string>, ISys_outmodelService
    {
		private readonly ISys_outmodelRepository _repository;
        private readonly ISys_outmodel_classifyService _classservice;
        private readonly ISys_sysRepository _sysrepository;
        public Sys_outmodelService(ISys_outmodelRepository repository, ISys_sysRepository sysrepository, ISys_outmodel_classifyService classService) : base(repository)
        {
			_repository=repository;
            _classservice = classService;
            _sysrepository = sysrepository;
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
        public override async Task<PageResult<Sys_outmodelOutputDto>> FindWithPagerAsync(SearchInputDto<Sys_outmodel> search)
        {
            string where = GetDataPrivilege();
            PagerInfo pagerInfo = new PagerInfo
            {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            if (search.Filter != null && !string.IsNullOrEmpty(search.Filter.Sysid))
            {
                where += string.Format(" and sysid = '{0}'", search.Filter.Sysid);
            }
            if (search.Filter != null && !string.IsNullOrEmpty(search.Filter.Classify_id))
            {
                where += string.Format(" and Classify_id in (select id from dp_sys_outmodel_classify where levelpath like '%{0}%' )", search.Filter.Classify_id);
            }
            List<Sys_outmodel> list = await repository.FindWithCheckPagerAsync(where, pagerInfo,false);
            List<Sys_outmodelOutputDto> resultList = list.MapTo<Sys_outmodelOutputDto>();
            List<Sys_outmodelOutputDto> listResult = new List<Sys_outmodelOutputDto>();
            foreach (Sys_outmodelOutputDto item in resultList)
            {
                if (!string.IsNullOrEmpty(item.Classify_id))
                {
                    item.Classify_Name = _classservice.Get(item.Classify_id).ClassName;
                }
                if (!string.IsNullOrEmpty(item.Sysid))
                {
                    item.Sys_Name = _sysrepository.Get(item.Sysid).Sysname;
                }
                listResult.Add(item);
            }
            PageResult<Sys_outmodelOutputDto> pageResult = new PageResult<Sys_outmodelOutputDto>
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