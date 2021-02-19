using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Models;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;
using Yuebon.DataProcess.IServices;
using Yuebon.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using Yuebon.Commons.Dtos;
using Yuebon.AspNetCore.ViewModel;
using Yuebon.Commons.Extensions;
using Yuebon.Commons.Core.Dtos;
using Yuebon.DataProcess.Core.OutSideDbService;
using Yuebon.DataProcess.Core.OutSideDbService.Entity;
using Yuebon.DataProcess.Core.common;
using Yuebon.DataProcess.Core.common.Entity;

namespace Yuebon.WebApi.Areas.DataProcess.Controllers
{
    /// <summary>
    /// 接口
    /// </summary>
    [ApiController]
    [Route("api/DataProcess/[controller]")]
    public class Sys_conf_detailsController : AreaApiController<Sys_conf_details, Sys_conf_detailsOutputDto,Sys_conf_detailsInputDto,ISys_conf_detailsService,string>
    {
        private ISys_confService SysconfService;
        private ISys_conf_objService ISysconfobjService;
        private IPlug_plugService IPlugService;
        private ISys_sysService SysService;
        private ISd_sysdbService SdService;
        private ISd_detailService SdDetailService;
        private IPlug_ConfDetailService PcdService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="_ISysconfobjService"></param>
        /// <param name="_SysconfService"></param>
        /// <param name="_IPlugService"></param>
        /// <param name="_SysService"></param>
        /// <param name="_SdService"></param>
        /// <param name="_SdDetailService"></param>
        public Sys_conf_detailsController(ISys_conf_detailsService _iService, ISys_conf_objService _ISysconfobjService, ISys_confService _SysconfService, IPlug_plugService _IPlugService, ISys_sysService _SysService, ISd_sysdbService _SdService, ISd_detailService _SdDetailService, IPlug_ConfDetailService _PcdService) : base(_iService)
        {
            iService = _iService;
            SysconfService = _SysconfService;
            ISysconfobjService = _ISysconfobjService;
            IPlugService = _IPlugService;
            SysService = _SysService;
            SdService = _SdService;
            SdDetailService = _SdDetailService;
            PcdService = _PcdService;
            AuthorizeKey.ListKey = "Sys_conf_details/List";
            AuthorizeKey.InsertKey = "Sys_conf_details/Add";
            AuthorizeKey.UpdateKey = "Sys_conf_details/Edit";
            AuthorizeKey.UpdateEnableKey = "Sys_conf_details/Enable";
            AuthorizeKey.DeleteKey = "Sys_conf_details/Delete";
            AuthorizeKey.DeleteSoftKey = "Sys_conf_details/DeleteSoft";
            AuthorizeKey.ViewKey = "Sys_conf_details/View";
        }
        /// <summary>
        /// 新增前处理数据
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(Sys_conf_details info)
        {
            info.Id = GuidUtils.CreateNo();
            info.CreatorTime = DateTime.Now;
            info.CreatorUserId = CurrentUser.UserId;
            info.EnabledMark = true;
            info.DeleteMark = false;
            if (info.SortCode == null)
            {
                info.SortCode = 99;
            }
        }
        
        /// <summary>
        /// 在更新数据前对数据的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeUpdate(Sys_conf_details info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 在软删除数据前对数据的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(Sys_conf_details info)
        {
            info.DeleteMark = true;
            info.DeleteTime = DateTime.Now;
            info.DeleteUserId = CurrentUser.UserId; 
        }

        /// <summary>
        /// 获取所有可用的
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAllEnableByConfId")]
        [YuebonAuthorize("List")]
        public async Task<IActionResult> GetAllEnableByConfId(SearchInputDto<Sys_conf_details> search)
        {
            CommonResult<List<Sys_conf_detailsOutputDto>> result = new CommonResult<List<Sys_conf_detailsOutputDto>>();
            if (search.Filter == null)
            {
                search.Filter = new Sys_conf_details();
            }
            IEnumerable<Sys_conf_details> list = await iService.GetAllByIsNotDeleteAndEnabledMarkAsync(string.Format(" sys_conf_id='{0}'", search.Filter.Sys_conf_id));
            List<Sys_conf_detailsOutputDto> resultList = list.MapTo<Sys_conf_detailsOutputDto>();
            resultList = resultList.OrderBy(x => x.Levelnum).ToList();
            result.ResData = resultList;
            result.ErrCode = ErrCode.successCode;
            result.ErrMsg = ErrCode.err0;

            return ToJsonContent(result);
        }

