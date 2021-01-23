using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Yuebon.Commons.Pages;
using Yuebon.DataProcess.Core.common.dbTools.baseDb;
using Yuebon.DataProcess.Core.common.dbTools.baseDb.Extension;
using Yuebon.DataProcess.Core.common.Enity;

namespace Yuebon.DataProcess.Core.common.dbTools.dbs
{
    /// <summary>
    /// 描 述：操作数据库
    /// </summary>
    public class SqlDatabase : IDatabase
    {
        #region 构造函数
        /// <summary>
        /// 构造方法
        /// </summary>
        public SqlDatabase(string connString)
        {
            DbHelper.DbType = DatabaseType.SqlServer;
            connectionString = connString;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 获取 数据库连接串
        /// </summary>
        public string connectionString { get; set; }
        protected DbConnection Connection
        {
            get
            {
                DbConnection dbconnection = new SqlConnection(connectionString);
                dbconnection.Open();
                return dbconnection;
            }
        }
        /// <summary>
        /// 事务对象
        /// </summary>
        public DbTransaction dbTransaction { get; set; }
        #endregion

        #region 事物提交
        /// <summary>
        /// 事务开始
        /// </summary>
        /// <returns></returns>
        public IDatabase BeginTrans()
        {
            DbConnection dbConnection = dbTransaction != null ? dbTransaction.Connection : Connection;
            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }
            dbTransaction = dbConnection.BeginTransaction();
            return this;
        }
        /// <summary>
        /// 提交当前操作的结果
        /// </summary>
        public int Commit()
        {
            try
            {
                if (dbTransaction != null)
                {
                    dbTransaction.Commit();
                    this.Close();
                }
                return 1;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.InnerException is SqlException)
                {
                    SqlException sqlEx = ex.InnerException.InnerException as SqlException;
                   
                }
                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 把当前操作回滚成未提交状态
        /// </summary>
        public void Rollback()
        {
            this.dbTransaction.Rollback();
            this.dbTransaction.Dispose();
            this.Close();
        }
        /// <summary>
        /// 关闭连接 内存回收
        /// </summary>
        public void Close()
        {
            DbConnection dbConnection = dbTransaction.Connection;
            if (dbConnection != null && dbConnection.State != ConnectionState.Closed)
            {
                dbConnection.Close();
            }

        }
        #endregion

        #region 执行 SQL 语句
        public int ExecuteBySql(string strSql)
        {
            if (dbTransaction == null)
            {
                using (var connection = Connection)
                {
                    return connection.Execute(strSql);
                }
            }
            else
            {
                dbTransaction.Connection.Execute(strSql, null, dbTransaction);
                return 0;

            }
        }
        
        public int ExecuteBySql(string strSql, params DbParameter[] dbParameter)
        {
            if (dbTransaction == null)
            {
                using (var connection = Connection)
                {
                    return connection.Execute(strSql, DbParameters.ToDynamicParameters(dbParameter));
                }
            }
            else
            {
                dbTransaction.Connection.Execute(strSql, DbParameters.ToDynamicParameters(dbParameter), dbTransaction);
                return 0;

            }
        }
        public int ExecuteByProc(string procName)
        {
            if (dbTransaction == null)
            {
                using (var connection = Connection)
                {
                    return connection.Execute(procName);
                }
            }
            else
            {
                dbTransaction.Connection.Execute(procName, null, dbTransaction);
                return 0;

            }
        }
        public int ExecuteByProc(string procName, params DbParameter[] dbParameter)
        {
            if (dbTransaction == null)
            {
                using (var connection = Connection)
                {
                    return connection.Execute(procName, DbParameters.ToDynamicParameters(dbParameter));
                }
            }
            else
            {
                dbTransaction.Connection.Execute(procName, DbParameters.ToDynamicParameters(dbParameter), dbTransaction);
                return 0;

            }
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
            return ExecuteBySql(DatabaseCommon.InsertSql(tbName, ht).ToString(), DatabaseCommon.GetParameter(ht));
        }
        /// <summary>
        /// 实体插入(dapper)不作实现
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int Insert(string tbName, IEnumerable<Hashtable> htList)
        {
            if (dbTransaction == null)
            {
                BeginTrans();
                foreach (var item in htList)
                {
                    Insert(tbName, item);
                }
                return Commit();
            }
            else
            {
                foreach (var item in htList)
                {
                    Insert(tbName, item);
                }
                return 0;

            }
        }
        public int Delete(string tbName)
        {
            return ExecuteBySql(DatabaseCommon.DeleteSql(tbName).ToString());
        }
        public int Delete(string tbName, Hashtable ht)
        {
            return ExecuteBySql(DatabaseCommon.DeleteSql(tbName, ht).ToString(), DatabaseCommon.GetParameter(ht));
        }
        public int Delete(string tbName, IEnumerable<Hashtable> htList)
        {
            if (dbTransaction == null)
            {
                BeginTrans();
                foreach (var item in htList)
                {
                    Delete(tbName, item);
                }
                return Commit();
            }
            else
            {
                foreach (var item in htList)
                {
                    Delete(tbName, item);
                }
                return 0;
            }
        }
        public int Update(string tableName, Hashtable ht, string pkName)
        {
            return ExecuteBySql(DatabaseCommon.UpdateSql(tableName, ht, pkName).ToString(), DatabaseCommon.GetParameter(ht));
        }
        public int UpdateNoPk(string tableName, Hashtable ht, string[] pkNames, string keyColumn)
        {
            return ExecuteBySql(DatabaseCommon.UpdateSqlNoPk(tableName, ht, pkNames, keyColumn).ToString(), DatabaseCommon.GetParameter(ht));
        }
        public int Update(string tableName, string pkName, IEnumerable<Hashtable> htList)
        {
            if (dbTransaction == null)
            {
                BeginTrans();
                foreach (var item in htList)
                {
                    Update(tableName, item, pkName);
                }
                return Commit();
            }
            else
            {
                foreach (var item in htList)
                {
                    Update(tableName, item, pkName);
                }
                return 0;
            }
        }
        public int UpdateNoPk(string tableName, string[] pkNames, IEnumerable<Hashtable> htList, string keyColumn)
        {
            if (dbTransaction == null)
            {
                BeginTrans();
                foreach (var item in htList)
                {
                    UpdateNoPk(tableName, item, pkNames, keyColumn);
                }
                return Commit();
            }
            else
            {
                foreach (var item in htList)
                {
                    UpdateNoPk(tableName, item, pkNames, keyColumn);
                }
                return 0;
            }
        }
        #endregion

        #region 数据源查询
        public DataTable FindTable(string strSql)
        {
            return FindTable(strSql, null);
        }
        public DataTable FindTable(string strSql, DbParameter[] dbParameter)
        {
            using (var dbConnection = Connection)
            {
                var IDataReader = new DbHelper(dbConnection).ExecuteReader(CommandType.Text, strSql, dbParameter);
                return ConvertExtension.IDataReaderToDataTable(IDataReader);
            }
        }

        public List<SqlBatchEntity> FindTableBatch(List<SqlBatchEntity> sbcEntity)
        {
            using (var dbConnection = Connection)
            {
                sbcEntity = new DbHelper(dbConnection).ExecuteReaderBatch(CommandType.Text, sbcEntity);
                foreach (SqlBatchEntity item in sbcEntity)
                {
                    item.finalValue = ConvertExtension.IDataReaderToDataTable((IDataReader)item.finalValue);
                }
                return sbcEntity;
            }
        }

        public DataTable FindTableTr(string strSql, DbParameter[] dbParameter)
        {
            if (dbTransaction == null)
            {
                using (var dbConnection = Connection)
                {
                    var IDataReader = new DbHelper(dbConnection).ExecuteReader(CommandType.Text, strSql, dbParameter);
                    return ConvertExtension.IDataReaderToDataTable(IDataReader);
                }
            }
            else
            {
                var IDataReader = dbTransaction.Connection.ExecuteReader(strSql, DbParameters.ToDynamicParameters(dbParameter), dbTransaction);
                return ConvertExtension.IDataReaderToDataTable(IDataReader);

            }

        }
        public DataTable FindTable(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex, out int total)
        {
            return FindTable(strSql, null, orderField, isAsc, pageSize, pageIndex, out total);
        }
        public DataTable FindTable(string strSql, DbParameter[] dbParameter, string orderField, bool isAsc, int pageSize, int pageIndex, out int total)
        {
            using (var dbConnection = Connection)
            {
                StringBuilder sb = new StringBuilder();
                if (pageIndex == 0)
                {
                    pageIndex = 1;
                }
                int num = (pageIndex - 1) * pageSize;
                int num1 = (pageIndex) * pageSize;
                string OrderBy = "";

                if (!string.IsNullOrEmpty(orderField))
                {
                    if (orderField.ToUpper().IndexOf("ASC") + orderField.ToUpper().IndexOf("DESC") > 0)
                    {
                        OrderBy = "Order By " + orderField;
                    }
                    else
                    {
                        OrderBy = "Order By " + orderField + " " + (isAsc ? "ASC" : "DESC");
                    }
                }
                else
                {
                    OrderBy = "order by (select 0)";
                }
                sb.Append("Select * From (Select ROW_NUMBER() Over (" + OrderBy + ")");
                sb.Append(" As rowNum, * From (" + strSql + ") As T ) As N Where rowNum > " + num + " And rowNum <= " + num1 + "");
                total = Convert.ToInt32(new DbHelper(dbConnection).ExecuteScalar(CommandType.Text, "Select Count(1) From (" + strSql + ") As t", dbParameter));
                var IDataReader = new DbHelper(dbConnection).ExecuteReader(CommandType.Text, sb.ToString(), dbParameter);
                DataTable resultTable = ConvertExtension.IDataReaderToDataTable(IDataReader);
                resultTable.Columns.Remove("rowNum");
                return resultTable;
            }
        }
        public object FindObject(string strSql)
        {
            return FindObject(strSql, null);
        }
        public object FindObject(string strSql, DbParameter[] dbParameter)
        {
            using (var dbConnection = Connection)
            {
                return new DbHelper(dbConnection).ExecuteScalar(CommandType.Text, strSql, dbParameter);
            }
        }
        #endregion

        #region 连接测试
        /// <summary>
        /// 测试连接数据库是否成功
        /// </summary>
        /// <returns></returns>
        public bool ConnectionTest()
        {
            bool IsCanConnectioned = false;
            DbConnection dbConnection = dbTransaction != null ? dbTransaction.Connection : Connection;
            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }
            if (dbConnection.State == ConnectionState.Open)
            {
                IsCanConnectioned = true;
                this.Close();
            }
            return IsCanConnectioned;
        }
        #endregion

