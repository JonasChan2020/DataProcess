using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.Commons.IRepositories;
using Yuebon.Security.Dtos;
using Yuebon.Security.Models;

namespace Yuebon.Security.IRepositories
{
    public interface IFunctionRepository:IRepository<Function, string>
    {
        /// <summary>
        /// ���ݽ�ɫID�ַ��������ŷֿ�)��ϵͳ����ID����ȡ��Ӧ�Ĳ��������б�
        /// </summary>
        /// <param name="roleIDs">��ɫID</param>
        /// <param name="typeID">ϵͳ����ID</param>
        /// <returns></returns>
        IEnumerable<Function> GetFunctions(string roleIDs, string typeID);


        /// <summary>
        /// ����ϵͳ����ID����ȡ��Ӧ�Ĳ��������б�
        /// </summary>
        /// <param name="typeID">ϵͳ����ID</param>
        /// <returns></returns>
        IEnumerable<Function> GetFunctions(string typeID);

    }
}