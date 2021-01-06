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


        /// <summary>
        /// ���ݽ�ɫID�ַ��������ŷֿ�)��ϵͳ����ID����ȡ��Ӧ�Ĳ��������б�
        /// </summary>
        /// <param name="roleIds">��ɫID</param>
        /// <param name="typeID">ϵͳ����ID</param>
        /// <param name="isMenu">�Ƿ��ǲ˵�</param>
        /// <returns></returns>
        List<Menu> GetFunctions(string roleIds, string typeID,bool isMenu=false);

        /// <summary>
        /// ����ϵͳ����ID����ȡ��Ӧ�Ĳ��������б�
        /// </summary>
        /// <param name="typeID">ϵͳ����ID</param>
        /// <returns></returns>
        List<Menu> GetFunctions(string typeID);

        /// <summary>
        /// ���ݸ������ܱ����ѯ�����Ӽ����ܣ���Ҫ����ҳ�������ťȨ��
        /// </summary>
        /// <param name="enCode">�˵����ܱ���</param>
        /// <returns></returns>
        Task<IEnumerable<MenuOutputDto>> GetListByParentEnCode(string enCode);
    }
}
