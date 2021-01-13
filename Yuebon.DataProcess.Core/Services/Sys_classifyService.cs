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
    public class Sys_classifyService: BaseService<Sys_classify,Sys_classifyOutputDto, string>, ISys_classifyService
    {
		private readonly ISys_classifyRepository _repository;
        private readonly ILogService _logService;
        public Sys_classifyService(ISys_classifyRepository repository,ILogService logService) : base(repository)
        {
			_repository=repository;
			_logService=logService;
            //_repository.OnOperationLog += _logService.OnOperationLog;
        }

        /// <summary>
        /// 获取分类适用于Vue 树形列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<Sys_classifyOutputDto>> GetAllClassifyTreeTable()
        {
            List<Sys_classifyOutputDto> reslist = new List<Sys_classifyOutputDto>();
            IEnumerable<Sys_classify> elist = await _repository.GetAllAsync();
            List<Sys_classify> list = elist.OrderBy(t => t.SortCode).ToList();
            List<Sys_classify> oneMenuList = list.FindAll(t => t.Parentid == "");
            foreach (Sys_classify item in oneMenuList)
            {
                Sys_classifyOutputDto menuTreeTableOutputDto = new Sys_classifyOutputDto();
                menuTreeTableOutputDto = item.MapTo<Sys_classifyOutputDto>();
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
        private List<Sys_classifyOutputDto> GetSubClasses(List<Sys_classify> data, string parentId)
        {
            List<Sys_classifyOutputDto> list = new List<Sys_classifyOutputDto>();
            Sys_classifyOutputDto OrganizeOutputDto = new Sys_classifyOutputDto();
            var ChilList = data.FindAll(t => t.Parentid == parentId);
            foreach (Sys_classify entity in ChilList)
            {
                OrganizeOutputDto = entity.MapTo<Sys_classifyOutputDto>();
                OrganizeOutputDto.Children = GetSubClasses(data, entity.Id).OrderBy(t => t.SortCode).MapTo<Sys_classifyOutputDto>();
                list.Add(OrganizeOutputDto);
            }
            return list;
        }

    }
}