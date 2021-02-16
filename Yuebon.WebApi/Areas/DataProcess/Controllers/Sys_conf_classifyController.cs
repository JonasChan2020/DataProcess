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
    public class Sys_conf_classifyController : AreaApiController<Sys_conf_classify, Sys_conf_classifyOutputDto,Sys_conf_classifyInputDto,ISys_conf_classifyService,string>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_iService"></param>
        public Sys_conf_classifyController(ISys_conf_classifyService _iService) : base(_iService)
        {
            iService = _iService;
            AuthorizeKey.ListKey = "Sys_conf_classify/List";
            AuthorizeKey.InsertKey = "Sys_conf_classify/Add";
            AuthorizeKey.UpdateKey = "Sys_conf_classify/Edit";
            AuthorizeKey.UpdateEnableKey = "Sys_conf_classify/Enable";
            AuthorizeKey.DeleteKey = "Sys_conf_classify/Delete";
            AuthorizeKey.DeleteSoftKey = "Sys_conf_classify/DeleteSoft";
            AuthorizeKey.ViewKey = "Sys_conf_classify/View";
        }
        /// <summary>
        /// 新增前处理数据
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(Sys_conf_classify info)
        {
            info.Id = GuidUtils.CreateNo();
            info.Parentid = string.IsNullOrEmpty(info.Parentid) ? "" : info.Parentid;
            info.Sysid = CurrentUser.SysId;
            info.State = "0";
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
        protected override void OnBeforeUpdate(Sys_conf_classify info)
        {
            info.Parentid = string.IsNullOrEmpty(info.Parentid) ? "" : info.Parentid;
            info.Sysid = CurrentUser.SysId;
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 在软删除数据前对数据的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(Sys_conf_classify info)
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
        public override async Task<IActionResult> InsertAsync(Sys_conf_classifyInputDto tinfo)
        {
            CommonResult result = new CommonResult();

            #region 验证非空及重复
            if (!string.IsNullOrEmpty(tinfo.ClassCode))
            {
                string where = string.Format("classcode='{0}'", tinfo.ClassCode);
                Sys_conf_classify model = iService.GetWhere(where);
                if (model != null)
                {
                    result.ErrMsg = "编码不能重复";
                    return ToJsonContent(result);
                }
            }
            else
            {
                result.ErrMsg = "编码不能为空";
                return ToJsonContent(result);
            }
            #endregion



            Sys_conf_classify info = tinfo.MapTo<Sys_conf_classify>();
            OnBeforeInsert(info);

            #region 补充层级路径
            if (!string.IsNullOrEmpty(info.Parentid))
            {
                string where = string.Format("id='{0}'", info.Parentid);
                Sys_conf_classify model = iService.GetWhere(where);
                if (model == null)
                {
                    result.ErrMsg = "所属上级分类不存在";
                    return ToJsonContent(result);
                }
                info.Levelpath = model.Levelpath + "," + info.Id;
            }
            else
            {
                info.Levelpath = info.Id;
            }
            #endregion

            result.Success = await iService.InsertAsync(info) > 0;
            if (result.Success)
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
        /// 异步更新数据
        /// </summary>
        /// <param name="tinfo"></param>
        /// <param name="id">主键Id</param>
        /// <returns></returns>
        [HttpPost("Update")]
        [YuebonAuthorize("Edit")]
        public override async Task<IActionResult> UpdateAsync(Sys_conf_classifyInputDto tinfo, string id)
        {
            CommonResult result = new CommonResult();

            #region 验证非空及重复
            if (!string.IsNullOrEmpty(tinfo.ClassCode))
            {
                string where = string.Format("classcode='{0}' and id<>'{1}'", tinfo.ClassCode, tinfo.Id);
                Sys_conf_classify model = iService.GetWhere(where);
                if (model != null)
                {
                    result.ErrMsg = "编码不能重复";
                    return ToJsonContent(result);
                }
            }
            else
            {
                result.ErrMsg = "编码不能为空";
                return ToJsonContent(result);
            }
            #endregion

            Sys_conf_classify info = iService.Get(id);
            info.ClassCode = tinfo.ClassCode;
            info.ClassName = tinfo.ClassName;
            info.Description = tinfo.Description;
            info.Parentid = tinfo.Parentid;
            info.SortCode = tinfo.SortCode;
            info.EnabledMark = tinfo.EnabledMark;



            OnBeforeUpdate(info);

            #region 补充层级路径
            if (!string.IsNullOrEmpty(tinfo.Parentid))
            {
                string where = string.Format("id='{0}'", tinfo.Parentid);
                Sys_conf_classify model = iService.GetWhere(where);
                if (model == null)
                {
                    result.ErrMsg = "所属上级分类不存在";
                    return ToJsonContent(result);
                }
                info.Levelpath = model.Levelpath + "," + tinfo.Id;
            }
            else
            {
                info.Levelpath = tinfo.Id;
            }
            #endregion

            bool bl = await iService.UpdateAsync(info, id).ConfigureAwait(false);
            if (bl)
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

        /// <summary>
        /// 获取功能菜单适用于Vue 树形列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllClassifyTreeTable")]
        [YuebonAuthorize("List")]
        public async Task<IActionResult> GetAllClassifyTreeTable()
        {
            CommonResult result = new CommonResult();
            try
            {
                if (string.IsNullOrEmpty(CurrentUser.SysId))
                {
                    result.ErrMsg = ErrCode.err80001;
                    result.ErrCode = "80001";
                }
                else
                {
                    List<Sys_conf_classifyOutputDto> list = await iService.GetAllClassifyTreeTable(CurrentUser.SysId);
                    result.Success = true;
                    result.ErrCode = ErrCode.successCode;
                    result.ResData = list;
                }
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error("获取组织结构异常", ex);
                result.ErrMsg = ErrCode.err40110;
                result.ErrCode = "40110";
            }
            return ToJsonContent(result);
        }
    }
}