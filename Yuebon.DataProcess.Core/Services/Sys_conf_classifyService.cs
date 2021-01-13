using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Services;
using Yuebon.Security.IServices;
using Yuebon.DataProcess.IRepositories;
using Yuebon.DataProcess.IServices;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;
using System.Linq;

namespace Yuebon.DataProcess.Services
{
    /// <summary>
    /// 服务接口实现
    /// </summary>
    public class Sys_conf_classifyService: BaseService<Sys_conf_classify,Sys_conf_classifyOutputDto, string>, ISys_conf_classifyService
    {
		private readonly ISys_conf_classifyRepository _repository;
        private readonly ILogService _logService;
        public Sys_conf_classifyService(ISys_conf_classifyRepository repository,ILogService logService) : base(repository)
        {
			_repository=repository;
			_logService=logService;
            //_repository.OnOperationLog += _logService.OnOperationLog;
        }

        /// <summary>
        /// 获取分类适用于Vue 树形列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<Sys_conf_classifyOutputDto>> GetAllClassifyTreeTable()
        {
            List<Sys_conf_classifyOutputDto> reslist = new List<Sys_conf_classifyOutputDto>();
            IEnumerable<Sys_conf_classify> elist = await _repository.GetAllAsync();
            List<Sys_conf_classify> list = elist.OrderBy(t => t.SortCode).ToList();
            List<Sys_conf_classify> oneMenuList = list.FindAll(t => t.Parentid == "");
            foreach (Sys_conf_classify item in oneMenuList)
            {
                Sys_conf_classifyOutputDto menuTreeTableOutputDto = new Sys_conf_classifyOutputDto();
                menuTreeTableOutputDto = item.MapTo<Sys_conf_classifyOutputDto>();
                menuTreeTableOutputDto.Children = GetSubClasses(list, item.Id).ToList();
                reslist.Add(menuTreeTableOutputDto);
            }

            return reslist;
        }


        /// <summary>
        /// 获取子菜单，递归调用
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parentId">父级Id</param>
        /// <returns></returns>
        private List<Sys_conf_classifyOutputDto> GetSubClasses(List<Sys_conf_classify> data, string parentId)
        {
            List<Sys_conf_classifyOutputDto> list = new List<Sys_conf_classifyOutputDto>();
            Sys_conf_classifyOutputDto OrganizeOutputDto = new Sys_conf_classifyOutputDto();
            var ChilList = data.FindAll(t => t.Parentid == parentId);
            foreach (Sys_conf_classify entity in ChilList)
            {
                OrganizeOutputDto = entity.MapTo<Sys_conf_classifyOutputDto>();
                OrganizeOutputDto.Children = GetSubClasses(data, entity.Id).OrderBy(t => t.SortCode).MapTo<Sys_conf_classifyOutputDto>();
                list.Add(OrganizeOutputDto);
            }
            return list;
        }
    }
}