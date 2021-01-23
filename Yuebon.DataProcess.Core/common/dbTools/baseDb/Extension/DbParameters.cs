using Dapper;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using Microsoft.Data.Sqlite;

namespace Yuebon.DataProcess.Core.common.dbTools.baseDb.Extension
{
    /// <summary>
    /// 描 述：SQL参数化
    /// </summary>
    public class DbParameters
    {
        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来获取命令参数中的参数符号oracle为":",sqlserver为"@"
        /// </summary>
        /// <returns></returns>
        public static string CreateDbParmCharacter()
        {
            string character = string.Empty;
            switch (DbHelper.DbType)
            {
                case DatabaseType.SqlServer:
                    character = "@";
                    break;
                case DatabaseType.Oracle:
                    character = ":";
                    break;
                case DatabaseType.MySql:
                    character = "@";
                    break;
                case DatabaseType.Access:
                    character = "@";
                    break;
                case DatabaseType.SQLite:
                    character = "@";
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            return character;
        }
        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public static DbParameter CreateDbParameter()
        {
            return new SqlParameter();
        }
        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public static DbParameter CreateDbParameter(string paramName, object value)
        {
            DbParameter param = DbParameters.CreateDbParameter();
            param.ParameterName = paramName;
            param.Value = value;
            return param;
        }
        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public static DbParameter CreateDbParameter(string paramName, object value, DbType dbType)
        {
            DbParameter param = DbParameters.CreateDbParameter();
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
        public static DbParameter[] ToDbParameter(DbParameter[] dbParameter)
        {
            int i = 0;
            int size = dbParameter.Length;
            DbParameter[] _dbParameter = null;
            switch (DbHelper.DbType)
            {
                case DatabaseType.SqlServer:
                    _dbParameter = new SqlParameter[size];
                    while (i < size)
                    {
                        _dbParameter[i] = new SqlParameter(dbParameter[i].ParameterName, dbParameter[i].Value);
                        i++;
                    }
                    break;
                case DatabaseType.MySql:
                    _dbParameter = new MySqlParameter[size];
                    while (i < size)
                    {
                        _dbParameter[i] = new MySqlParameter(dbParameter[i].ParameterName, dbParameter[i].Value);
                        i++;
                    }
                    break;
                case DatabaseType.Oracle:
                    _dbParameter = new OracleParameter[size];
                    while (i < size)
                    {
                        _dbParameter[i] = new OracleParameter(dbParameter[i].ParameterName, dbParameter[i].Value);
                        i++;
                    }
                    break;
                case DatabaseType.Access:
                    _dbParameter = new OleDbParameter[size];
                    while (i < size)
                    {
                        _dbParameter[i] = new OleDbParameter(dbParameter[i].ParameterName, dbParameter[i].Value);
                        i++;
                    }
                    break;
                case DatabaseType.SQLite:
                    _dbParameter = new SqlParameter[size];
                    while (i < size)
                    {
                        _dbParameter[i] = new SqlParameter(dbParameter[i].ParameterName, dbParameter[i].Value);
                        i++;
                    }
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            return _dbParameter;
        }

        /// <summary>
        /// 转换对应的数据库参数
        /// </summary>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        public static DbParameter[] ToDbParameter(DbParameter[] dbParameter,DatabaseType DbType)
        {
            int i = 0;
            int size = dbParameter.Length;
            DbParameter[] _dbParameter = null;
            switch (DbType)
            {
                case DatabaseType.SqlServer:
                    _dbParameter = new SqlParameter[size];
                    while (i < size)
                    {
                        _dbParameter[i] = new SqlParameter(dbParameter[i].ParameterName, dbParameter[i].Value);
                        i++;
                    }
                    break;
                case DatabaseType.MySql:
                    _dbParameter = new MySqlParameter[size];
                    while (i < size)
                    {
                        _dbParameter[i] = new MySqlParameter(dbParameter[i].ParameterName, dbParameter[i].Value);
                        i++;
                    }
                    break;
                case DatabaseType.Oracle:
                    _dbParameter = new OracleParameter[size];
                    while (i < size)
                    {
                        _dbParameter[i] = new OracleParameter(dbParameter[i].ParameterName, dbParameter[i].Value);
                        i++;
                    }
                    break;
                case DatabaseType.Access:
                    _dbParameter = new OleDbParameter[size];
                    while (i < size)
                    {
                        _dbParameter[i] = new OleDbParameter(dbParameter[i].ParameterName, dbParameter[i].Value);
                        i++;
                    }
                    break;
                case DatabaseType.SQLite:
                    _dbParameter = new SqlParameter[size];
                    while (i < size)
                    {
                        _dbParameter[i] = new SqlParameter(dbParameter[i].ParameterName, dbParameter[i].Value);
                        i++;
                    }
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            return _dbParameter;
        }

        /// <summary>
        /// 转换对应的数据库语句中参数
        /// </summary>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        public static string ToSqlDbParameter(string sql, DatabaseType DbType)
        {
            string sqlParamStr = "";
            switch (DbType)
            {
                case DatabaseType.SqlServer:
                    sqlParamStr = "@";
                    break;
                case DatabaseType.MySql:
                    sqlParamStr = "@";
                    break;
                case DatabaseType.Oracle:
                    sqlParamStr = ":";
                    break;
                case DatabaseType.Access:
                    sqlParamStr = "@";
                    break;
                case DatabaseType.SQLite:
                    sqlParamStr = "@";
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            if (!string.IsNullOrEmpty(sqlParamStr))
            {
                sql = sql.Replace("@P", sqlParamStr + "P");
            }
            return sql;
        }

        /// <summary>
        /// 将数据库参数转换为Dapper专用的数据库参数类型
        /// </summary>
        /// <param name="dbParameter"></param>
        /// <returns></returns>
        public static DynamicParameters ToDynamicParameters(DbParameter[] dbParameter)
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
    }
}