        /// <summary>
        /// 获取所有表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetTbNameList")]
        [YuebonAuthorize("List")]
        public IActionResult GetTbNameList([FromBody] dynamic formData)
        {
            CommonResult<List<DbTableInfo>> result = new CommonResult<List<DbTableInfo>>();
            string dataStr = formData.ToString();
            var paramsObj = new { SysId = "" };
            paramsObj = JsonConvert.DeserializeAnonymousType(dataStr, paramsObj);
            if (!string.IsNullOrEmpty(paramsObj.SysId))
            {
                Sys_sys sysModel = SysService.Get(paramsObj.SysId);
                if (sysModel != null)
                {
                    Sd_sysdb dbModel = SdService.Get(sysModel.MdbId);
                    if (dbModel != null)
                    {
                        Sd_detail sdDetailModel = SdDetailService.GetWhere(string.Format(" sd_id = '{0}'", dbModel.Id));
                        List<DbTableInfo> tbList = sdDetailModel.Tbs.ToObject<List<DbTableInfo>>();
                        result.ResData = tbList;
                        result.ErrCode = ErrCode.successCode;
                        result.ErrMsg = ErrCode.err0;
                    }
                    else
                    {
                        result.ErrCode = ErrCode.err80406;
                        result.ErrMsg = ErrCode.err80406;
                    }
                }
                else
                {
                    result.ErrCode = ErrCode.err80405;
                    result.ErrMsg = ErrCode.err80405;
                }
            }
            else
            {
                result.ErrCode = ErrCode.err1;
                result.ErrMsg = "未获取到所选系统参数";
            }
            return ToJsonContent(result);
        }

        /// <summary>
        ///  获取指定表列信息
        /// </summary>
        /// <param name="detailId">详情ID</param>
        /// <returns></returns>
        [HttpPost("GetColumnListsByDetailId")]
        [YuebonAuthorize("List")]
        public IActionResult GetColumnListsByDetailId([FromBody] dynamic formData)
        {
            CommonResult<List<DbTableInfo>> result = new CommonResult<List<DbTableInfo>>();
            string dataStr = formData.ToString();
            var paramsObj = new { SysId = "", detailId="" };
            paramsObj = JsonConvert.DeserializeAnonymousType(dataStr, paramsObj);
            if (!string.IsNullOrEmpty(paramsObj.SysId))
            {
                Sys_sys sysModel = SysService.Get(paramsObj.SysId);
                if (sysModel != null)
                {
                    Sd_sysdb dbModel = SdService.Get(sysModel.MdbId);
                    if (dbModel != null)
                    {
                        Sys_conf_details model = iService.Get(paramsObj.detailId);
                        DbTools bll = new DbTools(dbModel.Sdconnectionstr, dbModel.Sdtype);
                        List<DbFieldInfo> colList = bll.GetAllColumns(dbModel.dbName, model.Tbname);
                        if (colList != null && colList.Count > 0)
                        {
                            #region 获取列获取方式参数
                            Sys_conf_obj objModel = ISysconfobjService.GetWhere(string.Format(" sys_conf_detail_id='{0}'", paramsObj.detailId));
                            if (objModel != null)
                            {
                                Dictionary<string, string> dic = objModel.Configjson.ToObject<Dictionary<string, string>>();
                                foreach (DbFieldInfo item in colList)
                                {
                                    if (dic.ContainsKey(item.FieldName))
                                    {
                                        item.GetFunctionParamter = dic[item.FieldName];
                                    }
                                }
                            }
                            #endregion
                            result.ResData = colList;
                            result.ErrCode = ErrCode.successCode;
                            result.ErrMsg = ErrCode.err0;
                        }
                        else
                        {
                            result.ErrCode = ErrCode.err80012;
                            result.ErrMsg = ErrCode.err80012;
                        }
                    }
                    else
                    {
                        result.ErrCode = ErrCode.err80406;
                        result.ErrMsg = ErrCode.err80406;
                    }
                }
                else
                {
                    result.ErrCode = ErrCode.err80405;
                    result.ErrMsg = ErrCode.err80405;
                }
            }
            else
            {
                result.ErrCode = ErrCode.err1;
                result.ErrMsg = "未获取到所选系统参数";
            }
            return ToJsonContent(result);
        }

