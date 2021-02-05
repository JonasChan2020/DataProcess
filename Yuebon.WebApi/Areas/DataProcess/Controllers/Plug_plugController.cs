using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Models;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;
using Yuebon.DataProcess.IServices;
using Yuebon.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;

namespace Yuebon.WebApi.Areas.DataProcess.Controllers
{
    /// <summary>
    /// 接口
    /// </summary>
    [ApiController]
    [Route("api/DataProcess/[controller]")]
    public class Plug_plugController : AreaApiController<Plug_plug, Plug_plugOutputDto,Plug_plugInputDto,IPlug_plugService,string>
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="hostingEnvironment"></param>
        public Plug_plugController(IWebHostEnvironment hostingEnvironment, IPlug_plugService _iService) : base(_iService)
        {
            iService = _iService;
            _hostingEnvironment = hostingEnvironment;
            AuthorizeKey.ListKey = "Plug_plug/List";
            AuthorizeKey.InsertKey = "Plug_plug/Add";
            AuthorizeKey.UpdateKey = "Plug_plug/Edit";
            AuthorizeKey.UpdateEnableKey = "Plug_plug/Enable";
            AuthorizeKey.DeleteKey = "Plug_plug/Delete";
            AuthorizeKey.DeleteSoftKey = "Plug_plug/DeleteSoft";
            AuthorizeKey.ViewKey = "Plug_plug/View";
        }
        /// <summary>
        /// 新增前处理数据
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(Plug_plug info)
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
        protected override void OnBeforeUpdate(Plug_plug info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 在软删除数据前对数据的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(Plug_plug info)
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
        public override async Task<IActionResult> InsertAsync(Plug_plugInputDto tinfo)
        {
            CommonResult result = new CommonResult();
            try
            {
                bool has = tinfo.HasPage != null ? Convert.ToBoolean(tinfo.HasPage) : true; //如果为空则默认为具有编辑页面
                if (isFileExist(tinfo.Ppath, tinfo.Pcode, has))
                {
                    Plug_plug info = tinfo.MapTo<Plug_plug>();
                    OnBeforeInsert(info);

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
                }
                else
                {
                    result.ErrMsg = ErrCode.err80021;
                    result.ErrCode = "err80021";
                    FileHelper.DeleteFolder(tinfo.Ppath);
                }
            }
            catch (Exception ex)
            {
                result.ErrMsg = ex.Message.ToString();
                result.ErrCode = "err80021";
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
        public override async Task<IActionResult> UpdateAsync(Plug_plugInputDto tinfo, string id)
        {
            CommonResult result = new CommonResult();

            string oldFilePath = "";
            Plug_plug newInfo = tinfo.MapTo<Plug_plug>();
            Plug_plug info = iService.Get(id);
            if (!string.IsNullOrEmpty(info.Ppath))
            {
                oldFilePath = info.Ppath;
            }
            info = SwapValue(info, newInfo);

            OnBeforeUpdate(info);

            bool bl = await iService.UpdateAsync(info, id).ConfigureAwait(false);
            if (bl)
            {

                #region 删除旧的插件上传文件
                if (!string.IsNullOrEmpty(oldFilePath))
                {
                    FileHelper.DeleteFolder(oldFilePath);
                }
                #endregion

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
        /// 上传插件
        /// </summary>
        /// <returns></returns>
        [HttpPost("UpdateLoadplug")]
        [YuebonAuthorize("Edit")]
        public IActionResult UpdateLoadplug([FromForm] IFormFile file)
        {
            CommonResult result = new CommonResult();
            try
            {
                var fileData=file.OpenReadStream();
                string fName = GuidUtils.CreateNo();
                string baseDictory= _hostingEnvironment.WebRootPath;
                string filePath = baseDictory + "\\uploadplug\\" + fName;
                if (!Directory.Exists(filePath))//如果不存在就创建file文件夹
                {
                    Directory.CreateDirectory(filePath);
                }
                ZipHelper.ZipDeCompressStream(fileData, filePath);
                result.ResData = filePath;
                result.ErrCode = ErrCode.successCode;
                result.ErrMsg = ErrCode.err0;
            }
            catch (Exception)
            {
                result.ErrMsg = ErrCode.err43002;
                result.ErrCode = "43002";
            }
            return ToJsonContent(result);
        }

        #region 辅助方法

        /// <summary>
        /// 将新实体类中的非空值复制给原实体类
        /// </summary>
        /// <param name="info">原实体类</param>
        /// <param name="newInfo">新实体类</param>
        /// <returns></returns>
        private Plug_plug SwapValue(Plug_plug info, Plug_plug newInfo)
        {
            PropertyInfo[] propertys = newInfo.GetType().GetProperties();
            foreach (PropertyInfo property in propertys)
            {
                object val = newInfo.GetType().GetProperty(property.Name).GetValue(newInfo, null);
                if (val != null)
                {
                    info.GetType().GetProperty(property.Name).SetValue(info, val, null);
                }
            }
            return info;
        }

        /// <summary>
        /// 判断上传的文件是否存在且正确
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="plugCode"></param>
        /// <returns></returns>
        private bool isFileExist(string filePath,string plugCode,bool haspage)
        {
            if (haspage)
            {
                if (FileHelper.FileExist(filePath + "\\" + plugCode.ToLower() + ".dll") && FileHelper.FileExist(filePath + "\\" + plugCode.ToLower() + ".vue"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (FileHelper.FileExist(filePath + "\\" + plugCode.ToLower() + ".dll"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
        }
        #endregion
    }
}