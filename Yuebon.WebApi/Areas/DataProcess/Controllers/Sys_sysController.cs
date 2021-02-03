using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Log;
using Yuebon.Commons.Models;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;
using Yuebon.DataProcess.IServices;
using Yuebon.AspNetCore.Mvc;
using Yuebon.Commons.Json;
using Yuebon.AspNetCore.Mvc.Filter;
using Newtonsoft.Json;
using Yuebon.Commons.Cache;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Dtos;

namespace Yuebon.WebApi.Areas.DataProcess.Controllers
{
    /// <summary>
    /// 接口
    /// </summary>
    [ApiController]
    [Route("api/DataProcess/[controller]")]
    public class Sys_sysController : AreaApiController<Sys_sys, Sys_sysOutputDto,Sys_sysInputDto,ISys_sysService,string>
    {
        private ISd_sysdbService sysdbService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_iService"></param>
        public Sys_sysController(ISys_sysService _iService, ISd_sysdbService _sysdbService) : base(_iService)
        {
            iService = _iService;
            sysdbService = _sysdbService;
            AuthorizeKey.ListKey = "Sys_sys/List";
            AuthorizeKey.InsertKey = "Sys_sys/Add";
            AuthorizeKey.UpdateKey = "Sys_sys/Edit";
            AuthorizeKey.UpdateEnableKey = "Sys_sys/Enable";
            AuthorizeKey.DeleteKey = "Sys_sys/Delete";
            AuthorizeKey.DeleteSoftKey = "Sys_sys/DeleteSoft";
            AuthorizeKey.ViewKey = "Sys_sys/View";
        }
        /// <summary>
        /// 新增前处理数据
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(Sys_sys info)
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
        protected override void OnBeforeUpdate(Sys_sys info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 在软删除数据前对数据的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(Sys_sys info)
        {
            info.DeleteMark = true;
            info.DeleteTime = DateTime.Now;
            info.DeleteUserId = CurrentUser.UserId;
        }

        /// <summary>
        /// 配置主数据库
        /// </summary>
        /// <returns></returns>
        [HttpPost("UpdateMDb")]
        [YuebonAuthorize("Edit")]
        public async Task<IActionResult> UpdateMDb([FromBody] dynamic formData)
        {
            CommonResult result = new CommonResult();
            try
            {
                string dataStr = formData.ToString();
                var paramsObj = new { sysId = "", dbId = "" };
                paramsObj = JsonConvert.DeserializeAnonymousType(dataStr, paramsObj);
                Sys_sys model = iService.Get(paramsObj.sysId);
                model.MdbId = paramsObj.dbId;
                bool res = await iService.UpdateAsync(model, model.Id);
                if (res)
                {
                    result.Success = true;
                    result.ErrCode = ErrCode.successCode;
                }
                else
                {
                    result.Success = false;
                    result.ErrCode = ErrCode.err43001;
                }


            }
            catch (Exception ex)
            {
                Log4NetHelper.Error("配置主数据库失败", ex);
                result.ErrMsg = ErrCode.err43001;
                result.ErrCode = "43001";
            }
            return ToJsonContent(result);
        }

        /// <summary>
        /// 获取文章分类适用于Vue 树形列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("ChoseSys")]
        [YuebonAuthorize("List")]
        public async Task<IActionResult> ChoseSys([FromBody] dynamic formData)
        {
            CommonResult result = new CommonResult();
            try
            {
                var aa = formData;
                string dataStr = formData.ToString();
                var paramsObj = new { sysId = "" };
                paramsObj = JsonConvert.DeserializeAnonymousType(dataStr, paramsObj);
                Sys_sys model = iService.Get(paramsObj.sysId);
                CurrentUser.SysId = model.Id;
                CurrentUser.SysName = model.Sysname;
                #region 获取主数据库信息并写入
                if (!string.IsNullOrEmpty(model.MdbId))
                {
                    CurrentUser.MDbId = model.MdbId;
                    Sd_sysdb mDBModel = sysdbService.Get(model.MdbId);
                    if (mDBModel != null)
                    {
                        CurrentUser.MDbName = mDBModel.dbName;
                        CurrentUser.MDbType = mDBModel.Sdtype;
                        CurrentUser.MDbConnectionstr = mDBModel.Sdconnectionstr;
                    }
                }
                #endregion

                YuebonCacheHelper yuebonCacheHelper = new YuebonCacheHelper();
                yuebonCacheHelper.Replace("login_user_" + CurrentUser.UserId, CurrentUser);
                result.Success = true;
                result.ErrCode = ErrCode.successCode;

            }
            catch (Exception ex)
            {
                Log4NetHelper.Error("获取组织结构异常", ex);
                result.ErrMsg = ErrCode.err40110;
                result.ErrCode = "40110";
            }
            return ToJsonContent(result);
        }
    }
}