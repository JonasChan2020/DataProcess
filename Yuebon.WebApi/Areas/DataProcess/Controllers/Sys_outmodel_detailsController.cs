using Microsoft.AspNetCore.Mvc;
using System;
using Yuebon.AspNetCore.Controllers;
using Yuebon.Commons.Helpers;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;
using Yuebon.DataProcess.IServices;
using Yuebon.AspNetCore.Mvc;
using System.Threading.Tasks;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Models;
using System.Collections.Generic;
using System.Linq;
using Yuebon.AspNetCore.Models;
using Yuebon.Commons.Mapping;

namespace Yuebon.WebApi.Areas.DataProcess.Controllers
{
    /// <summary>
    /// 数据输出模型详情表接口
    /// </summary>
    [ApiController]
    [Route("api/DataProcess/[controller]")]
    public class Sys_outmodel_detailsController : AreaApiController<Sys_outmodel_details, Sys_outmodel_detailsOutputDto,Sys_outmodel_detailsInputDto,ISys_outmodel_detailsService,string>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_iService"></param>
        public Sys_outmodel_detailsController(ISys_outmodel_detailsService _iService) : base(_iService)
        {
            iService = _iService;
            AuthorizeKey.ListKey = "Sys_outmodel_details/List";
            AuthorizeKey.InsertKey = "Sys_outmodel_details/Add";
            AuthorizeKey.UpdateKey = "Sys_outmodel_details/Edit";
            AuthorizeKey.UpdateEnableKey = "Sys_outmodel_details/Enable";
            AuthorizeKey.DeleteKey = "Sys_outmodel_details/Delete";
            AuthorizeKey.DeleteSoftKey = "Sys_outmodel_details/DeleteSoft";
            AuthorizeKey.ViewKey = "Sys_outmodel_details/View";
        }
        /// <summary>
        /// 新增前处理数据
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(Sys_outmodel_details info)
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
        protected override void OnBeforeUpdate(Sys_outmodel_details info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 在软删除数据前对数据的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(Sys_outmodel_details info)
        {
            info.DeleteMark = true;
            info.DeleteTime = DateTime.Now;
            info.DeleteUserId = CurrentUser.UserId;
        }

        /// <summary>
        /// 获取所有可用的
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAllEnableByConfId")]
        [YuebonAuthorize("List")]
        public async Task<IActionResult> GetAllEnableByConfId(SearchInputDto<Sys_outmodel_details> search)
        {
            CommonResult<List<Sys_conf_detailsOutputDto>> result = new CommonResult<List<Sys_conf_detailsOutputDto>>();
            if (search.Filter == null)
            {
                search.Filter = new Sys_outmodel_details();
            }
            IEnumerable<Sys_outmodel_details> list = await iService.GetAllByIsNotDeleteAndEnabledMarkAsync(string.Format(" sys_outmodel_id='{0}'", search.Filter.Sys_outmodel_id));
            List<Sys_outmodel_detailsOutputDto> resultList = list.MapTo<Sys_outmodel_detailsOutputDto>();
            resultList = resultList.OrderBy(x => x.Levelnum).ToList();
            result.ResData = resultList;
            result.ErrCode = ErrCode.successCode;
            result.ErrMsg = ErrCode.err0;

            return ToJsonContent(result);
        } 
    }
}