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
using System.Linq;

namespace Yuebon.DataProcess.Services
{
    /// <summary>
    /// 服务接口实现
    /// </summary>
    public class Plug_plugService: BaseService<Plug_plug,Plug_plugOutputDto, string>, IPlug_plugService
    {
		private readonly IPlug_plugRepository _repository;
        private readonly IPlug_typeService _classservice;
        private readonly IPlug_sysrelationService _relaservice;
        private readonly ILogService _logService;
        public Plug_plugService(IPlug_plugRepository repository, IPlug_typeService classService, IPlug_sysrelationService relaservice, ILogService logService) : base(repository)
        {
			_repository=repository;
            _classservice = classService;
            _relaservice = relaservice;
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
        public override async Task<PageResult<Plug_plugOutputDto>> FindWithPagerAsync(SearchInputDto<Plug_plug> search)
        {

            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege();
            PagerInfo pagerInfo = new PagerInfo
            {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<Plug_plug> list = await _repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            List<Plug_plugOutputDto> resultList = list.MapTo<Plug_plugOutputDto>();
            List<Plug_plugOutputDto> listResult = new List<Plug_plugOutputDto>();
            foreach (Plug_plugOutputDto item in resultList)
            {
                if (!string.IsNullOrEmpty(item.Ptype))
                {
                    item.Classify_Name = _classservice.Get(item.Ptype).Ptname;
                }
                listResult.Add(item);
            }
            PageResult<Plug_plugOutputDto> pageResult = new PageResult<Plug_plugOutputDto>
            {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = listResult,
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }

        /// <summary>
        /// 根据系统ID查询所有可用插件
        /// </summary>
        /// <param name="SysId"></param>
        /// <returns></returns>
        public async Task<List<Plug_plugOutputDto>> GetEnableListWithSys(string SysId, string ptype)
        {
            IEnumerable<Plug_plug> list = await _repository.GetEnableListWithSysIdAsync(SysId, ptype);
            List<Plug_plugOutputDto> resultList = list.MapTo<Plug_plugOutputDto>();
            return resultList;
        }
    }
}