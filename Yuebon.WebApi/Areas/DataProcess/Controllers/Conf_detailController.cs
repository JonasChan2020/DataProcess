using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;
using Yuebon.AspNetCore.Models;
using Yuebon.Commons.Helpers;
using Yuebon.Commons.Models;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;
using Yuebon.DataProcess.IServices;
using Yuebon.AspNetCore.Mvc;
using Yuebon.Commons.Dtos;
using Yuebon.DataProcess.Core.common;
using Yuebon.DataProcess.Core.OutSideDbService.Entity;

namespace Yuebon.WebApi.Areas.DataProcess.Controllers
{
    /// <summary>
    /// 接口
    /// </summary>
    [ApiController]
    [Route("api/DataProcess/[controller]")]
    public class Conf_detailController : AreaApiController<Conf_detail, Conf_detailOutputDto,Conf_detailInputDto,IConf_detailService,string>
    {
        private readonly ISd_sysdbService sdService;
        private readonly ISd_detailService detailService;
        private readonly ISys_sysService sysService;
        private readonly IConf_confService confService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="_confService"></param>
        /// <param name="_sdService"></param>
        /// <param name="_sysService"></param>
        public Conf_detailController(IConf_detailService _iService, IConf_confService _confService, ISd_sysdbService _sdService, ISd_detailService _detailService, ISys_sysService _sysService) : base(_iService)
        {
            iService = _iService;
            sdService = _sdService;
            detailService = _detailService;
            sysService = _sysService;
            confService = _confService;
            AuthorizeKey.ListKey = "Conf_detail/List";
            AuthorizeKey.InsertKey = "Conf_detail/Add";
            AuthorizeKey.UpdateKey = "Conf_detail/Edit";
            AuthorizeKey.UpdateEnableKey = "Conf_detail/Enable";
            AuthorizeKey.DeleteKey = "Conf_detail/Delete";
            AuthorizeKey.DeleteSoftKey = "Conf_detail/DeleteSoft";
            AuthorizeKey.ViewKey = "Conf_detail/View";
        }
        /// <summary>
        /// 新增前处理数据
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(Conf_detail info)
        {
            info.Id = GuidUtils.CreateNo();
        }
        
        /// <summary>
        /// 在更新数据前对数据的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeUpdate(Conf_detail info)
        {

        }

        /// <summary>
        /// 在软删除数据前对数据的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(Conf_detail info)
        {
            
        }

        /// <summary>
        /// 获取所有可用的
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetConfTbContent")]
        [YuebonAuthorize("List")]
        public async Task<CommonResult<string>> GetConfTbContent(SearchInputDto<Conf_conf> search)
        {
            CommonResult<string> result = new CommonResult<string>();
            if (!string.IsNullOrEmpty(search.Pkey))
            {
                Conf_conf confModel = confService.Get(search.Pkey);
                if (confModel != null)
                {
                    string dbId = confModel.FromId; //所选系统主数据库或数据库ID
                    if (confModel.ConfFromType == 0) //系统
                    {
                        Sys_sys sysModel = sysService.Get(confModel.FromId);
                        dbId = sysModel.MdbId;
                    }
                    Sd_detail dbDetailModel = await detailService.GetWhereAsync(string.Format(" sd_id = '{0}'", dbId));
                    if (dbDetailModel != null)
                    {
                        List<DbTableInfo> tbModelList = dbDetailModel.Tbs.ToObject<List<DbTableInfo>>();
                        #region

                        #endregion

                        result.ResData = tbModelList;
                        result.ErrCode = ErrCode.successCode;
                        result.ErrMsg = ErrCode.err0;
                    }
                    else
                    {
                        result.ErrCode = ErrCode.err1;
                        result.ErrMsg = ErrCode.err80406;
                    }
                }
                else
                {
                    result.ErrCode = ErrCode.err1;
                    result.ErrMsg = ErrCode.err80407;
                }
            }
            else
            {
                result.ErrCode = ErrCode.err1;
                result.ErrMsg = "未接收到参数信息";
            }
            return result;
        }
    }
}