        ///// <summary>
        /////  获取指定表列信息
        ///// </summary>
        ///// <param name="tbName">表名</param>
        ///// <returns></returns>
        //[HttpPost("GetColumnListsBytbName")]
        //[YuebonAuthorize("List")]
        //public IActionResult GetColumnListsBytbName([FromBody] dynamic formData)
        //{
        //    CommonResult<List<DbTableInfo>> result = new CommonResult<List<DbTableInfo>>();
        //    string dataStr = formData.ToString();
        //    var paramsObj = new { SysId = "", tbName="" };
        //    paramsObj = JsonConvert.DeserializeAnonymousType(dataStr, paramsObj);
        //    if (!string.IsNullOrEmpty(paramsObj.SysId))
        //    {
        //        Sys_sys sysModel = SysService.Get(paramsObj.SysId);
        //        if (sysModel != null)
        //        {
        //            Sd_sysdb dbModel = SdService.Get(sysModel.MdbId);
        //            if (dbModel != null)
        //            {
        //                DbTools bll = new DbTools(dbModel.Sdconnectionstr, dbModel.Sdtype);
        //                List<DbFieldInfo> colList = bll.GetAllColumns(dbModel.dbName, paramsObj.tbName);
        //                if (colList != null && colList.Count > 0)
        //                {
        //                    result.ResData = colList;
        //                    result.ErrCode = ErrCode.successCode;
        //                    result.ErrMsg = ErrCode.err0;
        //                }
        //                else
        //                {
        //                    result.ErrCode = ErrCode.err80012;
        //                    result.ErrMsg = ErrCode.err80012;
        //                }
        //            }
        //            else
        //            {
        //                result.ErrCode = ErrCode.err80406;
        //                result.ErrMsg = ErrCode.err80406;
        //            }
        //        }
        //        else
        //        {
        //            result.ErrCode = ErrCode.err80405;
        //            result.ErrMsg = ErrCode.err80405;
        //        }
        //    }
        //    else
        //    {
        //        result.ErrCode = ErrCode.err1;
        //        result.ErrMsg = "未获取到所选系统参数";
        //    }
        //    return ToJsonContent(result);
        //}

