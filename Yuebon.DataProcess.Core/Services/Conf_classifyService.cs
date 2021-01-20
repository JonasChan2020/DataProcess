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
    public class Conf_classifyService: BaseService<Conf_classify,Conf_classifyOutputDto, string>, IConf_classifyService
    {
		private readonly IConf_classifyRepository _repository;
        private readonly ISys_sysRepository _sysrepository;
        private readonly ILogService _logService;
        public Conf_classifyService(IConf_classifyRepository repository, ISys_sysRepository sysrepository, ILogService logService) : base(repository)
        {
			_repository=repository;
            _sysrepository = sysrepository;
            _logService =logService;
            //_repository.OnOperationLog += _logService.OnOperationLog;
        }

        /// <summary>
        /// 获取分类适用于Vue 树形列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<Conf_classifyOutputDto>> GetAllClassifyTreeTable()
        {
            List<Conf_classifyOutputDto> reslist = new List<Conf_classifyOutputDto>();
            IEnumerable<Conf_classify> elist = await _repository.GetAllAsync();
            List<Conf_classify> list = elist.OrderBy(t => t.SortCode).ToList();
            List<Conf_classify> oneMenuList = list.FindAll(t => t.Parentid == "");
            foreach (Conf_classify item in oneMenuList)
            {
                Conf_classifyOutputDto menuTreeTableOutputDto = new Conf_classifyOutputDto();
                menuTreeTableOutputDto = item.MapTo<Conf_classifyOutputDto>();
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
        private List<Conf_classifyOutputDto> GetSubClasses(List<Conf_classify> data, string parentId)
        {
            List<Conf_classifyOutputDto> list = new List<Conf_classifyOutputDto>();
            Conf_classifyOutputDto OrganizeOutputDto = new Conf_classifyOutputDto();
            var ChilList = data.FindAll(t => t.Parentid == parentId);
            foreach (Conf_classify entity in ChilList)
            {
                OrganizeOutputDto = entity.MapTo<Conf_classifyOutputDto>();
                if (!string.IsNullOrEmpty(entity.Sysid))
                {
                    OrganizeOutputDto.Sys_Name = _sysrepository.Get(entity.Sysid).Sysname;
                }
                OrganizeOutputDto.Children = GetSubClasses(data, entity.Id).OrderBy(t => t.SortCode).MapTo<Conf_classifyOutputDto>();
                list.Add(OrganizeOutputDto);
            }
            return list;
        }
    }
}