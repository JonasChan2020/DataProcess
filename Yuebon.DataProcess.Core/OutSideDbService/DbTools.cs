using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Yuebon.DataProcess.Core.OutSideDbService.Entity;
using Yuebon.DataProcess.Core.OutSideDbService.Extension;
using Yuebon.DataProcess.Core.OutSideDbService.Factory;

namespace Yuebon.DataProcess.Core.OutSideDbService
{
    public class DbTools : RepositoryFactory
    {
        private string connString;
        private string dataType;

        public DbTools(string _connString, string _dataTypeStr)
        {
            connString = StringTools.DecodeBase64(_connString);
            dataType = _dataTypeStr;
        }

        #region 获取数据库信息
        /// <summary>
        /// 数据库连接测试
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool ConnectionTest()
        {
            IRepository db = BaseRepository(connString, dataType);
            bool result = db.ConnectionTest();
            db.Close();
            return result;
        }
        /// <summary>
        /// 获取数据库信息
        /// </summary>
        /// <returns></returns>
        public List<DataBaseInfo> GetAllDataBases()
        {
            IRepository db = BaseRepository(connString, dataType);
            List<DataBaseInfo> modelList = db.GetAllDataBases();
            db.Close();
            return modelList;
        }
        /// <summary>
        /// 获取数据库的所有表的信息
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableList"></param>
        /// <returns></returns>
        public List<DbTableInfo> GetAllTables(string dbName, string tableList)
        {
            IRepository db = BaseRepository(connString, dataType);
            List<DbTableInfo> modelList = this.BaseRepository(connString, dataType).GetAllTables(dbName, tableList);
            db.Close();
            return modelList;
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
            IRepository db = BaseRepository(connString, dataType);
            List<DbTableInfo> modelList = this.BaseRepository(connString, dataType).GetAllTables(dbName, strwhere, fieldNameToSort, isDescending, PageSize, CurrenetPageIndex, RecordCount);
            db.Close();
            return modelList;
        }
        /// <summary>
        /// 获取表的所有字段名及字段类型
        /// </summary>
        /// <param name="dbName">dbName</param>
        /// <param name="tableName">数据表的名称</param>
        /// <returns></returns>
        public List<DbFieldInfo> GetAllColumns(string dbName, string tableName)
        {
            IRepository db = BaseRepository(connString, dataType);
            List<DbFieldInfo> modelList = this.BaseRepository(connString, dataType).GetAllColumns(dbName, tableName);
            db.Close();
            return modelList;
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
            IRepository db = BaseRepository(connString, dataType);
            DataTable dt = this.BaseRepository(connString, dataType).FindTable(sqlStr);
            db.Close();
            return dt;
        }
        /// <summary>
        /// 参数查询
        /// </summary>
        /// <param name="sqlStr">查询语句</param>
        /// <param name="dbParameters">参数集合</param>
        /// <returns></returns>
        public DataTable FindTable(string sqlStr, string paramSymbol, string paramStr, DbParameter[] dbParameters)
        {
            IRepository db = BaseRepository(connString, dataType);
            sqlStr = db.ToSqlDbParameter(sqlStr, paramSymbol, paramStr);
            dbParameters = db.ToDbParameter(dbParameters);
            DataTable dt = db.FindTable(sqlStr, dbParameters);
            db.Close();
            return dt;
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
            IRepository db = BaseRepository(connString, dataType);
            int result = db.ExecuteBySql(InsertSql(tbName, ht, db).ToString(), GetParameter(ht,db));
            db.Close();
            return result;
        }
        /// <summary>
        /// 实体插入(dapper)不作实现
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int Insert(string tbName, IEnumerable<Hashtable> htList)
        {
            IRepository db = BaseRepository(connString, dataType);
            db.BeginTrans();
            foreach (var item in htList)
            {
                db.ExecuteBySql(InsertSql(tbName, item, db).ToString(), GetParameter(item, db));
            }
            int result = db.Commit();
            return result;
        }
        public int Delete(string tbName)
        {
            IRepository db = BaseRepository(connString, dataType);
            int result = db.ExecuteBySql(DeleteSql(tbName).ToString());
            db.Close();
            return result;
        }
        public int Delete(string tbName, Hashtable ht)
        {
            IRepository db = BaseRepository(connString, dataType);
            int result = db.ExecuteBySql(DeleteSql(tbName, ht, db).ToString(), GetParameter(ht, db));
            db.Close();
            return result;
        }
        public int Delete(string tbName, IEnumerable<Hashtable> htList)
        {
            IRepository db = BaseRepository(connString, dataType);
            db.BeginTrans();
            foreach (var item in htList)
            {
                db.ExecuteBySql(DeleteSql(tbName, item, db).ToString(), GetParameter(item, db));
            }
            int result = db.Commit();
            return result;
        }
        public int Update(string tableName, Hashtable ht, string pkName)
        {
            IRepository db = BaseRepository(connString, dataType);
            int result = db.ExecuteBySql(UpdateSql(tableName, ht, pkName, db).ToString(), GetParameter(ht, db));
            db.Close();
            return result;
        }
        public int UpdateNoPk(string tableName, Hashtable ht, string[] pkNames, string keyColumn)
        {
            IRepository db = BaseRepository(connString, dataType);
            int result = db.ExecuteBySql(UpdateSqlNoPk(tableName, ht, pkNames, keyColumn, db).ToString(), GetParameter(ht, db));
            db.Close();
            return result;
        }
        public int Update(string tableName, string pkName, IEnumerable<Hashtable> htList)
        {
            IRepository db = BaseRepository(connString, dataType);
            db.BeginTrans();
            foreach (var item in htList)
            {
                db.ExecuteBySql(UpdateSql(tableName, item, pkName, db).ToString(), GetParameter(item, db));
            }
            int result = db.Commit();
            return result;
        }
        public int UpdateNoPk(string tableName, string[] pkNames, IEnumerable<Hashtable> htList, string keyColumn)
        {
            IRepository db = BaseRepository(connString, dataType);
            db.BeginTrans();
            foreach (var item in htList)
            {
                db.ExecuteBySql(UpdateSqlNoPk(tableName, item, pkNames, keyColumn, db).ToString(), GetParameter(item, db));
            }
            int result = db.Commit();
            return result;
        }
        #endregion

