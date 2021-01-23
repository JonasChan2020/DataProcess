using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Yuebon.Commons.Pages;
using Yuebon.DataProcess.Core.common.dbTools.baseDb.Extension;
using Yuebon.DataProcess.Core.common.dbTools.Factory;
using Yuebon.DataProcess.Core.common.Enity;

namespace Yuebon.DataProcess.Core.common.dbTools
{
    public class DataTools : RepositoryFactory
    {
        private string connString;
        private DatabaseType dataType;

        public DataTools(string _connString, string _dataTypeStr)
        {
            connString = StringTools.DecodeBase64(_connString);
            if (!string.IsNullOrEmpty(_dataTypeStr))
            {
                switch (_dataTypeStr)
                {
                    case "Oracle":
                        dataType = DatabaseType.Oracle;
                        break;
                    case "MySql":
                        dataType = DatabaseType.MySql;
                        break;
                    case "SqlServer":
                        dataType = DatabaseType.SqlServer;
                        break;
                    case "Access":
                        dataType = DatabaseType.Access;
                        break;
                    case "SQLite":
                        dataType = DatabaseType.SQLite;
                        break;
                    default:
                        break;
                }
            }
        }

        #region  数据库连接测试
        /// <summary>
        /// 数据库连接测试
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool ConnectionTest()
        {
            bool result=this.BaseRepository(connString, dataType).ConnectionTest();
            return result;
        }

        /// <summary>
        /// 数据库连接测试
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool ConnectionTest(string dbConnStr,string dbTypeStr)
        {
            DatabaseType dbType = new DatabaseType();
            switch (dbTypeStr)
            {
                case "Oracle":
                    dbType = DatabaseType.Oracle;
                    break;
                case "MySql":
                    dbType = DatabaseType.MySql;
                    break;
                case "SqlServer":
                    dbType = DatabaseType.SqlServer;
                    break;
                case "Access":
                    dbType = DatabaseType.Access;
                    break;
                case "SqLite":
                    dbType = DatabaseType.SQLite;
                    break;
                default:
                    break;
            }
            return this.BaseRepository(dbConnStr, dbType).ConnectionTest();
        }
        #endregion

        #region 获取数据库中表集合
        /// <summary>
        /// 获取数据库中表集合
        /// </summary>
        /// <returns></returns>
        public DataTable GetTbList(string dbName)
        {
            DataTable dt = this.BaseRepository(connString, dataType).GetTbList(dbName);
            return dt;
        }

        /// <summary>
        /// 获取数据库中表集合
        /// </summary>
        /// <returns></returns>
        public List<DbTableInfo> GetTbList(string dbName, string tableList)
        {
            List<DbTableInfo> modelList = this.BaseRepository(connString, dataType).GetAllTables(dbName,tableList);
            return modelList;
        }

        /// <summary>
        /// 获取数据库中表集合
        /// </summary>
        /// <returns></returns>
        public List<DbTableInfo> GetTbList(string dbName, string strwhere, string fieldNameToSort, bool isDescending, PagerInfo info)
        {
            List<DbTableInfo> modelList = this.BaseRepository(connString, dataType).GetAllTables(dbName, strwhere,fieldNameToSort,isDescending,info);
            return modelList;
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
            DataTable dt = this.BaseRepository(connString, dataType).GetColListByTbName(dbName, tbName);
            return dt;
        }

        /// <summary>
        /// 获取表的所有字段名及字段类型
        /// </summary>
        /// <param name="dbName">dbName</param>
        /// <param name="tableName">数据表的名称</param>
        /// <returns></returns>
        public List<DbFieldInfo> GetAllColumns(string dbName, string tableName)
        {
            List<DbFieldInfo> modelList = this.BaseRepository(connString, dataType).GetAllColumns(dbName, tableName);
            return modelList;
        }
        #endregion