        /// <summary>
        /// 根据主键Id获取一个对象信息
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <returns></returns>
        [HttpPost("GetByConfDetailId")]
        [YuebonAuthorize("List")]
        public async Task<CommonResult<Sys_conf_detailsOutputDto>> GetByConfDetailId([FromBody] dynamic formData)
        {
            CommonResult<Sys_conf_detailsOutputDto> result = new CommonResult<Sys_conf_detailsOutputDto>();
            string dataStr = formData.ToString();
            var paramsObj = new { SysId = "", id = "" };
            paramsObj = JsonConvert.DeserializeAnonymousType(dataStr, paramsObj);
            if (!string.IsNullOrEmpty(paramsObj.SysId))
            {
                Sys_sys sysModel = SysService.Get(paramsObj.SysId);
                if (sysModel != null)
                {
                    Sd_sysdb dbModel = SdService.Get(sysModel.MdbId);
                    if (dbModel != null)
                    {
                        Sys_conf_detailsOutputDto model = await iService.GetOutDtoAsync(paramsObj.id);

                        #region 获取数据库中存储的表结构信息
                        Sd_detail sdDetailModel = SdDetailService.GetWhere(string.Format(" sd_id = '{0}'", dbModel.Id));
                        List<DbTableInfo> tbList = sdDetailModel.Tbs.ToObject<List<DbTableInfo>>();
                        #endregion

                        #region 获取配置字符串信息
                        List<DbTableOperaInfo> verifyConfs = null; //字段验证插件配置信息
                        List<DbTableOperaInfo> getDataConfs = null;//字段导入插件配置信息
                        IEnumerable<Plug_ConfDetail> pcModelList = await PcdService.GetListWhereAsync(string.Format(" obj_id = '{0}'", sdDetailModel.Id));
                        if (pcModelList != null&&pcModelList.Count()>0)
                        {
                            List<Plug_ConfDetail> pcdModelList = pcModelList.ToList();
                            Plug_ConfDetail verifyPcdModel = pcdModelList.Find(x => x.ConfType == 0);
                            if (verifyPcdModel != null&&!string.IsNullOrEmpty(verifyPcdModel.ConfJson))
                            {
                                verifyConfs = verifyPcdModel.ConfJson.ToObject<List<DbTableOperaInfo>>();
                            }
                            Plug_ConfDetail getDataPcdModel = pcdModelList.Find(x => x.ConfType == 1);
                            if (getDataPcdModel != null && !string.IsNullOrEmpty(getDataPcdModel.ConfJson))
                            {
                                getDataConfs = getDataPcdModel.ConfJson.ToObject<List<DbTableOperaInfo>>();
                            }
                        }
                        #endregion

                        #region 获取指定表列信息
                        DbTableInfo tbModel = tbList.Find(x => x.TableName == model.Tbname);
                        if (tbModel != null && tbModel.Fileds != null)
                        {
                            List<DbFieldInfo> colList = tbModel.Fileds;
                            if (colList != null && colList.Count > 0)
                            {
                                #region 获取列获取方式参数
                                Sys_conf_obj objModel = ISysconfobjService.GetWhere(string.Format(" sys_conf_detail_id='{0}'", paramsObj.id));
                                if (objModel != null)
                                {
                                    List<DbFieldInfo> dbColumnList = objModel.Configjson.ToObject<List<DbFieldInfo>>();
                                    foreach (DbFieldInfo item in colList)
                                    {
                                        DbFieldInfo tmpModel = dbColumnList.Find(x => x.FieldName == item.FieldName);
                                        if (tmpModel != null)
                                        {
                                            item.DataGetType = tmpModel.DataGetType;
                                            item.HasPage = tmpModel.HasPage;
                                            item.ConfigUri = tmpModel.ConfigUri;
                                            item.Is_SingleDataKey = tmpModel.Is_SingleDataKey;
                                            item.Is_Visible = tmpModel.Is_Visible;
                                            item.Is_KeyColumn = tmpModel.Is_KeyColumn;
                                            item.Is_NotNull = tmpModel.Is_NotNull;
                                            item.Is_NoUpdate = tmpModel.Is_NoUpdate;
                                            item.Is_ChangeWhite = tmpModel.Is_ChangeWhite;
                                            if (verifyConfs != null)
                                            {
                                                DbTableOperaInfo verifyModel = verifyConfs.Find(x => x.FieldName == item.FieldName);
                                                if (verifyModel != null)
                                                {
                                                    item.VerifyFunctionParamter = verifyModel.OperaParamers;
                                                }
                                            }
                                            if (getDataConfs != null)
                                            {
                                                DbTableOperaInfo getDataModel = getDataConfs.Find(x => x.FieldName == item.FieldName);
                                                if (getDataModel != null)
                                                {
                                                    item.GetFunctionParamter = getDataModel.OperaParamers;
                                                }
                                            }
                                        }
                                    }
                                }
                                model.configjson = colList.ToJson();
                                #endregion
                                result.ResData = model;
                                result.ErrCode = ErrCode.successCode;
                                result.ErrMsg = ErrCode.err0;
                            }
                            else
                            {
                                result.ErrCode = ErrCode.err80012;
                                result.ErrMsg = ErrCode.err80012;
                            }
                        }
                        else
                        {
                            result.ErrCode = ErrCode.err80406;
                            result.ErrMsg = ErrCode.err80406;
                        }
                        #endregion


                    }
                    else
                    {
                        result.ErrCode = ErrCode.err80406;
                        result.ErrMsg = ErrCode.err80406;
                    }
                }
                else
                {
                    result.ErrCode = ErrCode.err80405;
                    result.ErrMsg = ErrCode.err80405;
                }
            }
            else
            {
                result.ErrCode = ErrCode.err1;
                result.ErrMsg = "未获取到所选系统参数";
            }
            return result;
        }


        #region 增删改


