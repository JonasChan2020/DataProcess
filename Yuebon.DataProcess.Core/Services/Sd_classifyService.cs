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
    public class Sd_classifyService: BaseService<Sd_classify,Sd_classifyOutputDto, string>, ISd_classifyService
    {
		private readonly ISd_classifyRepository _repository;
        private readonly ISys_sysRepository _sysrepository;
        private readonly ILogService _logService;
        public Sd_classifyService(ISd_classifyRepository repository, ISys_sysRepository sysrepository, ILogService logService) : base(repository)
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
        public async Task<List<Sd_classifyOutputDto>> GetAllClassifyTreeTable(string SysId)
        {
            List<Sd_classifyOutputDto> reslist = new List<Sd_classifyOutputDto>();
            IEnumerable<Sd_classify> elist = _repository.GetListWhere("sysid='" + SysId + "'");
            List<Sd_classify> list = elist.OrderBy(t => t.SortCode).ToList();
            List<Sd_classify> oneMenuList = list.FindAll(t => t.Parentid == "");
            foreach (Sd_classify item in oneMenuList)
            {
                Sd_classifyOutputDto menuTreeTableOutputDto = new Sd_classifyOutputDto();
                menuTreeTableOutputDto = item.MapTo<Sd_classifyOutputDto>();
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
        private List<Sd_classifyOutputDto> GetSubClasses(List<Sd_classify> data, string parentId)
        {
            List<Sd_classifyOutputDto> list = new List<Sd_classifyOutputDto>();
            Sd_classifyOutputDto OrganizeOutputDto = new Sd_classifyOutputDto();
            var ChilList = data.FindAll(t => t.Parentid == parentId);
            foreach (Sd_classify entity in ChilList)
            {
                OrganizeOutputDto = entity.MapTo<Sd_classifyOutputDto>();
                if (!string.IsNullOrEmpty(entity.Sysid))
                {
                    OrganizeOutputDto.Sys_Name = _sysrepository.Get(entity.Sysid).Sysname;
                }
                OrganizeOutputDto.Children = GetSubClasses(data, entity.Id).OrderBy(t => t.SortCode).MapTo<Sd_classifyOutputDto>();
                list.Add(OrganizeOutputDto);
            }
            return list;
        }
    }
}