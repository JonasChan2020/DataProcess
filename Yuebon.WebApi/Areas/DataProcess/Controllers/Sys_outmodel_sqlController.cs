using Microsoft.AspNetCore.Mvc;
using Yuebon.AspNetCore.Controllers;
using Yuebon.Commons.Helpers;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;
using Yuebon.DataProcess.IServices;
using Yuebon.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Yuebon.Commons.Models;
using Yuebon.Commons.Dtos;
using System.Linq;
using Yuebon.AspNetCore.Models;
using Yuebon.Commons.Mapping;

namespace Yuebon.DataProcessApi.Areas.DataProcess.Controllers
{
    /// <summary>
    /// 数据输出模型最终查询语句接口
    /// </summary>
    [ApiController]
    [Route("api/DataProcess/[controller]")]
    public class Sys_outmodel_sqlController : AreaApiController<Sys_outmodel_sql, Sys_outmodel_sqlOutputDto,Sys_outmodel_sqlInputDto,ISys_outmodel_sqlService,string>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_iService"></param>
        public Sys_outmodel_sqlController(ISys_outmodel_sqlService _iService) : base(_iService)
        {
            iService = _iService;
            AuthorizeKey.ListKey = "Sys_outmodel_sql/List";
            AuthorizeKey.InsertKey = "Sys_outmodel_sql/Add";
            AuthorizeKey.UpdateKey = "Sys_outmodel_sql/Edit";
            AuthorizeKey.UpdateEnableKey = "Sys_outmodel_sql/Enable";
            AuthorizeKey.DeleteKey = "Sys_outmodel_sql/Delete";
            AuthorizeKey.DeleteSoftKey = "Sys_outmodel_sql/DeleteSoft";
            AuthorizeKey.ViewKey = "Sys_outmodel_sql/View";
        }
        /// <summary>
        /// 新增前处理数据
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(Sys_outmodel_sql info)
        {
            info.Id = GuidUtils.CreateNo();
            
        }
        
        /// <summary>
        /// 在更新数据前对数据的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeUpdate(Sys_outmodel_sql info)
        {
            
        }

        /// <summary>
        /// 在软删除数据前对数据的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(Sys_outmodel_sql info)
        {
            
        }

        /// <summary>
        /// 获取所有可用的
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetByOutModelId")]
        [YuebonAuthorize("List")]
        public async Task<IActionResult> GetByOutModelId(SearchInputDto<Sys_outmodel_sql> search)
        {
            CommonResult<List<Sys_conf_detailsOutputDto>> result = new CommonResult<List<Sys_conf_detailsOutputDto>>();
            if (search.Filter == null)
            {
                search.Filter = new Sys_outmodel_sql();
            }
            Sys_outmodel_sql list = await iService.GetWhereAsync(string.Format(" sys_outmodel_id='{0}'", search.Filter.Sys_outmodel_id));
            Sys_outmodel_sqlOutputDto resultList = list.MapTo<Sys_outmodel_sqlOutputDto>();
            result.ResData = resultList;
            result.ErrCode = ErrCode.successCode;
            result.ErrMsg = ErrCode.err0;

            return ToJsonContent(result);
        }
    }
}