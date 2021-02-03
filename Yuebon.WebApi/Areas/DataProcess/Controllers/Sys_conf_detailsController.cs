using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Log;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Models;
using Yuebon.Commons.Pages;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;
using Yuebon.DataProcess.IServices;
using Yuebon.AspNetCore.Mvc;
using Yuebon.DataProcess.Core.common.dbTools;
using Yuebon.DataProcess.Core.common.Enity;
using Yuebon.DataProcess.Core.common;
using Newtonsoft.Json;

namespace Yuebon.WebApi.Areas.DataProcess.Controllers
{
    /// <summary>
    /// 接口
    /// </summary>
    [ApiController]
    [Route("api/DataProcess/[controller]")]
    public class Sys_conf_detailsController : AreaApiController<Sys_conf_details, Sys_conf_detailsOutputDto,Sys_conf_detailsInputDto,ISys_conf_detailsService,string>
    {
        private ISys_conf_objService ISysconfobjService;
        private IPlug_plugService IPlugService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_iService"></param>
        public Sys_conf_detailsController(ISys_conf_detailsService _iService, ISys_conf_objService _ISysconfobjService, IPlug_plugService _IPlugService) : base(_iService)
        {
            iService = _iService;
            ISysconfobjService = _ISysconfobjService;
            IPlugService = _IPlugService;
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
        public async Task<IActionResult> GetAllEnableByConfId(string id)
        {
            CommonResult<List<Sys_conf_detailsOutputDto>> result = new CommonResult<List<Sys_conf_detailsOutputDto>>();
            IEnumerable<Sys_conf_details> list = await iService.GetAllByIsNotDeleteAndEnabledMarkAsync(string.Format(" sys_conf_id='{0}'", id));
            List<Sys_conf_detailsOutputDto> resultList = list.MapTo<Sys_conf_detailsOutputDto>();
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
        public IActionResult GetTbNameList()
        {
            CommonResult<List<DbTableInfo>> result = new CommonResult<List<DbTableInfo>>();
            if (!string.IsNullOrEmpty(CurrentUser.MDbName) && !string.IsNullOrEmpty(CurrentUser.MDbType) && !string.IsNullOrEmpty(CurrentUser.MDbConnectionstr))
            {
                DataTools bll = new DataTools(CurrentUser.MDbConnectionstr, CurrentUser.MDbType);
                List<DbTableInfo> tbList = bll.GetTbList(CurrentUser.MDbName, "");
                result.ResData = tbList;
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            }
            else
            {
                result.ErrCode = ErrCode.err80010;
                result.ErrMsg = ErrCode.err80010;
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
        public IActionResult GetColumnListsByDetailId(string detailId)
        {
            CommonResult<List<DbTableInfo>> result = new CommonResult<List<DbTableInfo>>();
            if (!string.IsNullOrEmpty(CurrentUser.MDbName) && !string.IsNullOrEmpty(CurrentUser.MDbType) && !string.IsNullOrEmpty(CurrentUser.MDbConnectionstr))
            {
                Sys_conf_details model = iService.Get(detailId);
                DataTools bll = new DataTools(CurrentUser.MDbConnectionstr, CurrentUser.MDbType);
                List<DbFieldInfo> colList = bll.GetAllColumns(CurrentUser.MDbName, model.Tbname);
                if (colList != null && colList.Count > 0)
                {
                    #region 获取列获取方式参数
                    Sys_conf_obj objModel = ISysconfobjService.GetWhere(string.Format(" sys_conf_detail_id='{0}'", detailId));
                    if (objModel != null) {
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
                result.ErrCode = ErrCode.err80010;
                result.ErrMsg = ErrCode.err80010;
            }


            return ToJsonContent(result);
        }

        /// <summary>
        ///  获取指定表列信息
        /// </summary>
        /// <param name="tbName">表名</param>
        /// <returns></returns>
        [HttpPost("GetColumnListsBytbName")]
        [YuebonAuthorize("List")]
        public IActionResult GetColumnListsBytbName([FromBody] dynamic formData)
        {
            CommonResult<List<DbTableInfo>> result = new CommonResult<List<DbTableInfo>>();
            if (!string.IsNullOrEmpty(CurrentUser.MDbName) && !string.IsNullOrEmpty(CurrentUser.MDbType) && !string.IsNullOrEmpty(CurrentUser.MDbConnectionstr))
            {
                string dataStr = formData.ToString();
                var paramsObj = new { tbName = "" };
                paramsObj = JsonConvert.DeserializeAnonymousType(dataStr, paramsObj);

                DataTools bll = new DataTools(CurrentUser.MDbConnectionstr, CurrentUser.MDbType);
                List<DbFieldInfo> colList = bll.GetAllColumns(CurrentUser.MDbName, paramsObj.tbName);
                if (colList != null && colList.Count > 0)
                {
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
                result.ErrCode = ErrCode.err80010;
                result.ErrMsg = ErrCode.err80010;
            }


            return ToJsonContent(result);
        }

        /// <summary>
        ///  获取数据获取方式下拉框数据集
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetDataGetTypeLists")]
        [YuebonAuthorize("List")]
        public async Task<IActionResult> GetDataGetTypeLists()
        {
            CommonResult<List<DbTableInfo>> result = new CommonResult<List<DbTableInfo>>();
            List<Plug_plugOutputDto> plugList = await IPlugService.GetEnableListWithSys(CurrentUser.SysId);
            result.ResData = plugList;
            result.ErrCode = ErrCode.successCode;
            result.ErrMsg = ErrCode.err0;
            return ToJsonContent(result);
        }
    }
}