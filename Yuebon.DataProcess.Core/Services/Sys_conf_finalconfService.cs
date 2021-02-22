using Yuebon.Commons.Services;
using Yuebon.Security.IServices;
using Yuebon.DataProcess.IRepositories;
using Yuebon.DataProcess.IServices;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;
using System.Collections.Generic;
using System.Linq;
using Yuebon.DataProcess.Core.OutSideDbService.Entity;
using Yuebon.DataProcess.Core.common;
using Yuebon.DataProcess.Core.common.Entity;
using Yuebon.DataProcess.Core.OutSideDbService.Extension;
using Yuebon.Commons.Helpers;
using System.Threading.Tasks;

namespace Yuebon.DataProcess.Services
{
    /// <summary>
    /// 服务接口实现
    /// </summary>
    public class Sys_conf_finalconfService : BaseService<Sys_conf_finalconf, Sys_conf_finalconfOutputDto, string>, ISys_conf_finalconfService
    {
		private readonly ISys_conf_finalconfRepository _repository;
        private readonly ISys_confService sysConfService;
        private readonly ISys_conf_detailsService sysConfDetailService;
        private readonly ISys_conf_objService sysconfobjService;
        private readonly IPlug_ConfDetailService PcdService;
        private readonly ILogService _logService;
        public Sys_conf_finalconfService(ISys_conf_finalconfRepository repository, ISys_confService _sysConfService, ISys_conf_detailsService _sysConfDetailService, ISys_conf_objService _sysconfobjService, IPlug_ConfDetailService _PcdService,ILogService logService) : base(repository)
        {
			_repository=repository;
            sysConfService = _sysConfService;
            sysConfDetailService = _sysConfDetailService;
            sysconfobjService = _sysconfobjService;
            PcdService = _PcdService;
            _logService =logService;
            //_repository.OnOperationLog += _logService.OnOperationLog;
        }

