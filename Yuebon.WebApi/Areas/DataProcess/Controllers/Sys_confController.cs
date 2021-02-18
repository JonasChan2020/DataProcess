using Microsoft.AspNetCore.Mvc;
using System;
using Yuebon.AspNetCore.Controllers;
using Yuebon.Commons.Helpers;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;
using Yuebon.DataProcess.IServices;

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


    }
}