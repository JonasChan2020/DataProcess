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
    /// 目标库配置信息表接口
    /// </summary>
    [ApiController]
    [Route("api/DataProcess/[controller]")]
    public class Sd_sdconfigController : AreaApiController<Sd_sdconfig, Sd_sdconfigOutputDto,Sd_sdconfigInputDto,ISd_sdconfigService,string>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_iService"></param>
        public Sd_sdconfigController(ISd_sdconfigService _iService) : base(_iService)
        {
            iService = _iService;
            AuthorizeKey.ListKey = "Sd_sdconfig/List";
            AuthorizeKey.InsertKey = "Sd_sdconfig/Add";
            AuthorizeKey.UpdateKey = "Sd_sdconfig/Edit";
            AuthorizeKey.UpdateEnableKey = "Sd_sdconfig/Enable";
            AuthorizeKey.DeleteKey = "Sd_sdconfig/Delete";
            AuthorizeKey.DeleteSoftKey = "Sd_sdconfig/DeleteSoft";
            AuthorizeKey.ViewKey = "Sd_sdconfig/View";
        }
        /// <summary>
        /// 新增前处理数据
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(Sd_sdconfig info)
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
        protected override void OnBeforeUpdate(Sd_sdconfig info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 在软删除数据前对数据的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(Sd_sdconfig info)
        {
            info.DeleteMark = true;
            info.DeleteTime = DateTime.Now;
            info.DeleteUserId = CurrentUser.UserId;
        }
    }
}