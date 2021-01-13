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
    public class Ds_classifyService: BaseService<Ds_classify,Ds_classifyOutputDto, string>, IDs_classifyService
    {
		private readonly IDs_classifyRepository _repository;
        private readonly ILogService _logService;
        public Ds_classifyService(IDs_classifyRepository repository,ILogService logService) : base(repository)
        {
			_repository=repository;
			_logService=logService;
            //_repository.OnOperationLog += _logService.OnOperationLog;
        }

        /// <summary>
        /// 获取分类适用于Vue 树形列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<Ds_classifyOutputDto>> GetAllClassifyTreeTable()
        {
            List<Ds_classifyOutputDto> reslist = new List<Ds_classifyOutputDto>();
            IEnumerable<Ds_classify> elist = await _repository.GetAllAsync();
            List<Ds_classify> list = elist.OrderBy(t => t.SortCode).ToList();
            List<Ds_classify> oneMenuList = list.FindAll(t => t.Parentid == "");
            foreach (Ds_classify item in oneMenuList)
            {
                Ds_classifyOutputDto menuTreeTableOutputDto = new Ds_classifyOutputDto();
                menuTreeTableOutputDto = item.MapTo<Ds_classifyOutputDto>();
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
        private List<Ds_classifyOutputDto> GetSubClasses(List<Ds_classify> data, string parentId)
        {
            List<Ds_classifyOutputDto> list = new List<Ds_classifyOutputDto>();
            Ds_classifyOutputDto OrganizeOutputDto = new Ds_classifyOutputDto();
            var ChilList = data.FindAll(t => t.Parentid == parentId);
            foreach (Ds_classify entity in ChilList)
            {
                OrganizeOutputDto = entity.MapTo<Ds_classifyOutputDto>();
                OrganizeOutputDto.Children = GetSubClasses(data, entity.Id).OrderBy(t => t.SortCode).MapTo<Ds_classifyOutputDto>();
                list.Add(OrganizeOutputDto);
            }
            return list;
        }
    }
}