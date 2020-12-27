using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Services;
using Yuebon.Security.Dtos;
using Yuebon.Security.IRepositories;
using Yuebon.Security.IServices;
using Yuebon.Security.Models;

namespace Yuebon.Security.Services
{
    public class FunctionService: BaseService<Function, FunctionOutputDto, string>, IFunctionService
    {
        private readonly IFunctionRepository functionRepository;
        private readonly IUserRepository userRepository;
        private readonly ISystemTypeRepository systemTypeRepository;
        private readonly ILogService _logService;
        public FunctionService(IFunctionRepository repository, ISystemTypeRepository _systemTypeRepository, ILogService logService) : base(repository)
        {
            functionRepository = repository;
            systemTypeRepository = _systemTypeRepository;
            _logService = logService;
        }

        /// <summary>
        /// ���ݽ�ɫID�ַ��������ŷֿ�)��ϵͳ����ID����ȡ��Ӧ�Ĳ��������б�
        /// </summary>
        /// <param name="roleIDs">��ɫID</param>
        /// <param name="typeID">ϵͳ����ID</param>
        /// <returns></returns>
        public IEnumerable<Function> GetFunctions(string roleIDs, string typeID)
        {
            return functionRepository.GetFunctions(roleIDs, typeID);
        }

        /// <summary>
        /// ���ݸ������ܱ����ѯ�����Ӽ����ܣ���Ҫ����ҳ�������ťȨ��
        /// </summary>
        /// <param name="enCode">�˵����ܱ���</param>
        /// <returns></returns>
        public async Task<IEnumerable<FunctionOutputDto>> GetListByParentEnCode(string enCode)
        {
            string where = string.Format("EnCode='{0}'",enCode);
            Function function = await functionRepository.GetWhereAsync(where);
            where = string.Format("ParentId='{0}'", function.ParentId);
            IEnumerable<Function> list = await functionRepository.GetAllByIsNotEnabledMarkAsync(where);
            return list.MapTo<FunctionOutputDto>().ToList();
        }



        /// <summary>
        /// ��ȡ���ܲ˵�������Vue �����б�
        /// </summary>
        /// <param name="systemTypeId">��ϵͳId</param>
        /// <returns></returns>
        public async Task<List<FunctionTreeTableOutputDto>> GetAllFunctionTreeTable(string systemTypeId)
        {
            string where = "1=1";
            List<FunctionTreeTableOutputDto> reslist = new List<FunctionTreeTableOutputDto>();
            if (!string.IsNullOrEmpty(systemTypeId))
            {
                IEnumerable<Function> elist = await functionRepository.GetListWhereAsync("SystemTypeId='" + systemTypeId + "'");
                List<Function> list = elist.OrderBy(t => t.SortCode).ToList();
                List<Function> oneMenuList = list.FindAll(t => t.ParentId == "");
                foreach (Function item in oneMenuList)
                {
                    FunctionTreeTableOutputDto menuTreeTableOutputDto = new FunctionTreeTableOutputDto();
                    menuTreeTableOutputDto = item.MapTo<FunctionTreeTableOutputDto>();
                    menuTreeTableOutputDto.Children = GetSubMenus(list, item.Id).ToList<FunctionTreeTableOutputDto>();
                    reslist.Add(menuTreeTableOutputDto);
                }
            }
            else
            {
                IEnumerable<SystemType> listSystemType = await systemTypeRepository.GetListWhereAsync(where);
                foreach (SystemType systemType in listSystemType)
                {
                    FunctionTreeTableOutputDto menuTreeTableOutputDto = new FunctionTreeTableOutputDto();
                    menuTreeTableOutputDto.Id = systemType.Id;
                    menuTreeTableOutputDto.FullName = systemType.FullName;
                    menuTreeTableOutputDto.EnCode = systemType.EnCode;
                    menuTreeTableOutputDto.UrlAddress = systemType.Url;
                    menuTreeTableOutputDto.EnabledMark = systemType.EnabledMark;

                    menuTreeTableOutputDto.SystemTag = true;

                    IEnumerable<Function> elist = await functionRepository.GetListWhereAsync("SystemTypeId='" + systemType.Id + "'");
                    if (elist.Count() > 0)
                    {
                        List<Function> list = elist.OrderBy(t => t.SortCode).ToList();
                        menuTreeTableOutputDto.Children = GetSubMenus(list, "").ToList<FunctionTreeTableOutputDto>();
                    }
                    reslist.Add(menuTreeTableOutputDto);
                }
            }
            return reslist;
        }


        /// <summary>
        /// ��ȡ�ӹ��ܣ��ݹ����
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parentId">����Id</param>
        /// <returns></returns>
        private List<FunctionTreeTableOutputDto> GetSubMenus(List<Function> data, string parentId)
        {
            List<FunctionTreeTableOutputDto> list = new List<FunctionTreeTableOutputDto>();
            FunctionTreeTableOutputDto menuTreeTableOutputDto = new FunctionTreeTableOutputDto();
            var ChilList = data.FindAll(t => t.ParentId == parentId);
            foreach (Function entity in ChilList)
            {
                menuTreeTableOutputDto = entity.MapTo<FunctionTreeTableOutputDto>();
                menuTreeTableOutputDto.Children = GetSubMenus(data, entity.Id).OrderBy(t => t.SortCode).MapTo<FunctionTreeTableOutputDto>();
                list.Add(menuTreeTableOutputDto);
            }
            return list;
        }

        /// <summary>
        /// ����������ѯ���ݿ�,�����ض��󼯺�(���ڷ�ҳ������ʾ)
        /// </summary>
        /// <param name="search">��ѯ������</param>
        /// <returns>ָ������ļ���</returns>
        public override async Task<PageResult<FunctionOutputDto>> FindWithPagerAsync(SearchInputDto<Function> search)
        {
            bool order = search.Order == "asc" ? false : true;
            string where = GetDataPrivilege(false);
            if (!string.IsNullOrEmpty(search.EnCode))
            {
                Function function = await repository.GetWhereAsync("EnCode='" + search.EnCode + "'");
                if (function != null)
                {
                    where += " and ParentId='" + function.Id + "'";
                }
            }
            if (!string.IsNullOrEmpty(search.Keywords))
            {
                where += " and (FullName like '%" + search.Keywords + "%' or EnCode like '%" + search.Keywords + "%')";
            }
            PagerInfo pagerInfo = new PagerInfo
            {
                CurrenetPageIndex = search.CurrenetPageIndex,
                PageSize = search.PageSize
            };
            List<Function> list = await repository.FindWithPagerAsync(where, pagerInfo, search.Sort, order);
            PageResult<FunctionOutputDto> pageResult = new PageResult<FunctionOutputDto>
            {
                CurrentPage = pagerInfo.CurrenetPageIndex,
                Items = list.MapTo<FunctionOutputDto>(),
                ItemsPerPage = pagerInfo.PageSize,
                TotalItems = pagerInfo.RecordCount
            };
            return pageResult;
        }
    }
}