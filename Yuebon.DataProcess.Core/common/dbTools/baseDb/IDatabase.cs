using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Yuebon.Commons.Pages;
using Yuebon.DataProcess.Core.common.dbTools.baseDb.Extension;
using Yuebon.DataProcess.Core.common.Enity;

namespace Yuebon.DataProcess.Core.common.dbTools.baseDb
{
    /// <summary>
    /// 描 述：操作数据库接口
    /// </summary>
    public interface IDatabase
    {
        IDatabase BeginTrans();
        int Commit();
        void Rollback();
        void Close();
        int ExecuteBySql(string strSql);
        int ExecuteBySql(string strSql, params DbParameter[] dbParameter);
        int ExecuteByProc(string procName);
        int ExecuteByProc(string procName, DbParameter[] dbParameter);
       
        object FindObject(string strSql);
        object FindObject(string strSql, DbParameter[] dbParameter);
       

        DataTable FindTable(string strSql);
        DataTable FindTable(string strSql, DbParameter[] dbParameter);

        List<SqlBatchEntity> FindTableBatch(List<SqlBatchEntity> sbcEntity);

        DataTable FindTableTr(string strSql, DbParameter[] dbParameter);
        
        DataTable FindTable(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex, out int total);
        DataTable FindTable(string strSql, DbParameter[] dbParameter, string orderField, bool isAsc, int pageSize, int pageIndex, out int total);

        #region hashtable方式添加，修改，删除

        /// <summary>
        /// 实体插入(dapper)不作实现
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insert(string tbName, Hashtable ht);
        /// <summary>
        /// 实体插入(dapper)不作实现
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        int Insert(string tbName, IEnumerable<Hashtable> htList);
        int Delete(string tbName);
        int Delete(string tbName, Hashtable ht);
        int Delete(string tbName, IEnumerable<Hashtable> htList);
        int Update(string tableName, Hashtable ht, string pkName);
        int UpdateNoPk(string tableName, Hashtable ht, string[] pkNames, string keyColumn);
        int Update(string tableName, string pkName, IEnumerable<Hashtable> htList);

        int UpdateNoPk(string tableName, string[] pkNames, IEnumerable<Hashtable> htList, string keyColumn);
        #endregion

        #region 连接测试
        /// <summary>
        /// 测试连接数据库是否成功
        /// </summary>
        /// <returns></returns>
        bool ConnectionTest();

        #endregion

        #region 获取数据库中表集合
        /// <summary>
        /// 获取数据库中表集合
        /// </summary>
        /// <returns></returns>
        DataTable GetTbList(string dbName);

        #endregion

        #region 根据表名获取字段集合
        /// <summary>
        /// 根据表名获取字段集合
        /// </summary>
        /// <param name="tbName"></param>
        /// <returns></returns>
        DataTable GetColListByTbName(string dbName, string tbName);
        #endregion

        #region 操作方法
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
        List<DbTableInfo> GetAllTables(string dbName, string strwhere, string fieldNameToSort, bool isDescending, PagerInfo info);
        /// <summary>
        /// 获取表的所有字段名及字段类型
        /// </summary>
        /// <param name="dbName">dbName</param>
        /// <param name="tableName">数据表的名称</param>
        /// <returns></returns>
        List<DbFieldInfo> GetAllColumns(string dbName, string tableName);
        #endregion
    }
}
