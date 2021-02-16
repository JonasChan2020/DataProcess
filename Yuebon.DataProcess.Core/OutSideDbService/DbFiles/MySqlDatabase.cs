using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using MySql.Data.MySqlClient;
using Dapper;
using System.Collections;
using System.Linq;
using Yuebon.DataProcess.Core.OutSideDbService.Extension;
using Yuebon.DataProcess.Core.OutSideDbService.Entity;
using Yuebon.DataProcess.Core.PlugIServices;
using Yuebon.DataProcess.Core.OutSideDbService.Factory;

namespace Yuebon.DataProcess.Core.OutSideDbService.DbFiles
{
    /// <summary>
    /// 描 述：操作数据库
    /// </summary>
    public class MySqlDatabase : IDatabase
    {
        #region 构造函数
        /// <summary>
        /// 构造方法
        /// </summary>
        public MySqlDatabase(string connString)
        {
            connectionString = connString;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 获取 数据库连接串
        /// </summary>
        public string connectionString { get; set; }
        /// <summary>
        /// 事务对象
        /// </summary>
        public DbTransaction dbTransaction { get; set; }
        protected DbConnection Connection
        {
            get
            {
                DbConnection dbconnection = new MySqlConnection(connectionString);
                dbconnection.Open();
                return dbconnection;
            }
        }
        
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
                dbTransaction.Rollback();
                throw;
            }
            finally
            {
                //if (dbTransaction == null)
                //{
                //    this.Close();
                //}
                this.Close();
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
            if (dbTransaction != null)
            {
                DbConnection dbConnection = dbTransaction.Connection;
                if (dbConnection != null && dbConnection.State != ConnectionState.Closed)
                {
                    dbConnection.Close();
                }
            }
            if (Connection != null)
            {
                Connection.Close();
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
                    return connection.Execute(strSql, ToDynamicParameters(dbParameter));
                }
            }
            else
            {
                dbTransaction.Connection.Execute(strSql, ToDynamicParameters(dbParameter), dbTransaction);
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
                    return connection.Execute(procName, ToDynamicParameters(dbParameter));
                }
            }
            else
            {
                dbTransaction.Connection.Execute(procName, ToDynamicParameters(dbParameter), dbTransaction);
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
                var IDataReader = new DbHelper(dbConnection).ExecuteReader(CommandType.Text, strSql, ToDbParameter(dbParameter));
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
                    OrderBy = "order by (select 0 )";
                }
                sb.Append(strSql + OrderBy);
                sb.Append(" limit " + num + "," + pageSize + "");
                total = Convert.ToInt32(new DbHelper(dbConnection).ExecuteScalar(CommandType.Text, "Select Count(1) From (" + strSql + ") As t", ToDbParameter(dbParameter)));
                var IDataReader = new DbHelper(dbConnection).ExecuteReader(CommandType.Text, sb.ToString(), ToDbParameter(dbParameter));
                DataTable resultTable = ConvertExtension.IDataReaderToDataTable(IDataReader);
                return resultTable;
            }
        }
        #endregion

        #region 操作方法

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
                dbConnection.Close();
            }
            return IsCanConnectioned;
        }
        #endregion

        /// <summary>
        /// 获取数据库信息
        /// </summary>
        /// <returns></returns>
        public List<DataBaseInfo> GetAllDataBases()
        {
            var sql = string.Format(@"select schema_name as DbName from information_schema.schemata");
            var list = new List<DataBaseInfo>();
            using (var dbConnection = Connection)
            {
                return dbConnection.Query<DataBaseInfo>(sql).ToList();
            }
        }
        /// <summary>
        /// 获取数据库的所有表的信息
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableList"></param>
        /// <returns></returns>
        public List<DbTableInfo> GetAllTables(string dbName, string tableList)
        {
            var sql = string.Format(@"select table_name as TableName,TABLE_COMMENT as Description from information_schema.tables where table_schema='{0}' ", dbName);
            if (!string.IsNullOrEmpty(tableList))
            {
                sql += string.Format(@" and table_name in('{0}')", tableList.Replace(",", "','"));
            }
            using (var dbConnection = Connection)
            {
                return dbConnection.Query<DbTableInfo>(sql).ToList();
            }
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
        public List<DbTableInfo> GetAllTables(string dbName, string strwhere, string fieldNameToSort, bool isDescending, int PageSize, int CurrenetPageIndex,int RecordCount)
        {
            var sql = string.Format(@"select table_name AS TableName,TABLE_COMMENT as Description from information_schema.tables where table_schema='{0}'", dbName);

            string sqlcount = string.Format(@"select count(*) as Total from({0}) AA where {1}", sql, strwhere);

            string strOrder = string.Format(" order by {0} {1}", fieldNameToSort, isDescending ? "DESC" : "ASC");
            int minRow = PageSize * (CurrenetPageIndex - 1) + 1;
            int maxRow = PageSize * CurrenetPageIndex;

            string pagesql = string.Format(@" {0} and  {1} {2} LIMIT {3},{4}", sql, strwhere, strOrder, minRow, maxRow);
            pagesql = sqlcount + ";" + pagesql;

            var list = new List<DbTableInfo>();
            using (DbConnection conn = Connection)
            {
                var reader = conn.QueryMultiple(sql);
                RecordCount = reader.ReadFirst<int>();
                list = reader.Read<DbTableInfo>().AsList();
            }
            return list;
        }
        /// <summary>
        /// 获取表的所有字段名及字段类型
        /// </summary>
        /// <param name="dbName">dbName</param>
        /// <param name="tableName">数据表的名称</param>
        /// <returns></returns>
        public List<DbFieldInfo> GetAllColumns(string dbName, string tableName)
        {
            if (tableName == null)
                throw new ArgumentNullException(nameof(tableName));

            var sqlFields = string.Format(@"select 
column_name as FieldName,
(case when is_nullable = 'YES' then '1' else '0'  end)  as  IsNullable,
 (case when Column_key = 'PRI' then '1' else '0'  end) as IsIdentity,
data_type as FieldType,
 (case when column_default is null then '' else column_default  end)  as FieldDefaultValue,
(case when character_maximum_length is null then 0 else character_maximum_length  end)  as FieldMaxLength,
(case when Numeric_Precision is null then 0 else Numeric_Precision  end)  as FieldPrecision,
(case when Numeric_scale is null then 0 else Numeric_scale  end)   as FieldScale,
column_comment as Description
from information_schema.columns where table_schema='{0}' and table_name='{1}' ORDER BY ORDINAL_POSITION asc;", dbName, tableName);
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
        #endregion

        #region 参数操作
        /// <summary>
        /// 获取命令参数中的参数符号oracle为":",sqlserver为"@"
        /// </summary>
        /// <returns></returns>
        public string CreateDbParmCharacter()
        {
            return "@";
        }
        /// <summary>
        /// 创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public DbParameter CreateDbParameter()
        {
            return new MySqlParameter();
        }
        /// <summary>
        /// 创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public DbParameter CreateDbParameter(string paramName, object value)
        {
            DbParameter param = CreateDbParameter();
            param.ParameterName = paramName;
            param.Value = value;
            return param;
        }
        /// <summary>
        /// 创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public DbParameter CreateDbParameter(string paramName, object value, DbType dbType)
        {
            DbParameter param = CreateDbParameter();
            param.DbType = dbType;
            param.ParameterName = paramName;
            param.Value = value;
            return param;
        }
        /// <summary>
        /// 转换对应的数据库参数
        /// </summary>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        public DbParameter[] ToDbParameter(DbParameter[] dbParameter)
        {
            int i = 0;
            int size = dbParameter.Length;
            DbParameter[] _dbParameter = new MySqlParameter[size];
            while (i < size)
            {
                _dbParameter[i] = new MySqlParameter(dbParameter[i].ParameterName, dbParameter[i].Value);
                i++;
            }
            return _dbParameter;
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
            sql = sql.Replace(paramSymbol + paramStr, "@" + paramStr);
            return sql;
        }
        #endregion

        #region 辅助方法

        #region 字段转换
        /// <summary>
        /// 将字段信息的类型转换为C#信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string ConvertDataType(DbFieldInfo info)
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
        /// <param name="sqlType">数据库字段类型</param>
        /// <param name="isNullable">字段是否可空</param>
        /// <returns></returns>
        private static string SqlType2CsharpTypeStr(string sqlType, bool isNullable = false)
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
                    val = "bool";
                    break;

                case "binary":
                case "image":
                case "varbinary":
                    val = "byte[]";
                    allowNull = true;
                    break;

                case "decimal":
                    val = "decimal";
                    break;
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
                    val = "DateTime";
                    break;
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
                    val = "string";
                    allowNull = true;
                    break;
                case "ntext":
                    val = "string";
                    allowNull = true;
                    break;
                case "char":
                    val = "string";
                    allowNull = true;
                    break;
                case "nchar":
                    val = "string";
                    allowNull = true;
                    break;
                case "varchar":
                    val = "string";
                    allowNull = true;
                    break;
                case "nvarchar":
                    val = "string";
                    allowNull = true;
                    break;
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

        /// <summary>
        /// 将数据库参数转换为Dapper专用的数据库参数类型
        /// </summary>
        /// <param name="dbParameter"></param>
        /// <returns></returns>
        private DynamicParameters ToDynamicParameters(DbParameter[] dbParameter)
        {
            DynamicParameters dynamicParameters = null;
            if (dbParameter != null && dbParameter.Length > 0)
            {
                dynamicParameters = new DynamicParameters();
                foreach (DbParameter item in dbParameter)
                {
                    dynamicParameters.Add(item.ParameterName, item.Value, item.DbType, item.Direction, item.Size);
                }
            }
            return dynamicParameters;
        }
        #endregion

    }
}
