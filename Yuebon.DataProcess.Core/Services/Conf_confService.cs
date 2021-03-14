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
using Yuebon.DataProcess.Core.OutSideDbService.Entity;
using Yuebon.DataProcess.Core.common;

namespace Yuebon.DataProcess.Services
{
    /// <summary>
    /// 服务接口实现
    /// </summary>
    public class Conf_confService: BaseService<Conf_conf,Conf_confOutputDto, string>, IConf_confService
    {
		private readonly IConf_confRepository _repository;
        private readonly ISd_sysdbService sdService;
        private readonly ISd_detailService sdDetailService;
        
        private readonly ISys_sysService sysService;
        private readonly ISys_confService sysConfService;
        private readonly ISys_outmodelService OutModelService;
        private readonly ILogService _logService;
        public Conf_confService(IConf_confRepository repository, ISd_sysdbService _sdService, ISd_detailService _sdDetailService, ISys_sysService _sysService, ISys_confService _sysConfService, ISys_outmodelService _OutModelService, ILogService logService) : base(repository)
        {
			_repository=repository;
            sdService = _sdService;
            sdDetailService = _sdDetailService;
            sysService = _sysService;
            sysConfService = _sysConfService;
            OutModelService = _OutModelService;
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
            if (search.Filter != null && !string.IsNullOrEmpty(search.Filter.ToId))
            {
                where += string.Format(" and toid = '{0}'", search.Filter.ToId);
            }
            List<Conf_conf> list = await repository.FindWithPagerAsync(where, pagerInfo);
            List<Conf_confOutputDto> resultList = list.MapTo<Conf_confOutputDto>();
            List<Conf_confOutputDto> listResult = new List<Conf_confOutputDto>();
            foreach (Conf_confOutputDto item in resultList)
            {
                if (!string.IsNullOrEmpty(item.FromId))
                {
                    if (item.ConfFromType == 0)
                    {
                        Sys_sys sysModel = sysService.Get(item.FromParentId);
                        Sys_outmodel sysOutModel = OutModelService.Get(item.FromId);
                        if (sysModel != null && sysOutModel != null)
                        {
                            item.FromTbName = sysOutModel.Modelname;
                            item.FromDescription = sysOutModel.Description;
                            item.FromParentName = sysModel.Sysname;
                            item.FromParentDescription = sysModel.Description;
                        }
                        else
                        {
                            if (sysModel == null)
                            {
                                item.FromTbName = "系统已丢失：" + item.FromParentId;
                            }
                            else
                            {
                                item.FromTbName = "模型已丢失：" + item.FromId;
                            }
                            
                        }
                    }
                    else if (item.ConfFromType == 1)
                    {
                        Sd_sysdb sdModel = sdService.Get(item.FromParentId);
                        Sd_detail detailModel = sdDetailService.GetWhere(string.Format(" sd_id = '{0}'", item.FromParentId));
                        if (sdModel != null && detailModel != null)
                        {
                            List<DbTableInfo> childModelList = detailModel.Tbs.ToObject<List<DbTableInfo>>();
                            DbTableInfo tbModel = childModelList.Find(x => x.TableName == item.FromId);
                            if (tbModel != null)
                            {
                                item.FromTbName = tbModel.TableName;
                                item.FromDescription = tbModel.Description;
                                item.FromParentName = sdModel.SdName;
                                item.FromParentDescription = sdModel.Description;
                            }
                            else
                            {
                                item.FromTbName = "数据表已丢失：" + item.ToId;
                            }
                        }
                        else
                        {
                            item.FromTbName = "数据库已丢失：" + item.FromParentId;
                        }
                        
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