using Dapper;
using System.Collections.Generic;
using Yuebon.Commons.IDbContext;
using Yuebon.Commons.Repositories;
using Yuebon.Security.IRepositories;
using Yuebon.Security.Models;

namespace Yuebon.Security.Repositories
{
    public class FunctionRepository : BaseRepository<Function, string>, IFunctionRepository
    {
        public FunctionRepository()
        {
        }

        public FunctionRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// ���ݽ�ɫID�ַ��������ŷֿ�)��ϵͳ����ID����ȡ��Ӧ�Ĳ��������б�
        /// </summary>
        /// <param name="roleIds">��ɫID</param>
        /// <param name="typeID">ϵͳ����ID</param>
        /// <returns></returns>
        public IEnumerable<Function> GetFunctions(string roleIds, string typeID)
        {
            string sql = $"SELECT DISTINCT b.* FROM Sys_Function as b INNER JOIN Sys_RoleAuthorize as a On b.Id = a.ItemId  WHERE ObjectId IN (" + roleIds + ")";
            if (!string.IsNullOrEmpty(typeID))
            {
                sql = sql + string.Format(" AND SystemTypeId='{0}' ", typeID);
            }
            return DapperConn.Query<Function>(sql);
        }


        /// <summary>
        /// ����ϵͳ����ID����ȡ��Ӧ�Ĳ��������б�
        /// </summary>
        /// <param name="typeID">ϵͳ����ID</param>
        /// <returns></returns>
        public IEnumerable<Function> GetFunctions(string typeID)
        {
            string sql = $"SELECT DISTINCT b.* FROM Sys_Function as b INNER JOIN Sys_RoleAuthorize as a On b.Id = a.ItemId  ";
            if (!string.IsNullOrEmpty(typeID))
            {
                sql = sql + string.Format(" Where SystemTypeId='{0}' ", typeID);
            }
            return DapperConn.Query<Function>(sql);
        }
    }
}