        /// <summary>
        /// 异步新增数据
        /// </summary>
        /// <param name="tinfo"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        [YuebonAuthorize("Add")]
        public override async Task<IActionResult> InsertAsync(Sys_conf_detailsInputDto tinfo)
        {
            CommonResult result = new CommonResult();

            Sys_conf_details info = tinfo.MapTo<Sys_conf_details>();
            OnBeforeInsert(info);
            #region 补充执行顺序
            IEnumerable<Sys_conf_details> modelList = iService.GetListWhere(string.Format(" sys_conf_id='{0}'", info.Sys_conf_id));
            List<Sys_conf_details> confModelList = new List<Sys_conf_details>(modelList);
            info.Levelnum = confModelList.Count + 1;
            #endregion

            Sys_conf_obj objInfo = new Sys_conf_obj();
            objInfo.Sys_conf_detail_id = info.Id;
            List<DbFieldInfo> dfModelList = tinfo.configjson.ToObject<List<DbFieldInfo>>();
            List<DbTableOperaInfo> verifyDtoModelList = new List<DbTableOperaInfo>();
            List<DbTableOperaInfo> getDataDtoModelList = new List<DbTableOperaInfo>();
            foreach (DbFieldInfo item in dfModelList)
            {
                #region 添加验证配置项
                if (!string.IsNullOrEmpty(item.VerifyFunctionParamter))
                {
                    DbTableOperaInfo verifyDtoModel = new DbTableOperaInfo();
                    verifyDtoModel.FieldName = item.FieldName;
                    verifyDtoModel.OperaParamers = item.VerifyFunctionParamter;
                    verifyDtoModelList.Add(verifyDtoModel);
                    item.VerifyFunctionParamter = null; //置空，详情中不做存储
                }
                #endregion

                #region 添加数据获取配置项
                if (!string.IsNullOrEmpty(item.GetFunctionParamter))
                {
                    DbTableOperaInfo getDataDtoModel = new DbTableOperaInfo();
                    getDataDtoModel.FieldName = item.FieldName;
                    getDataDtoModel.OperaType = item.DataGetType.value;
                    getDataDtoModel.OperaParamers = item.GetFunctionParamter;
                    getDataDtoModelList.Add(getDataDtoModel);
                    item.GetFunctionParamter = null; //置空，详情中不做存储
                }
                #endregion
            }
            objInfo.Configjson = dfModelList.ToJson();
            objInfo.Id = GuidUtils.CreateNo();
            bool objResult= await ISysconfobjService.InsertAsync(objInfo) > 0;

            #region 添加验证插件配置信息
            if (verifyDtoModelList.Count > 0)
            {
                Plug_ConfDetail verifyPcdModel = new Plug_ConfDetail();
                verifyPcdModel.Id = GuidUtils.CreateNo();
                verifyPcdModel.Obj_Id = info.Id;
                verifyPcdModel.ConfType = 0;
                verifyPcdModel.ConfJson = verifyDtoModelList.ToJson();
                bool verifyInsResult = await PcdService.InsertAsync(verifyPcdModel) > 0;
            }
            #endregion

            #region 添加数据获取插件配置信息
            if (getDataDtoModelList.Count > 0)
            {
                Plug_ConfDetail getDataPcdModel = new Plug_ConfDetail();
                getDataPcdModel.Id = GuidUtils.CreateNo();
                getDataPcdModel.Obj_Id = info.Id;
                getDataPcdModel.ConfType = 1;
                getDataPcdModel.ConfJson = getDataDtoModelList.ToJson();
                bool getDataInsResult = await PcdService.InsertAsync(getDataPcdModel) > 0;
            }
            #endregion

            result.Success = await iService.InsertAsync(info) > 0;
            if (result.Success)
            {
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            }
            else
            {
                result.ErrMsg = ErrCode.err43001;
                result.ErrCode = "43001";
            }
            return ToJsonContent(result);
        }
        /// <summary>
        /// 异步更新数据
        /// </summary>
        /// <param name="tinfo"></param>
        /// <param name="id">主键Id</param>
        /// <returns></returns>
        [HttpPost("Update")]
        [YuebonAuthorize("Edit")]
        public override async Task<IActionResult> UpdateAsync(Sys_conf_detailsInputDto tinfo, string id)
        {
            CommonResult result = new CommonResult();

            Sys_conf_details newInfo = tinfo.MapTo<Sys_conf_details>();
            Sys_conf_details info = iService.Get(id);
            info = SwapValue(info, newInfo);

            OnBeforeUpdate(info);

            #region 更新配置详情信息
            bool objResult = true;
            Sys_conf_obj objNewInfo = new Sys_conf_obj();
            objNewInfo.Sys_conf_detail_id = info.Id;
            List<DbFieldInfo> dfModelList = tinfo.configjson.ToObject<List<DbFieldInfo>>();
            List<DbTableOperaInfo> verifyDtoModelList = new List<DbTableOperaInfo>();
            List<DbTableOperaInfo> getDataDtoModelList = new List<DbTableOperaInfo>();
            foreach (DbFieldInfo item in dfModelList)
            {
                #region 添加验证配置项
                if (!string.IsNullOrEmpty(item.VerifyFunctionParamter))
                {
                    DbTableOperaInfo verifyDtoModel = new DbTableOperaInfo();
                    verifyDtoModel.FieldName = item.FieldName;
                    verifyDtoModel.OperaParamers = item.VerifyFunctionParamter;
                    verifyDtoModelList.Add(verifyDtoModel);
                    item.VerifyFunctionParamter = null; //置空，详情中不做存储
                }
                #endregion

                #region 添加数据获取配置项
                if (!string.IsNullOrEmpty(item.GetFunctionParamter))
                {
                    DbTableOperaInfo getDataDtoModel = new DbTableOperaInfo();
                    getDataDtoModel.FieldName = item.FieldName;
                    getDataDtoModel.OperaType = item.DataGetType.value;
                    getDataDtoModel.OperaParamers = item.GetFunctionParamter;
                    getDataDtoModelList.Add(getDataDtoModel);
                    item.GetFunctionParamter = null; //置空，详情中不做存储
                }
                #endregion
            }
            objNewInfo.Configjson = dfModelList.ToJson();
            Sys_conf_obj objInfo = ISysconfobjService.GetWhere(string.Format(" sys_conf_detail_id='{0}'", info.Id));
            if (objInfo != null)
            {
                objInfo.Configjson = objNewInfo.Configjson;
                objResult=await ISysconfobjService.UpdateAsync(objInfo,objInfo.Id).ConfigureAwait(false);

                #region 更新验证插件配置信息
                Plug_ConfDetail verifyPcdModel = PcdService.GetWhere(string.Format(" obj_id='{0}' and conftype=0", info.Id));
                if (verifyPcdModel != null)
                {
                    verifyPcdModel.ConfJson = verifyDtoModelList.ToJson();
                    bool verifyResult = await PcdService.UpdateAsync(verifyPcdModel, verifyPcdModel.Id);
                }
                else
                {
                    if (verifyDtoModelList.Count > 0)
                    {
                        verifyPcdModel = new Plug_ConfDetail();
                        verifyPcdModel.Id = GuidUtils.CreateNo();
                        verifyPcdModel.Obj_Id = info.Id;
                        verifyPcdModel.ConfType = 0;
                        verifyPcdModel.ConfJson = verifyDtoModelList.ToJson();
                        bool verifyInsResult = await PcdService.InsertAsync(verifyPcdModel) > 0;
                    }
                }
                #endregion

                #region 更新数据获取插件配置信息
                Plug_ConfDetail getDataPcdModel = PcdService.GetWhere(string.Format(" obj_id='{0}' and conftype=0", info.Id));
                if (verifyPcdModel != null)
                {
                    getDataPcdModel.ConfJson = verifyDtoModelList.ToJson();
                    bool verifyResult = await PcdService.UpdateAsync(getDataPcdModel, getDataPcdModel.Id);
                }
                else
                {
                    if (getDataDtoModelList.Count > 0)
                    {
                        getDataPcdModel = new Plug_ConfDetail();
                        getDataPcdModel.Id = GuidUtils.CreateNo();
                        getDataPcdModel.Obj_Id = info.Id;
                        getDataPcdModel.ConfType = 1;
                        getDataPcdModel.ConfJson = getDataDtoModelList.ToJson();
                        bool verifyInsResult = await PcdService.InsertAsync(getDataPcdModel) > 0;
                    }
                }
                #endregion

            }
            else
            {
                objInfo = objNewInfo;
                objInfo.Id= GuidUtils.CreateNo();
                objResult = await ISysconfobjService.InsertAsync(objInfo) > 0;
            }
            #endregion

            bool bl = await iService.UpdateAsync(info, id).ConfigureAwait(false);
            if (bl)
            {
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            }
            else
            {
                result.ErrMsg = ErrCode.err43002;
                result.ErrCode = "43002";
            }
            return ToJsonContent(result);
        }

