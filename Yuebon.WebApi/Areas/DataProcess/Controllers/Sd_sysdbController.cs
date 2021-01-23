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
using Yuebon.Commons.Dtos;
using Yuebon.AspNetCore.Mvc;
using System.Reflection;

namespace Yuebon.WebApi.Areas.DataProcess.Controllers
{
    /// <summary>
    /// 接口
    /// </summary>
    [ApiController]
    [Route("api/DataProcess/[controller]")]
    public class Sd_sysdbController : AreaApiController<Sd_sysdb, Sd_sysdbOutputDto,Sd_sysdbInputDto,ISd_sysdbService,string>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_iService"></param>
        public Sd_sysdbController(ISd_sysdbService _iService) : base(_iService)
        {
            iService = _iService;
            AuthorizeKey.ListKey = "Sd_sysdb/List";
            AuthorizeKey.InsertKey = "Sd_sysdb/Add";
            AuthorizeKey.UpdateKey = "Sd_sysdb/Edit";
            AuthorizeKey.UpdateEnableKey = "Sd_sysdb/Enable";
            AuthorizeKey.DeleteKey = "Sd_sysdb/Delete";
            AuthorizeKey.DeleteSoftKey = "Sd_sysdb/DeleteSoft";
            AuthorizeKey.ViewKey = "Sd_sysdb/View";
        }
        /// <summary>
        /// 新增前处理数据
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(Sd_sysdb info)
        {
            info.Id = GuidUtils.CreateNo();
            info.Sys_id = CurrentUser.SysId;
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
        protected override void OnBeforeUpdate(Sd_sysdb info)
        {
            info.Sys_id = CurrentUser.SysId;
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 在软删除数据前对数据的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(Sd_sysdb info)
        {
            info.DeleteMark = true;
            info.DeleteTime = DateTime.Now;
            info.DeleteUserId = CurrentUser.UserId;
        }

        #region 增删改查


        /// <summary>
        /// 异步新增数据
        /// </summary>
        /// <param name="tinfo"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        [YuebonAuthorize("Add")]
        public override async Task<IActionResult> InsertAsync(Sd_sysdbInputDto tinfo)
        {
            CommonResult result = new CommonResult();

            #region 验证非空及重复
            if (!string.IsNullOrEmpty(tinfo.SdName))
            {
                string where = string.Format("ccode='{0}' and Classify_id='{1}'", tinfo.SdName,tinfo.Classify_id);
                Sd_sysdb model = iService.GetWhere(where);
                if (model != null)
                {
                    result.ErrMsg = "名称不能重复";
                    return ToJsonContent(result);
                }
            }
            else
            {
                result.ErrMsg = "名称不能为空";
                return ToJsonContent(result);
            }
            #endregion

            Sd_sysdb info = tinfo.MapTo<Sd_sysdb>();
            OnBeforeInsert(info);

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
        public override async Task<IActionResult> UpdateAsync(Sd_sysdbInputDto tinfo, string id)
        {
            CommonResult result = new CommonResult();

            #region 验证非空及重复
            if (!string.IsNullOrEmpty(tinfo.SdName))
            {
                string where = string.Format("ccode='{0}' and Classify_id='{1}'", tinfo.SdName, tinfo.Classify_id);
                Sd_sysdb model = iService.GetWhere(where);
                if (model != null)
                {
                    result.ErrMsg = "名称不能重复";
                    return ToJsonContent(result);
                }
            }
            else
            {
                result.ErrMsg = "名称不能为空";
                return ToJsonContent(result);
            }
            #endregion
            Sd_sysdb newInfo = tinfo.MapTo<Sd_sysdb>();
            Sd_sysdb info = iService.Get(id);
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

        #endregion

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合(用于分页数据显示)
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("FindWithPagerAsync")]
        [YuebonAuthorize("List")]
        public override async Task<CommonResult<PageResult<Sd_sysdbOutputDto>>> FindWithPagerAsync(SearchInputDto<Sd_sysdb> search)
        {
            CommonResult<PageResult<Sd_sysdbOutputDto>> result = new CommonResult<PageResult<Sd_sysdbOutputDto>>();
            if (!string.IsNullOrEmpty(CurrentUser.SysId))
            {
                search.Filter = new Sd_sysdb();
                search.Filter.Sys_id = CurrentUser.SysId;
                result.ResData = await iService.FindWithPagerAsync(search);
                result.ErrCode = ErrCode.successCode;
            }
            else
            {
                result.ErrCode = ErrCode.successCode;
            }
            return result;
        }

        #region 辅助方法

        /// <summary>
        /// 将新实体类中的非空值复制给原实体类
        /// </summary>
        /// <param name="info">原实体类</param>
        /// <param name="newInfo">新实体类</param>
        /// <returns></returns>
        private Sd_sysdb SwapValue(Sd_sysdb info, Sd_sysdb newInfo)
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