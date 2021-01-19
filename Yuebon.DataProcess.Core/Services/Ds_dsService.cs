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
    public class Ds_dsService: BaseService<Ds_ds,Ds_dsOutputDto, string>, IDs_dsService
    {
		private readonly IDs_dsRepository _repository;
        private readonly IDs_classifyService _classservice;
        private readonly ILogService _logService;
        public Ds_dsService(IDs_dsRepository repository, IDs_classifyService classService, ILogService logService) : base(repository)
        {
			_repository=repository;
            _classservice = classService;
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
        public override async Task<PageResult<Ds_dsOutputDto>> FindWithPagerAsync(SearchInputDto<Ds_ds> search)
        {

            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege();
            PagerInfo pagerInfo = new PagerInfo
            {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<Ds_ds> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            List<Ds_dsOutputDto> resultList = list.MapTo<Ds_dsOutputDto>();
            List<Ds_dsOutputDto> listResult = new List<Ds_dsOutputDto>();
            foreach (Ds_dsOutputDto item in resultList)
            {
                if (!string.IsNullOrEmpty(item.Classify_id))
                {
                    item.Classify_Name = _classservice.Get(item.Classify_id).Dtname;
                }
                listResult.Add(item);
            }
            PageResult<Ds_dsOutputDto> pageResult = new PageResult<Ds_dsOutputDto>
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