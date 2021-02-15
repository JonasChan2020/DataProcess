using Microsoft.AspNetCore.Mvc;
using System;
using Yuebon.AspNetCore.Controllers;
using Yuebon.Commons.Helpers;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;
using Yuebon.DataProcess.IServices;
using Yuebon.AspNetCore.Mvc;
using System.Threading.Tasks;
using Yuebon.Commons.Models;
using Yuebon.AspNetCore.Models;
using Yuebon.Commons.Mapping;

namespace Yuebon.WebApi.Areas.DataProcess.Controllers
{
    /// <summary>
    /// 接口
    /// </summary>
    [ApiController]
    [Route("api/DataProcess/[controller]")]
    public class Ds_dsController : AreaApiController<Ds_ds, Ds_dsOutputDto,Ds_dsInputDto,IDs_dsService,string>
    {
        private IDs_detailService IDsDetailService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="_IDsDetailService"></param>
        public Ds_dsController(IDs_dsService _iService, IDs_detailService _IDsDetailService) : base(_iService)
        {
            iService = _iService;
            IDsDetailService = _IDsDetailService;
            AuthorizeKey.ListKey = "Ds_ds/List";
            AuthorizeKey.InsertKey = "Ds_ds/Add";
            AuthorizeKey.UpdateKey = "Ds_ds/Edit";
            AuthorizeKey.UpdateEnableKey = "Ds_ds/Enable";
            AuthorizeKey.DeleteKey = "Ds_ds/Delete";
            AuthorizeKey.DeleteSoftKey = "Ds_ds/DeleteSoft";
            AuthorizeKey.ViewKey = "Ds_ds/View";
        }
        /// <summary>
        /// 新增前处理数据
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(Ds_ds info)
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
        protected override void OnBeforeUpdate(Ds_ds info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 在软删除数据前对数据的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(Ds_ds info)
        {
            info.DeleteMark = true;
            info.DeleteTime = DateTime.Now;
            info.DeleteUserId = CurrentUser.UserId;
        }

        #region 增删改 
        /// <summary>
        /// 异步新增数据
        /// </summary>
        /// <param name="tinfo"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        [YuebonAuthorize("Add")]
        public override async Task<IActionResult> InsertAsync(Ds_dsInputDto tinfo)
        {
            CommonResult result = new CommonResult();
            Ds_ds info = tinfo.MapTo<Ds_ds>();
            OnBeforeInsert(info);
            
            long ln = await iService.InsertAsync(info).ConfigureAwait(false);
            if (ln > 0)
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
        /// 异步更新数据，需要在业务模块控制器重写该方法,否则更新无效
        /// </summary>
        /// <param name="tinfo"></param>
        /// <param name="id">主键Id</param>
        /// <returns></returns>
        [HttpPost("Update")]
        [YuebonAuthorize("Edit")]
        public override async Task<IActionResult> UpdateAsync(Ds_dsInputDto tinfo, string id)
        {
            CommonResult result = new CommonResult();
            Ds_ds newInfo = tinfo.MapTo<Ds_ds>();
            OnBeforeUpdate(newInfo);
            Ds_ds info = iService.Get(id);
            bool ln = await iService.UpdateAsync(SwapValue(info, newInfo), id).ConfigureAwait(false);
            if (ln)
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

    }
}