        #region 获取数据库中表集合
        /// <summary>
        /// 获取数据库中表集合
        /// </summary>
        /// <returns></returns>
        public DataTable GetTbList(string dbName)
        {
            StringBuilder sqlStr = new StringBuilder();
            int userStartIndex = connectionString.ToLower().Replace(" ", "").IndexOf("userid=") + 9;
            string ownerStr = connectionString.Substring(userStartIndex, connectionString.IndexOf(";", userStartIndex) - userStartIndex);
            sqlStr.Append("");
            sqlStr.Append("select TABLE_NAME from dba_tables where owner='" + ownerStr + "' order by TABLE_NAME");
            DataTable dt = FindTable(sqlStr.ToString());
            return dt;
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
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("select * from user_tab_columns where Table_Name='" + tbName + "' order by COLUMN_ID ");
            DataTable dt = FindTable(sqlStr.ToString());
            return dt;
        }
        #endregion

        #region 操作方法
        /// <summary>
        /// 获取数据库信息
        /// </summary>
        /// <returns></returns>
        public List<DataBaseInfo> GetAllDataBases()
        {
            var sql = string.Format(@"select name as DbName from master..sysdatabases as dbs");
            using (var dbConnection = Connection)
            {
                return dbConnection.Query<DataBaseInfo>(sql).ToList();
            }
        }
        /// <summary>
        /// 根据表名获取数据库表的信息
        /// </summary>
        /// <param name="tablelist">表名称</param>
        /// <returns></returns>
        public List<DbTableInfo> GetAllTables(string dbName,string tablelist)
        {
            var sql = string.Format(@"SELECT tbs.name as TableName ,ds.value as Description FROM sys.tables tbs
left join sys.extended_properties ds on ds.major_id=tbs.object_id and ds.minor_id=0");
            if (!string.IsNullOrEmpty(tablelist))
            {
                sql += string.Format(@" where tbs.name in('{0}')", tablelist.Replace(",", "','"));
            }
            using (var dbConnection = Connection)
            {
                return dbConnection.Query<DbTableInfo>(sql).ToList();
            }
        }


        /// <summary>
        /// 获取数据库的所有表的信息
        /// </summary>
        /// <param name="strwhere"></param>
        /// <param name="fieldNameToSort"></param>
        /// <param name="isDescending"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<DbTableInfo> GetAllTables(string dbName, string strwhere, string fieldNameToSort, bool isDescending, PagerInfo info)
        {
            var sql = string.Format(@"SELECT tbs.name as TableName ,ds.value as Description FROM sys.tables tbs
left join sys.extended_properties ds on ds.major_id=tbs.object_id and ds.minor_id=0");

            string sqlcount = string.Format(@"select count(*) as Total from({0}) AA where {1}", sql, strwhere);

            string strOrder = string.Format(" order by {0} {1}", fieldNameToSort, isDescending ? "DESC" : "ASC");
            int minRow = info.PageSize * (info.CurrenetPageIndex - 1) + 1;
            int maxRow = info.PageSize * info.CurrenetPageIndex;

            string pagesql = string.Format(@"With Paging AS
                ( SELECT ROW_NUMBER() OVER ({0}) as RowNumber, {1} FROM ({2}) AA Where {3})
                SELECT * FROM Paging WHERE RowNumber Between {4} and {5}", strOrder, "*", sql, strwhere,
            minRow, maxRow);
            pagesql = sqlcount + ";" + pagesql;
            var list = new List<DbTableInfo>();
            using (DbConnection conn = Connection)
            {
                var reader = conn.QueryMultiple(sql);
                info.RecordCount = reader.ReadFirst<int>();
                list = reader.Read<DbTableInfo>().AsList();
            }
            return list;
        }
        /// <summary>
        /// 获取表的所有字段名及字段类型
        /// </summary>
        /// <param name="tableName">数据表的名称</param>
        /// <returns></returns>
        public List<DbFieldInfo> GetAllColumns(string dbName, string tableName)
        {
            if (tableName == null)
                throw new ArgumentNullException(nameof(tableName));

            var sqlFields = string.Format(@"
               SELECT a.name as FieldName,
(case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then '1'else '0' end) as Increment, 
(case when (SELECT count(*) FROM sysobjects 
WHERE (name in (SELECT name FROM sysindexes 
WHERE (id = a.id) AND (indid in 
(SELECT indid FROM sysindexkeys 
WHERE (id = a.id) AND (colid in 
(SELECT colid FROM syscolumns WHERE (id = a.id) AND (name = a.name))))))) 
AND (xtype = 'PK'))>0 then '1' else '0' end) as IsIdentity,b.name as FieldType,a.length as FieldMaxLength, 
COLUMNPROPERTY(a.id,a.name,'PRECISION') as FieldPrecision, 
isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0) as FieldScale,(case when a.isnullable=1 then '1'else '0' end) IsNullable, 
isnull(g.[value], ' ') AS Description
FROM syscolumns a 
left join systypes b on a.xtype=b.xusertype 
inner join sysobjects d on a.id=d.id and d.xtype='U' and d.name<>'dtproperties' 
left join syscomments e on a.cdefault=e.id 
left join sys.extended_properties g on a.id=g.major_id AND a.colid=g.minor_id
left join sys.extended_properties f on d.id=f.class and f.minor_id=0
WHERE d.name='{0}' --如果只查询指定表,加上此条件
order by a.id,a.colorder", tableName);
            List<DbFieldInfo> list = new List<DbFieldInfo>();
            using (DbConnection conn = Connection)
            {
                IEnumerable<dynamic> dlist = conn.Query(sqlFields);
                foreach (var item in dlist)
                {
                    DbFieldInfo dbFieldInfo = new DbFieldInfo
                    {
                        FieldName = item.FieldName,
                        //Increment = item.Increment == "1" ? true : false,
                        IsIdentity = item.IsIdentity == "1" ? true : false,
                        FieldType = item.FieldType.ToString(),
                        DataType = item.FieldType.ToString(),
                        FieldMaxLength = item.FieldMaxLength,
                        FieldPrecision = item.FieldPrecision,
                        FieldScale = item.FieldScale,
                        IsNullable = item.IsNullable == "1" ? true : false,
                        FieldDefaultValue = item.FieldDefaultValue,
                        Description = item.Description
                    };
                    list.Add(dbFieldInfo);
                }
            }
            List<DbFieldInfo> reslist = new List<DbFieldInfo>();
            foreach (DbFieldInfo info in list)
            {
                info.DataType = ConvertDataType(info);
                reslist.Add(info);
            }
            return reslist;
        }


        #region 字段转换
        //所有类型转换 http://www.cnblogs.com/vjine/p/3462167.html
        /// <summary>
        /// 将字段信息的类型转换为C#信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string ConvertDataType(DbFieldInfo info)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));
            if (string.IsNullOrEmpty(info.FieldType))
                throw new ArgumentNullException(nameof(info.FieldType));
            info.DataType = SqlType2CsharpTypeStr(info.FieldType, info.IsNullable);
            return info.DataType;
        }

        /// <summary>
        /// 将数据库类型转为系统类型。
        /// </summary>
        /// <param name="sqlType"></param>
        /// <param name="isNullable">字段是否可空</param>
        /// <returns></returns>
        public static string SqlType2CsharpTypeStr(string sqlType, bool isNullable = false)
        {
            if (string.IsNullOrEmpty(sqlType))
                throw new ArgumentNullException(nameof(sqlType));
            var val = string.Empty;
            var allowNull = false;
            switch (sqlType.ToLower())
            {
                case "bit":
                    val = "bool";
                    break;
                case "int":
                    val = "int";
                    break;
                case "smallint":
                    val = "short";
                    break;
                case "bigint":
                    val = "long";
                    break;
                case "tinyint":
                    val = "byte";
                    break;

                case "binary":
                case "image":
                case "varbinary":
                    val = "byte[]";
                    allowNull = true;
                    break;

                case "decimal":
                case "numeric":
                case "money":
                case "smallmoney":
                    val = "decimal";
                    break;

                case "float":
                    val = "float";
                    break;
                case "real":
                    val = "Single";
                    break;

                case "datetime":
                case "smalldatetime":
                case "timestamp":
                    val = "DateTime";
                    break;

                case "uniqueidentifier":
                    val = "Guid";
                    break;
                case "Variant":
                    val = "object";
                    allowNull = true;
                    break;

                case "text":
                case "ntext":
                case "char":
                case "nchar":
                case "varchar":
                case "nvarchar":
                default:
                    val = "string";
                    allowNull = true;
                    break;
            }
            if (isNullable && !allowNull)
                return val + "?";
            return val;
        }
        #endregion

        #endregion
    }
}
