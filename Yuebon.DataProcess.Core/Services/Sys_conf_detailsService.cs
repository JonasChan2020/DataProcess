using Yuebon.Commons.Services;
using Yuebon.Security.IServices;
using Yuebon.DataProcess.IRepositories;
using Yuebon.DataProcess.IServices;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Yuebon.DataProcess.Services
{
    /// <summary>
    /// 服务接口实现
    /// </summary>
    public class Sys_conf_detailsService: BaseService<Sys_conf_details,Sys_conf_detailsOutputDto, string>, ISys_conf_detailsService
    {
		private readonly ISys_conf_detailsRepository _repository;
        private readonly ILogService _logService;
        public Sys_conf_detailsService(ISys_conf_detailsRepository repository,ILogService logService) : base(repository)
        {
			_repository=repository;
			_logService=logService;
            //_repository.OnOperationLog += _logService.OnOperationLog;
        }

        #region 移动关联配置顺序
        /// <summary>
        /// 移动关联配置顺序
        /// </summary>
        /// <param name="cmId">自定义模版ID</param>
        /// <param name="sheetID">sheetID</param>
        /// <param name="model">移动的自定义模版的实体类</param>
        /// <param name="actionStr">动作：上、下、置顶，置底</param>
        /// <returns></returns>
        public async Task<bool> ChangeLevelNumAsync(string id, string actionStr)
        {
            Sys_conf_details model = _repository.Get(id);
            
            if (model != null && !string.IsNullOrEmpty(model.Id))
            {
                
                IEnumerable<Sys_conf_details> confModelList = _repository.GetListWhere(string.Format(" sys_conf_id='{0}'", model.Sys_conf_id));
                List<Sys_conf_details> modelList = new List<Sys_conf_details>(confModelList);
                List<Sys_conf_details> updateModelList = new List<Sys_conf_details>();
                if (actionStr == "up")
                {
                    #region 上移方法
                    if (model.Levelnum <= 1)
                    {
                        throw new Exception("已经在最顶，无法移动");
                    }
                    Sys_conf_details nextModel= modelList.Find(x => x.Levelnum == model.Levelnum - 1);
                    if (nextModel != null)
                    {
                        nextModel.Levelnum += 1;
                        updateModelList.Add(nextModel);
                        model.Levelnum -= 1;
                        updateModelList.Add(model);

                    }
                    else
                    {
                        model.Levelnum -= 1;
                        updateModelList.Add(model);
                    }
                    #endregion
                }
                else if (actionStr == "down")
                {
                    #region 下移方法
                    if (model.Levelnum == modelList.Count)
                    {
                        throw new Exception("已经在最底，无法移动");
                    }
                    Sys_conf_details nextModel = modelList.Find(x => x.Levelnum == model.Levelnum + 1);
                    if (nextModel != null)
                    {
                        nextModel.Levelnum -= 1;
                        updateModelList.Add(nextModel);
                        model.Levelnum += 1;
                        updateModelList.Add(model);
                    }
                    else
                    {
                        model.Levelnum += 1;
                        updateModelList.Add(model);
                    }


                    #endregion
                }
                else if (actionStr == "top")
                {
                    #region 置顶方法

                    if (model.Levelnum == 1)
                    {
                        throw new Exception("已经在最顶，无法移动");
                    }
                    List<Sys_conf_details> beforeModelList = modelList.FindAll(x => x.Levelnum < model.Levelnum);
                    if (beforeModelList != null && beforeModelList.Count > 0)
                    {
                        foreach (Sys_conf_details item in beforeModelList)
                        {
                            item.Levelnum += 1;
                            updateModelList.Add(item);
                        }
                        model.Levelnum = 1;
                        updateModelList.Add(model);
                    }


                    #endregion
                }
                else if (actionStr == "buttom")
                {
                    #region 置底方法
                    if (model.Levelnum == modelList.Count)
                    {
                        throw new Exception("已经在最底，无法移动");
                    }
                    List<Sys_conf_details> afterModelList = modelList.FindAll(x => x.Levelnum > model.Levelnum);
                    if (afterModelList != null && afterModelList.Count > 0)
                    {
                        foreach (Sys_conf_details item in afterModelList)
                        {
                            item.Levelnum -= 1;
                            updateModelList.Add(item);
                        }
                        model.Levelnum = modelList.Count;
                        updateModelList.Add(model);
                    }

                    #endregion
                }
                if (updateModelList.Count > 0)
                {
                    #region 更新所有需要更新的关联配置，后期可改为批量更新功能，使用事务
                    foreach (Sys_conf_details item in updateModelList)
                    {
                        bool result = await _repository.UpdateAsync(item, item.Id);
                        if (!result)
                        {
                            throw new Exception("移动失败");
                        }
                    }
                    #endregion
                }
            }
            else
            {
                throw new Exception("未获取到需移动的项的信息");
            }
            return true;
        }
        #endregion
    }
}