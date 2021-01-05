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
using HtSoft.DataProcess.Dtos;
using HtSoft.DataProcess.Models;
using HtSoft.DataProcess.IServices;

namespace HtSoft.DataProcessApi.Areas.DataProcess.Controllers
{
    /// <summary>
    /// 接口
    /// </summary>
    [ApiController]
    [Route("api/DataProcess/[controller]")]
    public class Plug_sysrelationController : AreaApiController<Plug_sysrelation, Plug_sysrelationOutputDto,Plug_sysrelationInputDto,IPlug_sysrelationService,string>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_iService"></param>
        public Plug_sysrelationController(IPlug_sysrelationService _iService) : base(_iService)
        {
            iService = _iService;
            AuthorizeKey.ListKey = "Plug_sysrelation/List";
            AuthorizeKey.InsertKey = "Plug_sysrelation/Add";
            AuthorizeKey.UpdateKey = "Plug_sysrelation/Edit";
            AuthorizeKey.UpdateEnableKey = "Plug_sysrelation/Enable";
            AuthorizeKey.DeleteKey = "Plug_sysrelation/Delete";
            AuthorizeKey.DeleteSoftKey = "Plug_sysrelation/DeleteSoft";
            AuthorizeKey.ViewKey = "Plug_sysrelation/View";
        }
        /// <summary>
        /// 新增前处理数据
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(Plug_sysrelation info)
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
        protected override void OnBeforeUpdate(Plug_sysrelation info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 在软删除数据前对数据的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(Plug_sysrelation info)
        {
            info.DeleteMark = true;
            info.DeleteTime = DateTime.Now;
            info.DeleteUserId = CurrentUser.UserId;
        }
    }
}