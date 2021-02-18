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
using Yuebon.Commons.Pages;
using Yuebon.Commons.Dtos;
using Yuebon.AspNetCore.Models;
using Yuebon.Commons.Mapping;

namespace Yuebon.WebApi.Areas.DataProcess.Controllers
{
    /// <summary>
    /// 接口
    /// </summary>
    [ApiController]
    [Route("api/DataProcess/[controller]")]
    public class Conf_confController : AreaApiController<Conf_conf, Conf_confOutputDto,Conf_confInputDto,IConf_confService,string>
    {
        private readonly ISd_sysdbService sdService;
        private readonly ISys_sysService sysService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="_sdService"></param>
        /// <param name="_sysService"></param>
        public Conf_confController(IConf_confService _iService, ISd_sysdbService _sdService, ISys_sysService _sysService) : base(_iService)
        {
            iService = _iService;
            sdService = _sdService;
            sysService = _sysService;
            AuthorizeKey.ListKey = "Conf_conf/List";
            AuthorizeKey.InsertKey = "Conf_conf/Add";
            AuthorizeKey.UpdateKey = "Conf_conf/Edit";
            AuthorizeKey.UpdateEnableKey = "Conf_conf/Enable";
            AuthorizeKey.DeleteKey = "Conf_conf/Delete";
            AuthorizeKey.DeleteSoftKey = "Conf_conf/DeleteSoft";
            AuthorizeKey.ViewKey = "Conf_conf/View";
        }
        /// <summary>
        /// 新增前处理数据
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(Conf_conf info)
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
        protected override void OnBeforeUpdate(Conf_conf info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 在软删除数据前对数据的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(Conf_conf info)
        {
            info.DeleteMark = true;
            info.DeleteTime = DateTime.Now;
            info.DeleteUserId = CurrentUser.UserId;
        }

        /// <summary>
        /// 异步新增数据
        /// </summary>
        /// <param name="tinfo"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        [YuebonAuthorize("Add")]
        public override async Task<IActionResult> InsertAsync(Conf_confInputDto tinfo)
        {
            CommonResult result = new CommonResult();

            #region 验证重复
            string where = string.Format("fromid='{0}' and toid='{1}'", tinfo.FromId, tinfo.ToId);
            Conf_conf sameModel = iService.GetWhere(where);
            if (sameModel != null)
            {
                result.ErrMsg = "已存在关联";
                result.ErrCode = ErrCode.err1;
                return ToJsonContent(result);
            }
            #endregion

            #region 被关联项的逻辑判断
            if (tinfo.ConfToType == 0) //系统，判断其有没有设置主数据库
            {
                #region 判断系统是否设置了主数据库
                Sys_sys model = sysService.Get(tinfo.ToId);
                if (model != null)
                {
                    if (string.IsNullOrEmpty(model.MdbId))
                    {
                        result.ErrCode = ErrCode.successCode;
                        result.Success = false;
                        result.ErrMsg = ErrCode.err80401;
                        return ToJsonContent(result);
                    }
                }
                else
                {
                    result.ErrCode = ErrCode.successCode;
                    result.Success = false;
                    result.ErrMsg = ErrCode.err80402;
                    return ToJsonContent(result);
                }
                #endregion
            }
            else if(tinfo.ConfToType == 1) //数据库，判断其有没有加入系统，如果加入系统则通知前台变更为相应系统关联
            {
                #region
                Sd_sysdb model = sdService.Get(tinfo.ToId);
                if (model != null)
                {
                    if (!string.IsNullOrEmpty(model.Sys_id))
                    {
                        Sys_sys sysModel = sysService.Get(model.Sys_id);
                        if (sysModel != null)
                        {
                            result.ErrCode = ErrCode.successCode;
                            result.Success = false;
                            result.ErrMsg = ErrCode.err80404;
                            result.CustomCode = "err80404";
                            result.ResData = model.Sys_id;
                            return ToJsonContent(result);
                        }
                        else
                        {
                            result.ErrCode = ErrCode.successCode;
                            result.Success = false;
                            result.ErrMsg = ErrCode.err80405;
                            return ToJsonContent(result);
                        }
                       
                    }
                }
                else
                {
                    result.ErrCode = ErrCode.successCode;
                    result.Success = false;
                    result.ErrMsg = ErrCode.err80403;
                    return ToJsonContent(result);
                }
                #endregion
            }
            #endregion

            Conf_conf info = tinfo.MapTo<Conf_conf>();
            OnBeforeInsert(info);
            long ln = await iService.InsertAsync(info).ConfigureAwait(false);
            if (ln > 0)
            {
                result.Success = true;
                result.ErrMsg = ErrCode.err0;
                result.ErrCode = ErrCode.successCode;
            }
            else
            {
                result.ErrCode = ErrCode.successCode;
                result.Success = false;
                result.ErrMsg = ErrCode.err43001;
            }
            return ToJsonContent(result);
        }

    }
}