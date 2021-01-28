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
using Yuebon.AspNetCore.Mvc;

namespace Yuebon.WebApi.Areas.DataProcess.Controllers
{
    /// <summary>
    /// 接口
    /// </summary>
    [ApiController]
    [Route("api/DataProcess/[controller]")]
    public class Conf_detail_confController : AreaApiController<Conf_detail_conf, Conf_detail_confOutputDto,Conf_detail_confInputDto,IConf_detail_confService,string>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_iService"></param>
        public Conf_detail_confController(IConf_detail_confService _iService) : base(_iService)
        {
            iService = _iService;
            AuthorizeKey.ListKey = "Conf_detail_conf/List";
            AuthorizeKey.InsertKey = "Conf_detail_conf/Add";
            AuthorizeKey.UpdateKey = "Conf_detail_conf/Edit";
            AuthorizeKey.UpdateEnableKey = "Conf_detail_conf/Enable";
            AuthorizeKey.DeleteKey = "Conf_detail_conf/Delete";
            AuthorizeKey.DeleteSoftKey = "Conf_detail_conf/DeleteSoft";
            AuthorizeKey.ViewKey = "Conf_detail_conf/View";
        }
        /// <summary>
        /// 新增前处理数据
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(Conf_detail_conf info)
        {
            info.Id = GuidUtils.CreateNo();
           
        }
        
        /// <summary>
        /// 在更新数据前对数据的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeUpdate(Conf_detail_conf info)
        {
           
        }

        /// <summary>
        /// 在软删除数据前对数据的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(Conf_detail_conf info)
        {
           
        }
    }
}