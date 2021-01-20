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
    }
}