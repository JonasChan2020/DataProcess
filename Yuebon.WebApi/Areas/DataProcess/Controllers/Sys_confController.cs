using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Yuebon.AspNetCore.Controllers;
using Yuebon.Commons.Helpers;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;
using Yuebon.DataProcess.IServices;
using Yuebon.AspNetCore.Mvc;
using System.Threading.Tasks;
using Yuebon.Commons.Models;
using System.Collections.Generic;
using Yuebon.Commons.Mapping;
using Yuebon.AspNetCore.Models;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Log;
using Yuebon.Commons.Extensions;
using Yuebon.DataProcess.Core.common.Entity.TreeEntity;

namespace Yuebon.WebApi.Areas.DataProcess.Controllers
{
    /// <summary>
    /// 接口
    /// </summary>
    [ApiController]
    [Route("api/DataProcess/[controller]")]
    public class Sys_confController : AreaApiController<Sys_conf, Sys_confOutputDto,Sys_confInputDto,ISys_confService,string>
    {
        private ISys_sysService SysService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_iService"></param>
        /// <param name="_SysService"></param>
        public Sys_confController(ISys_confService _iService, ISys_sysService _SysService) : base(_iService)
        {
            iService = _iService;
            SysService = _SysService;
            AuthorizeKey.ListKey = "Sys_conf/List";
            AuthorizeKey.InsertKey = "Sys_conf/Add";
            AuthorizeKey.UpdateKey = "Sys_conf/Edit";
            AuthorizeKey.UpdateEnableKey = "Sys_conf/Enable";
            AuthorizeKey.DeleteKey = "Sys_conf/Delete";
            AuthorizeKey.DeleteSoftKey = "Sys_conf/DeleteSoft";
            AuthorizeKey.ViewKey = "Sys_conf/View";
        }
        /// <summary>
        /// 新增前处理数据
        /// </summary>
        /// <param name="info"></param>
        protected override void OnBeforeInsert(Sys_conf info)
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
        protected override void OnBeforeUpdate(Sys_conf info)
        {
            info.LastModifyUserId = CurrentUser.UserId;
            info.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 在软删除数据前对数据的修改操作 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void OnBeforeSoftDelete(Sys_conf info)
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
        public async Task<CommonResult<List<Sys_confOutputDto>>> GetEnableList(SearchInputDto<Sys_conf> search)
        {
            CommonResult<List<Sys_confOutputDto>> result = new CommonResult<List<Sys_confOutputDto>>();
            string where = " EnabledMark=1 ";
            if (search.Filter != null && !string.IsNullOrEmpty(search.Filter.Sysid))
            {
                where += string.Format(" and sysid = '{0}' ", search.Filter.Sysid);
            }
            IEnumerable<Sys_conf> list = await iService.GetListWhereAsync(where);
            List<Sys_confOutputDto> resultList = list.MapTo<Sys_confOutputDto>();
            result.ResData = resultList;
            result.ErrCode = ErrCode.successCode;
            result.ErrMsg = ErrCode.err0;

            return result;
        }

        /// <summary>
        /// 获取 系统和模型的树形列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetSysAndModelTree")]
        [YuebonAuthorize("List")]
        public async Task<IActionResult> GetSysAndModelTree(SearchInputDto<Sys_sys> search)
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
                    IEnumerable<Sys_conf> scList = iService.GetListWhere(tbWhere);
                    if (scList != null&& scList.Count()>0)
                    {
                        foreach (Sys_sys sysItem in list)
                        {
                            Sys_Db_TableTreeEntity treeModel = new Sys_Db_TableTreeEntity();
                            treeModel.Id = sysItem.Id;
                            treeModel.NodeName = sysItem.Sysname;
                            treeModel.Description = sysItem.Description;
                            treeModel.NodeType = "sys";
                            treeModel.ParentId = "0";
                            List<Sys_conf> childModelList = scList.Where(x => x.Sysid == sysItem.Id).ToList();
                            if (childModelList != null && childModelList.Count > 0)
                            {
                                List<Sys_Db_TableTreeEntity> childtreeList = new List<Sys_Db_TableTreeEntity>();
                                foreach (Sys_conf childItem in childModelList)
                                {
                                    Sys_Db_TableTreeEntity childTreeModel = new Sys_Db_TableTreeEntity();
                                    childTreeModel.Id = childItem.Id;
                                    childTreeModel.NodeName = childItem.Confname;
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