using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Yuebon.DataProcess.Core.OutSideDbService.Entity;
using Yuebon.DataProcess.Core.PlugIServices;


namespace Yuebon.DataProcess.Core.OutSideDbService.Factory
{
    /// <summary>
    /// 描 述：定义仓储模型中的数据标准操作
    /// </summary>
    public class Repository : IRepository
    {
        #region 构造
        public IDatabase db;
        public Repository(IDatabase idatabase)
        {
            this.db = idatabase;
        }
        #endregion

        #region 事物提交
        public IRepository BeginTrans()
        {
            db.BeginTrans();
            return this;
        }

        
        public int Commit()
        {
            return db.Commit();
        }
        public void Rollback()
        {
            db.Rollback();
        }
        public void Close()
        {
            db.Close();
        }
        #endregion

        #region 执行 SQL 语句
        public int ExecuteBySql(string strSql)
        {
            return db.ExecuteBySql(strSql);
        }
        public int ExecuteBySql(string strSql, params DbParameter[] dbParameter)
        {
            return db.ExecuteBySql(strSql, dbParameter);
        }
        public int ExecuteByProc(string procName)
        {
            return db.ExecuteByProc(procName);
        }
        public int ExecuteByProc(string procName, params DbParameter[] dbParameter)
        {
            return db.ExecuteByProc(procName, dbParameter);
        }
        #endregion

        #region 数据源 查询
        public DataTable FindTable(string strSql)
        {
            return db.FindTable(strSql);
        }
        public DataTable FindTable(string strSql, DbParameter[] dbParameter)
        {
            return db.FindTable(strSql, dbParameter);
        }

        public DataTable FindTable(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex, out int total)
        {
            return db.FindTable(strSql, orderField, isAsc, pageSize, pageIndex, out total);
        }
        public DataTable FindTable(string strSql, DbParameter[] dbParameter, string orderField, bool isAsc, int pageSize, int pageIndex, out int total)
        {
            return db.FindTable(strSql, dbParameter, orderField, isAsc, pageSize, pageIndex, out total);
        }


        #endregion


        #region 操作方法
        /// <summary>
        /// 测试连接数据库是否成功
        /// </summary>
        /// <returns></returns>
        public bool ConnectionTest()
        {
            return db.ConnectionTest();
        }
        /// <summary>
        /// 获取数据库信息
        /// </summary>
        /// <returns></returns>
        public List<DataBaseInfo> GetAllDataBases()
        {
            return db.GetAllDataBases();
        }
        /// <summary>
        /// 获取数据库的所有表的信息
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableList"></param>
        /// <returns></returns>
        public List<DbTableInfo> GetAllTables(string dbName, string tableList)
        {
            return db.GetAllTables(dbName, tableList);
        }


        /// <summary>
        /// 获取数据库的所有表的信息
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="strwhere"></param>
        /// <param name="fieldNameToSort"></param>
        /// <param name="isDescending"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<DbTableInfo> GetAllTables(string dbName, string strwhere, string fieldNameToSort, bool isDescending, int PageSize, int CurrenetPageIndex, int RecordCount)
        {
            return db.GetAllTables(dbName, strwhere, fieldNameToSort, isDescending, PageSize, CurrenetPageIndex, RecordCount);
        }
        /// <summary>
        /// 获取表的所有字段名及字段类型
        /// </summary>
        /// <param name="dbName">dbName</param>
        /// <param name="tableName">数据表的名称</param>
        /// <returns></returns>
        public List<DbFieldInfo> GetAllColumns(string dbName, string tableName)
        {
            return db.GetAllColumns(dbName, tableName);
        }

        #endregion

        #region 参数操作
        /// <summary>
        /// 获取命令参数中的参数符号oracle为":",sqlserver为"@"
        /// </summary>
        /// <returns></returns>
        public string CreateDbParmCharacter()
        {
            return db.CreateDbParmCharacter();
        }
        /// <summary>
        /// 创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public DbParameter CreateDbParameter()
        {
            return db.CreateDbParameter();
        }
        /// <summary>
        /// 创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public DbParameter CreateDbParameter(string paramName, object value)
        {
            return db.CreateDbParameter(paramName, value);
        }
        /// <summary>
        /// 创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public DbParameter CreateDbParameter(string paramName, object value, DbType dbType)
        {
            return db.CreateDbParameter(paramName, value, dbType);
        }
        /// <summary>
        /// 转换对应的数据库参数
        /// </summary>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        public DbParameter[] ToDbParameter(DbParameter[] dbParameter)
        {
            return db.ToDbParameter(dbParameter);
        }
        /// <summary>
        /// 转换对应的数据库语句中参数
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paramSymbol">当前使用的参数符号</param>
        /// <param name="paramStr">当前使用的参数字符</param>
        /// <returns></returns>
        public string ToSqlDbParameter(string sql, string paramSymbol, string paramStr)
        {
            return db.ToSqlDbParameter(sql, paramSymbol, paramStr);
        }
        #endregion
    }
}