        #region  使用参数的select 方法
        /// <summary>
        /// 使用参数的select 方法
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="colName">列名</param>
        /// <param name="val">值</param>
        /// <param name="ot">列数据类型</param>
        /// <param name="colSize">数据类型长度</param>
        /// <returns></returns>
        public DataTable SqlSelect(string tableName, string colList, string[] colName, string[] val)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("select " + colList + " from " + tableName + " where ");
            DbParameter[] parameters = new DbParameter[colName.Length];
            for (int i = 0; i < colName.Length; i++)
            {
                sqlStr.Append(" " + colName[i] + "=@" + colName[i] + " and");
                parameters[i] = DbParameters.CreateDbParameter("@" + colName[i] + "", val[i]);
            }
            DataTable dt = this.BaseRepository(connString, dataType).FindTable(sqlStr.ToString(), parameters);
            return dt;
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sqlStr">查询语句</param>
        /// <returns></returns>
        public DataTable FindTable(string sqlStr)
        {
            DataTable dt = this.BaseRepository(connString, dataType).FindTable(sqlStr);
            return dt;
        }
        #endregion

        #region 参数查询
        /// <summary>
        /// 参数查询
        /// </summary>
        /// <param name="sqlStr">查询语句</param>
        /// <param name="dbParameters">参数集合</param>
        /// <returns></returns>
        public DataTable FindTable(string sqlStr, DbParameter[] dbParameters)
        {
            sqlStr = DbParameters.ToSqlDbParameter(sqlStr, dataType);
            dbParameters = DbParameters.ToDbParameter(dbParameters, dataType);
            DataTable dt = this.BaseRepository(connString, dataType).FindTable(sqlStr, dbParameters);
            return dt;
        }

        /// <summary>
        /// 参数查询
        /// </summary>
        /// <returns></returns>
        public List<SqlBatchEntity> FindTableBatch(List<SqlBatchEntity> sbcEntity)
        {
            foreach (SqlBatchEntity item in sbcEntity)
            {
                item.sqlStr = DbParameters.ToSqlDbParameter(item.sqlStr, dataType);
                item.dpList = DbParameters.ToDbParameter(item.dpList, dataType);
            }

            sbcEntity = this.BaseRepository(connString, dataType).FindTableBatch(sbcEntity);
            return sbcEntity;
        }
        #endregion

        #region 添加
        public int AddData(List<sheetRow> rowModelList, DbImportTemplet ditModel)
        {
            List<Hashtable> htList = new List<Hashtable>();
            foreach (sheetRow rowItem in rowModelList)
            {
                Hashtable ht = new Hashtable();
                foreach (TempletColumn colItem in ditModel.templeColList.FindAll(x => x.rowIndex == rowItem.listRowIndex))
                {
                    ht.Add(colItem.columnName, rowItem.colList.Find(x => x.columnName == colItem.columnName).values);
                }
                htList.Add(ht);
            }
            return this.BaseRepository(connString, dataType).Insert(ditModel.tbName, htList);
        }
        #endregion

        #region 更新
        public int UpdateData(List<sheetRow> rowModelList, DbImportTemplet ditModel)
        {
            string keyColumName = "";
            foreach (TempletColumn item in ditModel.templeColList)
            {

                if (item.isSingleKey == "1")
                {
                    keyColumName = item.columnName;
                    continue;
                }
            }
            List<Hashtable> htList = new List<Hashtable>();
            foreach (sheetRow rowItem in rowModelList)
            {
                Hashtable ht = new Hashtable();
                foreach (TempletColumn colItem in ditModel.templeColList.FindAll(x => x.rowIndex == rowItem.listRowIndex))
                {
                    ht.Add(colItem.columnName, rowItem.colList.Find(x => x.columnName == colItem.columnName).values);
                }
                htList.Add(ht);
            }
            return this.BaseRepository(connString, dataType).Update(ditModel.tbName, keyColumName, htList);
        }

        public int UpdateData(List<sheetRow> rowModelList, DbImportTemplet ditModel, string[] pkKeys)
        {
            string keyColumName = "";
            foreach (TempletColumn item in ditModel.templeColList.FindAll(x => x.rowIndex == 0))
            {

                if (item.isSingleKey == "1")
                {
                    keyColumName = item.columnName;
                    continue;
                }
            }
            List<Hashtable> htList = new List<Hashtable>();
            foreach (sheetRow rowItem in rowModelList)
            {
                Hashtable ht = new Hashtable();
                foreach (TempletColumn colItem in ditModel.templeColList.FindAll(x => x.rowIndex == rowItem.listRowIndex))
                {
                    ht.Add(colItem.columnName, rowItem.colList.Find(x => x.columnName == colItem.columnName).values);
                }
                htList.Add(ht);
            }
            return this.BaseRepository(connString, dataType).UpdateNoPk(ditModel.tbName, pkKeys, htList, keyColumName);
        }
        #endregion

    }
}
