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
    /// 数据源配置信息表接口
    /// </summary>
    [ApiController]
    [Route("api/DataProcess/[controller]")]
    public class Ds_dsconfigController : AreaApiController<Ds_dsconfig, Ds_dsconfigOutputDto,Ds_dsconfigInputDto,IDs_dsconfigService,string>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_iService"></param>
        public Ds_dsconfigController(IDs_dsconfigService _iService) : base(_iService)
        {
            iService = _iService;
            AuthorizeKey.ListKey = "Ds_dsconfig/List";
            AuthorizeKey.InsertKey = "Ds_dsconfig/Add";
            AuthorizeKey.UpdateKey = "Ds_dsconfig/Edit";
            AuthorizeKey.UpdateEnableKey = "Ds_dsconfig/Enable";
            AuthorizeKey.DeleteKey = "Ds_dsconfig/Delete";
            AuthorizeKey.DeleteSoftKey = "Ds_dsconfig/DeleteSoft";
            AuthorizeKey.ViewKey = "Ds_dsconfig/View";
        }
        /// <summary>
        /// 新增前处理数据
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(Ds_dsconfig info)
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
        protected override void OnBeforeUpdate(Ds_dsconfig info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 在软删除数据前对数据的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(Ds_dsconfig info)
        {
            info.DeleteMark = true;
            info.DeleteTime = DateTime.Now;
            info.DeleteUserId = CurrentUser.UserId;
        }
    }
}