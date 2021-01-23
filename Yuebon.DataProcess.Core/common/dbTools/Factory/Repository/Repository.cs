using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Yuebon.Commons.Pages;
using Yuebon.DataProcess.Core.common.dbTools.baseDb;
using Yuebon.DataProcess.Core.common.dbTools.baseDb.Extension;
using Yuebon.DataProcess.Core.common.Enity;

namespace Yuebon.DataProcess.Core.common.dbTools.Factory
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

        
        public void Commit()
        {
            db.Commit();
        }
        public void Rollback()
        {
            db.Rollback();
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

        #region hashtable方式添加，修改，删除

        /// <summary>
        /// 实体插入(dapper)不作实现
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert(string tbName, Hashtable ht)
        {
            return db.Insert(tbName, ht);
        }
        /// <summary>
        /// 实体插入(dapper)不作实现
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int Insert(string tbName, IEnumerable<Hashtable> htList)
        {
            return db.Insert(tbName, htList);
        }
        public int Delete(string tbName)
        {
            return db.Delete(tbName);
        }
        public int Delete(string tbName, Hashtable ht)
        {
            return db.Delete(tbName, ht);
        }
        public int Delete(string tbName, IEnumerable<Hashtable> htList)
        {
            return db.Delete(tbName, htList);
        }
        public int Update(string tableName, Hashtable ht, string pkName)
        {
            return db.Update(tableName, ht, pkName);
        }
        public int Update(string tableName, string pkName, IEnumerable<Hashtable> htList)
        {
            return db.Update(tableName, pkName, htList);
        }
        public int UpdateNoPk(string tableName, Hashtable ht, string[] pkName, string keyColumn)
        {
            return db.UpdateNoPk(tableName, ht, pkName, keyColumn);
        }
        public int UpdateNoPk(string tableName, string[] pkName, IEnumerable<Hashtable> htList, string keyColumn)
        {
            return db.UpdateNoPk(tableName, pkName, htList, keyColumn);
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
        public List<SqlBatchEntity> FindTableBatch(List<SqlBatchEntity> sbcEntity)
        {
            return db.FindTableBatch(sbcEntity);
        }
        public object FindObject(string strSql)
        {
            return db.FindObject(strSql);
        }
        public object FindObject(string strSql, DbParameter[] dbParameter)
        {
            return db.FindObject(strSql, dbParameter);
        }


        #endregion

        #region 连接测试
        /// <summary>
        /// 测试连接数据库是否成功
        /// </summary>
        /// <returns></returns>
        public bool ConnectionTest()
        {
            return db.ConnectionTest();
        }
        #endregion

        #region 获取数据库中表集合
        /// <summary>
        /// 获取数据库中表集合
        /// </summary>
        /// <returns></returns>
        public DataTable GetTbList(string dbName)
        {
            return db.GetTbList(dbName);
        }
        #endregion

        #region 根据表名获取字段集合
        /// <summary>
        /// 根据表名获取字段集合
        /// </summary>
        /// <param name="tbName"></param>
        /// <returns></returns>
        public DataTable GetColListByTbName(string dbName, string tbName)
        {
            return db.GetColListByTbName(dbName, tbName);
        }
        #endregion

        #region 操作方法
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
        public List<DbTableInfo> GetAllTables(string dbName, string strwhere, string fieldNameToSort, bool isDescending, PagerInfo info)
        {
            return db.GetAllTables(dbName, strwhere, fieldNameToSort, isDescending, info);
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
    }
}