        /// <summary>
        /// 更新数据模型配置
        /// </summary>
        /// <param name="sysConfId">数据模型ID</param>
        /// <returns></returns>
        public async Task<bool> UpdateDetailConfig(string sysConfId)
        {
            Sys_conf sysConfModel = sysConfService.Get(sysConfId);
            List<SysConfDetailFinalInfo> modelList = new List<SysConfDetailFinalInfo>(); //完整数据模型配置信息
            List<DbFieldInfo> sfModelList = new List<DbFieldInfo>(); //用数据同步配置页面字段列表
            IEnumerable<Sys_conf_details> tmpconfDetailModelList = sysConfDetailService.GetListWhere(string.Format(" sys_conf_id='{0}' and EnabledMark=1", sysConfId));
            if (tmpconfDetailModelList != null)
            {
                List<Sys_conf_details> confDetailModelList = tmpconfDetailModelList.OrderBy(x => x.Levelnum).ToList();  //数据模型包含表列表
                for (int i = 0; i < confDetailModelList.Count; i++)
                {
                    #region 获取插件配置信息
                    IEnumerable<Plug_ConfDetail> tmppcdModelList = PcdService.GetListWhere(string.Format("Obj_Id='{0}'", confDetailModelList[i].Id));
                    List<Plug_ConfDetail> pcdModelList = null;
                    if (tmppcdModelList != null)
                    {
                        pcdModelList = tmppcdModelList.ToList();
                    }
                    #endregion

                    #region 获取字段配置信息
                    Sys_conf_obj confObjModel = sysconfobjService.GetWhere(string.Format("sys_conf_detail_id='{0}'", confDetailModelList[i].Id));
                    List<DbFieldInfo> fieldList = null;
                    if (confObjModel != null)
                    {
                        fieldList = confObjModel.Configjson.ToObject<List<DbFieldInfo>>();
                    }
                    if (fieldList != null && pcdModelList != null)
                    {
                        Plug_ConfDetail verifyPcdModel = pcdModelList.Find(x => x.ConfType == 0);
                        Plug_ConfDetail dataSyncPcdModel = pcdModelList.Find(x => x.ConfType == 1);
                        List<DbTableOperaInfo> verifyDoModelList = verifyPcdModel != null&& verifyPcdModel.ConfJson!=null ? verifyPcdModel.ConfJson.ToObject<List<DbTableOperaInfo>>() : null;
                        List<DbTableOperaInfo> dataSyncDoModelList = dataSyncPcdModel != null && dataSyncPcdModel.ConfJson != null? dataSyncPcdModel.ConfJson.ToObject<List<DbTableOperaInfo>>():null;
                        foreach (DbFieldInfo item in fieldList)
                        {
                            #region 补充插件验证配置信息
                            if (verifyDoModelList != null)
                            {
                                DbTableOperaInfo verifyDoModel = verifyDoModelList.Find(x => x.FieldName == item.FieldName);
                                if (verifyDoModel != null&& !string.IsNullOrEmpty(verifyDoModel.OperaParamers))
                                {
                                    string tmp = verifyDoModel.OperaParamers.Replace("\\\"", "").Replace("\"", "");
                                    item.VerifyFunctionParamterList =StringTools.DecodeBase64(tmp).ToObject<List<FieldPlugConfigInfo>>();
                                }
                            }
                            #endregion

                            #region 补充插件数据同步配置信息
                            if (dataSyncDoModelList != null)
                            {
                                DbTableOperaInfo dataSyncDoModel = dataSyncDoModelList.Find(x => x.FieldName == item.FieldName);
                                if (dataSyncDoModel != null && !string.IsNullOrEmpty(dataSyncDoModel.OperaParamers))
                                {
                                    string tmp = dataSyncDoModel.OperaParamers.Replace("\\\"", "").Replace("\"", "");
                                    item.GetFunctionParamterList = StringTools.DecodeBase64(tmp).ToObject<List<FieldPlugConfigInfo>>();
                                }
                            }
                            #endregion

                            #region 如果是配置时可见的列，则加入同步配置表内
                            if (item.Is_Visible == true)
                            {
                                item.TableLevelNum = confDetailModelList[i].Levelnum;
                                item.WriteTableName = confDetailModelList[i].Tbname;
                                item.WriteFieldName = item.FieldName;
                                item.WriteDescription = item.Description;
                                sfModelList.Add(item);
                            }
                            #endregion

                        }
                    }
                    #endregion

                    SysConfDetailFinalInfo scdfModel = new SysConfDetailFinalInfo();
                    scdfModel.SysConfDetail_Id = confDetailModelList[i].Id;
                    scdfModel.LevelNum = int.Parse(confDetailModelList[i].Levelnum.ToString());
                    scdfModel.SysConfDetailInfo = confDetailModelList[i];
                    scdfModel.FieldList = fieldList;
                    modelList.Add(scdfModel);
                }
            }

            bool result = true;
            List<Sys_conf_finalconf> scfModelList = new List<Sys_conf_finalconf>();
            IEnumerable<Sys_conf_finalconf> tmpscfModelList = _repository.GetListWhere(string.Format("sys_conf_id='{0}'", sysConfId));
            if (tmpconfDetailModelList != null && tmpconfDetailModelList.Count() > 0)
            {
                scfModelList = tmpscfModelList.ToList();
            }

            #region 数据同步配置页面字段列表新增修改
            Sys_conf_finalconf cmodel = scfModelList.Find(x => x.ConfType == 1);
            if (cmodel != null)
            {
                cmodel.ConfJson = sfModelList.ToJson();
                result = await _repository.UpdateAsync(cmodel, cmodel.Id);
            }
            else
            {
                cmodel = new Sys_conf_finalconf();
                cmodel.Id = GuidUtils.CreateNo();
                cmodel.Sys_Conf_Id = sysConfId;
                cmodel.Sys_Id = sysConfModel.Sysid;
                cmodel.ConfType = 1;
                cmodel.ConfJson = sfModelList.ToJson();
                result = await _repository.InsertAsync(cmodel) > 0;
            }
            #endregion

            #region 完整数据模型配置信息新增修改
            Sys_conf_finalconf model = scfModelList.Find(x => x.ConfType == 0);
            if (model != null)
            {
                model.ConfJson = modelList.ToJson();
                result = await _repository.UpdateAsync(model, model.Id);
            }
            else
            {
                model = new Sys_conf_finalconf();
                model.Id = GuidUtils.CreateNo();
                model.Sys_Conf_Id = sysConfId;
                model.Sys_Id = sysConfModel.Sysid;
                model.ConfType = 0;
                model.ConfJson = modelList.ToJson();
                result = await _repository.InsertAsync(model) > 0;
            }
            #endregion
            return result;
        }
    }
}