using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Services;
using Yuebon.DataProcess.IRepositories;
using Yuebon.DataProcess.IServices;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;
using System.Linq;

namespace Yuebon.DataProcess.Services
{
    /// <summary>
    /// 数据输出模型分类表服务接口实现
    /// </summary>
    public class Sys_outmodel_classifyService: BaseService<Sys_outmodel_classify,Sys_outmodel_classifyOutputDto, string>, ISys_outmodel_classifyService
    {
		private readonly ISys_outmodel_classifyRepository _repository;
        private readonly ISys_sysRepository _sysrepository;
        public Sys_outmodel_classifyService(ISys_outmodel_classifyRepository repository, ISys_sysRepository sysrepository) : base(repository)
        {
			_repository=repository;
            _sysrepository = sysrepository;
        }

        /// <summary>
        /// 获取分类适用于Vue 树形列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<Sys_outmodel_classifyOutputDto>> GetAllClassifyTreeTable(SearchInputDto<Sys_outmodel_classify> search)
        {
            List<Sys_outmodel_classifyOutputDto> reslist = new List<Sys_outmodel_classifyOutputDto>();
            string where = GetDataPrivilege();
            PagerInfo pagerInfo = new PagerInfo
            {
                CurrenetPageIndex = 1,
                PageSize = 999999999
            };
            if (search.Filter != null && !string.IsNullOrEmpty(search.Filter.Sysid))
            {
                where += string.Format(" and sysid = '{0}'", search.Filter.Sysid);
            }
            IEnumerable<Sys_outmodel_classify> elist = await _repository.FindWithPagerAsync(where, pagerInfo);
            List<Sys_outmodel_classify> list = elist.OrderBy(t => t.SortCode).ToList();
            List<Sys_outmodel_classify> oneMenuList = list.FindAll(t => t.Parentid == "");
            foreach (Sys_outmodel_classify item in oneMenuList)
            {
                Sys_outmodel_classifyOutputDto menuTreeTableOutputDto = new Sys_outmodel_classifyOutputDto();
                menuTreeTableOutputDto = item.MapTo<Sys_outmodel_classifyOutputDto>();
                if (!string.IsNullOrEmpty(item.Sysid))
                {
                    menuTreeTableOutputDto.Sys_Name = _sysrepository.Get(item.Sysid).Sysname;
                }
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
        private List<Sys_outmodel_classifyOutputDto> GetSubClasses(List<Sys_outmodel_classify> data, string parentId)
        {
            List<Sys_outmodel_classifyOutputDto> list = new List<Sys_outmodel_classifyOutputDto>();
            Sys_outmodel_classifyOutputDto OrganizeOutputDto = new Sys_outmodel_classifyOutputDto();
            var ChilList = data.FindAll(t => t.Parentid == parentId);
            foreach (Sys_outmodel_classify entity in ChilList)
            {
                OrganizeOutputDto = entity.MapTo<Sys_outmodel_classifyOutputDto>();
                if (!string.IsNullOrEmpty(entity.Sysid))
                {
                    OrganizeOutputDto.Sys_Name = _sysrepository.Get(entity.Sysid).Sysname;
                }
                OrganizeOutputDto.Children = GetSubClasses(data, entity.Id).OrderBy(t => t.SortCode).MapTo<Sys_outmodel_classifyOutputDto>();
                list.Add(OrganizeOutputDto);
            }
            return list;
        }
    }
}