using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.Commons.IServices;
using Yuebon.Security.Dtos;
using Yuebon.Security.Models;

namespace Yuebon.Security.IServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMenuService:IService<Menu, MenuOutputDto, string>
    {

        /// <summary>
        /// �����û���ȡ���ܲ˵�
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <returns></returns>
        List<Menu> GetMenuByUser(string userId);

        /// <summary>
        /// ��ȡ���ܲ˵�������Vue �����б�
        /// </summary>
        /// <param name="systemTypeId">��ϵͳId</param>
        /// <returns></returns>
        Task<List<MenuTreeTableOutputDto>> GetAllMenuTreeTable(string systemTypeId);
    }
}