        // <summary>
        /// 异步批量软删除信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost("DeleteSoftBatchAsync")]
        [YuebonAuthorize("DeleteSoft")]
        public override async Task<IActionResult> DeleteSoftBatchAsync(UpdateEnableViewModel info)
        {
            CommonResult result = new CommonResult();
            string where = "id in ('" + info.Ids.Join(",").Trim(',').Replace(",", "','") + "')";
            string objWhere= "sys_conf_detail_id in ('" + info.Ids.Join(",").Trim(',').Replace(",", "','") + "')";
            if (!string.IsNullOrEmpty(where))
            {
                bool bl = false;
                if (info.Flag == "1")
                {
                    bl = true;
                }
                bool blResult = await iService.DeleteSoftBatchAsync(bl, where, CurrentUser.UserId);
                bool ObjblResult = await ISysconfobjService.DeleteSoftBatchAsync(bl, where, CurrentUser.UserId);
                if (blResult)
                {
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                }
                else
                {
                    result.ErrMsg = ErrCode.err43002;
                    result.ErrCode = "43002";
                }
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 异步批量物理删除
        /// </summary>
        /// <param name="info"></param>
        [HttpDelete("DeleteBatchAsync")]
        [YuebonAuthorize("Delete")]
        public override async Task<IActionResult> DeleteBatchAsync(DeletesInputDto info)
        {
            CommonResult result = new CommonResult();
            string where = "id in ('" + info.Ids.Join(",").Trim(',').Replace(",", "','") + "')";
            string objWhere = "sys_conf_detail_id in ('" + info.Ids.Join(",").Trim(',').Replace(",", "','") + "')";
            if (!string.IsNullOrEmpty(where))
            {
                bool bl = await iService.DeleteBatchWhereAsync(where).ConfigureAwait(false);
                bool ObjblResult = await ISysconfobjService.DeleteBatchWhereAsync(objWhere).ConfigureAwait(false);
                if (bl)
                {
                    result.ErrCode = ErrCode.successCode;
                    result.ErrMsg = ErrCode.err0;
                }
                else
                {
                    result.ErrMsg = ErrCode.err43003;
                    result.ErrCode = "43003";
                }
            }
            return ToJsonContent(result);
        }



        #region 更改执行顺序方法
        /// <summary>
        /// 更改执行顺序
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="actionStr">执行动作</param>
        /// <returns></returns>
        [HttpPost("ChangeLevelNumAsync")]
        [YuebonAuthorize("Edit")]
        public async Task<IActionResult> ChangeLevelNumAsync([FromBody] dynamic formData)
        {
            CommonResult result = new CommonResult();
            try
            {
                string dataStr = formData.ToString();
                var paramsObj = new { Id = "", actionStr="" };
                paramsObj = JsonConvert.DeserializeAnonymousType(dataStr, paramsObj);
                await iService.ChangeLevelNumAsync(paramsObj.Id, paramsObj.actionStr);
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            }
            catch (Exception ex)
            {
                result.ErrMsg = ex.Message.ToString();
                result.ErrCode = "43002";
            }
            return ToJsonContent(result);
        }
        #endregion

        #endregion

        #region 辅助方法

        /// <summary>
        /// 将新实体类中的非空值复制给原实体类
        /// </summary>
        /// <param name="info">原实体类</param>
        /// <param name="newInfo">新实体类</param>
        /// <returns></returns>
        private Sys_conf_details SwapValue(Sys_conf_details info, Sys_conf_details newInfo)
        {
            PropertyInfo[] propertys = newInfo.GetType().GetProperties();
            foreach (PropertyInfo property in propertys)
            {
                object val = newInfo.GetType().GetProperty(property.Name).GetValue(newInfo, null);
                if (val != null)
                {
                    info.GetType().GetProperty(property.Name).SetValue(info, val, null);
                }
            }
            return info;
        }
        #endregion
    }
}