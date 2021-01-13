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
    public class Plug_typeService: BaseService<Plug_type,Plug_typeOutputDto, string>, IPlug_typeService
    {
		private readonly IPlug_typeRepository _repository;
        private readonly ILogService _logService;
        public Plug_typeService(IPlug_typeRepository repository,ILogService logService) : base(repository)
        {
			_repository=repository;
			_logService=logService;
            //_repository.OnOperationLog += _logService.OnOperationLog;
        }

        /// <summary>
        /// 获取分类适用于Vue 树形列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<Plug_typeOutputDto>> GetAllClassifyTreeTable()
        {
            List<Plug_typeOutputDto> reslist = new List<Plug_typeOutputDto>();
            IEnumerable<Plug_type> elist = await _repository.GetAllAsync();
            List<Plug_type> list = elist.OrderBy(t => t.SortCode).ToList();
            List<Plug_type> oneMenuList = list.FindAll(t => t.Parentid == "");
            foreach (Plug_type item in oneMenuList)
            {
                Plug_typeOutputDto menuTreeTableOutputDto = new Plug_typeOutputDto();
                menuTreeTableOutputDto = item.MapTo<Plug_typeOutputDto>();
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
        private List<Plug_typeOutputDto> GetSubClasses(List<Plug_type> data, string parentId)
        {
            List<Plug_typeOutputDto> list = new List<Plug_typeOutputDto>();
            Plug_typeOutputDto OrganizeOutputDto = new Plug_typeOutputDto();
            var ChilList = data.FindAll(t => t.Parentid == parentId);
            foreach (Plug_type entity in ChilList)
            {
                OrganizeOutputDto = entity.MapTo<Plug_typeOutputDto>();
                OrganizeOutputDto.Children = GetSubClasses(data, entity.Id).OrderBy(t => t.SortCode).MapTo<Plug_typeOutputDto>();
                list.Add(OrganizeOutputDto);
            }
            return list;
        }
    }
}