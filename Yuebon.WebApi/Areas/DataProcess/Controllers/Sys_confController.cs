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
    public class Sys_confController : AreaApiController<Sys_conf, Sys_confOutputDto,Sys_confInputDto,ISys_confService,string>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_iService"></param>
        public Sys_confController(ISys_confService _iService) : base(_iService)
        {
            iService = _iService;
            AuthorizeKey.ListKey = "Sys_conf/List";
            AuthorizeKey.InsertKey = "Sys_conf/Add";
            AuthorizeKey.UpdateKey = "Sys_conf/Edit";
            AuthorizeKey.UpdateEnableKey = "Sys_conf/Enable";
            AuthorizeKey.DeleteKey = "Sys_conf/Delete";
            AuthorizeKey.DeleteSoftKey = "Sys_conf/DeleteSoft";
            AuthorizeKey.ViewKey = "Sys_conf/View";
        }
        /// <summary>
        /// 新增前处理数据
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(Sys_conf info)
        {
            info.Id = GuidUtils.CreateNo();
            info.Sysid = CurrentUser.SysId;
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
        protected override void OnBeforeUpdate(Sys_conf info)
        {
            info.Sysid = CurrentUser.SysId;
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 在软删除数据前对数据的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(Sys_conf info)
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
        public override async Task<CommonResult<PageResult<Sys_confOutputDto>>> FindWithPagerAsync(SearchInputDto<Sys_conf> search)
        {
            CommonResult<PageResult<Sys_confOutputDto>> result = new CommonResult<PageResult<Sys_confOutputDto>>();
            if (!string.IsNullOrEmpty(CurrentUser.SysId))
            {
                search.Filter = new Sys_conf();
                search.Filter.Sysid = CurrentUser.SysId;
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