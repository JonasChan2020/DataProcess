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

namespace Yuebon.WebApi.Areas.DataProcess.Controllers
{
    /// <summary>
    /// 接口
    /// </summary>
    [ApiController]
    [Route("api/DataProcess/[controller]")]
    public class Sys_conf_objController : AreaApiController<Sys_conf_obj, Sys_conf_objOutputDto,Sys_conf_objInputDto,ISys_conf_objService,string>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_iService"></param>
        public Sys_conf_objController(ISys_conf_objService _iService) : base(_iService)
        {
            iService = _iService;
            AuthorizeKey.ListKey = "Sys_conf_obj/List";
            AuthorizeKey.InsertKey = "Sys_conf_obj/Add";
            AuthorizeKey.UpdateKey = "Sys_conf_obj/Edit";
            AuthorizeKey.UpdateEnableKey = "Sys_conf_obj/Enable";
            AuthorizeKey.DeleteKey = "Sys_conf_obj/Delete";
            AuthorizeKey.DeleteSoftKey = "Sys_conf_obj/DeleteSoft";
            AuthorizeKey.ViewKey = "Sys_conf_obj/View";
        }
        /// <summary>
        /// 新增前处理数据
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(Sys_conf_obj info)
        {
            info.Id = GuidUtils.CreateNo();
           
        }
        
        /// <summary>
        /// 在更新数据前对数据的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeUpdate(Sys_conf_obj info)
        {
            
        }

        /// <summary>
        /// 在软删除数据前对数据的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(Sys_conf_obj info)
        {
           
        }
    }
}