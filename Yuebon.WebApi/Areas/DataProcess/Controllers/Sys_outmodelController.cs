using Microsoft.AspNetCore.Mvc;
using System;
using Yuebon.AspNetCore.Controllers;
using Yuebon.Commons.Helpers;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;
using Yuebon.DataProcess.IServices;
using Yuebon.AspNetCore.Mvc;
using Yuebon.AspNetCore.Models;
using Yuebon.Commons.Log;
using System.Collections.Generic;
using System.Linq;
using Yuebon.Commons.Models;
using System.Threading.Tasks;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Extensions;
using Yuebon.DataProcess.Core.common.Entity.TreeEntity;

namespace Yuebon.WebApi.Areas.DataProcess.Controllers
{
    /// <summary>
    /// 数据输出模型接口
    /// </summary>
    [ApiController]
    [Route("api/DataProcess/[controller]")]
    public class Sys_outmodelController : AreaApiController<Sys_outmodel, Sys_outmodelOutputDto,Sys_outmodelInputDto,ISys_outmodelService,string>
    {
        private ISys_sysService SysService;
        private ISys_outmodel_detailsService OutModelDetailService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_iService"></param>
        public Sys_outmodelController(ISys_outmodelService _iService, ISys_sysService _SysService, ISys_outmodel_detailsService _OutModelDetailService) : base(_iService)
        {
            iService = _iService;
            SysService = _SysService;
            OutModelDetailService = _OutModelDetailService;
            AuthorizeKey.ListKey = "Sys_outmodel/List";
            AuthorizeKey.InsertKey = "Sys_outmodel/Add";
            AuthorizeKey.UpdateKey = "Sys_outmodel/Edit";
            AuthorizeKey.UpdateEnableKey = "Sys_outmodel/Enable";
            AuthorizeKey.DeleteKey = "Sys_outmodel/Delete";
            AuthorizeKey.DeleteSoftKey = "Sys_outmodel/DeleteSoft";
            AuthorizeKey.ViewKey = "Sys_outmodel/View";
        }
        /// <summary>
        /// 新增前处理数据
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(Sys_outmodel info)
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
        protected override void OnBeforeUpdate(Sys_outmodel info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 在软删除数据前对数据的修改操作
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(Sys_outmodel info)
        {
            info.DeleteMark = true;
            info.DeleteTime = DateTime.Now;
            info.DeleteUserId = CurrentUser.UserId;
        }

        /// <summary>
        /// 获取所有可用的
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetEnableList")]
        [YuebonAuthorize("List")]
        public async Task<CommonResult<List<Sys_outmodelOutputDto>>> GetEnableList(SearchInputDto<Sys_outmodel> search)
        {
            CommonResult<List<Sys_outmodelOutputDto>> result = new CommonResult<List<Sys_outmodelOutputDto>>();
            string where = " EnabledMark=1 ";
            if (search.Filter != null && !string.IsNullOrEmpty(search.Filter.Sysid))
            {
                where += string.Format(" and sysid = '{0}' ", search.Filter.Sysid);
            }
            IEnumerable<Sys_outmodel> list = await iService.GetListWhereAsync(where);
            List<Sys_outmodelOutputDto> resultList = list.MapTo<Sys_outmodelOutputDto>();
            result.ResData = resultList;
            result.ErrCode = ErrCode.successCode;
            result.ErrMsg = ErrCode.err0;

            return result;
        }

        /// <summary>
        /// 获取 系统和模型的树形列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetSysAndOutModelTree")]
        [YuebonAuthorize("List")]
        public async Task<IActionResult> GetSysAndOutModelTree(SearchInputDto<Sys_sys> search)
        {
            CommonResult result = new CommonResult();
            try
            {
                List<Sys_Db_TableTreeEntity> treeList = new List<Sys_Db_TableTreeEntity>();
                string sysWhere = " EnabledMark=1 ";
                if (search.Filter != null && !string.IsNullOrEmpty(search.Filter.Classify_id))
                {
                    sysWhere += string.Format(" and classify_id = '{0}' ", search.Filter.Classify_id);
                }
                IEnumerable<Sys_sys> list = await SysService.GetListWhereAsync(sysWhere);
                if (list != null && list.Count() > 0)
                {
                    string[] sysIds = new string[list.Count()];
                    int i = 0;
                    foreach (Sys_sys item in list)
                    {
                        sysIds[i] = item.Id;
                        i++;
                    }
                    string tbWhere = " EnabledMark=1 and sysid in ('" + sysIds.Join(",").Trim(',').Replace(",", "','") + "')";
                    IEnumerable<Sys_outmodel> scList = iService.GetListWhere(tbWhere);
                    if (scList != null && scList.Count() > 0)
                    {
                        foreach (Sys_sys sysItem in list)
                        {
                            Sys_Db_TableTreeEntity treeModel = new Sys_Db_TableTreeEntity();
                            treeModel.Id = sysItem.Id;
                            treeModel.NodeName = sysItem.Sysname;
                            treeModel.Description = sysItem.Description;
                            treeModel.NodeType = "sys";
                            treeModel.ParentId = "0";
                            List<Sys_outmodel> childModelList = scList.Where(x => x.Sysid == sysItem.Id).ToList();
                            if (childModelList != null && childModelList.Count > 0)
                            {
                                List<Sys_Db_TableTreeEntity> childtreeList = new List<Sys_Db_TableTreeEntity>();
                                foreach (Sys_outmodel childItem in childModelList)
                                {
                                    Sys_Db_TableTreeEntity childTreeModel = new Sys_Db_TableTreeEntity();
                                    childTreeModel.Id = childItem.Id;
                                    childTreeModel.NodeName = childItem.Modelname;
                                    childTreeModel.Description = childItem.Description;
                                    childTreeModel.NodeType = "tb";
                                    childTreeModel.ParentId = sysItem.Id;
                                    childtreeList.Add(childTreeModel);
                                }
                                treeModel.Children = childtreeList;
                            }
                            else
                            {
                                treeModel.Children = new List<Sys_Db_TableTreeEntity>();
                            }
                            treeList.Add(treeModel);
                        }
                    }
                }
                result.Success = true;
                result.ResData = treeList;
                result.ErrCode = ErrCode.successCode;
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