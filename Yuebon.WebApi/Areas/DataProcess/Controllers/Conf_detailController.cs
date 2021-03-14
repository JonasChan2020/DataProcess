using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Models;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;
using Yuebon.DataProcess.IServices;
using Yuebon.AspNetCore.Mvc;
using Yuebon.Commons.Dtos;
using Yuebon.DataProcess.Core.common;
using Yuebon.DataProcess.Core.OutSideDbService.Entity;
using Yuebon.Commons.Mapping;
using Yuebon.DataProcess.Core.common.Entity;
using System.Linq;

namespace Yuebon.WebApi.Areas.DataProcess.Controllers
{
    /// <summary>
    /// 接口
    /// </summary>
    [ApiController]
    [Route("api/DataProcess/[controller]")]
    public class Conf_detailController : AreaApiController<Conf_detail, Conf_detailOutputDto,Conf_detailInputDto,IConf_detailService,string>
    {
        private readonly ISd_sysdbService sdService;
        private readonly ISd_detailService detailService;
        private readonly ISys_sysService sysService;
        private readonly ISys_confService sysconfService;
        private readonly ISys_conf_finalconfService scfService;
        private readonly IConf_confService confService;
        private readonly ISys_outmodel_sqlService sysOutModelSqlService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="_confService"></param>
        /// <param name="_sdService"></param>
        /// <param name="_detailService"></param>
        /// <param name="_sysconfService"></param>
        /// <param name="_sysService"></param>
        public Conf_detailController(IConf_detailService _iService, IConf_confService _confService, ISd_sysdbService _sdService, ISd_detailService _detailService, ISys_confService _sysconfService, ISys_conf_finalconfService _scfService, ISys_sysService _sysService, ISys_outmodel_sqlService _sysOutModelSqlService) : base(_iService)
        {
            iService = _iService;
            sdService = _sdService;
            detailService = _detailService;
            sysService = _sysService;
            sysconfService = _sysconfService;
            scfService = _scfService;
            confService = _confService;
            sysOutModelSqlService = _sysOutModelSqlService;
            AuthorizeKey.ListKey = "Conf_detail/List";
            AuthorizeKey.InsertKey = "Conf_detail/Add";
            AuthorizeKey.UpdateKey = "Conf_detail/Edit";
            AuthorizeKey.UpdateEnableKey = "Conf_detail/Enable";
            AuthorizeKey.DeleteKey = "Conf_detail/Delete";
            AuthorizeKey.DeleteSoftKey = "Conf_detail/DeleteSoft";
            AuthorizeKey.ViewKey = "Conf_detail/View";
        }
        /// <summary>
        /// 新增前处理数据
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(Conf_detail info)
        {
            info.Id = GuidUtils.CreateNo();
        }
        
        /// <summary>
        /// 在更新数据前对数据的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeUpdate(Conf_detail info)
        {

        }

        /// <summary>
        /// 在软删除数据前对数据的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(Conf_detail info)
        {
            
        }

