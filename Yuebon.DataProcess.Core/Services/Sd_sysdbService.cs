using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.DataProcess.IRepositories;
using Yuebon.DataProcess.IServices;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;
using System.Data;
using Yuebon.Commons.Services;
using Yuebon.Security.IServices;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Mapping;
using Yuebon.DataProcess.Core.OutSideDbService;
using Yuebon.DataProcess.Core.OutSideDbService.Entity;
using Yuebon.DataProcess.Core.common;

namespace Yuebon.DataProcess.Services
{
    /// <summary>
    /// 服务接口实现
    /// </summary>
    public class Sd_sysdbService: BaseService<Sd_sysdb,Sd_sysdbOutputDto, string>, ISd_sysdbService
    {
		private readonly ISd_sysdbRepository _repository;
        private readonly ISd_detailService _detailService;
        private readonly ISd_classifyService _classservice;
        private readonly ISys_sysRepository _sysrepository;
        private readonly ILogService _logService;
        public Sd_sysdbService(ISd_sysdbRepository repository, ISd_detailService detailService, ISys_sysRepository sysrepository, ISd_classifyService classService, ILogService logService) : base(repository)
        {
			_repository=repository;
            _detailService = detailService;
            _classservice = classService;
            _sysrepository = sysrepository;
            _logService =logService;
            //_repository.OnOperationLog += _logService.OnOperationLog;
        }

        /// <summary>
        /// 异步步新增实体。
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public override async Task<long> InsertAsync(Sd_sysdb entity, IDbTransaction trans = null)
        {
            long addResult = await repository.InsertAsync(entity, trans);
            bool result = await UpdateDbContents(entity);
            if (addResult > 0 && result)
            {
                return addResult;
            }
            else
            {
                return -1;
            }
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
            if (!string.IsNullOrEmpty(search.Filter.Classify_id))
            {
                where += string.Format(" and classify_id = '{0}'", search.Filter.Classify_id);
            }
            List<Sd_sysdb> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            List<Sd_sysdbOutputDto> resultList = list.MapTo<Sd_sysdbOutputDto>();
            List<Sd_sysdbOutputDto> listResult = new List<Sd_sysdbOutputDto>();
            foreach (Sd_sysdbOutputDto item in resultList)
            {
                if (!string.IsNullOrEmpty(item.Classify_id))
                {
                    item.Classify_Name = _classservice.Get(item.Classify_id).ClassName;
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

        /// <summary>
        /// 异步步新增实体。
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public async Task<bool> UpdateDbContents(Sd_sysdb entity, IDbTransaction trans = null)
        {

            #region 获取所有表集合

            DbTools bll = new DbTools(entity.Sdconnectionstr, entity.Sdtype);
            List<DbTableInfo> tbList = bll.GetAllTables(entity.dbName, "");
            foreach (DbTableInfo item in tbList)
            {
                item.Fileds = bll.GetAllColumns(entity.dbName, item.TableName);
            }
            bool has = true;
            Sd_detail detailModel = _detailService.GetWhere(string.Format(" sd_id = '{0}'", entity.Id));
            if (detailModel == null)
            {
                has = false;
                detailModel = new Sd_detail();
                detailModel.Id = GuidUtils.CreateNo();
                detailModel.Sd_id = entity.Id;
            }
            detailModel.Tbs = tbList.ToJson();

            bool result = false;
            if (has)
            {
                result = await _detailService.UpdateAsync(detailModel, detailModel.Id, trans);
            }
            else
            {
                long addDetailResult = await _detailService.InsertAsync(detailModel, trans);
                if (addDetailResult > 0)
                {
                    result = true;
                }
            }

            #endregion
            return result;
        }


    }
}