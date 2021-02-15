using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Yuebon.DataProcess.Core.OutSideDbService.Entity;

namespace Yuebon.DataProcess.Core.PlugIServices
{
    /// <summary>
    /// 描 述：操作数据库接口
    /// </summary>
    public interface IDatabase
    {
        #region 基础操作
        IDatabase BeginTrans();
        int Commit();
        void Rollback();
        void Close();
        int ExecuteBySql(string strSql);
        int ExecuteBySql(string strSql, params DbParameter[] dbParameter);
        int ExecuteByProc(string procName);
        int ExecuteByProc(string procName, DbParameter[] dbParameter);
        DataTable FindTable(string strSql);
        DataTable FindTable(string strSql, DbParameter[] dbParameter);
        DataTable FindTable(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex, out int total);
        DataTable FindTable(string strSql, DbParameter[] dbParameter, string orderField, bool isAsc, int pageSize, int pageIndex, out int total);
        /// <summary>
        /// 测试连接数据库是否成功
        /// </summary>
        /// <returns></returns>
        bool ConnectionTest();
        /// <summary>
        /// 获取数据库信息
        /// </summary>
        /// <returns></returns>
        List<DataBaseInfo> GetAllDataBases();
        /// <summary>
        /// 获取数据库的所有表的信息
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableList"></param>
        /// <returns></returns>
        List<DbTableInfo> GetAllTables(string dbName, string tableList);
        /// <summary>
        /// 获取数据库的所有表的信息
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="strwhere"></param>
        /// <param name="fieldNameToSort"></param>
        /// <param name="isDescending"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        List<DbTableInfo> GetAllTables(string dbName, string strwhere, string fieldNameToSort, bool isDescending, int PageSize, int CurrenetPageIndex, int RecordCount);
        /// <summary>
        /// 获取表的所有字段名及字段类型
        /// </summary>
        /// <param name="dbName">dbName</param>
        /// <param name="tableName">数据表的名称</param>
        /// <returns></returns>
        List<DbFieldInfo> GetAllColumns(string dbName, string tableName);
        #endregion

        #region 参数操作
        /// <summary>
        /// 获取命令参数中的参数符号oracle为":",sqlserver为"@"
        /// </summary>
        /// <returns></returns>
        string CreateDbParmCharacter();
        /// <summary>
        /// 创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        DbParameter CreateDbParameter();
        /// <summary>
        /// 创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        DbParameter CreateDbParameter(string paramName, object value);
        /// <summary>
        /// 创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        DbParameter CreateDbParameter(string paramName, object value, DbType dbType);
        /// <summary>
        /// 转换对应的数据库参数
        /// </summary>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        DbParameter[] ToDbParameter(DbParameter[] dbParameter);
        /// <summary>
        /// 转换对应的数据库语句中参数
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paramSymbol">当前使用的参数符号</param>
        /// <param name="paramStr">当前使用的参数字符</param>
        /// <returns></returns>
        string ToSqlDbParameter(string sql, string paramSymbol, string paramStr);
        #endregion
    }
}