        #region 辅助方法
        #region 对象参数转换DbParameter
        /// <summary>
        /// 对象参数转换DbParameter
        /// </summary>
        /// <returns></returns>
        private DbParameter[] GetParameter(Hashtable ht,IRepository db)
        {
            IList<DbParameter> parameter = new List<DbParameter>();
            DbType dbtype = new DbType();
            foreach (string key in ht.Keys)
            {
                if (ht[key] is DateTime)
                    dbtype = DbType.DateTime;
                else
                    dbtype = DbType.String;
                parameter.Add(db.CreateDbParameter(db.CreateDbParmCharacter() + key, ht[key], dbtype));
            }
            return parameter.ToArray();
        }
        #endregion

        #region 拼接 Insert SQL语句
        /// <summary>
        /// 哈希表生成Insert语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">Hashtable</param>
        /// <returns>int</returns>
        private StringBuilder InsertSql(string tableName, Hashtable ht, IRepository db)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Insert Into ");
            sb.Append(tableName);
            sb.Append("(");
            StringBuilder sp = new StringBuilder();
            StringBuilder sb_prame = new StringBuilder();
            foreach (string key in ht.Keys)
            {
                if (ht[key] != null)
                {
                    sb_prame.Append("," + key);
                    sp.Append("," + db.CreateDbParmCharacter() + "" + key);
                }
            }
            sb.Append(sb_prame.ToString().Substring(1, sb_prame.ToString().Length - 1) + ") Values (");
            sb.Append(sp.ToString().Substring(1, sp.ToString().Length - 1) + ")");
            return sb;
        }
        #endregion

        #region 拼接 Update SQL语句
        /// <summary>
        /// 哈希表生成UpdateSql语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">Hashtable</param>
        /// <param name="pkName">主键</param>
        /// <returns></returns>
        private StringBuilder UpdateSql(string tableName, Hashtable ht, string pkName, IRepository db)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Update ");
            sb.Append(tableName);
            sb.Append(" Set ");
            bool isFirstValue = true;
            foreach (string key in ht.Keys)
            {
                if (ht[key] != null && pkName != key)
                {
                    if (isFirstValue)
                    {
                        isFirstValue = false;
                        sb.Append(key);
                        sb.Append("=");
                        sb.Append(db.CreateDbParmCharacter() + key);
                    }
                    else
                    {
                        sb.Append("," + key);
                        sb.Append("=");
                        sb.Append(db.CreateDbParmCharacter() + key);
                    }
                }
            }
            sb.Append(" Where ").Append(pkName).Append("=").Append(db.CreateDbParmCharacter() + pkName);
            return sb;
        }
        /// <summary>
        /// 哈希表生成UpdateSql语句,无主键
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">Hashtable</param>
        /// <param name="pkNames">唯一判定项</param>
        /// <returns></returns>
        private StringBuilder UpdateSqlNoPk(string tableName, Hashtable ht, string[] pkNames,string keyColumn, IRepository db)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Update ");
            sb.Append(tableName);
            sb.Append(" Set ");
            bool isFirstValue = true;
            foreach (string key in ht.Keys)
            {
                if (ht[key] != null && !pkNames.Contains(key)&&key!=keyColumn)
                {
                    if (isFirstValue)
                    {
                        isFirstValue = false;
                        sb.Append(key);
                        sb.Append("=");
                        sb.Append(db.CreateDbParmCharacter() + key);
                    }
                    else
                    {
                        sb.Append("," + key);
                        sb.Append("=");
                        sb.Append(db.CreateDbParmCharacter() + key);
                    }
                }
            }
            sb.Append(" Where ");
            for (int i = 0; i < pkNames.Length; i++)
            {
                sb.Append(pkNames[i]).Append("=").Append(db.CreateDbParmCharacter() + pkNames[i]);
                if(i!=pkNames.Length-1)
                {
                    sb.Append(" and ");
                }
            }

            return sb;
        }
        #endregion

        #region 拼接 Delete SQL语句
        /// <summary>
        /// 拼接删除SQL语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pkName">字段主键</param>
        /// <returns></returns>
        private StringBuilder DeleteSql(string tableName)
        {
            return new StringBuilder("Delete From " + tableName);
        }
        /// <summary>
        /// 拼接删除SQL语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pkName">字段主键</param>
        /// <returns></returns>
        private StringBuilder DeleteSql(string tableName, string pkName, IRepository db)
        {
            return new StringBuilder("Delete From " + tableName + " Where " + pkName + " = " + db.CreateDbParmCharacter() + pkName + "");
        }
        /// <summary>
        /// 拼接删除SQL语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">多参数</param>
        /// <returns></returns>
        private StringBuilder DeleteSql(string tableName, Hashtable ht, IRepository db)
        {
            StringBuilder sb = new StringBuilder("Delete From " + tableName + " Where 1=1");
            foreach (string key in ht.Keys)
            {
                sb.Append(" AND " + key + " = " + db.CreateDbParmCharacter() + "" + key + "");
            }
            return sb;
        }
       
        #endregion
        #endregion

    }
}
