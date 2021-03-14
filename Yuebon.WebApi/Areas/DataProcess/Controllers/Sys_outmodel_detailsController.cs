using Microsoft.AspNetCore.Mvc;
using System;
using Yuebon.AspNetCore.Controllers;
using Yuebon.Commons.Helpers;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;
using Yuebon.DataProcess.IServices;
using Yuebon.AspNetCore.Mvc;
using System.Threading.Tasks;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Models;
using System.Collections.Generic;
using System.Linq;
using Yuebon.AspNetCore.Models;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Core.Dtos;
using System.Reflection;
using Yuebon.Commons.Extensions;
using Yuebon.DataProcess.Core.OutSideDbService.Entity;
using Newtonsoft.Json;
using Yuebon.DataProcess.Core.common;

namespace Yuebon.WebApi.Areas.DataProcess.Controllers
{
    /// <summary>
    /// 数据输出模型详情表接口
    /// </summary>
    [ApiController]
    [Route("api/DataProcess/[controller]")]
    public class Sys_outmodel_detailsController : AreaApiController<Sys_outmodel_details, Sys_outmodel_detailsOutputDto,Sys_outmodel_detailsInputDto,ISys_outmodel_detailsService,string>
    {
        private ISys_sysService SysService;
        private ISd_sysdbService SdService;
        private ISd_detailService SdDetailService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="_SysService"></param>
        /// <param name="_SdService"></param>
        /// <param name="_SdDetailService"></param>
        public Sys_outmodel_detailsController(ISys_outmodel_detailsService _iService, ISys_sysService _SysService, ISd_sysdbService _SdService, ISd_detailService _SdDetailService) : base(_iService)
        {
            iService = _iService;
            SysService = _SysService;
            SdService = _SdService;
            SdDetailService = _SdDetailService;
            AuthorizeKey.ListKey = "Sys_outmodel_details/List";
            AuthorizeKey.InsertKey = "Sys_outmodel_details/Add";
            AuthorizeKey.UpdateKey = "Sys_outmodel_details/Edit";
            AuthorizeKey.UpdateEnableKey = "Sys_outmodel_details/Enable";
            AuthorizeKey.DeleteKey = "Sys_outmodel_details/Delete";
            AuthorizeKey.DeleteSoftKey = "Sys_outmodel_details/DeleteSoft";
            AuthorizeKey.ViewKey = "Sys_outmodel_details/View";
        }
        /// <summary>
        /// 新增前处理数据
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(Sys_outmodel_details info)
        {
            info.Id = GuidUtils.CreateNo();
            info.EnabledMark = true;
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
        protected override void OnBeforeUpdate(Sys_outmodel_details info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 在软删除数据前对数据的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(Sys_outmodel_details info)
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
        public async Task<IActionResult> GetAllEnableByConfId(SearchInputDto<Sys_outmodel_details> search)
        {
            CommonResult<List<Sys_conf_detailsOutputDto>> result = new CommonResult<List<Sys_conf_detailsOutputDto>>();
            if (search.Filter == null)
            {
                search.Filter = new Sys_outmodel_details();
            }
            IEnumerable<Sys_outmodel_details> list = await iService.GetAllByIsNotDeleteAndEnabledMarkAsync(string.Format(" sys_outmodel_id='{0}'", search.Filter.Sys_outmodel_id));
            List<Sys_outmodel_detailsOutputDto> resultList = list.MapTo<Sys_outmodel_detailsOutputDto>();
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
        [HttpPost("GetOutTbNameList")]
        [YuebonAuthorize("List")]
        public IActionResult GetOutTbNameList([FromBody] dynamic formData)
        {
            CommonResult<List<DbTableInfo>> result = new CommonResult<List<DbTableInfo>>();
            string dataStr = formData.ToString();
            var paramsObj = new { SysId = "",outModelId="" };
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

                        #region 获取并返回输出模型详情中的表
                        if (!string.IsNullOrEmpty(paramsObj.outModelId))
                        {
                            List<Sys_outmodel_details> outModelDetailList = iService.GetListWhere(string.Format(" sys_outmodel_id = '{0}'", paramsObj.outModelId)).ToList();
                            List<Sys_outmodel_detailsOutputDto> outDetailList = outModelDetailList.MapTo<Sys_outmodel_detailsOutputDto>();
                            foreach (Sys_outmodel_detailsOutputDto item in outDetailList)
                            {
                                
                                DbTableInfo tmpTbModel = tbList.Find(x => x.TableName == item.Tbname);
                                if (tmpTbModel != null)
                                {
                                    item.Fileds = tmpTbModel.Fileds;
                                }
                                #region 修改表名
                                item.Tbname = item.Levelnum + "." + item.Tbname;
                                #endregion
                            }
                            result.ResData = outDetailList;
                            result.ErrCode = ErrCode.successCode;
                            result.ErrMsg = ErrCode.err0;
                            return ToJsonContent(result);
                        }
                        
                        #endregion

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

        #region 增删改
        /// <summary>
        /// 异步新增数据
        /// </summary>
        /// <param name="tinfo"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        [YuebonAuthorize("Add")]
        public override async Task<IActionResult> InsertAsync(Sys_outmodel_detailsInputDto tinfo)
        {
            CommonResult result = new CommonResult();
            Sys_outmodel_details info = tinfo.MapTo<Sys_outmodel_details>();
            OnBeforeInsert(info);

            #region 补充执行顺序
            IEnumerable<Sys_outmodel_details> modelList = iService.GetListWhere(string.Format(" sys_outmodel_id='{0}'", info.Sys_outmodel_id));
            List<Sys_outmodel_details> confModelList = new List<Sys_outmodel_details>(modelList);
            info.Levelnum = confModelList.Count + 1;
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
        public override async Task<IActionResult> UpdateAsync(Sys_outmodel_detailsInputDto tinfo, string id)
        {
            CommonResult result = new CommonResult();

            Sys_outmodel_details newInfo = tinfo.MapTo<Sys_outmodel_details>();
            Sys_outmodel_details info = iService.Get(id);
            info = SwapValue(info, newInfo);

            OnBeforeUpdate(info);

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

        /// <summary>
        /// 异步批量物理删除
        /// </summary>
        /// <param name="info"></param>
        [HttpDelete("DeleteBatchAsync")]
        [YuebonAuthorize("Delete")]
        public override async Task<IActionResult> DeleteBatchAsync(DeletesInputDto info)
        {
            CommonResult result = new CommonResult();
            string id = info.Ids[0].ToString();
            Sys_outmodel_details model = iService.Get(id);
            string where = "id in ('" + info.Ids.Join(",").Trim(',').Replace(",", "','") + "')";
            if (!string.IsNullOrEmpty(where))
            {
                bool bl = await iService.DeleteBatchWhereAsync(where).ConfigureAwait(false);
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

        #endregion

        #region 辅助方法

        /// <summary>
        /// 将新实体类中的非空值复制给原实体类
        /// </summary>
        /// <param name="info">原实体类</param>
        /// <param name="newInfo">新实体类</param>
        /// <returns></returns>
        private Sys_outmodel_details SwapValue(Sys_outmodel_details info, Sys_outmodel_details newInfo)
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