        /// <summary>
        /// 获取所有可用的
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetConfTbContent")]
        [YuebonAuthorize("List")]
        public async Task<CommonResult<string>> GetConfTbContent(SearchInputDto<Conf_conf> search)
        {
            CommonResult<string> result = new CommonResult<string>();
            List<DbFieldInfo> sourceFields = new List<DbFieldInfo>(); //源字段集合
            List<DbFieldInfo> finFields = new List<DbFieldInfo>(); //目标字段集合
            string sysid = "";
            string confDetailId = "";
            if (!string.IsNullOrEmpty(search.Pkey))
            {
                Conf_conf confModel = confService.Get(search.Pkey);
                if (confModel != null)
                {
                    #region 获取读取表信息
                    if (confModel.ConfFromType == 0) //系统
                    {
                        //Sys_sys sysModel = sysService.Get(confModel.FromParentId);
                        Sys_outmodel_sql sysOutModelSqlModel = sysOutModelSqlService.GetWhere(string.Format(" sys_outmodel_id='{0}'", confModel.FromId));
                        if (sysOutModelSqlModel != null&& !string.IsNullOrEmpty(sysOutModelSqlModel.Sqlstr))
                        {
                            List<SysOutModelSqlEntity> outSqlModelList = sysOutModelSqlModel.Sqlstr.ToObject<List<SysOutModelSqlEntity>>();
                            foreach (SysOutModelSqlEntity item in outSqlModelList)
                            {
                                DbFieldInfo tmpModel = new DbFieldInfo();
                                tmpModel.FieldName = item.Tbname + "." + item.ColumnCode;
                                tmpModel.Description = item.ColumnName;
                                sourceFields.Add(tmpModel);
                            }
                        }
                        else
                        {
                            result.ErrMsg = ErrCode.err1;
                            result.ErrMsg = "数据模型未配置";
                            return result;
                        }

                    }
                    else if (confModel.ConfFromType == 1)
                    {
                        Sd_detail dbDetailModel = await detailService.GetWhereAsync(string.Format(" sd_id = '{0}'", confModel.FromParentId));
                        if (dbDetailModel != null)
                        {
                            List<DbTableInfo> tbModelList = dbDetailModel.Tbs.ToObject<List<DbTableInfo>>();
                            DbTableInfo tbModel = tbModelList.Find(x => x.TableName == confModel.FromId);
                            if (tbModel != null)
                            {
                                sourceFields = tbModel.Fileds;
                            }
                            else
                            {
                                result.ErrCode = ErrCode.err1;
                                result.ErrMsg = "未找到表信息";
                            }
                        }
                        else
                        {
                            result.ErrCode = ErrCode.err1;
                            result.ErrMsg = ErrCode.err80406;
                        }
                    }
                    #endregion

                    #region 获取写入表信息
                    if (confModel.ConfToType == 0) //系统
                    {
                        //Sys_sys sysModel = sysService.Get(confModel.ToParentId);
                        Sys_conf sysconfModel = sysconfService.Get(confModel.ToId);
                        Sys_confOutputDto outsysconfModel = sysconfModel.MapTo<Sys_confOutputDto>();
                        Sys_conf_finalconf scfModel = scfService.GetWhere(string.Format("sys_conf_id='{0}'",sysconfModel.Id));
                        if (outsysconfModel != null)
                        {
                            finFields = scfModel.ConfJson.ToObject<List<DbFieldInfo>>();
                        }
                        else
                        {
                            result.ErrMsg = ErrCode.err1;
                            result.ErrMsg = "数据模型未配置";
                            return result;
                        }
                       
                    }
                    else if (confModel.ConfToType == 1)
                    {
                        Sd_detail dbDetailModel = await detailService.GetWhereAsync(string.Format(" sd_id = '{0}'", confModel.ToParentId));
                        if (dbDetailModel != null)
                        {
                            List<DbTableInfo> tbModelList = dbDetailModel.Tbs.ToObject<List<DbTableInfo>>();
                            DbTableInfo tbModel = tbModelList.Find(x => x.TableName == confModel.ToId);
                            if (tbModel != null)
                            {
                                finFields = tbModel.Fileds;
                            }
                            else
                            {
                                result.ErrCode = ErrCode.err1;
                                result.ErrMsg = "未找到表信息";
                            }
                        }
                        else
                        {
                            result.ErrCode = ErrCode.err1;
                            result.ErrMsg = ErrCode.err80406;
                        }
                    }
                    sysid = confModel.ToParentId;
                    #endregion

                    #region 获取此关联配置详情
                    Conf_detail confDetailModel = iService.GetWhere(string.Format(" conf_id='{0}'", confModel.Id));
                    if (confDetailModel != null && !string.IsNullOrEmpty(confDetailModel.ConfStr))
                    {
                        confDetailId = confDetailModel.Id;
                        List<DbFieldInfo> dbFieldList = confDetailModel.ConfStr.ToObject<List<DbFieldInfo>>();
                        if (dbFieldList != null && dbFieldList.Count > 0)
                        {
                            foreach (DbFieldInfo item in finFields)
                            {
                                DbFieldInfo itemConfig = dbFieldList.Find(x =>
                                      x.TableLevelNum == item.TableLevelNum &&
                                      x.WriteTableName == item.WriteTableName &&
                                      x.FieldName == item.FieldName &&
                                      (
                                          (x.ReadFieldInfo != null && !string.IsNullOrEmpty(x.ReadFieldInfo.FieldName)) || !string.IsNullOrEmpty(x.DefaultValue)
                                      )
                                  );
                                if (itemConfig != null)
                                {
                                    item.ReadFieldInfo = itemConfig.ReadFieldInfo;
                                    item.DefaultValue = itemConfig.DefaultValue;
                                    item.Is_DynamicSingle = itemConfig.Is_DynamicSingle;
                                    item.SyncDataConfParamter = itemConfig.SyncDataConfParamter;
                                }
                            }
                        }
                    }
                    #endregion

                }
                else
                {
                    result.ErrCode = ErrCode.err1;
                    result.ErrMsg = ErrCode.err80407;
                }
            }
            else
            {
                result.ErrCode = ErrCode.err1;
                result.ErrMsg = "未接收到参数信息";
            }
            var resultList = new
            {
                sourceFields=sourceFields,
                finFields = finFields,
                sysid= sysid,
                confDetailId= confDetailId
            };
            result.ResData = resultList;
            result.ErrCode = ErrCode.successCode;
            result.ErrMsg = ErrCode.err0;
            return result;
        }
    }
}